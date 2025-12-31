
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

[StructLayout(LayoutKind.Sequential)]
internal partial struct OriginatorIdentifierOrKey
{
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.IssuerAndSerialNumber? IssuerAndSerialNumber;
    internal ReadOnlyMemory<byte>? SubjectKeyIdentifier;
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorPublicKey? OriginatorKey;

#if DEBUG
    static OriginatorIdentifierOrKey()
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
        
        ensureUniqueTag(Asn1Tag.Sequence, "IssuerAndSerialNumber");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 0), "SubjectKeyIdentifier");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 1), "OriginatorKey");
    }
#endif

    internal static OriginatorIdentifierOrKey Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, out OriginatorIdentifierOrKey decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out OriginatorIdentifierOrKey decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        Asn1Tag tag = reader.PeekTag();
        
        if (tag.HasSameClassAndValue(Asn1Tag.Sequence))
        {
            DSInternals.Common.Cryptography.Asn1.Pkcs7.IssuerAndSerialNumber tmpIssuerAndSerialNumber;
            DSInternals.Common.Cryptography.Asn1.Pkcs7.IssuerAndSerialNumber.Decode(reader, out tmpIssuerAndSerialNumber);
            decoded.IssuerAndSerialNumber = tmpIssuerAndSerialNumber;

        }
        else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
        {
        decoded.SubjectKeyIdentifier = reader.ReadOctetString(new Asn1Tag(TagClass.ContextSpecific, 0));

        }
        else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
        {
            DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorPublicKey tmpOriginatorKey;
            DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorPublicKey.Decode(reader, new Asn1Tag(TagClass.ContextSpecific, 1), out tmpOriginatorKey);
            decoded.OriginatorKey = tmpOriginatorKey;

        }
        else
        {
            throw new CryptographicException();
        }
    }
}
