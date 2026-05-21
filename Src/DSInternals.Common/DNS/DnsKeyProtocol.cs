using Windows.Win32;

namespace DSInternals.Common.DNS;

/// <summary>
/// Specifies the protocol field of a DNSKEY or KEY resource record.
/// </summary>
/// <remarks>
/// DNSKEY records always use <see cref="DnsSec"/> per RFC 4034 section 2.1.2.
/// Other values originate from the legacy KEY record (RFC 2535) and are obsolete.
/// </remarks>
public enum DnsKeyProtocol : byte
{
    /// <summary>
    /// Reserved.
    /// </summary>
    None = (byte)PInvoke.DNSSEC_PROTOCOL_NONE,

    /// <summary>
    /// Transport Layer Security (legacy KEY only).
    /// </summary>
    TLS = (byte)PInvoke.DNSSEC_PROTOCOL_TLS,

    /// <summary>
    /// Electronic mail (legacy KEY only).
    /// </summary>
    Email = (byte)PInvoke.DNSSEC_PROTOCOL_EMAIL,

    /// <summary>
    /// DNS Security. The only valid value for a DNSKEY record. (DNS_KEY_PROTOCOL_DNSSEC)
    /// </summary>
    DnsSec = (byte)PInvoke.DNSSEC_PROTOCOL_DNSSEC,

    /// <summary>
    /// IPsec (legacy KEY only).
    /// </summary>
    IPSec = (byte)PInvoke.DNSSEC_PROTOCOL_IPSEC,
}
