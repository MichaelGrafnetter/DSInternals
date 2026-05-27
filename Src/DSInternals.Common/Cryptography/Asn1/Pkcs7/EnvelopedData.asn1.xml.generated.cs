#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct EnvelopedData
    {
        internal int Version;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorInfo? OriginatorInfo;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientInfo[] RecipientInfos;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.EncryptedContentInfo EncryptedContentInfo;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute[]? UnprotectedAttributes;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            writer.WriteInteger(Version);

            if (OriginatorInfo.HasValue)
            {
                OriginatorInfo.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 0));
            }


            writer.PushSetOf();
            for (int i = 0; i < RecipientInfos.Length; i++)
            {
                RecipientInfos[i].Encode(writer);
            }
            writer.PopSetOf();

            EncryptedContentInfo.Encode(writer);

            if (UnprotectedAttributes != null)
            {

                writer.PushSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                for (int i = 0; i < UnprotectedAttributes.Length; i++)
                {
                    UnprotectedAttributes[i].Encode(writer);
                }
                writer.PopSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));

            }

            writer.PopSequence(tag);
        }

        internal static EnvelopedData Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static EnvelopedData Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, expectedTag, encoded, out EnvelopedData decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out EnvelopedData decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(AsnReader reader, out EnvelopedData decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out EnvelopedData decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EnvelopedData decoded)
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

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EnvelopedData decoded)
        {
            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnReader collectionReader;


            if (!sequenceReader.TryReadInt32(out decoded.Version))
            {
                sequenceReader.ThrowIfNotEmpty();
            }


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
            {
                DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorInfo tmpOriginatorInfo;
                DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorInfo.Decode(ref sequenceReader, new Asn1Tag(TagClass.ContextSpecific, 0), rebind, out tmpOriginatorInfo);
                decoded.OriginatorInfo = tmpOriginatorInfo;

            }


            // Decode SEQUENCE OF for RecipientInfos
            {
                collectionReader = sequenceReader.ReadSetOf();
                var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientInfo>();
                DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientInfo tmpItem;

                while (collectionReader.HasData)
                {
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientInfo.Decode(ref collectionReader, rebind, out tmpItem);
                    tmpList.Add(tmpItem);
                }

                decoded.RecipientInfos = tmpList.ToArray();
            }

            DSInternals.Common.Cryptography.Asn1.Pkcs7.EncryptedContentInfo.Decode(ref sequenceReader, rebind, out decoded.EncryptedContentInfo);

            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {

                // Decode SEQUENCE OF for UnprotectedAttributes
                {
                    collectionReader = sequenceReader.ReadSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                    var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute>();
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute tmpItem;

                    while (collectionReader.HasData)
                    {
                        DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute.Decode(ref collectionReader, rebind, out tmpItem);
                        tmpList.Add(tmpItem);
                    }

                    decoded.UnprotectedAttributes = tmpList.ToArray();
                }

            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
