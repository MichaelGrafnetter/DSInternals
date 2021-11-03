namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using System.Security.Principal;
    using DSInternals.DataStore;
    using DSInternals.Common.Data;
    using DSInternals.PowerShell.Properties;

    [Cmdlet(VerbsCommon.Add, "ADDBSidHistory")]
    [OutputType("None")]
    public class AddADDBSidHistoryCommand : ADDBModifyPrincipalCommandBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("hist", "History")]
        [ValidateNotNull]
        public SecurityIdentifier[] SidHistory
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            //TODO: Exception handling: Object not found, malformed DN, ...
            bool hasChanged;
            switch (this.ParameterSetName)
            {
                case ADDBObjectCommandBase.ParameterSetByDN:
                    // TODO: Extract these messages as a resource.
                    this.WriteVerbose(String.Format("Adding SID history to principal {0}.", this.DistinguishedName));
                    var dn = new DistinguishedName(this.DistinguishedName);
                    hasChanged = this.DirectoryAgent.AddSidHistory(dn, this.SidHistory, this.SkipMetaUpdate);
                    break;

                case ADDBPrincipalCommandBase.ParameterSetByName:
                    this.WriteVerbose(String.Format("Adding SID history to principal {0}.", this.SamAccountName));
                    hasChanged = this.DirectoryAgent.AddSidHistory(this.SamAccountName, this.SidHistory, this.SkipMetaUpdate);
                    break;

                case ADDBObjectCommandBase.ParameterSetByGuid:
                    this.WriteVerbose(String.Format("Adding SID history to principal {0}.", this.ObjectGuid));
                    hasChanged = this.DirectoryAgent.AddSidHistory(this.ObjectGuid, this.SidHistory, this.SkipMetaUpdate);
                    break;

                case ADDBPrincipalCommandBase.ParameterSetBySid:
                    this.WriteVerbose(String.Format("Adding SID history to principal {0}.", this.ObjectSid));
                    hasChanged = this.DirectoryAgent.AddSidHistory(this.ObjectSid, this.SidHistory, this.SkipMetaUpdate);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }
            this.WriteVerboseResult(hasChanged);
        }
    }
}
