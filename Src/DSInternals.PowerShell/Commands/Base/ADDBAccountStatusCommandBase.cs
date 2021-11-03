using DSInternals.Common.Data;
using DSInternals.PowerShell.Properties;
using System;
using System.Management.Automation;

namespace DSInternals.PowerShell.Commands
{
    /// <summary>
    /// Abstract cmdlet for enabling and disabling accounts in AD database.
    /// Implemented in the Enable-ADDBAccount and Disable-ADDBAccount cmdlets.
    /// </summary>
    public abstract class ADDBAccountStatusCommandBase : ADDBModifyPrincipalCommandBase
    {
        protected abstract bool Enabled
        {
            get;
        }

        protected override void ProcessRecord()
        {
            // TODO: Extract messages as resources
            string verboseMessage = this.Enabled ?
                "Enabling account {0}." :
                "Disabling account {0}.";
            bool hasChanged;
            switch (this.ParameterSetName)
            {
                case ParameterSetByDN:
                    this.WriteVerbose(String.Format(verboseMessage, this.DistinguishedName));
                    var dn = new DistinguishedName(this.DistinguishedName);
                    hasChanged = this.DirectoryAgent.SetAccountStatus(dn, this.Enabled, this.SkipMetaUpdate);
                    break;

                case ParameterSetByName:
                    this.WriteVerbose(String.Format(verboseMessage, this.SamAccountName));
                    hasChanged = this.DirectoryAgent.SetAccountStatus(this.SamAccountName, this.Enabled, this.SkipMetaUpdate);
                    break;

                case ParameterSetByGuid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectGuid));
                    hasChanged = this.DirectoryAgent.SetAccountStatus(this.ObjectGuid, this.Enabled, this.SkipMetaUpdate);
                    break;

                case ParameterSetBySid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectSid));
                    hasChanged = this.DirectoryAgent.SetAccountStatus(this.ObjectSid, this.Enabled, this.SkipMetaUpdate);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }
            this.WriteVerboseResult(hasChanged);
        }
    }
}
