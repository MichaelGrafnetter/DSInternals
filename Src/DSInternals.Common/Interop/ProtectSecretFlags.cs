using Windows.Win32.Security.Cryptography;

namespace DSInternals.Common.Interop;

/// <summary>
/// Specifies options for <c>NCryptProtectSecret</c>.
/// </summary>
[Flags]
internal enum ProtectSecretFlags : uint
{
    /// <summary>
    /// Uses the default protection behavior.
    /// </summary>
    None = 0,

    /// <summary>
    /// Requests that the key service provider not display a user interface.
    /// </summary>
    Silent = (uint)NCRYPT_FLAGS.NCRYPT_SILENT_FLAG
}
