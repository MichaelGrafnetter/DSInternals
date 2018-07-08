using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DSInternals.Common
{
    public static class RSAExtensions
    {
        /// <summary>
        /// Converts a RSA public key to BCRYPT_RSAKEY_BLOB.
        /// </summary>
        public static byte[] ExportPublicKeyBlob(this RSAParameters publicKey)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // Public key magic identifier
                    var magic = Encoding.ASCII.GetBytes("RSA1");
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
    }
}