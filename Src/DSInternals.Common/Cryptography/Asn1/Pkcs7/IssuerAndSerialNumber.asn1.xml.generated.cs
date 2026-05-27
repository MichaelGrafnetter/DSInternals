#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct IssuerAndSerialNumber
    {
        internal ReadOnlyMemory<byte> Issuer;
        internal ReadOnlyMemory<byte> SerialNumber;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            // Validator for tag constraint for Issuer
            {
                if (!Asn1Tag.TryDecode(Issuer.Span, out Asn1Tag validateTag, out _) ||
                    !validateTag.HasSameClassAndValue(new Asn1Tag((UniversalTagNumber)16)))
                {
                    throw new CryptographicException();
                }
            }

            try
            {
                writer.WriteEncodedValue(Issuer.Span);
            }
            catch (ArgumentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
            writer.WriteInteger(SerialNumber.Span);
            writer.PopSequence(tag);
        }

        internal static IssuerAndSerialNumber Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static IssuerAndSerialNumber Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, expectedTag, encoded, out IssuerAndSerialNumber decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out IssuerAndSerialNumber decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(AsnReader reader, out IssuerAndSerialNumber decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out IssuerAndSerialNumber decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out IssuerAndSerialNumber decoded)
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

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out IssuerAndSerialNumber decoded)
        {
            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);
            ReadOnlyMemory<byte> tmpSpan;

            if (!sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag((UniversalTagNumber)16)))
            {
                throw new CryptographicException();
            }

            tmpSpan = sequenceReader.ReadEncodedValue();
            decoded.Issuer = tmpSpan;
            tmpSpan = sequenceReader.ReadIntegerBytes();
            decoded.SerialNumber = tmpSpan;

            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
