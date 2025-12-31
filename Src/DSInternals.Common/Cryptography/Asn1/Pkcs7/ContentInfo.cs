using System.Formats.Asn1;
using System.Security.Cryptography;
using Windows.Win32;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

internal struct ContentInfo
{
    internal string ContentType;
    internal ReadOnlyMemory<byte> Content;
    internal ReadOnlyMemory<byte> AdditionalContent;

    public EnvelopedData? EnvelopedData
    {
        get
        {
            if (this.ContentType != PInvoke.szOID_PKCS_7_ENVELOPED)
            {
                return null;
            }

            // This is the OID for enveloped data content type in PKCS#7
            return Pkcs7.EnvelopedData.Decode(this.Content, AsnEncodingRules.DER);
        }
    }

    public ReadOnlyMemory<byte>? Data
    {
        get
        {
            if (this.ContentType != PInvoke.szOID_PKCS_7_DATA)
            {
                return null;
            }

            // This is the OID for data content type in PKCS#7
            // TODO: Replace byte[] with ReadOnlyMemory<byte> for better performance.
            byte[] decoded = AsnDecoder.ReadOctetString(this.Content.Span, AsnEncodingRules.DER, out int bytesConsumed);

            if (bytesConsumed != this.Content.Length)
            {
                throw new CryptographicException("The content is not a valid octet string.");
            }

            return decoded;
        }
    }

    public SignedData? SignedData
    {
        get
        {
            if (this.ContentType != PInvoke.szOID_PKCS_7_SIGNED)
            {
                return null;
            }

            // This is the OID for signed data content type in PKCS#7
            return Pkcs7.SignedData.Decode(this.Content, AsnEncodingRules.DER);
        }
    }

    internal static ContentInfo Decode(ReadOnlyMemory<byte> encoded)
    {
        AsnReader reader = new AsnReader(encoded, AsnEncodingRules.DER);
        var decoded = Decode(reader);

        // DPAPI NG stores the encrypted data after the ASN.1 sequence.
        AsnDecoder.ReadSequence(encoded.Span, AsnEncodingRules.DER, out _, out _, out int contentLength);
        decoded.AdditionalContent = encoded.Slice(contentLength);

        return decoded;
    }

    internal static ContentInfo Decode(AsnReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);

        // ContentInfo ::= SEQUENCE {
        //    contentType ContentType,
        //    content[0] EXPLICIT ANY DEFINED BY contentType OPTIONAL }

        ContentInfo decoded = default;

        var contentInfoReader = reader.ReadSequence();
        decoded.ContentType = contentInfoReader.ReadObjectIdentifier();

        var contentReader = contentInfoReader.ReadSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
        decoded.Content = contentReader.ReadEncodedValue();

        return decoded;
    }
}
