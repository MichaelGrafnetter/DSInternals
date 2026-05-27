using System.Formats.Asn1;
using System.Security.Cryptography;
using DSInternals.Common.Cryptography;
using Pkcs7 = DSInternals.Common.Cryptography.Asn1.Pkcs7;
using Windows.Win32;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs12;

internal partial struct Pfx
{
    private const int PfxVersionV3 = 3;
    private const string SecretBagId = "1.2.840.113549.1.12.10.1.5";
    private const string SafeContentsBagId = "1.2.840.113549.1.12.10.1.6";

    internal static CngProtectedDataBlob DecodeSidProtector(ReadOnlyMemory<byte> encoded)
    {
        Pfx pfx = Decode(encoded, AsnEncodingRules.BER);
        return pfx.GetSidProtector();
    }

    private readonly CngProtectedDataBlob GetSidProtector()
    {
        if (this.Version != PfxVersionV3)
        {
            throw new CryptographicException($"Unsupported PFX version: {this.Version}. Expected version: {PfxVersionV3}.");
        }

        if (!TryGetData(this.AuthSafe, out ReadOnlyMemory<byte> authSafeData))
        {
            throw new CryptographicException("The PFX AuthSafe is not formatted as PKCS #7 data.");
        }

        AuthenticatedSafe authenticatedSafe = AuthenticatedSafe.Decode(authSafeData, AsnEncodingRules.BER);

        foreach (Pkcs7.ContentInfo contentInfo in authenticatedSafe.Values)
        {
            if (TryGetData(contentInfo, out ReadOnlyMemory<byte> safeContentsData) &&
                TryDecodeSafeContents(safeContentsData, out CngProtectedDataBlob protector))
            {
                return protector;
            }
        }

        throw new CryptographicException("The PFX does not contain a SID-protected certificate protector.");
    }

    private static bool TryDecodeSafeContents(ReadOnlyMemory<byte> encoded, out CngProtectedDataBlob protector)
    {
        SafeContents safeContents = SafeContents.Decode(encoded, AsnEncodingRules.BER);

        foreach (SafeBag safeBag in safeContents.Values)
        {
            if (safeBag.BagId == SecretBagId && TryDecodeSecretBag(safeBag.BagValue, out protector))
            {
                return true;
            }

            if (safeBag.BagId == SafeContentsBagId && TryDecodeSafeContents(safeBag.BagValue, out protector))
            {
                return true;
            }
        }

        protector = null;
        return false;
    }

    private static bool TryDecodeSecretBag(ReadOnlyMemory<byte> encoded, out CngProtectedDataBlob protector)
    {
        SecretBag secretBag = SecretBag.Decode(encoded, AsnEncodingRules.BER);

        if (secretBag.SecretTypeId == DpapiNgConstants.MicrosoftPfxSidProtectorSecretType)
        {
            byte[] protectedSecret = AsnDecoder.ReadOctetString(secretBag.SecretValue.Span, AsnEncodingRules.BER, out int bytesConsumed);

            if (bytesConsumed != secretBag.SecretValue.Length)
            {
                throw new CryptographicException("The content is not a valid octet string.");
            }

            try
            {
                protector = CngProtectedDataBlob.Decode(protectedSecret);
                return true;
            }
            catch (CryptographicException)
            {
                // This Microsoft secret type can be present with non-SID DPAPI-NG descriptors.
            }
        }

        protector = null;
        return false;
    }

    private static bool TryGetData(Pkcs7.ContentInfo contentInfo, out ReadOnlyMemory<byte> data)
    {
        ReadOnlyMemory<byte>? contentData = contentInfo.Data;

        if (contentData.HasValue)
        {
            data = contentData.Value;
            return true;
        }

        Pkcs7.SignedData? signedData = contentInfo.SignedData;

        if (signedData.HasValue &&
            signedData.Value.EncapContentInfo.ContentType == PInvoke.szOID_PKCS_7_DATA &&
            signedData.Value.EncapContentInfo.Content.HasValue)
        {
            ReadOnlyMemory<byte> encoded = signedData.Value.EncapContentInfo.Content.Value;
            data = AsnDecoder.ReadOctetString(encoded.Span, AsnEncodingRules.BER, out int bytesConsumed);

            if (bytesConsumed != encoded.Length)
            {
                throw new CryptographicException("The content is not a valid octet string.");
            }

            return true;
        }

        data = default;
        return false;
    }
}
