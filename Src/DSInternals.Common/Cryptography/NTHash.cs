using System;
using System.Security;
using System.Security.Cryptography;
using DSInternals.Common.Interop;

namespace DSInternals.Common.Cryptography
{

    // See http://msdn.microsoft.com/en-us/library/system.security.cryptography.hashalgorithm%28v=vs.110%29.aspx
    /// <summary>
    /// Provides methods for computing NT (NTLM) password hashes used in Windows authentication.
    /// </summary>
    public static class NTHash
    {
        /// <summary>
        /// The size, in bytes, of the computed hash code.
        /// </summary>
        public const int HashSize = NativeMethods.NTHashNumBytes;
        /// <summary>
        /// The maximum length of a password that can be hashed.
        /// </summary>
        public const int MaxInputLength = NativeMethods.NTPasswordMaxChars;
        /// <summary>
        /// The maximum binary length of a password in bytes.
        /// </summary>
        public const int MaxBinaryLength = MaxInputLength * sizeof(char);

        /// <summary>
        /// Gets the NT hash of an empty password.
        /// </summary>
        public static readonly byte[] Empty = ComputeHash(string.Empty);

        /// <summary>
        /// Computes the NT hash of the specified password stored in a SecureString.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The NT hash of the password.</returns>
        public static byte[] ComputeHash(SecureString password)
        {
            Validator.AssertMaxLength(password, MaxInputLength, nameof(password));

            byte[] hash;
            using(SafeUnicodeSecureStringPointer passwordPtr = new SafeUnicodeSecureStringPointer(password))
            {
                NtStatus result = NativeMethods.RtlCalculateNtOwfPassword(passwordPtr, out hash);
                Validator.AssertSuccess(result);
            }
            return hash;
        }

        /// <summary>
        /// Computes the NT hash of the specified password provided as binary data.
        /// </summary>
        /// <param name="password">The password to hash as binary data.</param>
        /// <returns>The NT hash of the password.</returns>
        public static byte[] ComputeHash(ReadOnlyMemory<byte> password)
        {
            NtStatus result = NativeMethods.RtlCalculateNtOwfPassword(password, out byte[] hash);
            Validator.AssertSuccess(result);

            return hash;
        }

        /// <summary>
        /// Computes the NT hash of the specified password provided as a byte array.
        /// </summary>
        /// <param name="password">The password to hash as a byte array.</param>
        /// <returns>The NT hash of the password.</returns>
        public static byte[] ComputeHash(byte[] password)
        {
            Validator.AssertMaxLength(password, MaxInputLength*sizeof(char), nameof(password));

            byte[] hash;
            using (SafeUnicodeSecureStringPointer passwordPtr = new SafeUnicodeSecureStringPointer(password))
            {
                NtStatus result = NativeMethods.RtlCalculateNtOwfPassword(passwordPtr, out hash);
                Validator.AssertSuccess(result);
            }
            return hash;
        }

        /// <summary>
        /// Computes the NT hash of the specified password string.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The NT hash of the password.</returns>
        public static byte[] ComputeHash(string password)
        {
            Validator.AssertMaxLength(password, MaxInputLength, nameof(password));

            byte[] hash;
            NtStatus result = NativeMethods.RtlCalculateNtOwfPassword(password, out hash);
            Validator.AssertSuccess(result);
            return hash;
        }

        /// <summary>
        /// Generates a random byte array with the same length as an NT hash.
        /// </summary>
        /// <returns>A random byte array that can be used as a placeholder or test hash.</returns>
        public static byte[] GetRandom()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var randomHash = new byte[HashSize];
                rng.GetBytes(randomHash);
                return randomHash;
            }
        }
    }
}
