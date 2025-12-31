using Windows.Win32.Security.Authentication.Identity;

namespace DSInternals.Common.Kerberos;

/// <summary>
/// Dictates what type of trust has been designated for the trusted domain.
/// </summary>
/// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/36565693-b5e4-4f37-b0a8-c1b12138e18e</remarks>
public enum TrustType : uint
{
    Unknown = 0,

    /// <summary>
    /// The trusted domain is a Windows domain not running Active Directory.
    /// </summary>
    /// <remarks>TRUST_TYPE_DOWNLEVEL</remarks>
    Downlevel = TRUSTED_DOMAIN_TRUST_TYPE.TRUST_TYPE_DOWNLEVEL,

    /// <summary>
    /// The trusted domain is a Windows domain running Active Directory.
    /// </summary>
    /// <remarks>TRUST_TYPE_UPLEVEL</remarks>
    Uplevel = TRUSTED_DOMAIN_TRUST_TYPE.TRUST_TYPE_UPLEVEL,

    /// <summary>
    /// The trusted domain is running a non-Windows, RFC4120-compliant Kerberos distribution.
    /// </summary>
    /// <remarks>TRUST_TYPE_MIT</remarks>
    MIT = TRUSTED_DOMAIN_TRUST_TYPE.TRUST_TYPE_MIT,

    /// <summary>
    /// Historical reference; this value is not used in Windows.
    /// </summary>
    /// <remarks>TRUST_TYPE_DCE</remarks>
    DCE = TRUSTED_DOMAIN_TRUST_TYPE.TRUST_TYPE_DCE,

    /// <summary>
    /// The trusted domain is in Azure Active Directory.
    /// </summary>
    /// <remarks>TRUST_TYPE_AAD</remarks>
    AAD = 0x00000005
}
