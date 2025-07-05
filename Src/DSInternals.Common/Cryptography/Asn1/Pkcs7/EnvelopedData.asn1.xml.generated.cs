
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct EnvelopedData
    {
        internal int Version;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorInfo? OriginatorInfo;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientInfo[] RecipientInfos;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.EncryptedContentInfo EncryptedContentInfo;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute[] UnprotectedAttributes;
      

        internal static EnvelopedData Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }
        
        internal static EnvelopedData Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnReader reader = new AsnReader(encoded, ruleSet);
            
            Decode(reader, expectedTag, out EnvelopedData decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        internal static void Decode(AsnReader reader, out EnvelopedData decoded)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            Decode(reader, Asn1Tag.Sequence, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out EnvelopedData decoded)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

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
                DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorInfo.Decode(sequenceReader, new Asn1Tag(TagClass.ContextSpecific, 0), out tmpOriginatorInfo);
                decoded.OriginatorInfo = tmpOriginatorInfo;

            }


            // Decode SEQUENCE OF for RecipientInfos
            {
                collectionReader = sequenceReader.ReadSetOf();
                var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientInfo>();
                DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientInfo tmpItem;

                while (collectionReader.HasData)
                {
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientInfo.Decode(collectionReader, out tmpItem); 
                    tmpList.Add(tmpItem);
                }

                decoded.RecipientInfos = tmpList.ToArray();
            }

            DSInternals.Common.Cryptography.Asn1.Pkcs7.EncryptedContentInfo.Decode(sequenceReader, out decoded.EncryptedContentInfo);

            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {

                // Decode SEQUENCE OF for UnprotectedAttributes
                {
                    collectionReader = sequenceReader.ReadSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                    var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute>();
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute tmpItem;

                    while (collectionReader.HasData)
                    {
                        DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute.Decode(collectionReader, out tmpItem); 
                        tmpList.Add(tmpItem);
                    }

                    decoded.UnprotectedAttributes = tmpList.ToArray();
                }

            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
