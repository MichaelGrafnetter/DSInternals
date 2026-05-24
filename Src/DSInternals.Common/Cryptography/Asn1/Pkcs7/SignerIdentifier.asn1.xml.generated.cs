#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
#if DEBUG
    file static class ValidateSignerIdentifier
    {
        static ValidateSignerIdentifier()
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

            ensureUniqueTag(Asn1Tag.Sequence, "IssuerAndSerialNumber");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 0), "SubjectKeyIdentifier");
        }

        [System.Runtime.CompilerServices.MethodImpl(
            System.Runtime.CompilerServices.MethodImplOptions.NoInlining |
            System.Runtime.CompilerServices.MethodImplOptions.NoOptimization)]
        internal static void Validate() { }
    }
#endif

    [StructLayout(LayoutKind.Sequential)]
    internal partial struct SignerIdentifier
    {
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.IssuerAndSerialNumber? IssuerAndSerialNumber;
        internal ReadOnlyMemory<byte>? SubjectKeyIdentifier;

#if DEBUG
        static SignerIdentifier()
        {
            ValidateSignerIdentifier.Validate();
        }
#endif

        internal readonly void Encode(AsnWriter writer)
        {
            bool wroteValue = false;

            if (IssuerAndSerialNumber.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                IssuerAndSerialNumber.Value.Encode(writer);
                wroteValue = true;
            }

            if (SubjectKeyIdentifier.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                writer.WriteOctetString(SubjectKeyIdentifier.Value.Span, new Asn1Tag(TagClass.ContextSpecific, 0));
                wroteValue = true;
            }

            if (!wroteValue)
            {
                throw new CryptographicException();
            }
        }

        internal static SignerIdentifier Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, encoded, out SignerIdentifier decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(AsnReader reader, out SignerIdentifier decoded)
        {
            Decode(ref reader, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out SignerIdentifier decoded)
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

        private static void DecodeCore(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out SignerIdentifier decoded)
        {
            decoded = default;
            Asn1Tag tag = reader.PeekTag();
            ReadOnlyMemory<byte> tmpSpan;

            if (tag.HasSameClassAndValue(Asn1Tag.Sequence))
            {
                DSInternals.Common.Cryptography.Asn1.Pkcs7.IssuerAndSerialNumber tmpIssuerAndSerialNumber;
                DSInternals.Common.Cryptography.Asn1.Pkcs7.IssuerAndSerialNumber.Decode(ref reader, rebind, out tmpIssuerAndSerialNumber);
                decoded.IssuerAndSerialNumber = tmpIssuerAndSerialNumber;

            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
            {

                if (reader.TryReadPrimitiveOctetString(out tmpSpan, new Asn1Tag(TagClass.ContextSpecific, 0)))
                {
                    decoded.SubjectKeyIdentifier = tmpSpan;
                }
                else
                {
                    decoded.SubjectKeyIdentifier = reader.ReadOctetString(new Asn1Tag(TagClass.ContextSpecific, 0));
                }

            }
            else
            {
                throw new CryptographicException();
            }
        }
    }
}
