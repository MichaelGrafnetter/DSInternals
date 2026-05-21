using System.Diagnostics.CodeAnalysis;
using DSInternals.Common.Data;
using DSInternals.Common.Schema;

namespace DSInternals.Common.DNS;

/// <summary>
/// Represents an Active Directory-integrated DNS zone.
/// </summary>
public class DnsZone
{
    // Mirrors DNS_IP4_REVERSE_DOMAIN_STRING_W / DNS_IP6_REVERSE_DOMAIN_STRING_W from windns.h.
    // Not exposed by the Win32 metadata project, so defined locally.
    private const string Ip4ReverseLookupZoneSuffix = ".in-addr.arpa";
    private const string Ip6ReverseLookupZoneSuffix = ".ip6.arpa";

    /// <summary>
    /// The distinguished name of the DNS zone object in Active Directory.
    /// </summary>
    public string DistinguishedName
    {
        get;
        private set;
    }

    /// <summary>
    /// The fully qualified name of the DNS zone.
    /// </summary>
    public string ZoneName
    {
        get;
        private set;
    }

    /// <summary>
    /// Indicates whether the zone is integrated with Active Directory.
    /// </summary>
    /// <remarks>
    /// Always <see langword="true"/> because <see cref="DnsZone"/> instances are constructed from
    /// AD-integrated zones only.
    /// </remarks>
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Exposed as an instance property to match the Microsoft DnsServer PowerShell module shape.")]
    public bool IsDsIntegrated => true;

    /// <summary>
    /// Indicates whether the zone is a reverse-lookup zone.
    /// </summary>
    public bool IsReverseLookupZone =>
        !string.IsNullOrEmpty(this.ZoneName) &&
        (this.ZoneName.EndsWith(Ip4ReverseLookupZoneSuffix, StringComparison.OrdinalIgnoreCase) ||
         this.ZoneName.EndsWith(Ip6ReverseLookupZoneSuffix, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Indicates whether the zone is signed using DNSSEC.
    /// </summary>
    public bool IsSigned
    {
        get;
        private set;
    }

    /// <summary>
    /// Constructs a <see cref="DnsZone"/> from a directory object representing a dnsZone container.
    /// </summary>
    /// <param name="dnsZone">The directory object backing the AD-integrated DNS zone.</param>
    /// <returns>A populated <see cref="DnsZone"/> instance.</returns>
    public static DnsZone Create(DirectoryObject dnsZone)
    {
        ArgumentNullException.ThrowIfNull(dnsZone);

        dnsZone.ReadAttribute(CommonDirectoryAttributes.DnsIsSigned, out bool isSigned);

        return Create(dnsZone.DistinguishedName, isSigned);
    }

    /// <summary>
    /// Constructs a <see cref="DnsZone"/> from its directory distinguished name and signing state.
    /// </summary>
    /// <param name="distinguishedName">The distinguished name of the dnsZone object in Active Directory.</param>
    /// <param name="isSigned">Indicates whether the zone is signed using DNSSEC.</param>
    /// <returns>A populated <see cref="DnsZone"/> instance.</returns>
    public static DnsZone Create(string distinguishedName, bool isSigned)
    {
        ArgumentException.ThrowIfNullOrEmpty(distinguishedName);

        // The leading DC= RDN of the dnsZone object carries the FQDN of the zone.
        var parsedDN = new DistinguishedName(distinguishedName);

        return new DnsZone
        {
            DistinguishedName = distinguishedName,
            ZoneName = parsedDN.Components.Count > 0 ? parsedDN.Components[0].Value : null,
            IsSigned = isSigned
        };
    }
}
