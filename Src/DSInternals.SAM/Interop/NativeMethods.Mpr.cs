using System.Net;
using System.Runtime.InteropServices;
using DSInternals.Common;
using DSInternals.Common.Interop;
using Windows.Win32.Foundation;
using Windows.Win32.NetworkManagement.WNet;

namespace DSInternals.SAM.Interop;

/// <summary>
/// Contains P/Invoke signatures for mpr.dll functions.
/// </summary>
internal static partial class NativeMethods
{
    private const string Mpr = "mpr.dll";

    /// <summary>
    /// Makes a connection to a network resource using the specified credentials.
    /// </summary>
    /// <param name="shareName">The remote network resource to connect to (e.g., \\server\IPC$).</param>
    /// <param name="credential">The credentials to use for the connection, or <c>null</c> to use the default credentials.</param>
    /// <param name="flags">A set of connection options.</param>
    internal static unsafe Win32ErrorCode WNetAddConnection2(string shareName, NetworkCredential? credential, NET_CONNECT_FLAGS flags)
    {
        fixed (char* remoteNamePtr = shareName)
        {
            NETRESOURCEW resource = new()
            {
                dwScope = NET_RESOURCE_SCOPE.RESOURCE_GLOBALNET,
                dwType = NET_RESOURCE_TYPE.RESOURCETYPE_ANY,
                lpRemoteName = new PWSTR(remoteNamePtr)
            };

            string? userName = credential?.GetLogonName();
            IntPtr passwordPtr = credential != null
                ? Marshal.SecureStringToGlobalAllocUnicode(credential.SecurePassword)
                : IntPtr.Zero;

            try
            {
                return WNetAddConnection2(resource, passwordPtr, userName, flags);
            }
            finally
            {
                if (passwordPtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(passwordPtr);
                }
            }
        }
    }

    /// <see>https://learn.microsoft.com/windows/win32/api/winnetwk/nf-winnetwk-wnetaddconnection2w</see>
    [DllImport(Mpr, CharSet = CharSet.Unicode, SetLastError = true, EntryPoint = "WNetAddConnection2W")]
    private static extern Win32ErrorCode WNetAddConnection2(in NETRESOURCEW netResource, IntPtr password, string? userName, NET_CONNECT_FLAGS flags);

    /// <summary>
    /// The WNetCancelConnection2 function cancels an existing network connection. You can also call the function to remove remembered network connections that are not currently connected.
    /// </summary>
    /// <param name="name">The name of either the redirected local device or the remote network resource to disconnect from.</param>
    /// <param name="flags">Connection type.</param>
    /// <param name="force">Specifies whether the disconnection should occur if there are open files or jobs on the connection.</param>
    /// <see>https://learn.microsoft.com/windows/win32/api/winnetwk/nf-winnetwk-wnetcancelconnection2w</see>
    [DllImport(Mpr, CharSet = CharSet.Unicode, SetLastError = true, EntryPoint = "WNetCancelConnection2W")]
    internal static extern Win32ErrorCode WNetCancelConnection2(string name, NetCancelOptions flags, [MarshalAs(UnmanagedType.Bool)] bool force);
}
