#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct OriginatorPublicKey
    {
        internal DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier Algorithm;
        internal ReadOnlyMemory<byte> PublicKey;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            Algorithm.Encode(writer);
            writer.WriteBitString(PublicKey.Span, 0);
            writer.PopSequence(tag);
        }

        internal static OriginatorPublicKey Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static OriginatorPublicKey Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, expectedTag, encoded, out OriginatorPublicKey decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out OriginatorPublicKey decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(AsnReader reader, out OriginatorPublicKey decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out OriginatorPublicKey decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out OriginatorPublicKey decoded)
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

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out OriginatorPublicKey decoded)
        {
            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);
            ReadOnlyMemory<byte> tmpSpan;

            DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier.Decode(ref sequenceReader, rebind, out decoded.Algorithm);

            if (sequenceReader.TryReadPrimitiveBitString(out _, out tmpSpan))
            {
                decoded.PublicKey = tmpSpan;
            }
            else
            {
                decoded.PublicKey = sequenceReader.ReadBitString(out _);
            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
