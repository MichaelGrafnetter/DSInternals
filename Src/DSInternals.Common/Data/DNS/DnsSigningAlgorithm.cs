using System.Diagnostics.CodeAnalysis;
using Windows.Win32;

namespace DSInternals.Common.Data;

/// <summary>
/// Specifies the cryptographic algorithm used to generate DNSSEC signing keys.
/// </summary>
[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Identifier names are copied from Windows header files.")]
public enum DnsSigningAlgorithm : uint
{
    RSA_MD5 = PInvoke.DNSSEC_ALGORITHM_RSAMD5,
    RSA_SHA1 = PInvoke.DNSSEC_ALGORITHM_RSASHA1,
    RSA_SHA1_NSEC3 = PInvoke.DNSSEC_ALGORITHM_RSASHA1_NSEC3,
    RSA_SHA256 = PInvoke.DNSSEC_ALGORITHM_RSASHA256,
    RSA_SHA512 = PInvoke.DNSSEC_ALGORITHM_RSASHA512,
    P256_SHA256 = PInvoke.DNSSEC_ALGORITHM_ECDSAP256_SHA256,
    P384_SHA384 = PInvoke.DNSSEC_ALGORITHM_ECDSAP384_SHA384,
    Null = PInvoke.DNSSEC_ALGORITHM_NULL,
    Private = PInvoke.DNSSEC_ALGORITHM_PRIVATE
}
