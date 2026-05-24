#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
#if DEBUG
    file static class ValidateCertificateChoice
    {
        static ValidateCertificateChoice()
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

            ensureUniqueTag(new Asn1Tag((UniversalTagNumber)16), "Certificate");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 0), "ExtendedCertificate");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 1), "AttributeCertificateV1");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 2), "AttributeCertificateV2");
            ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 3), "OtherCertificateFormat");
        }

        [System.Runtime.CompilerServices.MethodImpl(
            System.Runtime.CompilerServices.MethodImplOptions.NoInlining |
            System.Runtime.CompilerServices.MethodImplOptions.NoOptimization)]
        internal static void Validate() { }
    }
#endif

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
            ValidateCertificateChoice.Validate();
        }
#endif

        internal readonly void Encode(AsnWriter writer)
        {
            bool wroteValue = false;

            if (Certificate.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                // Validator for tag constraint for Certificate
                {
                    if (!Asn1Tag.TryDecode(Certificate.Value.Span, out Asn1Tag validateTag, out _) ||
                        !validateTag.HasSameClassAndValue(new Asn1Tag((UniversalTagNumber)16)))
                    {
                        throw new CryptographicException();
                    }
                }

                try
                {
                    writer.WriteEncodedValue(Certificate.Value.Span);
                }
                catch (ArgumentException e)
                {
                    throw new CryptographicException("ASN1 corrupted data.", e);
                }
                wroteValue = true;
            }

            if (ExtendedCertificate.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                // Validator for tag constraint for ExtendedCertificate
                {
                    if (!Asn1Tag.TryDecode(ExtendedCertificate.Value.Span, out Asn1Tag validateTag, out _) ||
                        !validateTag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
                    {
                        throw new CryptographicException();
                    }
                }

                try
                {
                    writer.WriteEncodedValue(ExtendedCertificate.Value.Span);
                }
                catch (ArgumentException e)
                {
                    throw new CryptographicException("ASN1 corrupted data.", e);
                }
                wroteValue = true;
            }

            if (AttributeCertificateV1.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                // Validator for tag constraint for AttributeCertificateV1
                {
                    if (!Asn1Tag.TryDecode(AttributeCertificateV1.Value.Span, out Asn1Tag validateTag, out _) ||
                        !validateTag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
                    {
                        throw new CryptographicException();
                    }
                }

                try
                {
                    writer.WriteEncodedValue(AttributeCertificateV1.Value.Span);
                }
                catch (ArgumentException e)
                {
                    throw new CryptographicException("ASN1 corrupted data.", e);
                }
                wroteValue = true;
            }

            if (AttributeCertificateV2.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                // Validator for tag constraint for AttributeCertificateV2
                {
                    if (!Asn1Tag.TryDecode(AttributeCertificateV2.Value.Span, out Asn1Tag validateTag, out _) ||
                        !validateTag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 2)))
                    {
                        throw new CryptographicException();
                    }
                }

                try
                {
                    writer.WriteEncodedValue(AttributeCertificateV2.Value.Span);
                }
                catch (ArgumentException e)
                {
                    throw new CryptographicException("ASN1 corrupted data.", e);
                }
                wroteValue = true;
            }

            if (OtherCertificateFormat.HasValue)
            {
                if (wroteValue)
                    throw new CryptographicException();

                OtherCertificateFormat.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 3));
                wroteValue = true;
            }

            if (!wroteValue)
            {
                throw new CryptographicException();
            }
        }

        internal static CertificateChoice Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, encoded, out CertificateChoice decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(AsnReader reader, out CertificateChoice decoded)
        {
            Decode(ref reader, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out CertificateChoice decoded)
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

        private static void DecodeCore(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out CertificateChoice decoded)
        {
            decoded = default;
            Asn1Tag tag = reader.PeekTag();
            ReadOnlyMemory<byte> tmpSpan;

            if (tag.HasSameClassAndValue(new Asn1Tag((UniversalTagNumber)16)))
            {
                tmpSpan = reader.ReadEncodedValue();
                decoded.Certificate = tmpSpan;
            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
            {
                tmpSpan = reader.ReadEncodedValue();
                decoded.ExtendedCertificate = tmpSpan;
            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {
                tmpSpan = reader.ReadEncodedValue();
                decoded.AttributeCertificateV1 = tmpSpan;
            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 2)))
            {
                tmpSpan = reader.ReadEncodedValue();
                decoded.AttributeCertificateV2 = tmpSpan;
            }
            else if (tag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 3)))
            {
                DSInternals.Common.Cryptography.Asn1.Pkcs7.OtherCertificateFormat tmpOtherCertificateFormat;
                DSInternals.Common.Cryptography.Asn1.Pkcs7.OtherCertificateFormat.Decode(ref reader, new Asn1Tag(TagClass.ContextSpecific, 3), rebind, out tmpOtherCertificateFormat);
                decoded.OtherCertificateFormat = tmpOtherCertificateFormat;

            }
            else
            {
                throw new CryptographicException();
            }
        }
    }
}
