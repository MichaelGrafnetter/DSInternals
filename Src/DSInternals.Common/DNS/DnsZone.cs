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
    /// The FQDN of the pseudo-zone that holds the DNS root hints (cached NS records
    /// for the DNS root, not a real signable zone).
    /// </summary>
    public const string RootHintsZoneName = "RootDNSServers";

    /// <summary>
    /// The FQDN of the pseudo-zone that holds DNSSEC trust anchors.
    /// </summary>
    public const string TrustAnchorsZoneName = "..TrustAnchors";

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
    /// Indicates whether the zone is signed using NSEC3 (rather than NSEC) for authenticated denial of existence.
    /// </summary>
    public bool SignWithNSEC3
    {
        get;
        private set;
    }

    /// <summary>
    /// The current NSEC3 salt value used when signing the zone with NSEC3.
    /// </summary>
    /// <remarks>
    /// This is the salt currently in effect on the zone. The user-configured salt is stored
    /// separately in the msDNS-NSEC3UserSalt attribute.
    /// </remarks>
    public byte[]? NSEC3CurrentSalt
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
        dnsZone.ReadAttribute(CommonDirectoryAttributes.DnsSignWithNSEC3, out bool signWithNSEC3);

        // msDNS-NSEC3CurrentSalt has DirectoryString syntax and stores the salt as a hex string (e.g. "879006FFA707C0F7").
        dnsZone.ReadAttribute(CommonDirectoryAttributes.DnsNSEC3CurrentSalt, out string? nsec3CurrentSaltHex);
        byte[]? nsec3CurrentSalt = nsec3CurrentSaltHex.HexToBinary();

        return Create(dnsZone.DistinguishedName, isSigned, signWithNSEC3, nsec3CurrentSalt);
    }

    /// <summary>
    /// Constructs a <see cref="DnsZone"/> from its directory distinguished name and signing state.
    /// </summary>
    /// <param name="distinguishedName">The distinguished name of the dnsZone object in Active Directory.</param>
    /// <param name="isSigned">Indicates whether the zone is signed using DNSSEC.</param>
    /// <param name="signWithNSEC3">Indicates whether the zone is signed using NSEC3.</param>
    /// <param name="nsec3CurrentSalt">The current NSEC3 salt value in effect on the zone.</param>
    /// <returns>A populated <see cref="DnsZone"/> instance.</returns>
    public static DnsZone Create(string distinguishedName, bool isSigned = false, bool signWithNSEC3 = false, byte[]? nsec3CurrentSalt = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(distinguishedName);

        // The leading DC= RDN of the dnsZone object carries the FQDN of the zone.
        var parsedDN = new DistinguishedName(distinguishedName);

        return new DnsZone
        {
            DistinguishedName = distinguishedName,
            ZoneName = parsedDN.Components.Count > 0 ? parsedDN.Components[0].Value : null,
            IsSigned = isSigned,
            SignWithNSEC3 = signWithNSEC3,
            NSEC3CurrentSalt = nsec3CurrentSalt
        };
    }
}
