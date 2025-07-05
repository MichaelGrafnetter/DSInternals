
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Formats.Asn1;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs12
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct MacData
    {
        private static readonly byte[] s_defaultIterationCount = { 0x02, 0x01, 0x01 };
  
        internal DSInternals.Common.Cryptography.Asn1.Pkcs1.DigestInfo Mac;
        internal ReadOnlyMemory<byte> MacSalt;
        internal int IterationCount;
      
#if DEBUG
        static MacData()
        {
            MacData decoded = default;
            AsnReader reader;

            reader = new AsnReader(s_defaultIterationCount, AsnEncodingRules.DER);

            if (!reader.TryReadInt32(out decoded.IterationCount))
            {
                reader.ThrowIfNotEmpty();
            }

            reader.ThrowIfNotEmpty();
        }
#endif
 

        internal static MacData Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }
        
        internal static MacData Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            AsnReader reader = new AsnReader(encoded, ruleSet);
            
            Decode(reader, expectedTag, out MacData decoded);
            reader.ThrowIfNotEmpty();
            return decoded;
        }

        internal static void Decode(AsnReader reader, out MacData decoded)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            Decode(reader, Asn1Tag.Sequence, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out MacData decoded)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnReader defaultReader;
            
            DSInternals.Common.Cryptography.Asn1.Pkcs1.DigestInfo.Decode(sequenceReader, out decoded.Mac);
        decoded.MacSalt = sequenceReader.ReadOctetString();
    

            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(Asn1Tag.Integer))
            {

                if (!sequenceReader.TryReadInt32(out decoded.IterationCount))
                {
                    sequenceReader.ThrowIfNotEmpty();
                }

            }
            else
            {
                defaultReader = new AsnReader(s_defaultIterationCount, AsnEncodingRules.DER);

                if (!defaultReader.TryReadInt32(out decoded.IterationCount))
                {
                    defaultReader.ThrowIfNotEmpty();
                }

            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
