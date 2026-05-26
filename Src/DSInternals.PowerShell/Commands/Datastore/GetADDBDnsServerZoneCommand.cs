using System.Management.Automation;
using DSInternals.Common.DNS;
using DSInternals.DataStore;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "ADDBDnsServerZone")]
[Alias("Get-ADDBDnsZone")]
[OutputType(typeof(DnsZone))]
public class GetADDBDnsServerZoneCommand : ADDBCommandBase
{
    protected override void BeginProcessing()
    {
        base.BeginProcessing();

        using (var directoryAgent = new DirectoryAgent(this.DirectoryContext))
        {
            foreach (DnsZone dnsZone in directoryAgent.GetDnsZones())
            {
                this.WriteObject(dnsZone);
            }
        }
        // TODO: Exception handling
    }
}
