#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct SignedData
    {
        internal int Version;
        internal DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier[] DigestAlgorithms;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.EncapsulatedContentInfo EncapContentInfo;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.CertificateChoice[]? CertificateSet;
        internal ReadOnlyMemory<byte>[]? Crls;
        internal DSInternals.Common.Cryptography.Asn1.Pkcs7.SignerInfo[] SignerInfos;

        internal readonly void Encode(AsnWriter writer)
        {
            Encode(writer, Asn1Tag.Sequence);
        }

        internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
        {
            writer.PushSequence(tag);

            writer.WriteInteger(Version);

            writer.PushSetOf();
            for (int i = 0; i < DigestAlgorithms.Length; i++)
            {
                DigestAlgorithms[i].Encode(writer);
            }
            writer.PopSetOf();

            EncapContentInfo.Encode(writer);

            if (CertificateSet != null)
            {

                writer.PushSetOf(new Asn1Tag(TagClass.ContextSpecific, 0));
                for (int i = 0; i < CertificateSet.Length; i++)
                {
                    CertificateSet[i].Encode(writer);
                }
                writer.PopSetOf(new Asn1Tag(TagClass.ContextSpecific, 0));

            }


            if (Crls != null)
            {

                writer.PushSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                for (int i = 0; i < Crls.Length; i++)
                {
                    try
                    {
                        writer.WriteEncodedValue(Crls[i].Span);
                    }
                    catch (ArgumentException e)
                    {
                        throw new CryptographicException("ASN1 corrupted data.", e);
                    }
                }
                writer.PopSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));

            }


            writer.PushSetOf();
            for (int i = 0; i < SignerInfos.Length; i++)
            {
                SignerInfos[i].Encode(writer);
            }
            writer.PopSetOf();

            writer.PopSequence(tag);
        }

        internal static SignedData Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            return Decode(Asn1Tag.Sequence, encoded, ruleSet);
        }

        internal static SignedData Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
        {
            try
            {
                AsnReader reader = new AsnReader(encoded, ruleSet);

                DecodeCore(ref reader, expectedTag, encoded, out SignedData decoded);
                reader.ThrowIfNotEmpty();
                return decoded;
            }
            catch (AsnContentException e)
            {
                throw new CryptographicException("ASN1 corrupted data.", e);
            }
        }

        internal static void Decode(ref AsnReader reader, ReadOnlyMemory<byte> rebind, out SignedData decoded)
        {
            Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
        }

        internal static void Decode(AsnReader reader, out SignedData decoded)
        {
            Decode(ref reader, default, out decoded);
        }

        internal static void Decode(AsnReader reader, Asn1Tag expectedTag, out SignedData decoded)
        {
            Decode(ref reader, expectedTag, default, out decoded);
        }
        internal static void Decode(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out SignedData decoded)
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

        private static void DecodeCore(ref AsnReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out SignedData decoded)
        {
            decoded = default;
            AsnReader sequenceReader = reader.ReadSequence(expectedTag);
            AsnReader collectionReader;
            ReadOnlyMemory<byte> tmpSpan;


            if (!sequenceReader.TryReadInt32(out decoded.Version))
            {
                sequenceReader.ThrowIfNotEmpty();
            }


            // Decode SEQUENCE OF for DigestAlgorithms
            {
                collectionReader = sequenceReader.ReadSetOf();
                var tmpList = new List<DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier>();
                DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier tmpItem;

                while (collectionReader.HasData)
                {
                    DSInternals.Common.Cryptography.Asn1.X509.AlgorithmIdentifier.Decode(ref collectionReader, rebind, out tmpItem);
                    tmpList.Add(tmpItem);
                }

                decoded.DigestAlgorithms = tmpList.ToArray();
            }

            DSInternals.Common.Cryptography.Asn1.Pkcs7.EncapsulatedContentInfo.Decode(ref sequenceReader, rebind, out decoded.EncapContentInfo);

            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
            {

                // Decode SEQUENCE OF for CertificateSet
                {
                    collectionReader = sequenceReader.ReadSetOf(new Asn1Tag(TagClass.ContextSpecific, 0));
                    var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.CertificateChoice>();
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.CertificateChoice tmpItem;

                    while (collectionReader.HasData)
                    {
                        DSInternals.Common.Cryptography.Asn1.Pkcs7.CertificateChoice.Decode(ref collectionReader, rebind, out tmpItem);
                        tmpList.Add(tmpItem);
                    }

                    decoded.CertificateSet = tmpList.ToArray();
                }

            }


            if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 1)))
            {

                // Decode SEQUENCE OF for Crls
                {
                    collectionReader = sequenceReader.ReadSetOf(new Asn1Tag(TagClass.ContextSpecific, 1));
                    var tmpList = new List<ReadOnlyMemory<byte>>();
                    ReadOnlyMemory<byte> tmpItem;

                    while (collectionReader.HasData)
                    {
                        tmpSpan = collectionReader.ReadEncodedValue();
                        tmpItem = tmpSpan;
                        tmpList.Add(tmpItem);
                    }

                    decoded.Crls = tmpList.ToArray();
                }

            }


            // Decode SEQUENCE OF for SignerInfos
            {
                collectionReader = sequenceReader.ReadSetOf();
                var tmpList = new List<DSInternals.Common.Cryptography.Asn1.Pkcs7.SignerInfo>();
                DSInternals.Common.Cryptography.Asn1.Pkcs7.SignerInfo tmpItem;

                while (collectionReader.HasData)
                {
                    DSInternals.Common.Cryptography.Asn1.Pkcs7.SignerInfo.Decode(ref collectionReader, rebind, out tmpItem);
                    tmpList.Add(tmpItem);
                }

                decoded.SignerInfos = tmpList.ToArray();
            }


            sequenceReader.ThrowIfNotEmpty();
        }
    }
}
