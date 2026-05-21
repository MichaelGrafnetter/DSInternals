namespace DSInternals.Common.DNS;

/// <summary>
/// Specifies the flags carried by a DNSKEY or KEY resource record.
/// </summary>
/// <remarks>
/// Bit positions follow RFC 4034 section 2.1.1 (DNS network byte order).
/// </remarks>
[Flags]
public enum DnsKeyFlags : ushort
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = 0,

    /// <summary>
    /// Secure Entry Point (SEP) bit, per RFC 3757. When combined with <see cref="ZoneKey"/>, indicates a Key Signing Key (KSK). (DNS_KEY_SECURE_ENTRY_POINT)
    /// </summary>
    SecureEntryPoint = 0x0001,

    /// <summary>
    /// Revoke bit, per RFC 5011. Indicates that the key has been revoked. (DNS_KEY_REVOKE)
    /// </summary>
    Revoke = 0x0080,

    /// <summary>
    /// Zone Key bit, per RFC 4034. When set, the record holds a DNS zone key. (DNS_KEY_ZONE_KEY)
    /// </summary>
    ZoneKey = 0x0100
}
