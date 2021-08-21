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

        public SwitchParameter ComputerAccountsOnly
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
        public SwitchParameter CredsNTLMOnly
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

            if (CredsOnly.IsPresent && CredsNTLMOnly.IsPresent)
            {
                throw new Exception("Using -CredsOnly with -CredsNTLMOnly is not allowed.");
            }

            if (UserAccountsOnly.IsPresent && ComputerAccountsOnly.IsPresent)
            {
                throw new Exception("Using -UserAccountsOnly with -ComputerAccountsOnly is not allowed.");
            }

            if (this.ParameterSetName != ParameterSetAll && (UserAccountsOnly.IsPresent || ComputerAccountsOnly.IsPresent))
            {
                throw new Exception("Using -UserAccountsOnly or -ComputerAccountsOnly is not allowed without -All option set.");
            }

            if (this.ParameterSetName == ParameterSetAll)
            {
                this.ReturnAllAccounts(this.BootKey, Extra.IsPresent, UserAccountsOnly.IsPresent, ComputerAccountsOnly.IsPresent, CredsOnly.IsPresent, CredsNTLMOnly.IsPresent);
            }
            else
            {
                this.ReturnSingleAccount(this.BootKey, Extra.IsPresent, CredsOnly.IsPresent, CredsNTLMOnly.IsPresent);
            }
        }

        private void ReturnAllAccounts(byte[] bootKey, bool extra, bool onlyUser, bool onlyComputer, bool onlyCreds, bool onlyNTLMCreds)
        {
            // This operation might take some time so we report its status.
            var progress = new ProgressRecord(4, "Reading accounts from AD database", "Starting...");
            int accountCount = 0;

            // Disable the progress bar as we do not know the total number of accounts.
            progress.PercentComplete = -1;
            this.WriteProgress(progress);

            foreach (var account in this.DirectoryAgent.GetAccounts(bootKey, extra, onlyUser, onlyComputer, onlyCreds, onlyNTLMCreds))
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

        private void ReturnSingleAccount(byte[] bootKey, bool extra, bool onlyCreds, bool onlyNTLMCreds)
        {
            DSAccount account;
            switch (this.ParameterSetName)
            {
                case parameterSetByDN:
                    var dn = new DistinguishedName(this.DistinguishedName);
                    account = this.DirectoryAgent.GetAccount(dn, bootKey, extra, onlyCreds, onlyNTLMCreds);
                    break;

                case parameterSetByName:
                    account = this.DirectoryAgent.GetAccount(this.SamAccountName, bootKey, extra, onlyCreds, onlyNTLMCreds);
                    break;

                case parameterSetByGuid:
                    account = this.DirectoryAgent.GetAccount(this.ObjectGuid, bootKey, extra, onlyCreds, onlyNTLMCreds);
                    break;

                case parameterSetBySid:
                    account = this.DirectoryAgent.GetAccount(this.ObjectSid, bootKey, extra, onlyCreds, onlyNTLMCreds);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }
            this.WriteObject(account);
        }
    }
}
