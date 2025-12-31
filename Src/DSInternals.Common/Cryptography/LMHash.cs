using System.Security;
using System.Text;
using DSInternals.Common.Interop;

namespace DSInternals.Common.Cryptography;

/// <summary>
/// Computes the LAN Manager (LM) hash of a password.
/// </summary>
public static class LMHash
{
    /// <summary>
    /// The size, in bits, of the computed hash code.
    /// </summary>
    public const int HashSize = NativeMethods.LMHashNumBytes;

    public const int MaxChars = NativeMethods.LMPasswordMaxChars;

    public static byte[] ComputeHash(SecureString password)
    {
        ArgumentNullException.ThrowIfNull(password);
        Validator.AssertMaxLength(password, MaxChars, nameof(password));
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
