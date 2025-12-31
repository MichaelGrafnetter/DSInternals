
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

[StructLayout(LayoutKind.Sequential)]
internal partial struct KEKRecipientInfo
{
    internal int Version;
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.KEKIdentifier KekId;
    internal DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier KeyEncryptionAlgorithm;
    internal ReadOnlyMemory<byte> EncryptedKey;
  

    internal static KEKRecipientInfo Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        return Decode(Asn1Tag.Sequence, encoded, ruleSet);
    }
    
    internal static KEKRecipientInfo Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, expectedTag, out KEKRecipientInfo decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out KEKRecipientInfo decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        Decode(reader, Asn1Tag.Sequence, out decoded);
    }

    internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out KEKRecipientInfo decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        AsnReader sequenceReader = reader.ReadSequence(expectedTag);
        

        if (!sequenceReader.TryReadInt32(out decoded.Version))
        {
            sequenceReader.ThrowIfNotEmpty();
        }

        DSInternals.Common.Cryptography.Asn1.Pkcs7.KEKIdentifier.Decode(sequenceReader, out decoded.KekId);
        DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier.Decode(sequenceReader, out decoded.KeyEncryptionAlgorithm);
    decoded.EncryptedKey = sequenceReader.ReadOctetString();


        sequenceReader.ThrowIfNotEmpty();
    }
}
