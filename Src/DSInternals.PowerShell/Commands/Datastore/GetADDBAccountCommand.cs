using DSInternals.Common.Data;
using DSInternals.DataStore;
using DSInternals.PowerShell.Properties;
using System;
using System.Management.Automation;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "ADDBAccount")]
    [OutputType(typeof(DSAccount))]
    public class GetADDBAccountCommand : ADDBPrincipalCommandBase
    {
        #region Constants
        private int ProgressReportingInterval = 25;
        protected const string ParameterSetAll = "All";
        protected DSAccount.AccountType accountTypes = DSAccount.AccountType.All;
        protected DSAccount.CredType credTypes = DSAccount.CredType.All;
        protected ulong counter = 0;
        protected byte[] bootKey = null;
        #endregion Constants

        #region Parameters
        [Parameter(Mandatory = true, ParameterSetName = ParameterSetAll)]
        [Alias("AllAccounts", "ReturnAllAccounts")]
        public SwitchParameter All
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

        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        [ValidateCount(BootKeyRetriever.BootKeyLength, BootKeyRetriever.BootKeyLength)]
        [AcceptHexString]
        [Alias("key", "SysKey", "SystemKey")]
        public byte[] BootKey
        {
            get;
            set;
        }
        #endregion Parameters

        #region Cmdlet Overrides
        protected override void ProcessRecord()
        {
            // TODO: Exception handling: Object not found, malformed DN, ...
            // TODO: Map DSAccount to transfer object
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

            if (BootKey != null && BootKey.Length > 0)
            {
                bootKey = BootKey;
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
        private void ReturnAllAccounts()
        {
            // This operation might take some time so we report its status.
            var progress = new ProgressRecord(4, "Reading accounts from AD database", "Starting...");
            ulong accountCount = 0;

            if (counter < (ulong)ProgressReportingInterval)
            {
                ProgressReportingInterval = 2;
            }

            // Disable the progress bar as we do not know the total number of accounts.
            progress.PercentComplete = -1;
            this.WriteProgress(progress);

            foreach (var account in this.DirectoryAgent.GetAccounts(bootKey, Extra.IsPresent, accountTypes, credTypes))
            {
                if (account == null)
                    continue;

                this.WriteObject(account);
                accountCount++;

                // Update progress
                if(accountCount % (ulong)ProgressReportingInterval == 1)
                {
                    // We do not want to change the progress too often, for performance reasons.
                    progress.StatusDescription = String.Format("{0}+ accounts", accountCount);
                    this.WriteProgress(progress);
                }

                if (counter > 0 && accountCount >= counter)
                    break;
            }

            // Finished
            progress.RecordType = ProgressRecordType.Completed;
            this.WriteProgress(progress);
        }

        private void ReturnSingleAccount()
        {
            DSAccount account;
            switch (this.ParameterSetName)
            {
                case parameterSetByDN:
                    var dn = new DistinguishedName(this.DistinguishedName);
                    account = this.DirectoryAgent.GetAccount(dn, bootKey, Extra.IsPresent, credTypes);
                    break;

                case parameterSetByName:
                    account = this.DirectoryAgent.GetAccount(this.SamAccountName, bootKey, Extra.IsPresent, credTypes);
                    break;

                case parameterSetByGuid:
                    account = this.DirectoryAgent.GetAccount(this.ObjectGuid, bootKey, Extra.IsPresent, credTypes);
                    break;

                case parameterSetBySid:
                    account = this.DirectoryAgent.GetAccount(this.ObjectSid, bootKey, Extra.IsPresent, credTypes);
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
