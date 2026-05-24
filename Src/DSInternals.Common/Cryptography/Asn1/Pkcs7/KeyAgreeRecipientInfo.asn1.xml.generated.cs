#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct KeyAgreeRecipientInfo
    {
        internal int Version;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorIdentifierOrKey Originator;
        internal ReadOnlyMemory<byte>? Ukm;
        internal DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier KeyEncryptionAlgorithm;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientEncryptedKey[] RecipientEncryptedKeys;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            writer.WriteInteger(Version);
            writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
            Originator.Encode(writer);
            writer.PopSequence(new Asn1Tag(TagClass.ContextSpecific, 0));

            if (Ukm.HasValue)
            {
                writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 1));
                writer.WriteOctetString(Ukm.Value.Span);
                writer.PopSequence(new Asn1Tag(TagClass.ContextSpecific, 1));
            }

            KeyEncryptionAlgorithm.Encode(writer);

            writer.PushSequence();
            for (int i = 0; i < RecipientEncryptedKeys.Length; i++)
            {
                RecipientEncryptedKeys[i].Encode(writer);
            }
            writer.PopSequence();

            writer.PopSequence(tag);
        }

        internal static KeyAgreeRecipientInfo Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static KeyAgreeRecipientInfo Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, expectedTag, encoded, out KeyAgreeRecipientInfo decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out KeyAgreeRecipientInfo decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(AsnReader reader, out KeyAgreeRecipientInfo decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out KeyAgreeRecipientInfo decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out KeyAgreeRecipientInfo decoded)
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

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out KeyAgreeRecipientInfo decoded)
        {
            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnReader explicitReader;
            AsnReader collectionReader;
            ReadOnlyMemory<byte> tmpSpan;


            if (!sequenceReader.TryReadInt32(out decoded.Version))
            {
                sequenceReader.ThrowIfNotEmpty();
            }


            explicitReader = sequenceReader.ReadSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
            DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorIdentifierOrKey.Decode(ref explicitReader, rebind, out decoded.Originator);
            explicitReader.ThrowIfNotEmpty();


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {
                explicitReader = sequenceReader.ReadSequence(new Asn1Tag(TagClass.ContextSpecific, 1));

                if (explicitReader.TryReadPrimitiveOctetString(out tmpSpan))
                {
                    decoded.Ukm = tmpSpan;
                }
                else
                {
                    decoded.Ukm = explicitReader.ReadOctetString();
                }

                explicitReader.ThrowIfNotEmpty();
            }

            DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier.Decode(ref sequenceReader, rebind, out decoded.KeyEncryptionAlgorithm);

            // Decode SEQUENCE OF for RecipientEncryptedKeys
            {
                collectionReader = sequenceReader.ReadSequence();
                var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientEncryptedKey>();
                DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientEncryptedKey tmpItem;

                while (collectionReader.HasData)
                {
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientEncryptedKey.Decode(ref collectionReader, rebind, out tmpItem);
                    tmpList.Add(tmpItem);
                }

                decoded.RecipientEncryptedKeys = tmpList.ToArray();
            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
