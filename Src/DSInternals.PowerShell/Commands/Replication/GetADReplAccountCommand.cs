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
        #region Constants
        protected const string ParameterSetAll = "All";
        protected DSAccount.AccountType accountTypes = DSAccount.AccountType.Default;
        protected DSAccount.CredType credTypes = DSAccount.CredType.All;
        protected ulong counter = 0;
        #endregion Constants

        #region Parameters
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
        public string[] AccountTypes
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        public string[] CredTypes
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        public ulong Count
        {
            get;
            set;
        }
        #endregion Parameters

        #region Cmdlet Overrides
        protected override void ProcessRecord()
        {
            if (AccountTypes != null && AccountTypes.Length > 0)
            {
                accountTypes = DSAccount.GetAccountType(AccountTypes);
            }

            if (CredTypes != null && CredTypes.Length > 0)
            {
                credTypes = DSAccount.GetCredType(CredTypes);
            }

            if (Count > 0)
            {
                counter = Count;
            }

            if (this.ParameterSetName == ParameterSetAll)
            {
                this.ReturnAllAccounts();
            }
            else
            {
                this.ReturnSingleAccount();
            }
        }
        #endregion Cmdlet Overrides

        #region Helper Methods

        protected void ReturnAllAccounts()
        {
            // Write the initial progress
            // TODO: Extract strings as resources
            var progress = new ProgressRecord(1, "Replication", "Replicating Active Directory objects.");
            ulong accountCount = 0;

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
            foreach (var account in this.ReplicationClient.GetAccounts(domainNamingContext, progressReporter, Extra.IsPresent, accountTypes, credTypes))
            {
                if (account == null)
                    continue;

                this.WriteObject(account);

                if (counter > 0 && ++accountCount >= counter)
                    break;
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
                    account = this.ReplicationClient.GetAccount(this.DistinguishedName, Extra.IsPresent, credTypes);
                    break;

                case ParameterSetByName:
                    var accountName = new NTAccount(this.Domain, this.SamAccountName);
                    account = this.ReplicationClient.GetAccount(accountName, Extra.IsPresent, credTypes);
                    break;

                case ParameterSetByGuid:
                    account = this.ReplicationClient.GetAccount(this.ObjectGuid, Extra.IsPresent, credTypes);
                    break;

                case ParameterSetBySid:
                    account = this.ReplicationClient.GetAccount(this.ObjectSid, Extra.IsPresent, credTypes);
                    break;

                case ParameterSetByUPN:
                    var upn = new NTAccount(this.UserPrincipalName);
                    account = this.ReplicationClient.GetAccount(upn, Extra.IsPresent, credTypes);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }

            this.WriteObject(account);
        }
        #endregion Helper Methods
    }
}
