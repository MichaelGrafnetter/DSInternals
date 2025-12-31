using Windows.Win32.Security.Authentication.Identity;

namespace DSInternals.Common.Kerberos;

/// <summary>
/// Dictates the type of AuthInfo that is being stored.
/// </summary>
/// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/dfe16abb-4dfb-402d-bc54-84fcc9932fad</remarks>
public enum TrustAuthenticationInformationType : uint
{
    /// <summary>
    /// AuthInfo byte field is invalid/not relevant.
    /// </summary>
    None = LSA_AUTH_INFORMATION_AUTH_TYPE.TRUST_AUTH_TYPE_NONE,

    /// <summary>
    /// AuthInfo byte field contains an RC4 Key [RFC4757].
    /// </summary>
    NTHash = LSA_AUTH_INFORMATION_AUTH_TYPE.TRUST_AUTH_TYPE_NT4OWF,

    /// <summary>
    /// AuthInfo byte field contains a cleartext password, encoded as a Unicode string.
    /// </summary>
    CleartextPassword = LSA_AUTH_INFORMATION_AUTH_TYPE.TRUST_AUTH_TYPE_CLEAR,

    /// <summary>
    /// AuthInfo byte field contains a version number, used by Netlogon for versioning interdomain trust secrets.
    /// </summary>
    /// <remarks>TRUST_AUTH_TYPE_VERSION</remarks>
    Version = LSA_AUTH_INFORMATION_AUTH_TYPE.TRUST_AUTH_TYPE_VERSION
}
