using System.ComponentModel;
using System.Management.Automation;
using DSInternals.Common;
using DSInternals.Common.Interop;
using DSInternals.DataStore;

namespace DSInternals.PowerShell.Commands;
[Cmdlet(VerbsCommon.Get, "BootKey")]
[OutputType(typeof(string))]
public class GetBootKeyCommand : PSCmdletEx
{
    private const string OnlineParameterSet = "Online";
    private const string OfflineParameterSet = "Offline";

    [Parameter(Mandatory = true, Position = 0, ParameterSetName = OfflineParameterSet)]
    [ValidateNotNullOrEmpty]
    [Alias("Path", "FilePath", "SystemHivePath", "HivePath")]
    public string SystemHiveFilePath
    {
        get;
        set;
    }
    [Parameter(Mandatory = true, ParameterSetName = OnlineParameterSet)]
    public SwitchParameter Online
    {
        get;
        set;
    }

    protected override void BeginProcessing()
    {
        try
        {
            byte[] bootKey;
            if (Online.IsPresent)
            {
                // Online
                bootKey = BootKeyRetriever.GetBootKey();
            }
            else
            {
                // Offline
                string hivePathResolved = this.ResolveFilePath(this.SystemHiveFilePath);
                bootKey = BootKeyRetriever.GetBootKey(hivePathResolved);
            }
            this.WriteObject(bootKey.ToHex());
        }
        catch (SessionStateException ex)
        {
            // This may be DriveNotFoundException, ItemNotFoundException, ProviderNotFoundException, etc.
            // Terminate on this error:
            this.ThrowTerminatingError(new ErrorRecord(ex.ErrorRecord, ex));
        }
        catch (Win32Exception ex)
        {
            ErrorCategory category = ((Win32ErrorCode)ex.NativeErrorCode).ToPSCategory();
            ErrorRecord error = new ErrorRecord(ex, "GetBootKey_Win32Error", category, this.SystemHiveFilePath);
            this.ThrowTerminatingError(error);

        }
        catch (Exception ex)
        {
            ErrorRecord error = new ErrorRecord(ex, "GetBootKey_OtherError", ErrorCategory.OpenError, null);
            // Terminate on this error:
            this.ThrowTerminatingError(error);
        }
    }
}
