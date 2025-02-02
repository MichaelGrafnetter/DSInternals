using System;
using System.Management.Automation;
using DSInternals.Common.Data;
using DSInternals.DataStore;
using DSInternals.PowerShell.Properties;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "ADDBAccount")]
    [OutputType(typeof(DSAccount), typeof(DSUser), typeof(DSComputer))]
    public class GetADDBAccountCommand : ADDBPrincipalCommandBase
    {
        #region Constants
        private uint ProgressReportingInterval = 200;
        protected const string ParameterSetAll = "All";
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
        [Alias("Property", "PropertySets", "PropertySet")]
        [PSDefaultValue(Value = AccountPropertySets.Default)]
        public AccountPropertySets? Properties
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
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if(this.Properties == null)
            {
                this.Properties = AccountPropertySets.Default;
            }
        }

        protected override void ProcessRecord()
        {
            // TODO: Exception handling: Object not found, malformed DN, ...
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

            // Disable the progress bar as we do not know the total number of accounts.
            progress.PercentComplete = -1;
            this.WriteProgress(progress);

            foreach (var account in this.DirectoryAgent.GetAccounts(this.BootKey, this.Properties.Value))
            {
                this.WriteObject(account);
                accountCount++;

                // Update progress
                if(accountCount % ProgressReportingInterval == 1)
                {
                    // We do not want to change the progress too often, for performance reasons.
                    progress.StatusDescription = String.Format("{0}+ accounts", accountCount);
                    this.WriteProgress(progress);
                }
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
                case ParameterSetByDN:
                    var dn = new DistinguishedName(this.DistinguishedName);
                    account = this.DirectoryAgent.GetAccount(dn, this.BootKey, this.Properties.Value);
                    break;

                case ParameterSetByName:
                    account = this.DirectoryAgent.GetAccount(this.SamAccountName, this.BootKey, this.Properties.Value);
                    break;

                case ParameterSetByGuid:
                    account = this.DirectoryAgent.GetAccount(this.ObjectGuid, this.BootKey, this.Properties.Value);
                    break;

                case ParameterSetBySid:
                    account = this.DirectoryAgent.GetAccount(this.ObjectSid, this.BootKey, this.Properties.Value);
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
