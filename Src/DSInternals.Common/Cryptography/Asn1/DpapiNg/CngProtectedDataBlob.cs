using System.Security.Cryptography;
using System.Security.Principal;
using DSInternals.Common.Data;
using DSInternals.Common.Interop;

namespace DSInternals.Common.Cryptography.Asn1.DpapiNg;

/// <summary>
/// Represents DPAPI-NG protected data, formatted as CMS (certificate message syntax) enveloped content.
/// </summary>
public class CngProtectedDataBlob
{
    public ReadOnlyMemory<byte> RawData { get; private set; }
    public ProtectionKeyIdentifier ProtectionKeyIdentifier { get; private set; }
    public SecurityIdentifier? TargetSid { get; private set; }
    public Oid ContentEncryptionAlgorithm { get; private set; }
    public Oid KeyEncryptionAlgorithm { get; private set; }
    public ReadOnlyMemory<byte> EncryptedKey { get; private set; }
    public ReadOnlyMemory<byte> EncryptedData { get; private set; }
    public ReadOnlyMemory<byte> Nonce { get; private set; }


    public ReadOnlySpan<byte> Decrypt()
    {
        if (this.RawData.Length == 0)
        {
            return default;
        }

        // Use the native Win32 API to perform the decryption
        var resultCode = NativeMethods.NCryptUnprotectSecret(this.RawData.Span, out var decryptedData);
        Validator.AssertSuccess(resultCode);
        return decryptedData;
    }

    public bool TryDecrypt(out ReadOnlySpan<byte> cleartext)
    {
        if (this.RawData.Length == 0)
        {
            // Nothing to decrypt
            cleartext = default;
            return true;
        }

        // Use the native Win32 API to perform the decryption
        var resultCode = NativeMethods.NCryptUnprotectSecret(this.RawData.Span, out cleartext);

        return resultCode == Win32ErrorCode.Success;
    }

    public static CngProtectedDataBlob Decode(ReadOnlyMemory<byte> blob)
    {
        var cms = Cryptography.Asn1.Pkcs7.ContentInfo.Decode(blob);
        var envelopedData = cms.EnvelopedData;

        if (!envelopedData.HasValue)
        {
            throw new CryptographicException("The data is not formatted as CMS enveloped content.");
        }

        // TODO: Check enveloped data version

        // Search for the DPAPI-NG SID and SDDL data protectors
        foreach (var recipient in envelopedData.Value.RecipientInfos)
        {
            if (recipient.KEKRecipientInfo.HasValue)
            {
                var kekRecipientInfo = recipient.KEKRecipientInfo.Value;
                var kekId = kekRecipientInfo.KekId;

                if (kekRecipientInfo.KekId.Other.HasValue)
                {
                    SecurityIdentifier? targetSid = kekId.Other.Value.SidProtector;

                    if (targetSid != null)
                    {
                        var groupKeyIdentifier = kekId.KeyIdentifier;

                        // TODO: Support reading multiple protectors, instead of stopping after reading the first one.
                        return new CngProtectedDataBlob()
                        {
                            RawData = blob,
                            TargetSid = targetSid,
                            ProtectionKeyIdentifier = new ProtectionKeyIdentifier(groupKeyIdentifier),
                            ContentEncryptionAlgorithm = envelopedData.Value.EncryptedContentInfo.ContentEncryptionAlgorithm.Algorithm,
                            KeyEncryptionAlgorithm = kekRecipientInfo.KeyEncryptionAlgorithm.Algorithm,
                            EncryptedKey = kekRecipientInfo.EncryptedKey,
                            EncryptedData = envelopedData.Value.EncryptedContentInfo.EncryptedContent ?? default
                            // TODO: Parse Nonce
                        };
                    }
                }
            }
        }

        // Sentinel
        throw new CryptographicException("Could not parse the DPAPI-NG protected data blob.");
    }
}
