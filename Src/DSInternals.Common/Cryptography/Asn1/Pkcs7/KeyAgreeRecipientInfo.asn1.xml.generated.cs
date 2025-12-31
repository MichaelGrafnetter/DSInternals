
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

[StructLayout(LayoutKind.Sequential)]
internal partial struct KeyAgreeRecipientInfo
{
    internal int Version;
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorIdentifierOrKey Originator;
    internal ReadOnlyMemory<byte>? Ukm;
    internal DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier KeyEncryptionAlgorithm;
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientEncryptedKey[] RecipientEncryptedKeys;
  

    internal static KeyAgreeRecipientInfo Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        return Decode(Asn1Tag.Sequence, encoded, ruleSet);
    }
    
    internal static KeyAgreeRecipientInfo Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, expectedTag, out KeyAgreeRecipientInfo decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out KeyAgreeRecipientInfo decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        Decode(reader, Asn1Tag.Sequence, out decoded);
    }

    internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out KeyAgreeRecipientInfo decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        AsnReader sequenceReader = reader.ReadSequence(expectedTag);
        AsnReader explicitReader;
        AsnReader collectionReader;
        

        if (!sequenceReader.TryReadInt32(out decoded.Version))
        {
            sequenceReader.ThrowIfNotEmpty();
        }


        explicitReader = sequenceReader.ReadSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
        DSInternals.Common.Cryptography.Asn1.Pkcs7.OriginatorIdentifierOrKey.Decode(explicitReader, out decoded.Originator);
        explicitReader.ThrowIfNotEmpty();


        if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
        {
            explicitReader = sequenceReader.ReadSequence(new Asn1Tag(TagClass.ContextSpecific, 1));
        decoded.Ukm = explicitReader.ReadOctetString();

            explicitReader.ThrowIfNotEmpty();
        }

        DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier.Decode(sequenceReader, out decoded.KeyEncryptionAlgorithm);

        // Decode SEQUENCE OF for RecipientEncryptedKeys
        {
            collectionReader = sequenceReader.ReadSequence();
            var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientEncryptedKey>();
            DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientEncryptedKey tmpItem;

            while (collectionReader.HasData)
            {
                DSInternals.Common.Cryptography.Asn1.Pkcs7.RecipientEncryptedKey.Decode(collectionReader, out tmpItem); 
                tmpList.Add(tmpItem);
            }

            decoded.RecipientEncryptedKeys = tmpList.ToArray();
        }


        sequenceReader.ThrowIfNotEmpty();
    }
}
