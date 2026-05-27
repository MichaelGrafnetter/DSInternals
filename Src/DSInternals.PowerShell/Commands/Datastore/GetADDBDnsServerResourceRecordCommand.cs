using System.Management.Automation;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "ADDBDnsServerResourceRecord")]
[Alias("Get-ADDBDnsResourceRecord", "Get-ADDBDnsRecord")]
[OutputType(typeof(DSInternals.Common.DNS.DnsResourceRecord))]
public class GetADDBDnsServerResourceRecordCommand : ADDBDnsCommandBase
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

    [Parameter(Mandatory = false)]
    [Alias("TrustAnchors")]
    public SwitchParameter IncludeTrustAnchors
    {
        get;
        set;
    }

    protected override void ProcessRecord()
    {
        // Invert the selection logic
        bool skipRootHints = !this.IncludeRootHints.IsPresent;
        bool skipTombstones = !this.IncludeTombstones.IsPresent;
        bool skipTrustAnchors = !this.IncludeTrustAnchors.IsPresent;

        foreach (var dnsRecord in this.DirectoryAgent.GetDnsRecords(skipRootHints, skipTombstones, skipTrustAnchors, this.ZoneName))
        {
            this.WriteObject(dnsRecord);
        }
    }
}
