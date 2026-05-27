using System.Management.Automation;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;

/// <summary>
/// Deletes all KDS root key derived DPAPI-NG group keys cached on the local machine by the current user
/// by invoking the <c>kdscli!DeleteAllCachedKeys</c> API.
/// </summary>
[Cmdlet(VerbsCommon.Clear, "DpapiNgSidKeyCache", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
[Alias("Remove-DpapiNgSidKey", "Clear-CngDpapiSidKeyCache", "Remove-CngDpapiSidKey")]
[OutputType("None")]
public class ClearDpapiNgSidKeyCacheCommand : PSCmdlet
{
    private const string CacheTarget = "DPAPI-NG SID key cache";

    protected override void ProcessRecord()
    {
        if (!this.ShouldProcess(CacheTarget, "Delete all cached KDS root key derived DPAPI-NG group keys"))
        {
            return;
        }

        this.WriteVerbose("Deleting all KDS root key derived DPAPI-NG group keys cached for the current user.");
        GroupKeyEnvelope.DeleteAllCachedKeys();
    }
}
