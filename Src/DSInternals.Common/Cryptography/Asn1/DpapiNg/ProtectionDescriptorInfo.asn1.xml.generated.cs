#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.DpapiNg
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct ProtectionDescriptorInfo
    {
        internal DSInternals.Common.Cryptography.Asn1.DpapiNg.OrAlternative[] Values;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);
            foreach (DSInternals.Common.Cryptography.Asn1.DpapiNg.OrAlternative item in Values)
            {
                item.Encode(writer);
            }
            writer.PopSequence(tag);
        }

        internal static ProtectionDescriptorInfo Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static ProtectionDescriptorInfo Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);
                Decode(ref reader, expectedTag, encoded, out ProtectionDescriptorInfo decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(AsnReader reader, out ProtectionDescriptorInfo decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out ProtectionDescriptorInfo decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out ProtectionDescriptorInfo decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out ProtectionDescriptorInfo decoded)
        {
            try
            {
                DecodeCore(ref reader, expectedTag, rebind, out decoded);
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out ProtectionDescriptorInfo decoded)
        {
            decoded = default;
            AsnReader collectionReader = reader.ReadSequence(expectedTag);
            var tmpList = new List<DSInternals.Common.Cryptography.Asn1.DpapiNg.OrAlternative>();
            DSInternals.Common.Cryptography.Asn1.DpapiNg.OrAlternative tmpItem;

            while (collectionReader.HasData)
            {
                DSInternals.Common.Cryptography.Asn1.DpapiNg.OrAlternative.Decode(ref collectionReader, rebind, out tmpItem);
                tmpList.Add(tmpItem);
            }

            decoded.Values = tmpList.ToArray();
            collectionReader.ThrowIfNotEmpty();
        }
    }
}
