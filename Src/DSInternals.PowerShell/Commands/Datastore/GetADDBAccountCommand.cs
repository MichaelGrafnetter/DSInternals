using DSInternals.Common;
using DSInternals.Common.Cryptography;
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
        private const int ProgressReportingInterval = 25;
        protected const string ParameterSetAll = "All";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetAll)]
        [Alias("AllAccounts", "ReturnAllAccounts")]
        public SwitchParameter All
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

        protected override void ProcessRecord()
        {
            // TODO: Exception handling: Object not found, malformed DN, ...
            // TODO: Map DSAccount to transfer object
            if(this.ParameterSetName == ParameterSetAll)
            {
                this.ReturnAllAccounts(this.BootKey);
            }
            else
            {
                this.ReturnSingleAccount(this.BootKey);
            }
        }

        private void ReturnAllAccounts(byte[] bootKey)
        {
            // This operation might take some time so we report its status.
            var progress = new ProgressRecord(4, "Reading accounts from AD database", "Starting...");
            int accountCount = 0;

            // Disable the progress bar as we do not know the total number of accounts.
            progress.PercentComplete = -1;
            this.WriteProgress(progress);

            foreach (var account in this.DirectoryAgent.GetAccounts(bootKey))
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

        private void ReturnSingleAccount(byte[] bootKey)
        {
            DSAccount account;
            switch (this.ParameterSetName)
            {
                case parameterSetByDN:
                    var dn = new DistinguishedName(this.DistinguishedName);
                    account = this.DirectoryAgent.GetAccount(dn, bootKey);
                    break;

                case parameterSetByName:
                    account = this.DirectoryAgent.GetAccount(this.SamAccountName, bootKey);
                    break;

                case parameterSetByGuid:
                    account = this.DirectoryAgent.GetAccount(this.ObjectGuid, bootKey);
                    break;

                case parameterSetBySid:
                    account = this.DirectoryAgent.GetAccount(this.ObjectSid, bootKey);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }
            this.WriteObject(account);
        }
    }
}