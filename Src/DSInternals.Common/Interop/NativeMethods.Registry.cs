using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Common.Interop;

internal static partial class NativeMethods
{
    /// <summary>
    /// Creates a subkey under HKEY_USERS or HKEY_LOCAL_MACHINE and loads the data from the specified registry hive into that subkey.
    /// </summary>
    /// <param name="hKey">A handle to the key where the subkey will be created.</param>
    /// <param name="subKey">The name of the key to be created under hKey. This subkey is where the registration information from the file will be loaded.</param>
    /// <param name="file">The name of the file containing the registry data. This file must be a local file that was created with the RegSaveKey function. If this file does not exist, a file is created with the specified name.</param>
    /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
    [DllImport(Advapi, CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern Win32ErrorCode RegLoadKey(RegistryHive hKey, string subKey, string file);

    /// <summary>
    /// Unloads the specified registry key and its subkeys from the registry.
    /// </summary>
    /// <param name="hKey">A handle to the registry key to be unloaded.</param>
    /// <param name="SubKey">The name of the subkey to be unloaded. The key referred to by the lpSubKey parameter must have been created by using the RegLoadKey function.</param>
    /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
    [DllImport(Advapi, CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern Win32ErrorCode RegUnLoadKey(RegistryHive hKey, string subKey);

    [DllImport(Advapi, CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern Win32ErrorCode RegQueryInfoKey(SafeRegistryHandle hKey, StringBuilder lpClass, ref int lpcbClass, IntPtr lpReserved, int[] lpcSubKeys, int[] lpcbMaxSubKeyLen, int[] lpcbMaxClassLen, int[] lpcValues, int[] lpcbMaxValueNameLen, int[] lpcbMaxValueLen, int[] lpcbSecurityDescriptor, out long lpftLastWriteTime);
    internal static Win32ErrorCode RegQueryInfoKey(SafeRegistryHandle hKey, out string keyClass, out DateTime lastWriteTime)
    {
        StringBuilder buffer = new(MaxRegistryKeyClassSize);
        int bufferSize = buffer.Capacity;
        Win32ErrorCode result = NativeMethods.RegQueryInfoKey(hKey, buffer, ref bufferSize, IntPtr.Zero, null, null, null, null, null, null, null, out long fileTime);
        keyClass = buffer.ToString();
        lastWriteTime = DateTime.FromFileTimeUtc(fileTime);
        return result;
    }
}
