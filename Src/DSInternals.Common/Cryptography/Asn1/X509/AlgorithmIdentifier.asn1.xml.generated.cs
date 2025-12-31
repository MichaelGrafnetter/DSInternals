
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.X509;

[StructLayout(LayoutKind.Sequential)]
internal partial struct AlgorithmIdentifier
{
    internal Oid Algorithm;
    internal ReadOnlyMemory<byte>? Parameters;
  

    internal static AlgorithmIdentifier Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        return Decode(Asn1Tag.Sequence, encoded, ruleSet);
    }
    
    internal static AlgorithmIdentifier Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, expectedTag, out AlgorithmIdentifier decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out AlgorithmIdentifier decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        Decode(reader, Asn1Tag.Sequence, out decoded);
    }

    internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out AlgorithmIdentifier decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        AsnReader sequenceReader = reader.ReadSequence(expectedTag);
        
        decoded.Algorithm = new Oid(sequenceReader.ReadObjectIdentifier());

        if (sequenceReader.HasData)
        {
            decoded.Parameters = sequenceReader.ReadEncodedValue();
        }


        sequenceReader.ThrowIfNotEmpty();
    }
}
