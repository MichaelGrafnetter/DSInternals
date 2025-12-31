
using System.Management.Automation;
using System.Security.Principal;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;
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
                this.WriteVerbose($"Adding SID history to principal {DistinguishedName}.");
                var dn = new DistinguishedName(this.DistinguishedName);
                hasChanged = this.DirectoryAgent.AddSidHistory(dn, this.SidHistory, this.SkipMetaUpdate);
                break;

            case ADDBPrincipalCommandBase.ParameterSetByName:
                this.WriteVerbose($"Adding SID history to principal {SamAccountName}.");
                hasChanged = this.DirectoryAgent.AddSidHistory(this.SamAccountName, this.SidHistory, this.SkipMetaUpdate);
                break;

            case ADDBObjectCommandBase.ParameterSetByGuid:
                this.WriteVerbose($"Adding SID history to principal {ObjectGuid}.");
                hasChanged = this.DirectoryAgent.AddSidHistory(this.ObjectGuid, this.SidHistory, this.SkipMetaUpdate);
                break;

            case ADDBPrincipalCommandBase.ParameterSetBySid:
                this.WriteVerbose($"Adding SID history to principal {ObjectSid}.");
                hasChanged = this.DirectoryAgent.AddSidHistory(this.ObjectSid, this.SidHistory, this.SkipMetaUpdate);
                break;

            default:
                // This should never happen:
                throw new PSInvalidOperationException(InvalidParameterSetMessage);
        }
        this.WriteVerboseResult(hasChanged);
    }
}
