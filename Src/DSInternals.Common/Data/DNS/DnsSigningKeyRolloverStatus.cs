namespace DSInternals.Common.Data;

/// <summary>
/// Defines the current rollover status of a signing key descriptor.
/// </summary>
public enum DnsSigningKeyRolloverStatus : uint
{
    /// <summary>
    /// The signing key descriptor is not currently in the process of rolling over keys.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATUS_NOT_ROLLING.
    /// </remarks>
    NotRolling = 0x00000000,

    /// <summary>
    /// This signing key descriptor is waiting for another rollover to complete before its rollover can begin.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATUS_QUEUED.
    /// </remarks>
    Queued = 0x00000001,

    /// <summary>
    /// This signing key descriptor has begun the process of key rollover.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATUS_ROLL_STARTED.
    /// </remarks>
    RollStarted = 0x00000002,

    /// <summary>
    /// This ZSK signing key descriptor is waiting for the previous key to expire in all caching resolvers.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATUS_ZSK_WAITING_FOR_DNSKEY_TTL.
    /// </remarks>
    ZskWaitingForDnsKeyTtl = 0x00000003,

    /// <summary>
    /// This ZSK signing key descriptor is waiting for the signatures using the previous key to expire in all caching resolvers.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATUS_ZSK_WAITING_FOR_MAXZONE_TTL.
    /// </remarks>
    ZskWaitingForMaxZoneTtl = 0x00000004,

    /// <summary>
    /// This KSK signing key descriptor is waiting for a DS record corresponding to the new key to appear in the parent zone.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATUS_KSK_WAITING_FOR_DS_UPDATE.
    /// </remarks>
    KskWaitingForDsUpdate = 0x00000005,

    /// <summary>
    /// This KSK signing key descriptor is waiting for the DS record set in the parent zone to expire in all caching resolvers.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATUS_KSK_WAITING_FOR_DS_TTL.
    /// </remarks>
    KskWaitingForDsTtl = 0x00000006,

    /// <summary>
    /// This KSK signing key descriptor is waiting for the previous key to expire in all caching resolvers.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATUS_KSK_WAITING_FOR_DNSKEY_TTL.
    /// </remarks>
    KskWaitingForDnsKeyTtl = 0x00000007,

    /// <summary>
    /// This KSK signing key descriptor is waiting for the RFC5011 remove hold-down time before the revoked previous key can be removed.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATUS_KSK_WAITING_FOR_5011_REMOVE_HOLD_DOWN.
    /// </remarks>
    KskWaitingForRfc5011RemoveHoldDown = 0x00000008,

    /// <summary>
    /// This signing key descriptor experienced an unrecoverable error during the key rollover.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATUS_ROLL_ERROR.
    /// </remarks>
    RollError = 0x00000009
}
