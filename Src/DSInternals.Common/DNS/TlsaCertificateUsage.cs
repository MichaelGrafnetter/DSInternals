namespace DSInternals.Common.DNS;

/// <summary>
/// Specifies the certificate usage field of a TLSA resource record (RFC 6698 §2.1.1).
/// </summary>
/// <remarks>
/// The underlying value matches the 1-byte certificate usage identifier on the DNS wire format
/// per the IANA "TLSA Certificate Usages" registry.
/// </remarks>
public enum TlsaCertificateUsage : byte
{
    /// <summary>
    /// CA constraint: the certificate or public key MUST be found in the PKIX certification path.
    /// </summary>
    PkixTA = 0,

    /// <summary>
    /// Service certificate constraint: the certificate or public key MUST match the end-entity certificate from the PKIX chain.
    /// </summary>
    PkixEE = 1,

    /// <summary>
    /// Trust anchor assertion: the certificate or public key is used as a trust anchor, without requiring a PKIX chain.
    /// </summary>
    DaneTA = 2,

    /// <summary>
    /// Domain-issued certificate: the certificate or public key MUST match the end-entity certificate, without requiring PKIX validation.
    /// </summary>
    DaneEE = 3,
}
