using DSInternals.Common.Data;
using DSInternals.DataStore;
using DSInternals.PowerShell.Properties;
using System;
using System.Management.Automation;
using System.Security;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "ADDBAccountPassword")]
    [OutputType("None")]
    public class SetADDBAccountPasswordCommand : ADDBModifyPrincipalCommandBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Password", "Pwd", "Pass", "AccountPassword", "p")]
        public SecureString NewPassword
        {
            get;
            set;
        }

        [Parameter(Mandatory = true)]
        [ValidateCount(BootKeyRetriever.BootKeyLength, BootKeyRetriever.BootKeyLength)]
        [AcceptHexString]
        [Alias("Key", "SysKey", "SystemKey")]
        public byte[] BootKey
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            //TODO: Exception handling: Object not found, malformed DN, ...
            // TODO: Extract as Resource
            string verboseMessage = "Setting password for account {0}.";
            bool hasChanged;
            switch (this.ParameterSetName)
            {
                case ParameterSetByDN:
                    this.WriteVerbose(String.Format(verboseMessage, this.DistinguishedName));
                    var dn = new DistinguishedName(this.DistinguishedName);
                    hasChanged = this.DirectoryAgent.SetAccountPassword(dn, this.NewPassword, this.BootKey, this.SkipMetaUpdate);
                    break;

                case ParameterSetByName:
                    this.WriteVerbose(String.Format(verboseMessage, this.SamAccountName));
                    hasChanged = this.DirectoryAgent.SetAccountPassword(this.SamAccountName, this.NewPassword, this.BootKey, this.SkipMetaUpdate);
                    break;

                case ParameterSetByGuid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectGuid));
                    hasChanged = this.DirectoryAgent.SetAccountPassword(this.ObjectGuid, this.NewPassword, this.BootKey, this.SkipMetaUpdate);
                    break;

                case ParameterSetBySid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectSid));
                    hasChanged = this.DirectoryAgent.SetAccountPassword(this.ObjectSid, this.NewPassword, this.BootKey, this.SkipMetaUpdate);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }
            this.WriteVerboseResult(hasChanged);
        }
    }
}