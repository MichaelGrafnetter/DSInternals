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
    // TODO: output type
    // TODO: Accept *
    [OutputType(typeof(DSAccount))]
    public class GetADDBAccountCommand : ADDBPrincipalCommandBase
    {
        protected const string parameterSetAll = "All";

        [Parameter(
            Mandatory = true,
            HelpMessage = "TODO",
            ParameterSetName = parameterSetAll
        )]
        [Alias("AllAccounts", "ReturnAllAccounts")]
        public SwitchParameter All
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = false,
            HelpMessage = "TODO"
        )]
        [ValidateNotNull]
        [ValidateCount(BootKeyRetriever.BootKeyLength, BootKeyRetriever.BootKeyLength)]
        [AcceptHexString]
        [Alias("key", "SysKey")]
        public byte[] BootKey
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            // TODO: Exception handling: Object not found, malformed DN, ...
            // TODO: Map DSAccount to transfer object
            if(this.ParameterSetName == parameterSetAll)
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
            foreach (var account in this.DirectoryAgent.GetAccounts(bootKey))
            {
                this.WriteObject(account);
            }
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