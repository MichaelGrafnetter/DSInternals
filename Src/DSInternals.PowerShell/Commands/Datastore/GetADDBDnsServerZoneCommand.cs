using System.Management.Automation;
using DSInternals.Common.DNS;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "ADDBDnsServerZone")]
[Alias("Get-ADDBDnsZone")]
[OutputType(typeof(DnsZone))]
public class GetADDBDnsServerZoneCommand : ADDBDnsCommandBase
{
    protected override void ProcessRecord()
    {
        foreach (DnsZone dnsZone in this.DirectoryAgent.GetDnsZones(this.ZoneName))
        {
            this.WriteObject(dnsZone);
        }
    }
}
