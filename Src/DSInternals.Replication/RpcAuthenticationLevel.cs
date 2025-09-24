using Windows.Win32.System.Com;

namespace DSInternals.Replication;

/// <summary>
/// A numeric value indicating the level of authentication or 
/// message protection that remote procedure call (RPC) 
/// will apply to a specific message exchange. 
/// </summary>
public enum RpcAuthenticationLevel : uint
{
    /// <summary>
    /// Same as Connect.
    /// </summary>
    Default = RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_DEFAULT,

    /// <summary>
    /// Performs no authentication
    /// </summary>
    None = RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_NONE,

    /// <summary>
    /// Authenticates the credentials of the client and server.
    /// </summary>
    Connect = RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_CONNECT,

    /// <summary>
    /// Same as Packet.
    /// </summary>
    Call = RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_CALL,

    /// <summary>
    /// Same as Connect but also prevents replay attacks.
    /// </summary>
    Packet = RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT,

    /// <summary>
    /// Same as Packet but also verifies that none of the 
    /// data transferred between the client and server has been modified.
    /// </summary>
    PacketIntegrity = RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_INTEGRITY,

    /// <summary>
    /// Same as PacketIntegrity but also ensures that the data 
    /// transferred can only be seen unencrypted by the client and the server.
    /// </summary>
    PacketPrivacy = RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY
}
