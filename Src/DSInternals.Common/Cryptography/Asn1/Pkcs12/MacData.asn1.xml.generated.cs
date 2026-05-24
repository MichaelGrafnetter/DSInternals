#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs12
{
    file static class SharedMacData
    {
        internal static readonly byte[] DefaultIterationCount = { 0x02, 0x01, 0x01 };

#if DEBUG
        static SharedMacData()
        {
            MacData decoded = default;
            AsnReader reader;

            reader = new AsnReader(SharedMacData.DefaultIterationCount, AsnEncodingRules.DER);

            if (!reader.TryReadInt32(out decoded.IterationCount))
            {
                reader.ThrowIfNotEmpty();
            }

            reader.ThrowIfNotEmpty();
        }
#endif
    }

    [StructLayout(LayoutKind.Sequential)]
    internal partial struct MacData
    {
        internal DSInternals.Common.Cryptography.Asn1.Pkcs1.DigestInfo Mac;
        internal ReadOnlyMemory<byte> MacSalt;
        internal int IterationCount;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            Mac.Encode(writer);
            writer.WriteOctetString(MacSalt.Span);

            // DEFAULT value handler for IterationCount.
            {
                const int AsnManagedIntegerDerMaxEncodeSize = 6;
                AsnWriter tmp = new AsnWriter(AsnEncodingRules.DER, initialCapacity: AsnManagedIntegerDerMaxEncodeSize);
                tmp.WriteInteger(IterationCount);

                if (!tmp.EncodedValueEquals(SharedMacData.DefaultIterationCount))
                {
                    tmp.CopyTo(writer);
                }
            }

            writer.PopSequence(tag);
        }

        internal static MacData Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static MacData Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, expectedTag, encoded, out MacData decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out MacData decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(AsnReader reader, out MacData decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out MacData decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out MacData decoded)
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

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out MacData decoded)
        {
            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnReader defaultReader;
            ReadOnlyMemory<byte> tmpSpan;

            DSInternals.Common.Cryptography.Asn1.Pkcs1.DigestInfo.Decode(ref sequenceReader, rebind, out decoded.Mac);

            if (sequenceReader.TryReadPrimitiveOctetString(out tmpSpan))
            {
                decoded.MacSalt = tmpSpan;
            }
            else
            {
                decoded.MacSalt = sequenceReader.ReadOctetString();
            }


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(Asn1Tag.Integer))
            {

                if (!sequenceReader.TryReadInt32(out decoded.IterationCount))
                {
                    sequenceReader.ThrowIfNotEmpty();
                }

            }
            else
            {
                defaultReader = new AsnReader(SharedMacData.DefaultIterationCount, AsnEncodingRules.DER);

                if (!defaultReader.TryReadInt32(out decoded.IterationCount))
                {
                    defaultReader.ThrowIfNotEmpty();
                }

            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
