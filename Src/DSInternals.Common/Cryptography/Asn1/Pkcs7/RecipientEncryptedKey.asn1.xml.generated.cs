#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct RecipientEncryptedKey
    {
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyAgreeRecipientIdentifier Rid;
        internal ReadOnlyMemory<byte> EncryptedKey;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            Rid.Encode(writer);
            writer.WriteOctetString(EncryptedKey.Span);
            writer.PopSequence(tag);
        }

        internal static RecipientEncryptedKey Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static RecipientEncryptedKey Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, expectedTag, encoded, out RecipientEncryptedKey decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out RecipientEncryptedKey decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(AsnReader reader, out RecipientEncryptedKey decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out RecipientEncryptedKey decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out RecipientEncryptedKey decoded)
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

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out RecipientEncryptedKey decoded)
        {
            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);
            ReadOnlyMemory<byte> tmpSpan;

            DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyAgreeRecipientIdentifier.Decode(ref sequenceReader, rebind, out decoded.Rid);

            if (sequenceReader.TryReadPrimitiveOctetString(out tmpSpan))
            {
                decoded.EncryptedKey = tmpSpan;
            }
            else
            {
                decoded.EncryptedKey = sequenceReader.ReadOctetString();
            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
