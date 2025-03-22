using System.Management.Automation;
using DSInternals.DataStore;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "ADDBDnsZone")]
    [OutputType(typeof(string))]
    public class GetADDBDnsZoneCommand : ADDBCommandBase
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            using(var directoryAgent = new DirectoryAgent(this.DirectoryContext))
            {
                foreach (string dnsZone in directoryAgent.GetDnsZone())
                {
                    this.WriteObject(dnsZone);
                }
            }
            // TODO: Exception handling
        }
    }
}
