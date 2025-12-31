
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

[StructLayout(LayoutKind.Sequential)]
internal partial struct RecipientEncryptedKey
{
    internal DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyAgreeRecipientIdentifier Rid;
    internal ReadOnlyMemory<byte> EncryptedKey;
  

    internal static RecipientEncryptedKey Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        return Decode(Asn1Tag.Sequence, encoded, ruleSet);
    }
    
    internal static RecipientEncryptedKey Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
    {
        AsnReader reader = new AsnReader(encoded, ruleSet);
        
        Decode(reader, expectedTag, out RecipientEncryptedKey decoded);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    internal static void Decode(AsnReader reader, out RecipientEncryptedKey decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        Decode(reader, Asn1Tag.Sequence, out decoded);
    }

    internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out RecipientEncryptedKey decoded)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader));

        decoded = default;
        AsnReader sequenceReader = reader.ReadSequence(expectedTag);
        
        DSInternals.Common.Cryptography.Asn1.Pkcs7.KeyAgreeRecipientIdentifier.Decode(sequenceReader, out decoded.Rid);
    decoded.EncryptedKey = sequenceReader.ReadOctetString();


        sequenceReader.ThrowIfNotEmpty();
    }
}
