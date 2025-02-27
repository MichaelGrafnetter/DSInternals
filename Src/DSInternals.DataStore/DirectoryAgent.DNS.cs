using System;
using DSInternals.Common.Data;
using System.Collections.Generic;

namespace DSInternals.DataStore
{
    public partial class DirectoryAgent : IDisposable
    {
        public IEnumerable<DnsResourceRecord> GetDnsRecords(bool skipRootHints = true, bool skipTombstoned = true)
        {
            foreach (var node in this.FindObjectsByCategory(CommonDirectoryClasses.DnsNode))
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

                node.ReadAttribute(CommonDirectoryAttributes.DNTag, out DistinguishedName dn);

                // Record host name is the first RDN in the node distinguished name.
                string name = dn.Components[0].Value;

                // Parent DNS zone is the second RDN in the node distinguished name.
                string zone = dn.Components[1].Value;

                foreach (var binaryRecord in records)
                {
                    var record = DnsResourceRecord.Create(zone, name, binaryRecord);

                    if (record.Rank == ResourceRecordRank.RootHint && skipRootHints)
                    {
                        // Skip root hints
                        continue;
                    }

                    yield return record;
                }
            }
        }
    }
}
