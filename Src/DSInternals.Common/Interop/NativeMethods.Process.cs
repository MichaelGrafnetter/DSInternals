using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;
using Windows.Win32.Foundation;
using Windows.Win32.Security;

namespace DSInternals.Common.Interop;

internal static partial class NativeMethods
{
    private const string Kernel32 = "kernel32.dll";

    /// <summary>
    /// Retrieves a pseudo handle for the current process.
    /// </summary>
    [DllImport(Kernel32)]
    internal static extern SafeProcessHandle GetCurrentProcess();

    /// <summary>
    /// Opens the access token associated with a process.
    /// </summary>
    /// <param name="processHandle">A handle to the process whose access token is opened.</param>
    /// <param name="desiredAccess">Specifies an access mask that specifies the requested types of access to the access token.</param>
    /// <param name="tokenHandle">A handle to the opened access token.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    [DllImport(Advapi, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool OpenProcessToken(SafeProcessHandle processHandle, TokenAccessLevels desiredAccess, out SafeAccessTokenHandle tokenHandle);

    /// <summary>
    /// Retrieves the locally unique identifier (LUID) used on a specified system to locally represent the specified privilege name.
    /// </summary>
    /// <param name="systemName">The name of the system.</param>
    /// <param name="name">The name of the privilege.</param>
    /// <param name="luid">The locally unique identifier (LUID) for the privilege.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    [DllImport(Advapi, SetLastError = true, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool LookupPrivilegeValue(string? systemName, string name, out LUID luid);

    /// <summary>
    /// Enables or disables the specified privilege in the specified access token.
    /// </summary>
    /// <param name="tokenHandle">A handle to the access token.</param>
    /// <param name="privilegeLuid">The locally unique identifier (LUID) for the privilege.</param>
    /// <param name="enabled">Specifies whether the privilege is enabled or disabled.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    internal static bool AdjustTokenPrivileges(SafeAccessTokenHandle tokenHandle, LUID privilegeLuid, bool enabled)
    {
        TOKEN_PRIVILEGES newState = new();
        newState.PrivilegeCount = 1;
        Span<LUID_AND_ATTRIBUTES> privileges = newState.Privileges.AsSpan(1);
        privileges[0].Luid = privilegeLuid;
        privileges[0].Attributes = enabled ? TOKEN_PRIVILEGES_ATTRIBUTES.SE_PRIVILEGE_ENABLED : 0;

        return AdjustTokenPrivileges(tokenHandle, disableAllPrivileges: false, ref newState, bufferLength: 0, previousState: IntPtr.Zero, out _);
    }

    /// <summary>
    /// Enables or disables privileges in the specified access token.
    /// </summary>
    /// <param name="tokenHandle">A handle to the access token that contains the privileges to be modified.</param>
    /// <param name="disableAllPrivileges">Specifies whether all privileges are to be disabled.</param>
    /// <param name="newState">The new state of the privileges.</param>
    /// <param name="bufferLength">The size of the buffer that receives the previous state of the privileges.</param>
    /// <param name="previousState">The previous state of the privileges.</param>
    /// <param name="returnLength">The size of the previous state of the privileges.</param>
    /// <returns>True if the function succeeds; otherwise, false.</returns>
    [DllImport(Advapi, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AdjustTokenPrivileges(SafeAccessTokenHandle tokenHandle, [MarshalAs(UnmanagedType.Bool)] bool disableAllPrivileges, ref TOKEN_PRIVILEGES newState, uint bufferLength, IntPtr previousState, out uint returnLength);
}
