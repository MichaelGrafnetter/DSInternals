namespace DSInternals.Common.Cryptography
{
    using DSInternals.Common.Interop;
    using System.Security;
    using System.Security.Cryptography;

    // See http://msdn.microsoft.com/en-us/library/system.security.cryptography.hashalgorithm%28v=vs.110%29.aspx
    public static class NTHash
    {
        /// <summary>
        /// The size, in bytes, of the computed hash code.
        /// </summary>
        public const int HashSize = NativeMethods.NTHashNumBytes;
        public const int MaxInputLength = NativeMethods.NTPasswordMaxChars;

        /// <summary>
        /// Gets the NT hash of an empty password.
        /// </summary>
        public static readonly byte[] Empty = ComputeHash(string.Empty);

        public static byte[] ComputeHash(SecureString password)
        {
            Validator.AssertMaxLength(password, MaxInputLength, "password");

            byte[] hash;
            using(SafeUnicodeSecureStringPointer passwordPtr = new SafeUnicodeSecureStringPointer(password))
            {
                NtStatus result = NativeMethods.RtlCalculateNtOwfPassword(passwordPtr, out hash);
                Validator.AssertSuccess(result);
            }
            return hash;
        }

        public static byte[] ComputeHash(string password)
        {
            Validator.AssertMaxLength(password, MaxInputLength, "password");

            byte[] hash;
            NtStatus result = NativeMethods.RtlCalculateNtOwfPassword(password, out hash);
            Validator.AssertSuccess(result);
            return hash;
        }

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
