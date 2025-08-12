namespace NDceRpc.Microsoft.Interop
{
    /// <summary>
    /// The authentication service constants represent the authentication services passed to various run-time functions.
    ///The following constants are valid values for the AuthnSvc parameter.
    /// The authentication type to be used for connection, GSS_NEGOTIATE / WINNT
    /// are the most common.  Be aware that GSS_NEGOTIATE is not available unless
    /// the machine is a member of a domain that is not running WinNT (or in legacy 
    /// mode).
    /// </summary>
    /// <remarks>
    /// Specify RPC_C_AUTHN_NONE to turn off authentication for remote procedure calls made over a binding handle. When you specify RPC_C_AUTHN_DEFAULT, the RPC run-time library uses the RPC_C_AUTHN_WINNT authentication service for remote procedure calls made using the binding handle.
    /// </remarks>
    public enum RPC_C_AUTHN : uint
    {
        /// <summary>
        /// No authentication.
        /// </summary>
        RPC_C_AUTHN_NONE = 0,
        RPC_C_AUTHN_DCE_PRIVATE = 1,
        RPC_C_AUTHN_DCE_PUBLIC = 2,
        RPC_C_AUTHN_DEC_PUBLIC = 4,
        RPC_C_AUTHN_GSS_NEGOTIATE = 9,
        RPC_C_AUTHN_WINNT = 10,
        RPC_C_AUTHN_GSS_SCHANNEL = 14,
        RPC_C_AUTHN_GSS_KERBEROS = 16,
        RPC_C_AUTHN_DPA = 17,
        RPC_C_AUTHN_MSN = 18,
        RPC_C_AUTHN_DIGEST = 21,
        RPC_C_AUTHN_MQ = 100,
        RPC_C_AUTHN_DEFAULT = 0xFFFFFFFFu
    }
}