#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct SignerInfo
    {
        internal int Version;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.SignerIdentifier Sid;
        internal DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier DigestAlgorithm;
        internal ReadOnlyMemory<byte>? SignedAttributes;
        internal DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier SignatureAlgorithm;
        internal ReadOnlyMemory<byte> SignatureValue;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute[]? UnsignedAttributes;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            writer.WriteInteger(Version);
            Sid.Encode(writer);
            DigestAlgorithm.Encode(writer);

            if (SignedAttributes.HasValue)
            {
                // Validator for tag constraint for SignedAttributes
                {
                    if (!Asn1Tag.TryDecode(SignedAttributes.Value.Span, out Asn1Tag validateTag, out _) ||
                        !validateTag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
                    {
                        throw new CryptographicException();
                    }
                }

                try
                {
                    writer.WriteEncodedValue(SignedAttributes.Value.Span);
                }
                catch (ArgumentException e)
                {
                    throw new CryptographicException("ASN1 corrupted data.", e);
                }
            }

            SignatureAlgorithm.Encode(writer);
            writer.WriteOctetString(SignatureValue.Span);

            if (UnsignedAttributes != null)
            {

                writer.PushSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                for (int i = 0; i < UnsignedAttributes.Length; i++)
                {
                    UnsignedAttributes[i].Encode(writer);
                }
                writer.PopSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));

            }

            writer.PopSequence(tag);
        }

        internal static SignerInfo Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static SignerInfo Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, expectedTag, encoded, out SignerInfo decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out SignerInfo decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(AsnReader reader, out SignerInfo decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out SignerInfo decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out SignerInfo decoded)
        {
            try
            {
                DecodeCore(ref reader, expectedTag, rebind, out decoded);
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out SignerInfo decoded)
        {
            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnReader collectionReader;
            ReadOnlyMemory<byte> tmpSpan;


            if (!sequenceReader.TryReadInt32(out decoded.Version))
            {
                sequenceReader.ThrowIfNotEmpty();
            }

            DSInternals.Common.Cryptography.Asn1.Pkcs7.SignerIdentifier.Decode(ref sequenceReader, rebind, out decoded.Sid);
            DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier.Decode(ref sequenceReader, rebind, out decoded.DigestAlgorithm);

            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
            {
                tmpSpan = sequenceReader.ReadEncodedValue();
                decoded.SignedAttributes = tmpSpan;
            }

            DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier.Decode(ref sequenceReader, rebind, out decoded.SignatureAlgorithm);

            if (sequenceReader.TryReadPrimitiveOctetString(out tmpSpan))
            {
                decoded.SignatureValue = tmpSpan;
            }
            else
            {
                decoded.SignatureValue = sequenceReader.ReadOctetString();
            }


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {

                // Decode SEQUENCE OF for UnsignedAttributes
                {
                    collectionReader = sequenceReader.ReadSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                    var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute>();
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute tmpItem;

                    while (collectionReader.HasData)
                    {
                        DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute.Decode(ref collectionReader, rebind, out tmpItem);
                        tmpList.Add(tmpItem);
                    }

                    decoded.UnsignedAttributes = tmpList.ToArray();
                }

            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
