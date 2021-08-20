using System;
using System.Management.Automation;
using System.Security.Principal;
using DSInternals.Common.Data;
using DSInternals.PowerShell.Properties;
using DSInternals.Replication;
using DSInternals.Replication.Model;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "ADReplAccount")]
    [OutputType(typeof(DSAccount))]
    public class GetADReplAccountCommand : ADReplPrincipalCommandBase
    {
        protected const string ParameterSetAll = "All";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetAll)]
        [Alias("AllAccounts", "ReturnAllAccounts")]
        public SwitchParameter All
        {
            get;
            set;
        }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetAll)]
        [ValidateNotNullOrEmpty]
        [Alias("NC", "DomainNC", "DomainNamingContext")]
        public string NamingContext
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        public SwitchParameter Extra
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        public SwitchParameter UserAccountsOnly
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        public SwitchParameter CredsOnly
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        public SwitchParameter CredsNTOnly
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        public SwitchParameter CredsLMOnly
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            int checkCredsParams = (CredsOnly.IsPresent ? 1 : 0) + (CredsNTOnly.IsPresent ? 1 : 0) + (CredsLMOnly.IsPresent ? 1 : 0);
            if (checkCredsParams > 1)
            {
                throw new Exception("Only one Creds* filter is allowed");
            }

            if (this.ParameterSetName != ParameterSetAll && UserAccountsOnly.IsPresent)
            {
                throw new Exception("Using -UserAccountsOnly is not allowed without -All option set.");
            }

            if (this.ParameterSetName == ParameterSetAll)
            {
                this.ReturnAllAccounts(Extra.IsPresent, UserAccountsOnly.IsPresent, CredsOnly.IsPresent, CredsNTOnly.IsPresent, CredsLMOnly.IsPresent);
            }
            else
            {
                this.ReturnSingleAccount(Extra.IsPresent, CredsOnly.IsPresent, CredsNTOnly.IsPresent, CredsLMOnly.IsPresent);
            }
        }

        protected void ReturnAllAccounts(bool extra, bool onlyUser, bool onlyCreds, bool onlyNTCreds, bool onlyLMCreds)
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

            // Automatically infer domain name if no value is provided
            string domainNamingContext = this.NamingContext ?? this.ReplicationClient.DomainNamingContext;

            // Replicate all accounts
            foreach (var account in this.ReplicationClient.GetAccounts(domainNamingContext, progressReporter, extra, onlyUser, onlyCreds, onlyNTCreds, onlyLMCreds))
            {
                this.WriteObject(account);
            }

            // Write progress completed
            progress.RecordType = ProgressRecordType.Completed;
            this.WriteProgress(progress);
        }

        protected void ReturnSingleAccount(bool extra, bool onlyCreds, bool onlyNTCreds, bool onlyLMCreds)
        {
            DSAccount account;
            switch (this.ParameterSetName)
            {
                case ParameterSetByDN:
                    account = this.ReplicationClient.GetAccount(this.DistinguishedName, extra, onlyCreds, onlyNTCreds, onlyLMCreds);
                    break;

                case ParameterSetByName:
                    var accountName = new NTAccount(this.Domain, this.SamAccountName);
                    account = this.ReplicationClient.GetAccount(accountName, extra, onlyCreds, onlyNTCreds, onlyLMCreds);
                    break;

                case ParameterSetByGuid:
                    account = this.ReplicationClient.GetAccount(this.ObjectGuid, extra, onlyCreds, onlyNTCreds, onlyLMCreds);
                    break;

                case ParameterSetBySid:
                    account = this.ReplicationClient.GetAccount(this.ObjectSid, extra, onlyCreds, onlyNTCreds, onlyLMCreds);
                    break;

                case ParameterSetByUPN:
                    var upn = new NTAccount(this.UserPrincipalName);
                    account = this.ReplicationClient.GetAccount(upn, extra, onlyCreds, onlyNTCreds, onlyLMCreds);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }

            this.WriteObject(account);
        }
    }
}
