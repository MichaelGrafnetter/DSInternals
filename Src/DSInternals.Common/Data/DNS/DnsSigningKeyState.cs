namespace DSInternals.Common.Data;

/// <summary>
/// Defines the current state of a signing key descriptor.
/// </summary>
public enum DnsSigningKeyState : uint
{
    /// <summary>
    /// The signing key descriptor is active and in use for online signing of the zone.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATE_ACTIVE.
    /// </remarks>
    Active = 0,

    /// <summary>
    /// The signing key descriptor is no longer in use for online signing.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SKD_STATE_RETIRED.
    /// </remarks>
    Retired
}
