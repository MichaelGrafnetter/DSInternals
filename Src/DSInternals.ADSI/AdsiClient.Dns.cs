using System.DirectoryServices;
using DSInternals.Common.Data;
using DSInternals.Common.DNS;
using DSInternals.Common.Schema;

namespace DSInternals.ADSI;

public sealed partial class AdsiClient
{
    private const string DnsZoneFilter = "(objectCategory=dnsZone)";
    private const string DnsNodeFilter = "(objectCategory=dnsNode)";
    private const string RootHintsZoneName = "RootDNSServers";
    private const string TrustAnchorsZoneName = "..TrustAnchors";

    private static readonly string[] DnsZoneSearchProperties = [
        CommonDirectoryAttributes.DistinguishedName,
        CommonDirectoryAttributes.DomainComponent,
        CommonDirectoryAttributes.DnsIsSigned
    ];
    private static readonly string[] DnsNodeSearchProperties = [
        CommonDirectoryAttributes.DistinguishedName,
        CommonDirectoryAttributes.DnsRecord,
        CommonDirectoryAttributes.DnsTombstoned
    ];

    /// <summary>
    /// Retrieves the DNS zones stored in Active Directory.
    /// </summary>
    /// <remarks>Both legacy DNS zones (stored under the domain naming context) and modern application
    /// partitions (such as DomainDnsZones and ForestDnsZones) are searched. Pseudo-zones used for root hints and
    /// DNSSEC trust anchors are excluded from the result.</remarks>
    /// <returns>An enumerable collection of <see cref="DnsZone"/> objects.</returns>
    public IEnumerable<DnsZone> GetDnsZones()
    {
        // Search the domain NC (legacy DNS) and every application partition (modern DNS zones such as
        // DomainDnsZones and ForestDnsZones).
        List<DirectoryEntry> applicationPartitions = _applicationPartitions.Value;
        var partitions = new List<DirectoryEntry>(applicationPartitions.Count + 1) { _domainNamingContext };
        partitions.AddRange(applicationPartitions);

        foreach (DirectoryEntry partition in partitions)
        {
            using var searcher = new DirectorySearcher(partition, DnsZoneFilter, DnsZoneSearchProperties, SearchScope.Subtree)
            {
                CacheResults = false
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

            using (results)
            {
                foreach (SearchResult result in results)
                {
                    if (result.Properties[CommonDirectoryAttributes.DomainComponent].Count == 0)
                    {
                        continue;
                    }

                    string? fqdn = result.Properties[CommonDirectoryAttributes.DomainComponent][0] as string;

                    if (fqdn is null || fqdn == RootHintsZoneName || fqdn == TrustAnchorsZoneName)
                    {
                        continue;
                    }

                    var adsiObject = new AdsiObjectAdapter(result);
                    yield return DnsZone.Create(adsiObject);
                }
            }
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
    /// <returns>An enumerable collection of <see cref="DnsResourceRecord"/> objects.</returns>
    public IEnumerable<DnsResourceRecord> GetDnsRecords(bool skipRootHints = true, bool skipTombstoned = true, bool skipTrustAnchors = true)
    {
        // Search the domain NC (legacy DNS) and every application partition (modern DNS zones such as
        // DomainDnsZones and ForestDnsZones).
        List<DirectoryEntry> applicationPartitions = _applicationPartitions.Value;
        var partitions = new List<DirectoryEntry>(applicationPartitions.Count + 1) { _domainNamingContext };
        partitions.AddRange(applicationPartitions);

        foreach (DirectoryEntry partition in partitions)
        {
            using var searcher = new DirectorySearcher(partition, DnsNodeFilter, DnsNodeSearchProperties, SearchScope.Subtree)
            {
                CacheResults = false
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

            using (results)
            {
                foreach (SearchResult result in results)
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

                    foreach (var binaryRecord in result.Properties[CommonDirectoryAttributes.DnsRecord].Cast<byte[]>())
                    {
                        var record = DnsResourceRecord.Create(zone, name, binaryRecord);

                        if (skipRootHints && record.Rank == ResourceRecordRank.RootHint)
                        {
                            continue;
                        }

                        if (skipTrustAnchors && record.Zone == TrustAnchorsZoneName)
                        {
                            continue;
                        }

                        yield return record;
                    }
                }
            }
        }
    }
}
