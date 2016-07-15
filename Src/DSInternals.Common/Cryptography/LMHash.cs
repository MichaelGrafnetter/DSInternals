using DSInternals.Common;
using DSInternals.Common.Interop;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace DSInternals.Common.Cryptography
{
    public static class LMHash
    {
        /// <summary>
        /// The size, in bits, of the computed hash code.
        /// </summary>
        public const int HashSize = NativeMethods.LMHashNumBytes;

        public const int MaxChars = NativeMethods.LMPasswordMaxChars;

        public static byte[] ComputeHash(SecureString password)
        {
            Validator.AssertNotNull(password, "password");
            Validator.AssertMaxLength(password, MaxChars, "password");
            int oemPwdBufferLength = Encoding.ASCII.GetMaxByteCount(MaxChars);
            byte[] hash;
            using (SafeOemStringPointer oemPwdBuffer = SafeOemStringPointer.Allocate(oemPwdBufferLength))
            {
                using (SafeUnicodeSecureStringPointer unicodePwdBuffer = new SafeUnicodeSecureStringPointer(password))
                {

                    SecureUnicodeString unicodePwd = new SecureUnicodeString(unicodePwdBuffer);
                    OemString oemPwd = new OemString(oemPwdBuffer);
                    NtStatus result1 = NativeMethods.RtlUpcaseUnicodeStringToOemString(oemPwd, unicodePwd);
                    Validator.AssertSuccess(result1);
                }

                NtStatus result2 = NativeMethods.RtlCalculateLmOwfPassword(oemPwdBuffer, out hash);
                Validator.AssertSuccess(result2);
            }
            return hash;
        }
    }
}
