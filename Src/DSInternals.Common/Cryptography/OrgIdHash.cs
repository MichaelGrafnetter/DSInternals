using CryptSharp.Utility;
using DSInternals.Common;

using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace DSInternals.Common.Cryptography
{
    public static class OrgIdHash
    {
        public const int SaltSize = 10;
        /// <summary>
        /// The size, in bytes, of the computed hash code.
        /// </summary>
        public const int HashSize = 32;
        private const int Iterations = 100;
        private const string HashFormat = "v1;PPH1_MD4,{0},{1},{2};";

        public static byte[] GenerateSalt()
        {
            using(var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);
                return salt;
            }
        }

        public static byte[] ComputeHash(SecureString password, byte[] salt)
        {
            byte[] ntHash = NTHash.ComputeHash(password);
            return ComputeHash(ntHash, salt);

        }
        public static byte[] ComputeHash(byte[] ntHash, byte[] salt)
        {
            Validator.AssertLength(ntHash, NTHash.HashSize, "ntHash");
            Validator.AssertLength(salt, SaltSize, "salt");
            string hexHash = ntHash.ToHex(true);
            byte[] hexHashBytes = UnicodeEncoding.Unicode.GetBytes(hexHash);
            // No need to call Dispose on HMACSHA256, because PBKDF2 does that.
            var sha256 = new HMACSHA256(hexHashBytes);
            byte[] orgIdHashBytes = Pbkdf2.ComputeDerivedKey(sha256, salt, Iterations, HashSize); 
            return orgIdHashBytes;
        }

        public static string ComputeFormattedHash(SecureString password, byte[] salt = null)
        {
            byte[] ntHash = NTHash.ComputeHash(password);
            return ComputeFormattedHash(ntHash, salt);
        }

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
