namespace DSInternals.Common.DNS;

/// <summary>
/// Specifies the flags carried by an NSEC3 or NSEC3PARAM resource record (RFC 5155 §3.1.2).
/// </summary>
/// <remarks>
/// Bit positions follow the IANA "DNSSEC NSEC3 Flags" registry.
/// </remarks>
[Flags]
public enum Nsec3Flags : byte
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = 0,

    /// <summary>
    /// Opt-Out flag (RFC 5155 §3.1.2.1). When set, the NSEC3 record covers zero or more unsigned delegations.
    /// </summary>
    OptOut = 0x01,
}
