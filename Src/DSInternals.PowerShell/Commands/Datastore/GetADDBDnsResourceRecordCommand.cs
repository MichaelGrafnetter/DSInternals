namespace DSInternals.PowerShell.Commands
{
    using System.Management.Automation;
    using DSInternals.DataStore;

    [Cmdlet(VerbsCommon.Get, "ADDBDnsResourceRecord")]
    [OutputType(typeof(DSInternals.Common.Data.DnsResourceRecord))]
    public class GetADDBDnsResourceRecordCommand : ADDBCommandBase
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            using(var directoryAgent = new DirectoryAgent(this.DirectoryContext))
            {
                foreach(var dnsRecord in directoryAgent.GetDnsRecords())
                {
                    this.WriteObject(dnsRecord);
                }
            }
            // TODO: Exception handling
        }
    }
}
