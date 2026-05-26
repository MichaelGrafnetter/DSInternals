using Windows.Win32;

namespace DSInternals.Common.DNS;

/// <summary>
/// Specifies the cryptographic algorithm used to generate DNSSEC signing keys.
/// </summary>
/// <remarks>
/// The underlying value matches the 1-byte algorithm identifier on the DNS wire format (RFC 4034 Appendix A.1).
/// </remarks>
public enum DnsSigningAlgorithm : byte
{
    /// <summary>
    /// RSA/MD5 (RFC 4034; deprecated by RFC 6944, must not be used).
    /// </summary>
    RsaMd5 = (byte)PInvoke.DNSSEC_ALGORITHM_RSAMD5,

    /// <summary>
    /// RSA/SHA-1 (RFC 4034).
    /// </summary>
    RsaSha1 = (byte)PInvoke.DNSSEC_ALGORITHM_RSASHA1,

    /// <summary>
    /// RSA/SHA-1 with NSEC3 hashed authenticated denial of existence (RFC 5155).
    /// </summary>
    RsaNSec3Sha1 = (byte)PInvoke.DNSSEC_ALGORITHM_RSASHA1_NSEC3,

    /// <summary>
    /// RSA/SHA-256 (RFC 5702).
    /// </summary>
    RsaSha256 = (byte)PInvoke.DNSSEC_ALGORITHM_RSASHA256,

    /// <summary>
    /// RSA/SHA-512 (RFC 5702).
    /// </summary>
    RsaSha512 = (byte)PInvoke.DNSSEC_ALGORITHM_RSASHA512,

    /// <summary>
    /// ECDSA Curve P-256 with SHA-256 (RFC 6605).
    /// </summary>
    EcDsaP256Sha256 = (byte)PInvoke.DNSSEC_ALGORITHM_ECDSAP256_SHA256,

    /// <summary>
    /// ECDSA Curve P-384 with SHA-384 (RFC 6605).
    /// </summary>
    EcDsaP384Sha384 = (byte)PInvoke.DNSSEC_ALGORITHM_ECDSAP384_SHA384,

    /// <summary>
    /// Reserved algorithm identifier indicating that no signing algorithm is in use.
    /// </summary>
    Null = (byte)PInvoke.DNSSEC_ALGORITHM_NULL,

    /// <summary>
    /// Private algorithm identifier reserved for non-standard, implementation-defined algorithms (RFC 4034).
    /// </summary>
    Private = (byte)PInvoke.DNSSEC_ALGORITHM_PRIVATE
}
