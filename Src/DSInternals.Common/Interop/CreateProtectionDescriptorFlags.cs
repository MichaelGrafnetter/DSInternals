using Windows.Win32;
using Windows.Win32.Security.Cryptography;

namespace DSInternals.Common.Interop;

/// <summary>
/// Specifies how <c>NCryptCreateProtectionDescriptor</c> interprets a protection descriptor string.
/// </summary>
[Flags]
internal enum CreateProtectionDescriptorFlags : uint
{
    /// <summary>
    /// Treats the descriptor string as a complete protection descriptor rule string.
    /// </summary>
    None = 0,

    /// <summary>
    /// Treats the descriptor string as a registered display name.
    /// </summary>
    NamedDescriptor = PInvoke.NCRYPT_NAMED_DESCRIPTOR_FLAG,

    /// <summary>
    /// Searches the local machine registry hive for the registered display name.
    /// </summary>
    MachineKey = (uint)NCRYPT_FLAGS.NCRYPT_MACHINE_KEY_FLAG
}
