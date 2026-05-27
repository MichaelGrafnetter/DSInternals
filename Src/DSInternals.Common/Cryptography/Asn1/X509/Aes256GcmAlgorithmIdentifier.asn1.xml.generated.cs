#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.X509
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct Aes256GcmAlgorithmIdentifier
    {
        internal string Algorithm;
        internal DSInternals.Common.Cryptography.Asn1.X509.GCMParameters Parameters;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            try
            {
                writer.WriteObjectIdentifier(Algorithm);
            }
            catch (ArgumentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
            Parameters.Encode(writer);
            writer.PopSequence(tag);
        }

        internal static Aes256GcmAlgorithmIdentifier Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static Aes256GcmAlgorithmIdentifier Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, expectedTag, encoded, out Aes256GcmAlgorithmIdentifier decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out Aes256GcmAlgorithmIdentifier decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(AsnReader reader, out Aes256GcmAlgorithmIdentifier decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out Aes256GcmAlgorithmIdentifier decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out Aes256GcmAlgorithmIdentifier decoded)
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

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out Aes256GcmAlgorithmIdentifier decoded)
        {
            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);

            decoded.Algorithm = sequenceReader.ReadObjectIdentifier();
            DSInternals.Common.Cryptography.Asn1.X509.GCMParameters.Decode(ref sequenceReader, rebind, out decoded.Parameters);

            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
