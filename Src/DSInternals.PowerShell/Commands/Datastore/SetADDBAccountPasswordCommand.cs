﻿using System.Management.Automation;
using System.Security;
using DSInternals.Common.Data;
using DSInternals.DataStore;

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
                case parameterSetByDN:
                    this.WriteVerbose(String.Format(verboseMessage, this.DistinguishedName));
                    var dn = new DistinguishedName(this.DistinguishedName);
                    hasChanged = this.DirectoryAgent.SetAccountPassword(dn, this.NewPassword, this.BootKey, this.SkipMetaUpdate);
                    break;

                case parameterSetByName:
                    this.WriteVerbose(String.Format(verboseMessage, this.SamAccountName));
                    hasChanged = this.DirectoryAgent.SetAccountPassword(this.SamAccountName, this.NewPassword, this.BootKey, this.SkipMetaUpdate);
                    break;

                case parameterSetByGuid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectGuid));
                    hasChanged = this.DirectoryAgent.SetAccountPassword(this.ObjectGuid, this.NewPassword, this.BootKey, this.SkipMetaUpdate);
                    break;

                case parameterSetBySid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectSid));
                    hasChanged = this.DirectoryAgent.SetAccountPassword(this.ObjectSid, this.NewPassword, this.BootKey, this.SkipMetaUpdate);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(InvalidParameterSetMessage);
            }
            this.WriteVerboseResult(hasChanged);
        }
    }
}
