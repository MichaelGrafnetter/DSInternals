
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs12;

[StructLayout(LayoutKind.Sequential)]
internal partial struct SafeBag
{
    internal string BagId;
    internal ReadOnlyMemory<byte> BagValue;
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute[] BagAttributes;
  

    internal static SafeBag Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        return Decode(Asn1Tag.Sequence, encoded, ruleSet);
    }
    
    internal static SafeBag Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, expectedTag, out SafeBag decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out SafeBag decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        Decode(reader, Asn1Tag.Sequence, out decoded);
    }

    internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out SafeBag decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        AsnReader sequenceReader = reader.ReadSequence(expectedTag);
        AsnReader explicitReader;
        AsnReader collectionReader;
        
        decoded.BagId = sequenceReader.ReadObjectIdentifier();

        explicitReader = sequenceReader.ReadSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
        decoded.BagValue = explicitReader.ReadEncodedValue();
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
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute.Decode(collectionReader, out tmpItem); 
                    tmpList.Add(tmpItem);
                }

                decoded.BagAttributes = tmpList.ToArray();
            }

        }


        sequenceReader.ThrowIfNotEmpty();
    }
}
