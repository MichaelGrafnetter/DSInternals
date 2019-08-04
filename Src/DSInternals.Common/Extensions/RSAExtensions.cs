using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security;

namespace DSInternals.Common
{
    public static class RSAExtensions
    {
        private static readonly CngKeyBlobFormat BCryptRSAPublicKeyFormat = new CngKeyBlobFormat("RSAPUBLICBLOB");
        private const int BCryptKeyBlobHeaderSize = 6 * sizeof(uint);
        private const uint BCryptRSAPublicKeyMagic = 0x31415352; // "RSA1" in ASCII

        /// <summary>
        /// Converts a RSA public key to BCRYPT_RSAKEY_BLOB.
        /// </summary>
        public static byte[] ExportRSAPublicKeyBCrypt(this X509Certificate2 certificate)
        {
            Validator.AssertNotNull(certificate, nameof(certificate));

            var publicKey = (RSACryptoServiceProvider)certificate.PublicKey.Key;
            return publicKey.ExportRSAPublicKeyBCrypt();
        }

        /// <summary>
        /// Converts a RSA public key to BCRYPT_RSAKEY_BLOB.
        /// </summary>
        public static byte[] ExportRSAPublicKeyBCrypt(this RSACryptoServiceProvider publicKey)
        {
            var parameters = publicKey.ExportParameters(false);
            return parameters.ExportRSAPublicKeyBCrypt();
        }

        /// <summary>
        /// Converts a RSA public key to BCRYPT_RSAKEY_BLOB.
        /// </summary>
        public static byte[] ExportRSAPublicKeyBCrypt(this RSAParameters publicKey)
        {
            using (var rsa = new RSACng())
            {
                rsa.ImportParameters(publicKey);
                using (var key = rsa.Key)
                {
                    return key.Export(BCryptRSAPublicKeyFormat);
                }
            }
        }

        /// <summary>
        /// Decodes a public key from a BCRYPT_RSAKEY_BLOB structure.
        /// </summary>
        public static RSAParameters ImportRSAPublicKeyBCrypt(this byte[] blob)
        {
            Validator.AssertNotNull(blob, nameof(blob));

            using (var key = CngKey.Import(blob, BCryptRSAPublicKeyFormat))
            {
                using (var rsa = new RSACng(key))
                {
                    return rsa.ExportParameters(false);
                }
            }
        }

        /// <summary>
        /// Decodes a DER RSA public key.
        /// </summary>
        public static RSAParameters ImportRSAPublicKeyDER(this byte[] blob)
        {
            Validator.AssertNotNull(blob, nameof(blob));

            var asn1 = Asn1Object.FromByteArray(blob);
            var rsaPublicKey = RsaPublicKeyStructure.GetInstance(asn1);

            return new RSAParameters()
            {
                Modulus = rsaPublicKey.Modulus.ToByteArrayUnsigned(),
                Exponent = rsaPublicKey.PublicExponent.ToByteArrayUnsigned()
            };
        }

        /// <summary>
        /// Exports a RSA public key to the DER format.
        /// </summary>
        public static byte[] ExportRSAPublicKeyDER(this X509Certificate2 certificate)
        {
            Validator.AssertNotNull(certificate, nameof(certificate));

            var publicKey = (RSACryptoServiceProvider)certificate.PublicKey.Key;
            return publicKey.ExportRSAPublicKeyDER();
        }

        /// <summary>
        /// Exports a RSA public key to the DER format.
        /// </summary>
        public static byte[] ExportRSAPublicKeyDER(this RSACryptoServiceProvider publicKey)
        {
            var rsaParameters = publicKey.ExportParameters(false);
            return rsaParameters.ExportRSAPublicKeyDER();
        }

        /// <summary>
        /// Exports a RSA public key to the DER format.
        /// </summary>
        public static byte[] ExportRSAPublicKeyDER(this RSAParameters publicKey)
        {
            var bouncyPublicKey = DotNetUtilities.GetRsaPublicKey(publicKey);
            var asn1PublicKey = new RsaPublicKeyStructure(bouncyPublicKey.Modulus, bouncyPublicKey.Exponent);
            return asn1PublicKey.GetDerEncoded();
        }

        /// <summary>
        /// CHecks whether the input blob is in the BCRYPT_RSAKEY_BLOB format.
        /// </summary>
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
