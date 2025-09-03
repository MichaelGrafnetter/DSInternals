using DSInternals.Common;

using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace DSInternals.Common.Cryptography
{
    /// <summary>
    /// Provides methods for computing Microsoft Org ID password hashes used in cloud authentication.
    /// </summary>
    public static class OrgIdHash
    {
        /// <summary>
        /// The size of the salt in bytes.
        /// </summary>
        public const int SaltSize = 10;
        /// <summary>
        /// The size, in bytes, of the computed hash code.
        /// </summary>
        public const int HashSize = 32;
        private const int Iterations = 1000;
        private const string HashFormat = "v1;PPH1_MD4,{0},{1},{2};";
        private const string InternalHashFunction = "HMACSHA256";

        /// <summary>
        /// Generates a random salt for password hashing.
        /// </summary>
        /// <returns>A randomly generated salt.</returns>
        public static byte[] GenerateSalt()
        {
            using(var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);
                return salt;
            }
        }

        /// <summary>
        /// Computes the Org ID hash of the specified password using the provided salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to use for hashing.</param>
        /// <returns>The Org ID hash of the password.</returns>
        public static byte[] ComputeHash(SecureString password, byte[] salt)
        {
            byte[] ntHash = NTHash.ComputeHash(password);
            return ComputeHash(ntHash, salt);

        }
        /// <summary>
        /// ComputeHash implementation.
        /// </summary>
        public static byte[] ComputeHash(byte[] ntHash, byte[] salt)
        {
            Validator.AssertLength(ntHash, NTHash.HashSize, "ntHash");
            Validator.AssertLength(salt, SaltSize, "salt");
            string hexHash = ntHash.ToHex(true);
            byte[] hexHashBytes = UnicodeEncoding.Unicode.GetBytes(hexHash);
            var pbkdf2 = new Pbkdf2(hexHashBytes, salt, Iterations, InternalHashFunction);
            byte[] orgIdHashBytes = pbkdf2.GetBytes(HashSize);
            return orgIdHashBytes;
        }

        /// <summary>
        /// ComputeFormattedHash implementation.
        /// </summary>
        public static string ComputeFormattedHash(SecureString password, byte[] salt = null)
        {
            byte[] ntHash = NTHash.ComputeHash(password);
            return ComputeFormattedHash(ntHash, salt);
        }

        /// <summary>
        /// ComputeFormattedHash implementation.
        /// </summary>
        public static string ComputeFormattedHash(byte[] ntHash, byte[] salt = null)
        {
            if (salt == null)
            {
                salt = GenerateSalt();
            }
            else
            {
                Validator.AssertLength(salt, SaltSize, "salt");
            }
            byte[] hash = ComputeHash(ntHash, salt);
            return FormatHash(hash, salt);
        }

        private static string FormatHash(byte[] hash, byte[] salt)
        {
            return string.Format(HashFormat, salt.ToHex(false), Iterations, hash.ToHex(false));
        }
    }
}
