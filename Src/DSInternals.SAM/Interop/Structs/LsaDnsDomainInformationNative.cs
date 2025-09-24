using System.Runtime.InteropServices;
using System.Security.Principal;
using DSInternals.Common.Interop;

namespace DSInternals.SAM.Interop;

/// <summary>
/// Contains information about an Active Directory domain.
/// </summary>
/// <remarks>
/// Corresponds to the POLICY_DNS_DOMAIN_INFO structure.
/// See https://learn.microsoft.com/en-us/windows/win32/api/lsalookup/ns-lsalookup-policy_dns_domain_info
/// </remarks>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct LsaDnsDomainInformationNative
{
    /// <summary>
    /// Name of the primary domain.
    /// </summary>
    internal UnicodeString Name;

    /// <summary>
    /// DNS name of the primary domain.
    /// </summary>
    internal UnicodeString DnsDomainName;

    /// <summary>
    /// DNS forest name of the primary domain. This is the DNS name of the domain at the root of the enterprise.
    /// </summary>
    internal UnicodeString DnsForestName;

    /// <summary>
    /// The GUID of the primary domain.
    /// </summary>
    internal Guid DomainGuid;

    /// <summary>
    /// The SID of the primary domain.
    /// </summary>
    internal IntPtr DomainSid;

    public SecurityIdentifier? GetDomainSid()
    {
        return this.DomainSid != IntPtr.Zero ? new SecurityIdentifier(this.DomainSid) : null;
    }
}
