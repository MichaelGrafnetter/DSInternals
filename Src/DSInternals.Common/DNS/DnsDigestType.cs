using System.Diagnostics.CodeAnalysis;
using Windows.Win32;

namespace DSInternals.Common.DNS;

/// <summary>
/// Specifies the digest algorithm used by a DS (Delegation Signer) resource record.
/// </summary>
/// <remarks>
/// The underlying value matches the 1-byte digest type identifier on the DNS wire format
/// per the IANA "DS Resource Record (RR) TYPE Digest Algorithms" registry.
/// </remarks>
public enum DnsDigestType : byte
{
    /// <summary>
    /// SHA-1 (RFC 3658).
    /// </summary>
    SHA1 = (byte)PInvoke.DNSSEC_DIGEST_ALGORITHM_SHA1,

    /// <summary>
    /// SHA-256 (RFC 4509).
    /// </summary>
    SHA256 = (byte)PInvoke.DNSSEC_DIGEST_ALGORITHM_SHA256,

    /// <summary>
    /// GOST R 34.11-94 (RFC 5933; deprecated by RFC 9558).
    /// </summary>
    GOST = 3,

    /// <summary>
    /// SHA-384 (RFC 6605).
    /// </summary>
    SHA384 = (byte)PInvoke.DNSSEC_DIGEST_ALGORITHM_SHA384,
}
