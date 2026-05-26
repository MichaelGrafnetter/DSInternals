using System.Management.Automation;
using DSInternals.Common.DNS;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "ADSIDnsServerZone")]
[OutputType(typeof(DnsZone))]
public class GetADSIDnsServerZoneCommand : ADSIDnsCommandBase
{
    protected override void ProcessRecord()
    {
        foreach (DnsZone dnsZone in this.Client.GetDnsZones(this.ZoneName))
        {
            this.WriteObject(dnsZone);
        }
    }
}
