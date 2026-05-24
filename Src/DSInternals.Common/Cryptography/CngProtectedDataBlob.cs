using System.Formats.Asn1;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DSInternals.Common.Cryptography.Asn1.DpapiNg;
using DSInternals.Common.Data;
using DSInternals.Common.Interop;
using Pkcs12 = DSInternals.Common.Cryptography.Asn1.Pkcs12;
using Pkcs7 = DSInternals.Common.Cryptography.Asn1.Pkcs7;

namespace DSInternals.Common.Cryptography;

/// <summary>
/// Represents DPAPI-NG protected data, formatted as CMS (certificate message syntax) enveloped content.
/// </summary>
public class CngProtectedDataBlob
{
    /// <summary>
    /// Gets the raw, undecoded DPAPI-NG blob bytes that this instance was decoded from.
    /// </summary>
    /// <value>The full CMS enveloped data, including any DPAPI-NG-specific trailing content.</value>
    public ReadOnlyMemory<byte> RawData { get; private set; }

    /// <summary>
    /// Gets the CNG protection descriptor rule string that identifies the protected data recipients.
    /// </summary>
    /// <remarks>
    /// Descriptor rule strings are composed from provider assignments such as <c>SID=...</c>,
    /// <c>SDDL=...</c>, <c>LOCAL=...</c>, or <c>CERTIFICATE=...</c>, joined by capitalized
    /// <c>AND</c> and <c>OR</c> operators as documented by Microsoft.
    /// </remarks>
    /// <value>The protection descriptor rule string.</value>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301 AND SID=S-1-5-21-3101812</code>
    /// <code language="text">SDDL=O:S-1-5-5-0-290724G:SYD:(A;;CCDC;;;S-1-5-5-0-290724)(A;;DC;;;WD)</code>
    /// <code language="text">CERTIFICATE=HashID:5BD6F6AF8477755554026221BC81D1F3DAF5C4CD</code>
    /// </example>
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/seccng/protection-descriptors">Protection Descriptors</seealso>
    public string? Descriptor { get; private set; }

    /// <summary>
    /// Gets the flattened list of SID and SDDL protectors associated with this blob.
    /// </summary>
    /// <remarks>
    /// AND combiners contribute their SID and SDDL children to this list. Non-SID, non-SDDL protectors
    /// (such as LOCAL, CERTIFICATE, or DRACERTIFICATE) are not represented in this collection.
    /// </remarks>
    /// <value>A read-only list of SID and SDDL protectors, or an empty list when none are present.</value>
    public IReadOnlyList<SidKeyProtector> SidKeyProtectors { get; private set; } = Array.Empty<SidKeyProtector>();

    /// <summary>
    /// Gets the content encryption algorithm identifier used to encrypt the payload.
    /// </summary>
    /// <value>An object identifier such as <c>2.16.840.1.101.3.4.1.46</c> for AES-256-GCM.</value>
    public Oid ContentEncryptionAlgorithm { get; private set; } = new Oid();

    /// <summary>
    /// Gets the encrypted payload bytes.
    /// </summary>
    /// <value>The ciphertext extracted from the CMS encrypted content info, or the DPAPI-NG trailing content when the CMS encrypted content is absent.</value>
    public ReadOnlyMemory<byte> EncryptedData { get; private set; }

    /// <summary>
    /// Gets the AES-GCM nonce associated with the content encryption algorithm.
    /// </summary>
    /// <value>The 12-byte AES-GCM nonce, or an empty buffer when the algorithm does not use a nonce.</value>
    public ReadOnlyMemory<byte> Nonce { get; private set; }

    /// <summary>
    /// Derives and writes group keys to the local DPAPI-NG key cache for every SID or SDDL protector
    /// that has a matching root key in <paramref name="kdsRootKeys" />.
    /// </summary>
    /// <param name="kdsRootKeys">The KDS root keys to search.</param>
    /// <returns><see langword="true" /> if at least one matching root key was found and cached; otherwise, <see langword="false" />.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="kdsRootKeys" /> parameter is <see langword="null" />.</exception>
    /// <exception cref="InvalidOperationException">This protected data blob is not protected by a SID or SDDL recipient.</exception>
    public bool CacheGroupKey(KdsRootKey[] kdsRootKeys)
    {
        ArgumentNullException.ThrowIfNull(kdsRootKeys);

        return this.CacheGroupKey(new StaticKdsRootKeyResolver(kdsRootKeys));
    }

    /// <summary>
    /// Looks up, derives, and writes group keys to the local DPAPI-NG key cache for every SID or SDDL protector
    /// whose root key can be resolved by <paramref name="rootKeyResolver" />.
    /// </summary>
    /// <param name="rootKeyResolver">The KDS root key resolver used to find matching root keys.</param>
    /// <returns><see langword="true" /> if at least one matching root key was found and cached; otherwise, <see langword="false" />.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="rootKeyResolver" /> parameter is <see langword="null" />.</exception>
    /// <exception cref="InvalidOperationException">This protected data blob is not protected by a SID or SDDL recipient.</exception>
    public bool CacheGroupKey(IKdsRootKeyResolver rootKeyResolver)
    {
        ArgumentNullException.ThrowIfNull(rootKeyResolver);

        if (this.SidKeyProtectors.Count == 0)
        {
            throw new InvalidOperationException("The protected data blob is not protected by a SID or SDDL recipient.");
        }

        bool anyCached = false;

        foreach (SidKeyProtector protector in this.SidKeyProtectors)
        {
            KdsRootKey? rootKey = rootKeyResolver.GetKdsRootKey(protector.ProtectionKeyIdentifier.RootKeyId);

            if (rootKey == null)
            {
                continue;
            }

            GroupKeyEnvelope groupKey = GroupKeyEnvelope.Create(
                rootKey,
                protector.ProtectionKeyIdentifier.L0KeyId,
                protector.ProtectionKeyIdentifier.L1KeyId,
                protector.ProtectionKeyIdentifier.L2KeyId,
                protector.TargetSDDLRaw,
                protector.ProtectionKeyIdentifier.DomainName,
                protector.ProtectionKeyIdentifier.ForestName);
            groupKey.WriteToCache();
            anyCached = true;
        }

        return anyCached;
    }

    /// <summary>
    /// Decrypts this protected blob via the native DPAPI-NG API.
    /// </summary>
    /// <returns>The decrypted bytes, or an empty span if this instance does not contain encrypted data.</returns>
    /// <exception cref="CryptographicException">The protected data blob cannot be decrypted.</exception>
    public ReadOnlySpan<byte> Decrypt()
    {
        if (this.RawData.Length == 0)
        {
            return default;
        }

        // Use the native Win32 API to perform the decryption
        Win32ErrorCode resultCode = NativeMethods.NCryptUnprotectSecret(this.RawData.Span, out byte[] decryptedData);
        Validator.AssertSuccess(resultCode);
        return decryptedData;
    }

    /// <summary>
    /// Attempts to decrypt this protected blob via the native DPAPI-NG API.
    /// </summary>
    /// <param name="cleartext">When this method returns, contains the decrypted bytes, or an empty span if decryption failed or this instance does not contain encrypted data.</param>
    /// <returns><see langword="true" /> if decryption succeeded or this instance is empty; otherwise, <see langword="false" />.</returns>
    public bool TryDecrypt(out ReadOnlySpan<byte> cleartext)
    {
        if (this.RawData.Length == 0)
        {
            cleartext = default;
            return true;
        }

        // Use the native Win32 API to perform the decryption
        Win32ErrorCode resultCode = NativeMethods.NCryptUnprotectSecret(this.RawData.Span, out byte[] decryptedData);
        cleartext = decryptedData;

        return resultCode == Win32ErrorCode.Success;
    }

    /// <summary>
    /// Decrypts this protected blob and decodes the cleartext as text.
    /// </summary>
    /// <param name="encoding">The text encoding of the decrypted data.</param>
    /// <returns>The decrypted text, without a single trailing null terminator if one is present, or <see langword="null" /> if this instance does not contain encrypted data.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="encoding" /> parameter is <see langword="null" />.</exception>
    /// <exception cref="FormatException">The decrypted text is not valid for the specified UTF-16 encoding.</exception>
    /// <exception cref="CryptographicException">The protected data blob cannot be decrypted.</exception>
    public string? DecryptText(Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(encoding);

        if (this.RawData.Length == 0)
        {
            return null;
        }

        ReadOnlySpan<byte> cleartext = this.Decrypt();

        if (cleartext.Length == 0)
        {
            return string.Empty;
        }

        bool isUtf16 = encoding.CodePage == Encoding.Unicode.CodePage ||
            encoding.CodePage == Encoding.BigEndianUnicode.CodePage;
        if (isUtf16 && cleartext.Length % sizeof(char) != 0)
        {
            throw new FormatException("The decrypted text is not a valid UTF-16 string.");
        }

        string text = encoding.GetString(cleartext);
        return text.Length > 0 && text[^1] == '\0' ? text[..^1] : text;
    }

    /// <summary>
    /// Returns the protection descriptor rule string that identifies this blob's recipients.
    /// </summary>
    /// <returns>The value of <see cref="Descriptor" />, or an empty string when no descriptor was decoded.</returns>
    public override string ToString() => this.Descriptor ?? string.Empty;

    /// <summary>
    /// Decodes a DPAPI-NG protected data blob from its CMS enveloped representation.
    /// </summary>
    /// <param name="blob">The DER or BER encoded DPAPI-NG blob.</param>
    /// <returns>A <see cref="CngProtectedDataBlob" /> instance describing the protectors and encrypted payload.</returns>
    /// <exception cref="CryptographicException">The blob is malformed or is not formatted as CMS enveloped content.</exception>
    public static CngProtectedDataBlob Decode(ReadOnlyMemory<byte> blob)
    {
        var cms = Pkcs7.ContentInfo.Decode(blob);
        var envelopedDataValue = cms.EnvelopedData;

        if (!envelopedDataValue.HasValue)
        {
            throw new CryptographicException("The data is not formatted as CMS enveloped content.");
        }

        var envelopedData = envelopedDataValue.Value;
        var encryptedContentInfo = envelopedData.EncryptedContentInfo;
        string? descriptor = ProtectionDescriptorFormatter.FormatOr(
            envelopedData.RecipientInfos.Select(static recipientInfo => recipientInfo.Descriptor));

        var protectors = new List<SidKeyProtector>();
        foreach (var recipient in envelopedData.RecipientInfos)
        {
            CollectSidKeyProtectors(recipient, protectors);
        }

        return new CngProtectedDataBlob()
        {
            RawData = blob,
            Descriptor = descriptor,
            SidKeyProtectors = protectors,
            ContentEncryptionAlgorithm = DpapiNgConstants.CreateAlgorithmOid(encryptedContentInfo.ContentEncryptionAlgorithm.Algorithm),
            EncryptedData = encryptedContentInfo.EncryptedContent ?? cms.AdditionalContent,
            Nonce = DecodeNonce(encryptedContentInfo.ContentEncryptionAlgorithm)
        };
    }

    /// <summary>
    /// Decodes a SID-based certificate protector from a PKCS #12 PFX file.
    /// </summary>
    /// <param name="pfx">The DER or BER encoded PFX file content.</param>
    /// <returns>A DPAPI-NG protected data blob that contains the certificate password protector.</returns>
    /// <exception cref="CryptographicException">The PFX content is malformed or does not contain a SID-based certificate protector.</exception>
    public static CngProtectedDataBlob DecodeFromPfx(ReadOnlyMemory<byte> pfx)
    {
        return Pkcs12.Pfx.DecodeSidProtector(pfx);
    }

    /// <summary>
    /// Decodes a SID-based certificate protector from a PKCS #12 PFX file.
    /// </summary>
    /// <param name="filePath">The path to the DER or BER encoded PFX file.</param>
    /// <returns>A DPAPI-NG protected data blob that contains the certificate password protector.</returns>
    /// <exception cref="ArgumentException">The <paramref name="filePath" /> parameter is <see langword="null" /> or empty.</exception>
    /// <exception cref="UnauthorizedAccessException">Access to the PFX file is denied.</exception>
    /// <exception cref="IOException">The PFX file cannot be read.</exception>
    /// <exception cref="CryptographicException">The PFX content is malformed or does not contain a SID-based certificate protector.</exception>
    public static CngProtectedDataBlob DecodeFromPfx(string filePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);

        byte[] pfx = File.ReadAllBytes(filePath);
        return DecodeFromPfx(pfx);
    }

    /// <summary>
    /// Dispatches a CMS recipient to the appropriate SID or SDDL collector.
    /// </summary>
    /// <param name="recipientInfo">The CMS recipient to inspect.</param>
    /// <param name="protectors">The target list to append matching SID or SDDL protectors to.</param>
    private static void CollectSidKeyProtectors(Pkcs7.RecipientInfo recipientInfo, List<SidKeyProtector> protectors)
    {
        if (recipientInfo.KEKRecipientInfo.HasValue)
        {
            TryCollectKekRecipientInfo(recipientInfo.KEKRecipientInfo.Value, protectors);
            return;
        }

        if (recipientInfo.OtherRecipientInfo.HasValue)
        {
            TryCollectOtherRecipientInfo(recipientInfo.OtherRecipientInfo.Value, protectors);
        }
    }

    /// <summary>
    /// Appends a SID or SDDL protector parsed from a KEK recipient, ignoring KEK recipients of other types.
    /// </summary>
    /// <param name="kekRecipientInfo">The KEK recipient to inspect.</param>
    /// <param name="protectors">The target list to append the protector to.</param>
    private static void TryCollectKekRecipientInfo(Pkcs7.KEKRecipientInfo kekRecipientInfo, List<SidKeyProtector> protectors)
    {
        if (!kekRecipientInfo.KekId.Other.HasValue)
        {
            return;
        }

        var otherKey = kekRecipientInfo.KekId.Other.Value;

        if (otherKey.SidProtector is { } targetSid)
        {
            protectors.Add(SidKeyProtector.FromSid(
                targetSid,
                new ProtectionKeyIdentifier(kekRecipientInfo.KekId.KeyIdentifier),
                DpapiNgConstants.CreateAlgorithmOid(kekRecipientInfo.KeyEncryptionAlgorithm.Algorithm),
                kekRecipientInfo.EncryptedKey));
            return;
        }

        if (otherKey.SddlProtector is { } sddl)
        {
            protectors.Add(SidKeyProtector.FromSddl(
                sddl,
                new ProtectionKeyIdentifier(kekRecipientInfo.KekId.KeyIdentifier),
                DpapiNgConstants.CreateAlgorithmOid(kekRecipientInfo.KeyEncryptionAlgorithm.Algorithm),
                kekRecipientInfo.EncryptedKey));
        }
    }

    /// <summary>
    /// Flattens an AND combiner recipient by recursively collecting its child SID and SDDL protectors.
    /// </summary>
    /// <param name="otherRecipientInfo">The other recipient to inspect.</param>
    /// <param name="protectors">The target list to append matching SID or SDDL protectors to.</param>
    private static void TryCollectOtherRecipientInfo(Pkcs7.OtherRecipientInfo otherRecipientInfo, List<SidKeyProtector> protectors)
    {
        if (otherRecipientInfo.OriType != DpapiNgConstants.AndCombinerProtected)
        {
            return;
        }

        var combiner = CombinerRecipientInfo.Decode(otherRecipientInfo.OriValue, AsnEncodingRules.DER);

        if (combiner.RecipientInfos == null)
        {
            return;
        }

        foreach (var recipient in combiner.RecipientInfos)
        {
            CollectSidKeyProtectors(recipient, protectors);
        }
    }

    /// <summary>
    /// Extracts the AES-GCM nonce from the content encryption algorithm identifier.
    /// </summary>
    /// <param name="algorithmIdentifier">The content encryption algorithm identifier.</param>
    /// <returns>The 12-byte AES-GCM nonce, or an empty buffer when the algorithm is not AES-256-GCM or its parameters are missing.</returns>
    private static ReadOnlyMemory<byte> DecodeNonce(DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier algorithmIdentifier)
    {
        if (algorithmIdentifier.Algorithm != DpapiNgConstants.OidAes256Gcm || !algorithmIdentifier.Parameters.HasValue)
        {
            return default;
        }

        var gcmParameters = GCMParameters.Decode(algorithmIdentifier.Parameters.Value, AsnEncodingRules.DER);
        return gcmParameters.AesNonce;
    }
}
