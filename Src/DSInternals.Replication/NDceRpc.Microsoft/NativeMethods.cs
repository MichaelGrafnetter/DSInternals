using System;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
    /// <summary>
    /// Native RPC methods required for NativeClient
    /// </summary>
    public static class NativeMethods
    {
        /// <summary>
        /// Creates a string binding handle
        /// </summary>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcStringBindingComposeW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcStringBindingCompose(
            String ObjUuid,
            String ProtSeq,
            String NetworkAddr,
            String EndPoint,
            String Options,
            out IntPtr StringBinding);

        /// <summary>
        /// Returns a binding handle from a string representation of a binding handle
        /// </summary>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingFromStringBindingW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingFromStringBinding(String bindingString, out IntPtr lpBinding);

        /// <summary>
        /// Sets authentication information for a binding handle
        /// </summary>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingSetAuthInfoW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingSetAuthInfo(IntPtr Binding, String ServerPrincName,
                                                             RPC_C_AUTHN_LEVEL AuthnLevel, RPC_C_AUTHN AuthnSvc,
                                                             [In] ref SEC_WINNT_AUTH_IDENTITY AuthIdentity,
                                                             uint AuthzSvc);

        /// <summary>
        /// Sets authentication information for a binding handle (without identity)
        /// </summary>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingSetAuthInfoW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingSetAuthInfo2(IntPtr Binding, String ServerPrincName,
                                                              RPC_C_AUTHN_LEVEL AuthnLevel, RPC_C_AUTHN AuthnSvc,
                                                              IntPtr AuthIdentity,
                                                              uint AuthzSvc);

        /// <summary>
        /// Frees a string allocated by RPC
        /// </summary>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcStringFreeW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcStringFree(ref IntPtr lpString);

        /// <summary>
        /// Releases binding handle resources
        /// </summary>
        [DllImport("Rpcrt4.dll", EntryPoint = "RpcBindingFree", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern RPC_STATUS RpcBindingFree(ref IntPtr lpString);
    }
}