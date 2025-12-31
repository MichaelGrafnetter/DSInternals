
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

[StructLayout(LayoutKind.Sequential)]
internal partial struct OriginatorPublicKey
{
    internal DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier Algorithm;
    internal ReadOnlyMemory<byte> PublicKey;
  

    internal static OriginatorPublicKey Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        return Decode(Asn1Tag.Sequence, encoded, ruleSet);
    }
    
    internal static OriginatorPublicKey Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, expectedTag, out OriginatorPublicKey decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out OriginatorPublicKey decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        Decode(reader, Asn1Tag.Sequence, out decoded);
    }

    internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out OriginatorPublicKey decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        AsnReader sequenceReader = reader.ReadSequence(expectedTag);
        
        DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier.Decode(sequenceReader, out decoded.Algorithm);
        decoded.PublicKey = sequenceReader.ReadBitString(out _);


        sequenceReader.ThrowIfNotEmpty();
    }
}
