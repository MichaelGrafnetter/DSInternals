namespace DSInternals.PowerShell.Commands
{
    using System.Management.Automation;
    using DSInternals.Common.Data;

    [Cmdlet(VerbsCommon.Get, "ADReplBackupKey")]
    [OutputType(typeof(DPAPIBackupKey))]
    public class GetADReplBackupKeyCommand : ADReplCommandBase
    {
        [Parameter(Mandatory = false)]
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

            string domainDN;
            if(string.IsNullOrEmpty(this.Domain))
            {
                // Automatically infer DC's domain name.
                domainDN = this.ReplicationClient.DomainNamingContext;
            }
            else
            {
                if (!this.Domain.Contains("."))
                {
                    // This is not a hard check, because root domain does not need to have a dot in it.
                    this.WriteWarning("The domain name supplied appears to be a NetBIOS name instead of DNS name.");
                }

                // Convert Domain DNS name to distinguished name
                domainDN = DistinguishedName.GetDNFromDNSName(this.Domain).ToString();
            }

            // TODO: Error processing
            foreach (var backupKey in this.ReplicationClient.GetDPAPIBackupKeys(domainDN))
            {
                this.WriteObject(backupKey);
            }
        }
    }
}
