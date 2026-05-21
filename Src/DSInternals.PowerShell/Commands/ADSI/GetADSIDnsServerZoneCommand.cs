using System.Management.Automation;
using DSInternals.Common.DNS;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "ADSIDnsServerZone")]
[OutputType(typeof(DnsZone))]
public class GetADSIDnsServerZoneCommand : ADSICommandBase
{
    protected override void ProcessRecord()
    {
        foreach (DnsZone dnsZone in this.Client.GetDnsZones())
        {
            this.WriteObject(dnsZone);
        }
    }
}
