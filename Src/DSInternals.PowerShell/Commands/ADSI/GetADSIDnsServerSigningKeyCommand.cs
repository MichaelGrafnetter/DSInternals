using System.Management.Automation;
using DSInternals.Common.DNS;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "ADSIDnsServerSigningKey")]
[Alias("Get-ADSIDnsSigningKey")]
[OutputType(typeof(DnsSigningKeyDescriptor))]
public class GetADSIDnsServerSigningKeyCommand : ADSIDnsCommandBase
{
    protected override void ProcessRecord()
    {
        foreach (DnsSigningKeyDescriptor descriptor in this.Client.GetDnsSigningKeyDescriptors(this.ZoneName))
        {
            this.WriteObject(descriptor);
        }
    }
}
