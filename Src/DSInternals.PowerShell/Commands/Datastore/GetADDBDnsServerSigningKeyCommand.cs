using System.Management.Automation;
using DSInternals.Common.DNS;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "ADDBDnsServerSigningKey")]
[Alias("Get-ADDBDnsSigningKey")]
[OutputType(typeof(DnsSigningKeyDescriptor))]
public class GetADDBDnsServerSigningKeyCommand : ADDBDnsCommandBase
{
    protected override void ProcessRecord()
    {
        foreach (DnsSigningKeyDescriptor descriptor in this.DirectoryAgent.GetDnsSigningKeyDescriptors(this.ZoneName))
        {
            this.WriteObject(descriptor);
        }
    }
}
