using Windows.Win32.Security.Cryptography;

namespace DSInternals.Common.Interop;

/// <summary>
/// Specifies which registry hive contains a registered protection descriptor display name.
/// </summary>
[Flags]
internal enum DescriptorNameFlags : uint
{
    /// <summary>
    /// Uses the current user registry hive.
    /// </summary>
    None = 0,

    /// <summary>
    /// Uses the local machine registry hive.
    /// </summary>
    MachineKey = (uint)NCRYPT_FLAGS.NCRYPT_MACHINE_KEY_FLAG
}
