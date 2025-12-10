namespace DSInternals.Common.Data;

/// <summary>
/// Specifies possible key rollover actions for a signing key descriptor.
/// </summary>
public enum DnsSigningKeyRolloverAction : uint
{
    /// <summary>
    /// The server MUST never send this value.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_ROLLOVER_ACTION_DEFAULT.
    /// </remarks>
    Default = 0x00000000,

    /// <summary>
    /// The server will perform a normal key rollover the next time the keys for this signing key descriptor are rolled over.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_ROLLOVER_ACTION_NORMAL.
    /// </remarks>
    Normal = 0x00000001,

    /// <summary>
    /// The server will revoke the standby key for this signing key descriptor the next time the keys for this signing key descriptor are rolled over.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_ROLLOVER_ACTION_REVOKE_STANDBY.
    /// </remarks>
    RevokeStandby = 0x00000002,

    /// <summary>
    /// The server will retire this signing key descriptor and remove all signatures associated with it the next time the keys for this signing key descriptor are rolled over.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_ROLLOVER_ACTION_RETIRE.
    /// </remarks>
    Retire = 0x00000003
}
