using System.Management.Automation;
using DSInternals.Common.DNS;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "ADSIDnsServerResourceRecord")]
[Alias("Get-ADSIDnsServerRecord")]
[OutputType(typeof(DnsResourceRecord))]
public class GetADSIDnsServerResourceRecordCommand : ADSICommandBase
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

    protected override void ProcessRecord()
    {
        bool skipRootHints = !this.IncludeRootHints.IsPresent;
        bool skipTombstones = !this.IncludeTombstones.IsPresent;

        foreach (var dnsRecord in this.Client.GetDnsRecords(skipRootHints, skipTombstones))
        {
            this.WriteObject(dnsRecord);
        }
    }
}
