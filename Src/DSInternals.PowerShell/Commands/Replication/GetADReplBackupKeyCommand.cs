namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common.Data;
    using DSInternals.PowerShell.Properties;
    using System;
    using System.Management.Automation;
    using System.Security.Principal;

    [Cmdlet(VerbsCommon.Get, "ADReplBackupKey")]
    [OutputType(typeof(DPAPIBackupKey))]
    public class GetADReplBackupKeyCommand : ADReplCommandBase
    {
        [Parameter(
            Mandatory = true,
            HelpMessage = "TODO"
        )]
        [ValidateNotNullOrEmpty]
        [Alias("FQDN", "DomainName", "DNSDomainName")]
        public string Domain
        {
            get;
            set;
        }


        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if(!this.Domain.Contains("."))
            {
                // This is not a hard check, because root domain does not need to have a dot in it.
                // TODO: Extract as a resource
                this.WriteWarning("The domain name supplied appears to be a NetBIOS name instead of DNS name.");
            }

            // TODO: Error processing
            // Convert Domain DNS name to distinguished name
            var domainDN = DistinguishedName.GetDNFromDNSName(this.Domain);
            foreach (var backupKey in this.ReplicationClient.GetDPAPIBackupKeys(domainDN.ToString()))
            {
                this.WriteObject(backupKey);
            }
        }
    }
}
