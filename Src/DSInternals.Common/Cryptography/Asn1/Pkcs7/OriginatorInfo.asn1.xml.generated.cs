
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

[StructLayout(LayoutKind.Sequential)]
internal partial struct OriginatorInfo
{
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.CertificateChoice[] CertificateSet;
    internal ReadOnlyMemory<byte>[] RevocationInfoChoices;
  

    internal static OriginatorInfo Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        return Decode(Asn1Tag.Sequence, encoded, ruleSet);
    }
    
    internal static OriginatorInfo Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, expectedTag, out OriginatorInfo decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out OriginatorInfo decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        Decode(reader, Asn1Tag.Sequence, out decoded);
    }

    internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out OriginatorInfo decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        AsnReader sequenceReader = reader.ReadSequence(expectedTag);
        AsnReader collectionReader;
        

        if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
        {

            // Decode SEQUENCE OF for CertificateSet
            {
                collectionReader = sequenceReader.ReadSetOf(new Asn1Tag(TagClass.ContextSpecific, 0));
                var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.CertificateChoice>();
                DSInternals.Common.Cryptography.Asn1.Pkcs7.CertificateChoice tmpItem;

                while (collectionReader.HasData)
                {
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.CertificateChoice.Decode(collectionReader, out tmpItem); 
                    tmpList.Add(tmpItem);
                }

                decoded.CertificateSet = tmpList.ToArray();
            }

        }


        if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
        {

            // Decode SEQUENCE OF for RevocationInfoChoices
            {
                collectionReader = sequenceReader.ReadSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                var tmpList = new List<ReadOnlyMemory<byte>>();
                ReadOnlyMemory<byte> tmpItem;

                while (collectionReader.HasData)
                {
                    tmpItem = collectionReader.ReadEncodedValue(); 
                    tmpList.Add(tmpItem);
                }

                decoded.RevocationInfoChoices = tmpList.ToArray();
            }

        }


        sequenceReader.ThrowIfNotEmpty();
    }
}
