
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

[StructLayout(LayoutKind.Sequential)]
internal partial struct SignerInfo
{
    internal int Version;
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.SignerIdentifier Sid;
    internal DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier DigestAlgorithm;
    internal ReadOnlyMemory<byte>? SignedAttributes;
    internal DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier SignatureAlgorithm;
    internal ReadOnlyMemory<byte> SignatureValue;
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute[] UnsignedAttributes;
  

    internal static SignerInfo Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        return Decode(Asn1Tag.Sequence, encoded, ruleSet);
    }
    
    internal static SignerInfo Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, expectedTag, out SignerInfo decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out SignerInfo decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        Decode(reader, Asn1Tag.Sequence, out decoded);
    }

    internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out SignerInfo decoded)
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

        DSInternals.Common.Cryptography.Asn1.Pkcs7.SignerIdentifier.Decode(sequenceReader, out decoded.Sid);
        DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier.Decode(sequenceReader, out decoded.DigestAlgorithm);

        if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
        {
            decoded.SignedAttributes = sequenceReader.ReadEncodedValue();
        }

        DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier.Decode(sequenceReader, out decoded.SignatureAlgorithm);
    decoded.SignatureValue = sequenceReader.ReadOctetString();


        if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
        {

            // Decode SEQUENCE OF for UnsignedAttributes
            {
                collectionReader = sequenceReader.ReadSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute>();
                DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute tmpItem;

                while (collectionReader.HasData)
                {
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.Attribute.Decode(collectionReader, out tmpItem); 
                    tmpList.Add(tmpItem);
                }

                decoded.UnsignedAttributes = tmpList.ToArray();
            }

        }


        sequenceReader.ThrowIfNotEmpty();
    }
}
