using System.Runtime.InteropServices;
using System.Security.Principal;
using DSInternals.Common.Interop;

namespace DSInternals.SAM.Interop;

/// <summary>
/// Contains information about an account domain.
/// </summary>
/// <remarks>
/// Corresponds to the POLICY_ACCOUNT_DOMAIN_INFO structure.
/// See https://learn.microsoft.com/en-us/windows/win32/api/lsalookup/ns-lsalookup-policy_account_domain_info
/// </remarks>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct LsaDomainInformationNative
{
    /// <summary>
    /// Name of the account domain.
    /// </summary>
    internal UnicodeString DomainName;

    /// <summary>
    /// SID of the account domain.
    /// </summary>
    internal IntPtr DomainSid;

    public SecurityIdentifier? GetDomainSid()
    {
        return this.DomainSid != IntPtr.Zero ? new SecurityIdentifier(this.DomainSid) : null;
    }
}
