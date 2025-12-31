
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

[StructLayout(LayoutKind.Sequential)]
internal partial struct RecipientInfo
{
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyTransRecipientInfo? KeyTransRecipientInfo;
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyAgreeRecipientInfo? KeyAgreeRecipientInfo;
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.KEKRecipientInfo? KEKRecipientInfo;

#if DEBUG
    static RecipientInfo()
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
        
        ensureUniqueTag(Asn1Tag.Sequence, "KeyTransRecipientInfo");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 1), "KeyAgreeRecipientInfo");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 2), "KEKRecipientInfo");
    }
#endif

    internal static RecipientInfo Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, out RecipientInfo decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out RecipientInfo decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        Asn1Tag tag = reader.PeekTag();
        
        if (tag.HasSameClassAndValue(Asn1Tag.Sequence))
        {
            DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyTransRecipientInfo tmpKeyTransRecipientInfo;
            DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyTransRecipientInfo.Decode(reader, out tmpKeyTransRecipientInfo);
            decoded.KeyTransRecipientInfo = tmpKeyTransRecipientInfo;

        }
        else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
        {
            DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyAgreeRecipientInfo tmpKeyAgreeRecipientInfo;
            DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyAgreeRecipientInfo.Decode(reader, new Asn1Tag(TagClass.ContextSpecific, 1), out tmpKeyAgreeRecipientInfo);
            decoded.KeyAgreeRecipientInfo = tmpKeyAgreeRecipientInfo;

        }
        else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 2)))
        {
            DSInternals.Common.Cryptography.Asn1.Pkcs7.KEKRecipientInfo tmpKEKRecipientInfo;
            DSInternals.Common.Cryptography.Asn1.Pkcs7.KEKRecipientInfo.Decode(reader, new Asn1Tag(TagClass.ContextSpecific, 2), out tmpKEKRecipientInfo);
            decoded.KEKRecipientInfo = tmpKEKRecipientInfo;

        }
        else
        {
            throw new CryptographicException();
        }
    }
}
