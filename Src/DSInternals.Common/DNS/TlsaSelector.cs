namespace DSInternals.Common.DNS;

/// <summary>
/// Specifies the selector field of a TLSA resource record (RFC 6698 §2.1.2).
/// </summary>
/// <remarks>
/// The underlying value matches the 1-byte selector identifier on the DNS wire format
/// per the IANA "TLSA Selectors" registry.
/// </remarks>
public enum TlsaSelector : byte
{
    /// <summary>
    /// Full certificate.
    /// </summary>
    Certificate = 0,

    /// <summary>
    /// SubjectPublicKeyInfo (DER-encoded).
    /// </summary>
    SubjectPublicKeyInfo = 1,
}
