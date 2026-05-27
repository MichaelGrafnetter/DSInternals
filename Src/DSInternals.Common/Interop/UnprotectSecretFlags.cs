using Windows.Win32.Security.Cryptography;

namespace DSInternals.Common.Interop;

/// <summary>
/// Specifies options for <c>NCryptUnprotectSecret</c>.
/// </summary>
[Flags]
internal enum UnprotectSecretFlags : uint
{
    /// <summary>
    /// Uses the default unprotection behavior.
    /// </summary>
    None = 0,

    /// <summary>
    /// Decodes only the header of the protected data blob without decrypting the data.
    /// </summary>
    NoDecrypt = (uint)NCRYPT_FLAGS.NCRYPT_UNPROTECT_NO_DECRYPT,

    /// <summary>
    /// Requests that the key service provider not display a user interface.
    /// </summary>
    Silent = (uint)NCRYPT_FLAGS.NCRYPT_SILENT_FLAG
}
