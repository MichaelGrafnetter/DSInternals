using System.Management.Automation;
using DSInternals.DataStore;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "ADDBDnsResourceRecord")]
    [OutputType(typeof(DSInternals.Common.Data.DnsResourceRecord))]
    public class GetADDBDnsResourceRecordCommand : ADDBCommandBase
    {
        [Parameter(Mandatory = false)]
        [Alias("Tombstones", "IncludeTombstoned")]
        public SwitchParameter IncludeTombstones
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        [Alias("RootHints", "RootServers")]
        public SwitchParameter IncludeRootHints
        {
            get;
            set;
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            using(var directoryAgent = new DirectoryAgent(this.DirectoryContext))
            {
                // Invert the selection logic
                bool skipRootHints = !this.IncludeRootHints.IsPresent;
                bool skipTombstones = !this.IncludeTombstones.IsPresent;

                foreach (var dnsRecord in directoryAgent.GetDnsRecords(skipRootHints, skipTombstones))
                {
                    this.WriteObject(dnsRecord);
                }
            }
            // TODO: Exception handling
        }
    }
}
