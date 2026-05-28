using System.DirectoryServices;
using System.Runtime.InteropServices;
using DSInternals.Common.Data;
using DSInternals.Common.DNS;
using DSInternals.Common.Schema;

namespace DSInternals.ADSI;

public sealed partial class AdsiClient
{
    private const string DnsZoneFilter = "(objectCategory=dnsZone)";
    private const string DnsSignedZoneFilter = "(&(objectCategory=dnsZone)(msDNS-IsSigned=TRUE))";
    private const string DnsNodeFilter = "(objectCategory=dnsNode)";

    private static readonly string[] DnsZoneSearchProperties = [
        CommonDirectoryAttributes.DistinguishedName,
        CommonDirectoryAttributes.DomainComponent,
        CommonDirectoryAttributes.DnsIsSigned,
        CommonDirectoryAttributes.DnsSignWithNSEC3,
        CommonDirectoryAttributes.DnsNSEC3CurrentSalt
    ];
    private static readonly string[] DnsNodeSearchProperties = [
        CommonDirectoryAttributes.DistinguishedName,
        CommonDirectoryAttributes.DnsRecord,
        CommonDirectoryAttributes.DnsTombstoned
    ];
    private static readonly string[] DnsSigningKeyDescriptorsSearchProperties = [
        CommonDirectoryAttributes.DomainComponent,
        CommonDirectoryAttributes.DnsSigningKeyDescriptors
    ];
    private static readonly string[] DnsSigningKeysSearchProperties = [
        CommonDirectoryAttributes.DomainComponent,
        CommonDirectoryAttributes.DnsSigningKeys
    ];

    /// <summary>
    /// Retrieves the DNS zones stored in Active Directory.
    /// </summary>
    /// <remarks>Both legacy DNS zones (stored under the domain naming context) and modern application
    /// partitions (such as DomainDnsZones and ForestDnsZones) are searched. When <paramref name="zoneName"/> is
    /// <see langword="null"/>, pseudo-zones used for root hints and DNSSEC trust anchors are excluded from the
    /// result; when supplied, the explicit zone name takes precedence over the pseudo-zone exclusion.</remarks>
    /// <param name="zoneName">When non-<see langword="null"/>, restricts the result to the zone whose FQDN matches
    /// this value (case-insensitive, exact match performed by the LDAP server).</param>
    /// <returns>An enumerable collection of <see cref="DnsZone"/> objects.</returns>
    public IEnumerable<DnsZone> GetDnsZones(string? zoneName = null)
    {
        string filter = BuildDnsZoneFilter(signedOnly: false, zoneName);

        foreach (SearchResult result in SearchPartitions(filter, DnsZoneSearchProperties))
        {
            if (result.Properties[CommonDirectoryAttributes.DomainComponent].Count == 0)
            {
                continue;
            }

            string? fqdn = result.Properties[CommonDirectoryAttributes.DomainComponent][0] as string;

            if (fqdn is null)
            {
                continue;
            }

            // Skip the pseudo-zones only when the caller did not request a specific zone.
            if (zoneName is null && (fqdn == DnsZone.RootHintsZoneName || fqdn == DnsZone.TrustAnchorsZoneName))
            {
                continue;
            }

            var adsiObject = new AdsiObjectAdapter(result);
            yield return DnsZone.Create(adsiObject);
        }
    }

    /// <summary>
    /// Retrieves DNS resource records stored in Active Directory.
    /// </summary>
    /// <remarks>Both legacy DNS records (stored under the domain naming context) and modern application
    /// partitions (such as DomainDnsZones and ForestDnsZones) are searched.</remarks>
    /// <param name="skipRootHints">When true, records with the <see cref="ResourceRecordRank.RootHint"/> rank are omitted.</param>
    /// <param name="skipTombstoned">When true, tombstoned DNS nodes are omitted.</param>
    /// <param name="skipTrustAnchors">When true, records belonging to the DNSSEC trust anchors pseudo-zone are omitted.</param>
    /// <param name="zoneName">When non-<see langword="null"/>, restricts the result to records belonging to the
    /// zone whose FQDN matches this value (case-insensitive, exact match).</param>
    /// <returns>An enumerable collection of <see cref="DnsResourceRecord"/> objects.</returns>
    public IEnumerable<DnsResourceRecord> GetDnsRecords(bool skipRootHints = true, bool skipTombstoned = true, bool skipTrustAnchors = true, string? zoneName = null)
    {
        foreach (SearchResult result in SearchPartitions(DnsNodeFilter, DnsNodeSearchProperties))
        {
            if (skipTombstoned && result.Properties[CommonDirectoryAttributes.DnsTombstoned].Count > 0)
            {
                bool isTombstoned = (bool)result.Properties[CommonDirectoryAttributes.DnsTombstoned][0];
                if (isTombstoned)
                {
                    // Tombstoned nodes do not contain any DNS records.
                    continue;
                }
            }

            if (result.Properties[CommonDirectoryAttributes.DnsRecord].Count == 0)
            {
                // The node does not contain any DNS records.
                continue;
            }

            string? dn = result.Properties[CommonDirectoryAttributes.DistinguishedName][0] as string;
            var parsedDN = new DistinguishedName(dn);

            if (parsedDN.Components.Count < 2)
            {
                // Malformed DN, skip.
                continue;
            }

            // Record host name is the first RDN.
            string name = parsedDN.Components[0].Value;

            // Parent DNS zone is the second RDN.
            string zone = parsedDN.Components[1].Value;

            if (zoneName != null && !string.Equals(zone, zoneName, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            foreach (var binaryRecord in result.Properties[CommonDirectoryAttributes.DnsRecord].Cast<byte[]>())
            {
                var record = DnsResourceRecord.Create(zone, name, binaryRecord);

                if (skipRootHints && record.Rank == ResourceRecordRank.RootHint)
                {
                    continue;
                }

                if (skipTrustAnchors && record.Zone == DnsZone.TrustAnchorsZoneName)
                {
                    continue;
                }

                yield return record;
            }
        }
    }

    /// <summary>
    /// Retrieves DNSSEC signing key descriptors (msDNS-SigningKeyDescriptors) from every
    /// AD-integrated DNS zone whose <c>msDNS-IsSigned</c> attribute is <see langword="true"/>.
    /// No private key material is read or decrypted.
    /// </summary>
    /// <param name="zoneName">When non-<see langword="null"/>, restricts the result to descriptors belonging to
    /// the zone whose FQDN matches this value (case-insensitive, exact match performed by the LDAP server).</param>
    /// <returns>An enumerable collection of <see cref="DnsSigningKeyDescriptor"/> objects.</returns>
    public IEnumerable<DnsSigningKeyDescriptor> GetDnsSigningKeyDescriptors(string? zoneName = null)
    {
        string filter = BuildDnsZoneFilter(signedOnly: true, zoneName);

        foreach (SearchResult result in SearchPartitions(filter, DnsSigningKeyDescriptorsSearchProperties))
        {
            if (result.Properties[CommonDirectoryAttributes.DomainComponent].Count == 0)
            {
                continue;
            }

            string? fqdn = result.Properties[CommonDirectoryAttributes.DomainComponent][0] as string;
            if (string.IsNullOrEmpty(fqdn))
            {
                continue;
            }

            foreach (byte[] binaryDescriptor in result.Properties[CommonDirectoryAttributes.DnsSigningKeyDescriptors].Cast<byte[]>())
            {
                yield return DnsSigningKeyDescriptor.Decode(fqdn, binaryDescriptor);
            }
        }
    }

    /// <summary>
    /// Retrieves DNSSEC signing keys (msDNS-SigningKeys) from every AD-integrated DNS zone whose
    /// <c>msDNS-IsSigned</c> attribute is <see langword="true"/> and attempts to decrypt each
    /// private key using the client's KDS root key resolver.
    /// </summary>
    /// <param name="zoneName">When non-<see langword="null"/>, restricts the result to keys belonging to the
    /// zone whose FQDN matches this value (case-insensitive, exact match performed by the LDAP server).</param>
    /// <returns>An enumerable collection of <see cref="DnsSigningKey"/> objects.</returns>
    public IEnumerable<DnsSigningKey> GetDnsSigningKeys(string? zoneName = null)
    {
        string filter = BuildDnsZoneFilter(signedOnly: true, zoneName);

        foreach (SearchResult result in SearchPartitions(filter, DnsSigningKeysSearchProperties))
        {
            if (result.Properties[CommonDirectoryAttributes.DomainComponent].Count == 0)
            {
                continue;
            }

            string? fqdn = result.Properties[CommonDirectoryAttributes.DomainComponent][0] as string;
            if (string.IsNullOrEmpty(fqdn))
            {
                continue;
            }

            if (result.Properties[CommonDirectoryAttributes.DnsSigningKeys].Count == 0)
            {
                continue;
            }

            foreach (byte[] binaryKey in result.Properties[CommonDirectoryAttributes.DnsSigningKeys].Cast<byte[]>())
            {
                var signingKey = DnsSigningKey.Decode(fqdn, binaryKey);
                signingKey.TryDecrypt(_kdsRootKeyResolver);
                yield return signingKey;
            }
        }
    }

    /// <summary>
    /// Runs the supplied LDAP filter against the domain naming context (legacy DNS storage) and every
    /// application partition (modern DNS storage such as DomainDnsZones and ForestDnsZones), yielding
    /// each matching <see cref="SearchResult"/>. Partitions that cannot be searched (not replicated,
    /// unreachable DC, etc.) are silently skipped.
    /// </summary>
    private IEnumerable<SearchResult> SearchPartitions(string filter, string[] propertiesToLoad)
    {
        List<DirectoryEntry> applicationPartitions = _applicationPartitions.Value;
        var partitions = new List<DirectoryEntry>(applicationPartitions.Count + 1) { _domainNamingContext };
        partitions.AddRange(applicationPartitions);

        foreach (DirectoryEntry partition in partitions)
        {
            using var searcher = new DirectorySearcher(partition, filter, propertiesToLoad, SearchScope.Subtree)
            {
                CacheResults = false,
                PageSize = LdapPageSize
            };

            SearchResultCollection results;
            try
            {
                results = searcher.FindAll();
            }
            catch (DirectoryServicesCOMException)
            {
                // The partition cannot be searched (e.g., not replicated to the target server). Skip it.
                continue;
            }
            catch (COMException)
            {
                // Binding to the partition failed (e.g., its preferred DC is unreachable). Skip it.
                // TODO: Log the error for the affected partition.
                continue;
            }

            using (results)
            {
                foreach (SearchResult result in results)
                {
                    yield return result;
                }
            }
        }
    }

    /// <summary>
    /// Combines a base DNS zone filter with an optional zone-name assertion. The zone name is escaped per
    /// RFC 4515 before being embedded into the filter.
    /// </summary>
    private static string BuildDnsZoneFilter(bool signedOnly, string? zoneName)
    {
        string baseFilter = signedOnly ? DnsSignedZoneFilter : DnsZoneFilter;

        if (zoneName is null || zoneName.Length == 0)
        {
            return baseFilter;
        }

        string escaped = EscapeLdapFilterString(zoneName);
        return $"(&{baseFilter}(dc={escaped}))";
    }
}
