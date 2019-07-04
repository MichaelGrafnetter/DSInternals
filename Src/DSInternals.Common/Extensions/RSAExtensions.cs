using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DSInternals.Common
{
    public static class RSAExtensions
    {
        private const string RSAPublicKeyMagic = "RSA1";
        private const int MinPublicKeyBlobSize = 4 * sizeof(byte) + 5 * sizeof(UInt32);

        /// <summary>
        /// Converts a RSA public key to BCRYPT_RSAKEY_BLOB.
        /// </summary>
        public static byte[] ExportPublicKeyBlob(this RSAParameters publicKey)
        {
            // HACK: Use System.Security.Cryptography.RSACng instead of a custom implementation!
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // Public key magic identifier
                    var magic = Encoding.ASCII.GetBytes(RSAPublicKeyMagic);
                    writer.Write(magic);

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
            return parameters.ExportPublicKeyBlob();
        }

        /// <summary>
        /// Converts a RSA public key to BCRYPT_RSAKEY_BLOB.
        /// </summary>
        public static byte[] ExportPublicKeyBlob(this X509Certificate2 certificate)
        {
            // TODO: We currently only support RSA certificates.
            var publicKey = (RSACryptoServiceProvider)certificate.PublicKey.Key;
            return publicKey.ExportPublicKeyBlob();
        }

        /// <summary>
        /// Converts a BCRYPT_RSAKEY_BLOB to RSA public key.
        /// </summary>
        public static RSAParameters ToRSAParameters(this byte[] blob)
        {
            Validator.AssertNotNull(blob, nameof(blob));
            Validator.AssertMinLength(blob, MinPublicKeyBlobSize, nameof(blob));

            // HACK: Use System.Security.Cryptography.RSACng instead of a custom implementation!
            using (var stream = new MemoryStream(blob, false))
            {
                using (var reader = new BinaryReader(stream))
                {
                    // Public key magic identifier
                    var magic = reader.ReadBytes(RSAPublicKeyMagic.Length);
                    // TOD: Validate the magic

                    // Modulus and Exponent lengths
                    int bitLength = reader.ReadInt32();
                    int cbPublicExp = reader.ReadInt32();
                    int cbModulus = reader.ReadInt32();
                    int cbPrime1 = reader.ReadInt32();
                    int cbPrime2 = reader.ReadInt32();
                    
                    // TODO: Validate the length of the remainder.

                    var result = new RSAParameters();
                    result.Exponent = reader.ReadBytes(cbPublicExp);
                    result.Modulus = reader.ReadBytes(cbModulus);
                    return result;
                }
            }
        }
    }
}
