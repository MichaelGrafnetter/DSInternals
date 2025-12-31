
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

[StructLayout(LayoutKind.Sequential)]
internal partial struct Attribute
{
    internal Oid AttrType;
    internal ReadOnlyMemory<byte>[] AttrValues;
  

    internal static Attribute Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        return Decode(Asn1Tag.Sequence, encoded, ruleSet);
    }
    
    internal static Attribute Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, expectedTag, out Attribute decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out Attribute decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        Decode(reader, Asn1Tag.Sequence, out decoded);
    }

    internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out Attribute decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        AsnReader sequenceReader = reader.ReadSequence(expectedTag);
        AsnReader collectionReader;
        
        decoded.AttrType = new Oid(sequenceReader.ReadObjectIdentifier());

        // Decode SEQUENCE OF for AttrValues
        {
            collectionReader = sequenceReader.ReadSetOf();
            var tmpList = new List<ReadOnlyMemory<byte>>();
            ReadOnlyMemory<byte> tmpItem;

            while (collectionReader.HasData)
            {
                tmpItem = collectionReader.ReadEncodedValue(); 
                tmpList.Add(tmpItem);
            }

            decoded.AttrValues = tmpList.ToArray();
        }


        sequenceReader.ThrowIfNotEmpty();
    }
}
