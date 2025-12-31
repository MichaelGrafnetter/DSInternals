
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

[StructLayout(LayoutKind.Sequential)]
internal partial struct CertificateChoice
{
    internal ReadOnlyMemory<byte>? Certificate;
    internal ReadOnlyMemory<byte>? ExtendedCertificate;
    internal ReadOnlyMemory<byte>? AttributeCertificateV1;
    internal ReadOnlyMemory<byte>? AttributeCertificateV2;
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.OtherCertificateFormat? OtherCertificateFormat;

#if DEBUG
    static CertificateChoice()
    {
        var usedTags = new System.Collections.Generic.Dictionary<Asn1Tag, string>();
        Action<Asn1Tag, string> ensureUniqueTag = (tag, fieldName) =>
        {
            if (usedTags.TryGetValue(tag, out string existing))
            {
                throw new InvalidOperationException($"Tag '{tag}' is in use by both '{existing}' and '{fieldName}'");
            }

            usedTags.Add(tag, fieldName);
        };
        
        ensureUniqueTag(new Asn1Tag((UniversalTagNumber)16), "Certificate");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 0), "ExtendedCertificate");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 1), "AttributeCertificateV1");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 2), "AttributeCertificateV2");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 3), "OtherCertificateFormat");
    }
#endif

    internal static CertificateChoice Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, out CertificateChoice decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out CertificateChoice decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        Asn1Tag tag = reader.PeekTag();
        
        if (tag.HasSameClassAndValue(new Asn1Tag((UniversalTagNumber)16)))
        {
            decoded.Certificate = reader.ReadEncodedValue();
        }
        else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
        {
            decoded.ExtendedCertificate = reader.ReadEncodedValue();
        }
        else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
        {
            decoded.AttributeCertificateV1 = reader.ReadEncodedValue();
        }
        else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 2)))
        {
            decoded.AttributeCertificateV2 = reader.ReadEncodedValue();
        }
        else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 3)))
        {
            DSInternals.Common.Cryptography.Asn1.Pkcs7.OtherCertificateFormat tmpOtherCertificateFormat;
            DSInternals.Common.Cryptography.Asn1.Pkcs7.OtherCertificateFormat.Decode(reader, new Asn1Tag(TagClass.ContextSpecific, 3), out tmpOtherCertificateFormat);
            decoded.OtherCertificateFormat = tmpOtherCertificateFormat;

        }
        else
        {
            throw new CryptographicException();
        }
    }
}
