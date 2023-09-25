using System;
using System.Management.Automation;
using DSInternals.Common.Data;
using DSInternals.PowerShell.Properties;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Unlock, "ADDBAccount")]
    [OutputType("None")]
    public class UnlockADDBAccountCommand : ADDBModifyPrincipalCommandBase
    {
        protected override void ProcessRecord()
        {
            // TODO: Extract message as resource
            string verboseMessage = "Unlocking account {0}.";
            bool hasChanged;

            switch (this.ParameterSetName)
            {
                case parameterSetByDN:
                    this.WriteVerbose(String.Format(verboseMessage, this.DistinguishedName));
                    var dn = new DistinguishedName(this.DistinguishedName);
                    hasChanged = this.DirectoryAgent.UnlockAccount(dn, this.SkipMetaUpdate);
                    break;

                case parameterSetByName:
                    this.WriteVerbose(String.Format(verboseMessage, this.SamAccountName));
                    hasChanged = this.DirectoryAgent.UnlockAccount(this.SamAccountName, this.SkipMetaUpdate);
                    break;

                case parameterSetByGuid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectGuid));
                    hasChanged = this.DirectoryAgent.UnlockAccount(this.ObjectGuid, this.SkipMetaUpdate);
                    break;

                case parameterSetBySid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectSid));
                    hasChanged = this.DirectoryAgent.UnlockAccount(this.ObjectSid, this.SkipMetaUpdate);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }

            this.WriteVerboseResult(hasChanged);
        }
    }
}
