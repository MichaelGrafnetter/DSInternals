#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
#if DEBUG
    file static class ValidateRecipientInfo
    {
        static ValidateRecipientInfo()
        {
            var usedTags = new System.Collections.Generic.Dictionary<Asn1Tag, string>();
            Action<Asn1Tag, string> ensureUniqueTag = (tag, fieldName) =>
            {
                if (usedTags.TryGetValue(tag, out string? existing))
                {
                    throw new InvalidOperationException($"Tag '{tag}' is in use by both '{existing}' and '{fieldName}'");
                }

                usedTags.Add(tag, fieldName);
            };

            ensureUniqueTag(Asn1Tag.Sequence, "KeyTransRecipientInfo");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 1), "KeyAgreeRecipientInfo");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 2), "KEKRecipientInfo");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 4), "OtherRecipientInfo");
        }

        [System.Runtime.CompilerServices.MethodImpl(
            System.Runtime.CompilerServices.MethodImplOptions.NoInlining |
            System.Runtime.CompilerServices.MethodImplOptions.NoOptimization)]
        internal static void Validate() { }
    }
#endif

    [StructLayout(LayoutKind.Sequential)]
    internal partial struct RecipientInfo
    {
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyTransRecipientInfo? KeyTransRecipientInfo;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyAgreeRecipientInfo? KeyAgreeRecipientInfo;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.KEKRecipientInfo? KEKRecipientInfo;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.OtherRecipientInfo? OtherRecipientInfo;

#if DEBUG
        static RecipientInfo()
        {
            ValidateRecipientInfo.Validate();
        }
#endif

        internal readonly void Encode(AsnWriter writer)
        {
            bool wroteValue = false;

            if (KeyTransRecipientInfo.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                KeyTransRecipientInfo.Value.Encode(writer);
                wroteValue = true;
            }

            if (KeyAgreeRecipientInfo.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                KeyAgreeRecipientInfo.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 1));
                wroteValue = true;
            }

            if (KEKRecipientInfo.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                KEKRecipientInfo.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 2));
                wroteValue = true;
            }

            if (OtherRecipientInfo.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                OtherRecipientInfo.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 4));
                wroteValue = true;
            }

            if (!wroteValue)
            {
                throw new CryptographicException();
            }
        }

        internal static RecipientInfo Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, encoded, out RecipientInfo decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(AsnReader reader, out RecipientInfo decoded)
        {
            Decode(ref reader, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out RecipientInfo decoded)
        {
            try
            {
                DecodeCore(ref reader, rebind, out decoded);
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        private static void DecodeCore(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out RecipientInfo decoded)
        {
            decoded = default;
            Asn1Tag tag = reader.PeekTag();

            if (tag.HasSameClassAndValue(Asn1Tag.Sequence))
            {
                DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyTransRecipientInfo tmpKeyTransRecipientInfo;
                DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyTransRecipientInfo.Decode(ref reader, rebind, out tmpKeyTransRecipientInfo);
                decoded.KeyTransRecipientInfo = tmpKeyTransRecipientInfo;

            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {
                DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyAgreeRecipientInfo tmpKeyAgreeRecipientInfo;
                DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyAgreeRecipientInfo.Decode(ref reader, new Asn1Tag(TagClass.ContextSpecific, 1), rebind, out tmpKeyAgreeRecipientInfo);
                decoded.KeyAgreeRecipientInfo = tmpKeyAgreeRecipientInfo;

            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 2)))
            {
                DSInternals.Common.Cryptography.Asn1.Pkcs7.KEKRecipientInfo tmpKEKRecipientInfo;
                DSInternals.Common.Cryptography.Asn1.Pkcs7.KEKRecipientInfo.Decode(ref reader, new Asn1Tag(TagClass.ContextSpecific, 2), rebind, out tmpKEKRecipientInfo);
                decoded.KEKRecipientInfo = tmpKEKRecipientInfo;

            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 4)))
            {
                DSInternals.Common.Cryptography.Asn1.Pkcs7.OtherRecipientInfo tmpOtherRecipientInfo;
                DSInternals.Common.Cryptography.Asn1.Pkcs7.OtherRecipientInfo.Decode(ref reader, new Asn1Tag(TagClass.ContextSpecific, 4), rebind, out tmpOtherRecipientInfo);
                decoded.OtherRecipientInfo = tmpOtherRecipientInfo;

            }
            else
            {
                throw new CryptographicException();
            }
        }
    }
}
