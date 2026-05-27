#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs12
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct Pfx
    {
        internal int Version;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.ContentInfo AuthSafe;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs12.MacData? MacData;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            writer.WriteInteger(Version);

            AuthSafe.Encode(writer);

            if (MacData.HasValue)
            {
                MacData.Value.Encode(writer);
            }

            writer.PopSequence(tag);
        }

        internal static Pfx Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static Pfx Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, expectedTag, encoded, out Pfx decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out Pfx decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(AsnReader reader, out Pfx decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out Pfx decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out Pfx decoded)
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

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out Pfx decoded)
        {
            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);


            if (!sequenceReader.TryReadInt32(out decoded.Version))
            {
                sequenceReader.ThrowIfNotEmpty();
            }

            DSInternals.Common.Cryptography.Asn1.Pkcs7.ContentInfo.Decode(ref sequenceReader, rebind, out decoded.AuthSafe);

            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(Asn1Tag.Sequence))
            {
                DSInternals.Common.Cryptography.Asn1.Pkcs12.MacData tmpMacData;
                DSInternals.Common.Cryptography.Asn1.Pkcs12.MacData.Decode(ref sequenceReader, rebind, out tmpMacData);
                decoded.MacData = tmpMacData;

            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
