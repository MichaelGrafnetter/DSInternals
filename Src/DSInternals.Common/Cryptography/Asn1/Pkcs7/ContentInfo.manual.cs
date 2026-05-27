using System.Formats.Asn1;
using System.Security.Cryptography;
using Windows.Win32;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

/// <summary>
/// Adds DPAPI-NG specific extensions to the generated <see cref="ContentInfo" /> parser.
/// </summary>
internal partial struct ContentInfo
{
    /// <summary>
    /// Gets any DPAPI-NG-specific bytes that appear after the ASN.1 sequence in the original encoding.
    /// </summary>
    /// <value>The trailing bytes that follow the canonical CMS content info, or an empty buffer when none are present.</value>
    internal ReadOnlyMemory<byte> AdditionalContent;

    /// <summary>
    /// Gets the inner content decoded as a CMS <see cref="Pkcs7.EnvelopedData" /> when <see cref="ContentType" /> identifies enveloped data.
    /// </summary>
    /// <value>The decoded enveloped data, or <see langword="null" /> when this instance is not enveloped data or has no content.</value>
    /// <exception cref="CryptographicException">The inner content is not a valid CMS enveloped data sequence.</exception>
    public EnvelopedData? EnvelopedData
    {
        get
        {
            if (this.ContentType != PInvoke.szOID_PKCS_7_ENVELOPED || !this.Content.HasValue)
            {
                return null;
            }

            return Pkcs7.EnvelopedData.Decode(this.Content.Value, AsnEncodingRules.DER);
        }
    }

    /// <summary>
    /// Gets the inner content as a PKCS #7 data octet string when <see cref="ContentType" /> identifies raw data.
    /// </summary>
    /// <value>The octet-string-decoded content, or <see langword="null" /> when this instance is not raw data or has no content.</value>
    /// <exception cref="CryptographicException">The inner content is not a valid octet string.</exception>
    public ReadOnlyMemory<byte>? Data
    {
        get
        {
            if (this.ContentType != PInvoke.szOID_PKCS_7_DATA || !this.Content.HasValue)
            {
                return null;
            }

            byte[] decoded = AsnDecoder.ReadOctetString(this.Content.Value.Span, AsnEncodingRules.BER, out int bytesConsumed);

            if (bytesConsumed != this.Content.Value.Length)
            {
                throw new CryptographicException("The content is not a valid octet string.");
            }

            return decoded;
        }
    }

    /// <summary>
    /// Gets the inner content decoded as a CMS <see cref="Pkcs7.SignedData" /> when <see cref="ContentType" /> identifies signed data.
    /// </summary>
    /// <value>The decoded signed data, or <see langword="null" /> when this instance is not signed data or has no content.</value>
    /// <exception cref="CryptographicException">The inner content is not a valid CMS signed data sequence.</exception>
    public SignedData? SignedData
    {
        get
        {
            if (this.ContentType != PInvoke.szOID_PKCS_7_SIGNED || !this.Content.HasValue)
            {
                return null;
            }

            return Pkcs7.SignedData.Decode(this.Content.Value, AsnEncodingRules.DER);
        }
    }

    /// <summary>
    /// Decodes a CMS content info and captures any DPAPI-NG-specific bytes that follow the ASN.1 sequence.
    /// </summary>
    /// <param name="encoded">The DER or BER encoded content info, optionally followed by DPAPI-NG trailing bytes.</param>
    /// <returns>The decoded content info with <see cref="AdditionalContent" /> populated from any trailing bytes.</returns>
    /// <exception cref="CryptographicException">The content info is malformed.</exception>
    internal static ContentInfo Decode(ReadOnlyMemory<byte> encoded)
    {
        AsnReader reader = new AsnReader(encoded, AsnEncodingRules.DER);
        Decode(reader, out ContentInfo decoded);

        // DPAPI-NG stores the encrypted data after the ASN.1 sequence.
        AsnDecoder.ReadSequence(encoded.Span, AsnEncodingRules.DER, out _, out _, out int contentLength);
        decoded.AdditionalContent = encoded.Slice(contentLength);

        return decoded;
    }
}
