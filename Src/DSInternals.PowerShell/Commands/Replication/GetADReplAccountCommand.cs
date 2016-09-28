namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common.Data;
    using DSInternals.PowerShell.Properties;
    using DSInternals.Replication;
    using DSInternals.Replication.Model;
    using System;
    using System.Linq;
    using System.Management.Automation;
    using System.Security.Principal;

    [Cmdlet(VerbsCommon.Get, "ADReplAccount")]
    [OutputType(typeof(DSAccount))]
    public class GetADReplAccountCommand : ADReplObjectCommandBase
    {
        protected const string parameterSetByName = "ByName";
        protected const string parameterSetBySid = "BySID";

        // Validate Mask domain\user
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "TODO",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = parameterSetByName
        )]
        [ValidateNotNullOrEmpty]
        [Alias("Login", "sam", "AccountName","User")]
        public string SamAccountName
        {
            get;
            set;
        }
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "TODO",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = parameterSetByName
        )]
        [ValidateNotNullOrEmpty]
        [Alias("AccountDomain", "UserDomain")]
        public string Domain
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
            HelpMessage = "TODO",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = parameterSetBySid
        )]
        [ValidateNotNullOrEmpty]
        [Alias("Sid")]
        public SecurityIdentifier ObjectSid
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            // TODO: Error processing
            if (this.ParameterSetName == ParameterSetAll)
            {
                this.ReturnAllAccounts();
            }
            else
            {
                this.ReturnSingleAccount();
            }
        }

        protected void ReturnAllAccounts()
        {
            // Write the initial progress
            // TODO: Extract strings as resources
            var progress = new ProgressRecord(1, "Replication", "Replicating Active Directory objects.");
            progress.PercentComplete = 0;
            this.WriteProgress(progress);

            // Update the progress after each replication cycle
            ReplicationProgressHandler progressReporter = (ReplicationCookie cookie, int processedObjectCount, int totalObjectCount) =>
            {
                int percentComplete = (int)(((double)processedObjectCount / (double)totalObjectCount) * 100);
                // AD's object count estimate is sometimes lower than the actual count, so we cap the value to 100%.
                progress.PercentComplete = Math.Min(percentComplete, 100);
                this.WriteProgress(progress);
            };
            
            // Replicate all accounts
            foreach (var account in this.ReplicationClient.GetAccounts(this.NamingContext, progressReporter))
            {
                this.WriteObject(account);
            }

            // Write progress completed
            progress.RecordType = ProgressRecordType.Completed;
            this.WriteProgress(progress);
        }

        protected void ReturnSingleAccount()
        {
            DSAccount account;
            switch (this.ParameterSetName)
            {
                case ParameterSetByDN:
                    account = this.ReplicationClient.GetAccount(this.DistinguishedName);
                    break;

                case parameterSetByName:
                    if(this.Domain.Contains("."))
                    {
                        // This is not a hard check, because dots are actually allowed in NetBIOS names, although not recommended.
                        // TODO: Extract as a resource
                        this.WriteWarning("The domain name supplied appears to be a DNS name instead of NetBIOS name.");
                    }
                    var accountName = new NTAccount(this.Domain, this.SamAccountName);
                    account = this.ReplicationClient.GetAccount(accountName);
                    break;

                case ParameterSetByGuid:
                    account = this.ReplicationClient.GetAccount(this.ObjectGuid);
                    break;

                case parameterSetBySid:
                    account = this.ReplicationClient.GetAccount(this.ObjectSid);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }
            this.WriteObject(account);
        }
    }
}
