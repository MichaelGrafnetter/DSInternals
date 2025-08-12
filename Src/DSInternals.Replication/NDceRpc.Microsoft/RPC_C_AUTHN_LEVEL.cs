namespace NDceRpc.Microsoft.Interop
{
    /// <summary>
    /// The authentication-level constants represent authentication levels passed to various run-time functions. These levels are listed in order of increasing authentication. Each new level adds to the authentication provided by the previous level. If the RPC run-time library does not support the specified level, it automatically upgrades to the next higher supported level.
    /// The protection level of the communications, <see cref="RPC_C_AUTHN_LEVEL_PKT_PRIVACY"/> is the default for authenticated communications.
    /// </summary>
    /// <remarks>
    /// Regardless of the value specified by the constant, ncalrpc always uses <see cref="RPC_C_AUTHN_LEVEL_PKT_PRIVACY"/>.
    /// </remarks>
    public enum RPC_C_AUTHN_LEVEL : uint
    {
        RPC_C_AUTHN_LEVEL_DEFAULT = 0,
        /// <summary>
        /// Performs no authentication
        /// </summary>
        RPC_C_AUTHN_LEVEL_NONE = 1,
        RPC_C_AUTHN_LEVEL_CONNECT = 2,
        RPC_C_AUTHN_LEVEL_CALL = 3,
        RPC_C_AUTHN_LEVEL_PKT = 4,
        RPC_C_AUTHN_LEVEL_PKT_INTEGRITY = 5,
        RPC_C_AUTHN_LEVEL_PKT_PRIVACY = 6,
    }
}