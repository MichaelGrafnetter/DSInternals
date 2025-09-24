using System.Net;
using System.Runtime.InteropServices;
using DSInternals.Common.Interop;
using Windows.Win32.System.Rpc;

namespace DSInternals.Replication;

/// <summary>
/// Contains the P/Invoke signatures for the native methods used in the DSInternals.Replication assembly.
/// </summary>
internal static class NativeMethods
{
    /// <summary>
    /// The name of the DLL that contains the RPC functions.
    /// </summary>
    private const string RpcRt4 = "Rpcrt4.dll";

    /// <summary>
    /// Creates a string binding from its components.
    /// </summary>
    public static RPC_STATUS RpcStringBindingCompose(
        Guid? objUuid,
        RpcProtseq protSeq,
        string networkAddress,
        string endpoint,
        string options,
        out string binding)
    {
        RPC_STATUS result = RpcStringBindingCompose(objUuid?.ToString(), protSeq.ToString(), networkAddress, endpoint, options, out SafeRpcStringHandle stringBindingHandle);

        if (stringBindingHandle.IsInvalid)
        {
            binding = null;
        }
        else
        {
            binding = stringBindingHandle.Value;
            stringBindingHandle.Dispose();
        }

        return result;
    }

    /// <summary>
    /// Creates a string binding from its components.
    /// </summary>
    [DllImport(RpcRt4, CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern RPC_STATUS RpcStringBindingCompose(
        string objUuid,
        string protseq,
        string networkAddress,
        string EndPoint,
        string Options,
        out SafeRpcStringHandle stringBindingHandle
    );

    /// <summary>
    /// Creates a binding handle from a string representation of the binding.
    /// </summary>
    [DllImport(RpcRt4, CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern RPC_STATUS RpcBindingFromStringBinding(string binding, out SafeRpcBindingHandle bindingHandle);

    /// <summary>
    /// Sets the authentication information that will be used to make calls on the specified binding handle.
    /// </summary>
    public static RPC_STATUS RpcBindingSetAuthInfo(
        SafeRpcBindingHandle bindingHandle,
        string serverPrincipalName,
        RpcAuthenticationLevel authenticationLevel = RpcAuthenticationLevel.PacketPrivacy,
        RpcAuthenticationType authenticationType = RpcAuthenticationType.Negotiate,
        NetworkCredential authIdentity = null,
        uint authzSvc = 0)
    {
        RPC_STATUS result;

        if (authIdentity != null)
        {
            // Use explicit credentials
            WindowsAuthenticationIdentity identity = new(authIdentity);
            try
            {
                result = RpcBindingSetAuthInfo(bindingHandle, serverPrincipalName, authenticationLevel, authenticationType, ref identity, authzSvc);
            }
            finally
            {
                // Remove the clear text password from memory ASAP.
                identity.Dispose();
            }
        }
        else
        {
            // Use the current identity
            result = RpcBindingSetAuthInfo(bindingHandle, serverPrincipalName, authenticationLevel, authenticationType, authIdentity: IntPtr.Zero, authzSvc);
        }

        return result;
    }

    /// <summary>
    /// Sets the authentication information that will be used to make calls on the specified binding handle.
    /// </summary>
    [DllImport(RpcRt4, CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern RPC_STATUS RpcBindingSetAuthInfo(
        SafeRpcBindingHandle bindingHandle,
        string serverPrincipalName,
        RpcAuthenticationLevel authenticationLevel,
        RpcAuthenticationType authenticationType,
        [In] ref WindowsAuthenticationIdentity authIdentity,
        uint authzSvc);

    [DllImport(RpcRt4, CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern RPC_STATUS RpcBindingSetAuthInfo(
        SafeRpcBindingHandle bindingHandle,
        string serverPrincipalName,
        RpcAuthenticationLevel authenticationLevel,
        RpcAuthenticationType authenticationType,
        IntPtr authIdentity,
        uint authzSvc);

    /// <summary>
    /// Frees a string allocated by the RPC runtime.
    /// </summary>
    [DllImport(RpcRt4, CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern RPC_STATUS RpcStringFree(ref IntPtr stringPointer);

    /// <summary>
    /// Frees a binding handle allocated by the RPC runtime.
    /// </summary>
    [DllImport(RpcRt4, SetLastError = true)]
    internal static extern RPC_STATUS RpcBindingFree(ref IntPtr binding);
}
