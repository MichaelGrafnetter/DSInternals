
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

[StructLayout(LayoutKind.Sequential)]
internal partial struct RecipientKeyIdentifier
{
    internal ReadOnlyMemory<byte> SubjectKeyIdentifier;
    internal DateTimeOffset? Date;
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.OtherKeyAttribute? Other;
  

    internal static RecipientKeyIdentifier Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        return Decode(Asn1Tag.Sequence, encoded, ruleSet);
    }
    
    internal static RecipientKeyIdentifier Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, expectedTag, out RecipientKeyIdentifier decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out RecipientKeyIdentifier decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        Decode(reader, Asn1Tag.Sequence, out decoded);
    }

    internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out RecipientKeyIdentifier decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        AsnReader sequenceReader = reader.ReadSequence(expectedTag);
        
    decoded.SubjectKeyIdentifier = sequenceReader.ReadOctetString();


        if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(Asn1Tag.GeneralizedTime))
        {
            decoded.Date = sequenceReader.ReadGeneralizedTime();
        }


        if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(Asn1Tag.Sequence))
        {
            DSInternals.Common.Cryptography.Asn1.Pkcs7.OtherKeyAttribute tmpOther;
            DSInternals.Common.Cryptography.Asn1.Pkcs7.OtherKeyAttribute.Decode(sequenceReader, out tmpOther);
            decoded.Other = tmpOther;

        }


        sequenceReader.ThrowIfNotEmpty();
    }
}
