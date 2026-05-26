#nullable enable

using DSInternals.Common.Data;
using DSInternals.Common.DNS;
using DSInternals.Common.Schema;

namespace DSInternals.DataStore;

public partial class DirectoryAgent : IDisposable
{
    public IEnumerable<DnsResourceRecord> GetDnsRecords(bool skipRootHints = true, bool skipTombstoned = true, bool skipTrustAnchors = true, string? zoneName = null)
    {
        DNTag? dnsNodeCategory = this.context.Schema.FindObjectCategory(CommonDirectoryClasses.DnsNode);

        if (!dnsNodeCategory.HasValue)
        {
            // This must be some initial AD schema or ADAM schema, which does not support DNS records.
            yield break;
        }

        // TODO: Consider a containerized search when a zone name is specified, to avoid iterating over all DNS nodes in the partition.
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

            if (zoneName != null && !string.Equals(zone, zoneName, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            foreach (var binaryRecord in records)
            {
                var record = DnsResourceRecord.Create(zone, name, binaryRecord);

                if (record.Rank == ResourceRecordRank.RootHint && skipRootHints)
                {
                    // Skip root hints
                    continue;
                }

                if (record.Zone == DnsZone.TrustAnchorsZoneName && skipTrustAnchors)
                {
                    // Skip DNSSEC trust anchors
                    continue;
                }

                yield return record;
            }
        }
    }

    public IEnumerable<DnsZone> GetDnsZones(string? zoneName = null)
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

            if (zoneName != null)
            {
                if (!string.Equals(fqdn, zoneName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
            }
            else if (fqdn == DnsZone.RootHintsZoneName || fqdn == DnsZone.TrustAnchorsZoneName)
            {
                continue;
            }

            yield return DnsZone.Create(zone);
        }
    }

    /// <summary>
    /// Retrieves DNSSEC signing key descriptors (msDNS-SigningKeyDescriptors) from
    /// every AD-integrated DNS zone in the database. No private key material is read or decrypted.
    /// </summary>
    public IEnumerable<DnsSigningKeyDescriptor> GetDnsSigningKeyDescriptors(string? zoneName = null)
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

            if (zoneName != null)
            {
                if (!string.Equals(fqdn, zoneName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
            }
            else if (fqdn == DnsZone.RootHintsZoneName || fqdn == DnsZone.TrustAnchorsZoneName)
            {
                continue;
            }

            zone.ReadAttribute(CommonDirectoryAttributes.DnsSigningKeyDescriptors, out byte[][] descriptors);
            if (descriptors == null)
            {
                continue;
            }

            foreach (byte[] binaryDescriptor in descriptors)
            {
                yield return DnsSigningKeyDescriptor.Decode(fqdn, binaryDescriptor);
            }
        }
    }

    /// <summary>
    /// Retrieves DNSSEC signing keys (msDNS-SigningKeys) from every AD-integrated DNS zone in
    /// the database and attempts to decrypt each private key using KDS root keys read from the
    /// same database.
    /// </summary>
    public IEnumerable<DnsSigningKey> GetDnsSigningKeys(string? zoneName = null)
    {
        DNTag? dnsZoneCategory = this.context.Schema.FindObjectCategory(CommonDirectoryClasses.DnsZone);

        if (!dnsZoneCategory.HasValue)
        {
            // This must be some initial AD schema or ADAM schema, which does not support DNS records.
            yield break;
        }

        IKdsRootKeyResolver? rootKeyResolver = null;

        foreach (var zone in this.FindObjectsByCategory(dnsZoneCategory.Value, includeReadOnly: true))
        {
            zone.ReadAttribute(CommonDirectoryAttributes.DomainComponent, out string fqdn);

            if (zoneName != null)
            {
                if (!string.Equals(fqdn, zoneName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
            }
            else if (fqdn == DnsZone.RootHintsZoneName || fqdn == DnsZone.TrustAnchorsZoneName)
            {
                continue;
            }

            zone.ReadAttribute(CommonDirectoryAttributes.DnsSigningKeys, out byte[][] signingKeys);
            if (signingKeys == null)
            {
                continue;
            }

            // Lazily build the KDS root key resolver on first signed zone.
            rootKeyResolver ??= new KdsRootKeyCache(new DatastoreRootKeyResolver(this.context));

            foreach (byte[] binaryKey in signingKeys)
            {
                var signingKey = DnsSigningKey.Decode(fqdn, binaryKey);
                signingKey.TryDecrypt(rootKeyResolver);
                yield return signingKey;
            }
        }
    }
}
