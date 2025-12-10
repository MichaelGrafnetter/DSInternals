using System;
using DSInternals.Common.Data;
using DSInternals.Common.Schema;

namespace DSInternals.DataStore
{
    public partial class DirectoryAgent : IDisposable
    {
        private const string RootHintsZoneName = "RootDNSServers";
        private const string TrustAnchorsZoneName = "..TrustAnchors";

        public IEnumerable<DnsResourceRecord> GetDnsRecords(bool skipRootHints = true, bool skipTombstoned = true, bool skipTrustAnchors = true)
        {
            DNTag? dnsNodeCategory = this.context.Schema.FindObjectCategory(CommonDirectoryClasses.DnsNode);

            if (!dnsNodeCategory.HasValue)
            {
                // This must be some initial AD schema or ADAM schema, which does not support DNS records.
                yield break;
            }

            foreach (var node in this.FindObjectsByCategory(dnsNodeCategory.Value, includeReadOnly: true))
            {
                if (skipTombstoned)
                {
                    node.ReadAttribute(CommonDirectoryAttributes.DnsTombstoned, out bool isTombstoned);
                    if (isTombstoned)
                    {
                        // Tombstone nodes do not contain any DNS records.
                        continue;
                    }
                }

                node.ReadAttribute(CommonDirectoryAttributes.DnsRecord, out byte[][] records);
                if (records == null)
                {
                    // Skip the node, as it does not contain any DNS records.
                    // The object might come from a partial replica of the domain partition.
                    continue;
                }

                string dn = node.DistinguishedName;
                var parsedDN = new DistinguishedName(dn);

                // Record host name is the first RDN in the node distinguished name.
                string name = parsedDN.Components[0].Value;

                // Parent DNS zone is the second RDN in the node distinguished name.
                string zone = parsedDN.Components[1].Value;

                foreach (var binaryRecord in records)
                {
                    var record = DnsResourceRecord.Create(zone, name, binaryRecord);

                    if (record.Rank == ResourceRecordRank.RootHint && skipRootHints)
                    {
                        // Skip root hints
                        continue;
                    }

                    if (record.Zone == TrustAnchorsZoneName && skipTrustAnchors)
                    {
                        // Skip DNSSEC trust anchors
                        continue;
                    }

                    yield return record;
                }
            }
        }

        public IEnumerable<string> GetDnsZone()
        {
            DNTag? dnsZoneCategory = this.context.Schema.FindObjectCategory(CommonDirectoryClasses.DnsZone);

            if (!dnsZoneCategory.HasValue)
            {
                // This must be some initial AD schema or ADAM schema, which does not support DNS records.
                yield break;
            }

            foreach (var zone in this.FindObjectsByCategory(dnsZoneCategory.Value, includeReadOnly: true))
            {
                zone.ReadAttribute(CommonDirectoryAttributes.DomainComponent, out string fqdn);

                if (fqdn != RootHintsZoneName && fqdn != TrustAnchorsZoneName)
                {
                    yield return fqdn;
                }
            }
        }

        public IEnumerable<DnsSigningKey> GetDnsSigningKeys()
        {
            DNTag? dnsZoneCategory = this.context.Schema.FindObjectCategory(CommonDirectoryClasses.DnsZone);

            if (!dnsZoneCategory.HasValue)
            {
                // This must be some initial AD schema or ADAM schema, which does not support DNS records.
                yield break;
            }

            foreach (var zone in this.FindObjectsByCategory(dnsZoneCategory.Value, includeReadOnly: true))
            {
                // Check if the current DNS zone has signing keys
                zone.ReadAttribute(CommonDirectoryAttributes.DnsSigningKeys, out byte[][]? signingKeys);

                if (signingKeys != null)
                {
                    zone.ReadAttribute(CommonDirectoryAttributes.DomainComponent, out string fqdn);
                    // TODO: Consider fetching the msDNS-IsSigned and msDNS-SigningKeyDescriptors attributes

                    foreach (var signingKey in signingKeys)
                    {
                        var dnsSigningKey = DnsSigningKey.Decode(fqdn, signingKey);

                        // TODO: Fetch KDS root keys and decrypt

                        yield return dnsSigningKey;
                    }
                }
            }
        }
    }
}
