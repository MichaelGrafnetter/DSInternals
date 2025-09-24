using Windows.Win32;

namespace DSInternals.Replication;

/// <summary>
/// Specifies the authentication service to use in RPC calls.
/// </summary>
public enum RpcAuthenticationType : uint
{
    /// <summary>
    /// No authentication.
    /// </summary>
    None = PInvoke.RPC_C_AUTHN_NONE,

    /// <summary>
    /// SPNEGO
    /// </summary>
    Negotiate = PInvoke.RPC_C_AUTHN_GSS_NEGOTIATE,

    /// <summary>
    /// NTLM
    /// </summary>
    [Obsolete("Use Negotiate or Kerberos instead.")]
    NTLM = PInvoke.RPC_C_AUTHN_WINNT,

    /// <summary>
    /// Kerberos
    /// </summary>
    Kerberos = PInvoke.RPC_C_AUTHN_GSS_KERBEROS,

    /// <summary>
    /// Same as NTLM
    /// </summary>
    [Obsolete("Use Negotiate or Kerberos instead.")]
    Default = unchecked((uint)PInvoke.RPC_C_AUTHN_DEFAULT)
}
