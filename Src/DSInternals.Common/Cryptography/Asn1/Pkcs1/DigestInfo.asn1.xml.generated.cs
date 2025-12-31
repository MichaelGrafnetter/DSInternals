
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs1;

[StructLayout(LayoutKind.Sequential)]
internal partial struct DigestInfo
{
    internal DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier DigestAlgorithm;
    internal ReadOnlyMemory<byte> Digest;
  

    internal static DigestInfo Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        return Decode(Asn1Tag.Sequence, encoded, ruleSet);
    }
    
    internal static DigestInfo Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, expectedTag, out DigestInfo decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out DigestInfo decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        Decode(reader, Asn1Tag.Sequence, out decoded);
    }

    internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out DigestInfo decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        AsnReader sequenceReader = reader.ReadSequence(expectedTag);
        
        DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier.Decode(sequenceReader, out decoded.DigestAlgorithm);
    decoded.Digest = sequenceReader.ReadOctetString();


        sequenceReader.ThrowIfNotEmpty();
    }
}
