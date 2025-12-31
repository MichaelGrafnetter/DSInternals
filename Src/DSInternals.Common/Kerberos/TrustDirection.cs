using Windows.Win32.Security.Authentication.Identity;

namespace DSInternals.Common.Kerberos;

/// <summary>
/// Dictates in which direction the trust flows.
/// </summary>
/// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/5026a939-44ba-47b2-99cf-386a9e674b04</remarks>
public enum TrustDirection : uint
{
    /// <summary>
    /// The trust relationship exists but has been disabled.
    /// </summary>
    /// <remarks>TRUST_DIRECTION_DISABLED</remarks>
    Disabled = TRUSTED_DOMAIN_TRUST_DIRECTION.TRUST_DIRECTION_DISABLED,

    /// <summary>
    /// The trusted domain trusts the primary domain to perform operations such as name lookups and authentication.
    /// </summary>
    /// <remarks>TRUST_DIRECTION_INBOUND</remarks>
    Inbound = TRUSTED_DOMAIN_TRUST_DIRECTION.TRUST_DIRECTION_INBOUND,

    /// <summary>
    /// The primary domain trusts the trusted domain to perform operations such as name lookups and authentication.
    /// </summary>
    /// <remarks>TRUST_DIRECTION_OUTBOUND</remarks>
    Outbound = TRUSTED_DOMAIN_TRUST_DIRECTION.TRUST_DIRECTION_OUTBOUND,

    /// <summary>
    /// Both domains trust one another for operations such as name lookups and authentication.
    /// </summary>
    /// <remarks>TRUST_DIRECTION_BIDIRECTIONAL</remarks>
    Bidirectional = TRUSTED_DOMAIN_TRUST_DIRECTION.TRUST_DIRECTION_BIDIRECTIONAL
}
