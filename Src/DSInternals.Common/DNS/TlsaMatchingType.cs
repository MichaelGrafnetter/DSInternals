namespace DSInternals.Common.DNS;

/// <summary>
/// Specifies the matching type field of a TLSA resource record (RFC 6698 §2.1.3).
/// </summary>
/// <remarks>
/// The underlying value matches the 1-byte matching type identifier on the DNS wire format
/// per the IANA "TLSA Matching Types" registry.
/// </remarks>
public enum TlsaMatchingType : byte
{
    /// <summary>
    /// Exact match on the selected content.
    /// </summary>
    Full = 0,

    /// <summary>
    /// SHA-256 hash of the selected content (RFC 6234).
    /// </summary>
    SHA256 = 1,

    /// <summary>
    /// SHA-512 hash of the selected content (RFC 6234).
    /// </summary>
    SHA512 = 2,
}
