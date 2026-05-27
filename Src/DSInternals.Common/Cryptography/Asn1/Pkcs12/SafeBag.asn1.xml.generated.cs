#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs12
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct SafeBag
    {
        internal string BagId;
        internal ReadOnlyMemory<byte> BagValue;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute[]? BagAttributes;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            try
            {
                writer.WriteObjectIdentifier(BagId);
            }
            catch (ArgumentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
            writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
            try
            {
                writer.WriteEncodedValue(BagValue.Span);
            }
            catch (ArgumentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
            writer.PopSequence(new Asn1Tag(TagClass.ContextSpecific, 0));

            if (BagAttributes != null)
            {

                writer.PushSetOf();
                for (int i = 0; i < BagAttributes.Length; i++)
                {
                    BagAttributes[i].Encode(writer);
                }
                writer.PopSetOf();

            }

            writer.PopSequence(tag);
        }

        internal static SafeBag Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static SafeBag Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, expectedTag, encoded, out SafeBag decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out SafeBag decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(AsnReader reader, out SafeBag decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out SafeBag decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out SafeBag decoded)
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

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out SafeBag decoded)
        {
            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnReader explicitReader;
            AsnReader collectionReader;
            ReadOnlyMemory<byte> tmpSpan;

            decoded.BagId = sequenceReader.ReadObjectIdentifier();

            explicitReader = sequenceReader.ReadSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
            tmpSpan = explicitReader.ReadEncodedValue();
            decoded.BagValue = tmpSpan;
            explicitReader.ThrowIfNotEmpty();


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(Asn1Tag.SetOf))
            {

                // Decode SEQUENCE OF for BagAttributes
                {
                    collectionReader = sequenceReader.ReadSetOf();
                    var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute>();
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute tmpItem;

                    while (collectionReader.HasData)
                    {
                        DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute.Decode(ref collectionReader, rebind, out tmpItem);
                        tmpList.Add(tmpItem);
                    }

                    decoded.BagAttributes = tmpList.ToArray();
                }

            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
