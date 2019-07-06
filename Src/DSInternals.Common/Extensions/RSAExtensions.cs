using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;

namespace DSInternals.Common
{
    public static class RSAExtensions
    {
        private const uint BCryptRSAPublicKeyMagic = 0x31415352; // "RSA1" in ASCII
        private const int BCryptKeyBlobHeaderSize = 6 * sizeof(uint);

        /// <summary>
        /// Converts a RSA public key to BCRYPT_RSAKEY_BLOB.
        /// </summary>
        public static byte[] ExportBCryptRSAPublicKey(this RSAParameters publicKey)
        {
            // HACK: Use System.Security.Cryptography.RSACng instead of a custom implementation!
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // Public key magic identifier
                    writer.Write(BCryptRSAPublicKeyMagic);

                    // Modulus and Exponent lengths
                    writer.Write(publicKey.Modulus.Length * 8);
                    writer.Write(publicKey.Exponent.Length);
                    writer.Write(publicKey.Modulus.Length);

                    // Zero prime lengths, as we are not exporting the corresponding private key
                    writer.Write(UInt32.MinValue);
                    writer.Write(UInt32.MinValue);

                    // Now come the actual values
                    writer.Write(publicKey.Exponent);
                    writer.Write(publicKey.Modulus);
                }
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Converts a RSA public key to BCRYPT_RSAKEY_BLOB.
        /// </summary>
        public static byte[] ExportPublicKeyBlob(this RSACryptoServiceProvider publicKey)
        {
            var parameters = publicKey.ExportParameters(false);
            return parameters.ExportBCryptRSAPublicKey();
        }

        /// <summary>
        /// Converts a RSA public key to BCRYPT_RSAKEY_BLOB.
        /// </summary>
        public static byte[] ExportBCryptRSAPublicKey(this X509Certificate2 certificate)
        {
            // TODO: We currently only support RSA certificates.
            var publicKey = (RSACryptoServiceProvider)certificate.PublicKey.Key;
            return publicKey.ExportPublicKeyBlob();
        }

        /// <summary>
        /// Decodes a public key from a BCRYPT_RSAKEY_BLOB structure.
        /// </summary>
        public static RSAParameters ImportBCryptRSAPublicKey(this byte[] blob)
        {
            Validator.AssertNotNull(blob, nameof(blob));
            Validator.AssertMinLength(blob, BCryptKeyBlobHeaderSize, nameof(blob));

            // HACK: Use System.Security.Cryptography.RSACng instead of a custom implementation!
            using (var stream = new MemoryStream(blob, false))
            {
                using (var reader = new BinaryReader(stream))
                {
                    // Public key magic identifier
                    uint magic = reader.ReadUInt32();
                    if (magic != BCryptRSAPublicKeyMagic)
                    {
                        // TODO: Extract Exception text as a resouce.
                        throw new ArgumentException("The input data does not correspond to a RSA public key.", nameof(blob));
                    }

                    // Modulus and Exponent lengths
                    int bitLength = reader.ReadInt32();
                    int cbPublicExp = reader.ReadInt32();
                    int cbModulus = reader.ReadInt32();
                    int cbPrime1 = reader.ReadInt32();
                    int cbPrime2 = reader.ReadInt32();

                    // Validate the length of the remainder.
                    int expectedLength = BCryptKeyBlobHeaderSize + cbPublicExp + cbModulus + cbPrime1 + cbPrime2;
                    Validator.AssertLength(blob, expectedLength, nameof(blob));

                    var result = new RSAParameters();
                    result.Exponent = reader.ReadBytes(cbPublicExp);
                    result.Modulus = reader.ReadBytes(cbModulus);
                    return result;
                }
            }
        }

        /// <summary>
        /// Decodes a DER RSA public key.
        /// </summary>
        public static RSAParameters ImportDERPublicKey(this byte[] blob)
        {
            Validator.AssertNotNull(blob, nameof(blob));

            var asn1 = Asn1Object.FromByteArray(blob);
            var rsaPublicKey = RsaPublicKeyStructure.GetInstance(asn1);
            return new RSAParameters()
            {
                Modulus = rsaPublicKey.Modulus.ToByteArray(),
                Exponent = rsaPublicKey.PublicExponent.ToByteArray()
            };
        }

        public static bool IsBCryptRSAPublicKeyBlob(this byte[] blob)
        {
            if (blob == null || blob.Length < BCryptKeyBlobHeaderSize)
            {
                return false;
            }

            // Check if the byte sequence starts with the magic
            return BitConverter.ToUInt32(blob, 0) == BCryptRSAPublicKeyMagic;
        }
    }
}
