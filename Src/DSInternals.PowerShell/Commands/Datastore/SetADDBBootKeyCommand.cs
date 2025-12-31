using System.Management.Automation;
using DSInternals.DataStore;

namespace DSInternals.PowerShell.Commands;
[Cmdlet(VerbsCommon.Set, "ADDBBootKey")]
[OutputType("None")]
public class SetADDBBootKeyCommand : ADDBCommandBase
{
    [Parameter(Mandatory = true)]
    [ValidateNotNull]
    [ValidateCount(BootKeyRetriever.BootKeyLength, BootKeyRetriever.BootKeyLength)]
    [AcceptHexString]
    [Alias("OldKey", "Old", "OldSysKey")]
    public byte[] OldBootKey
    {
        get;
        set;
    }

    [Parameter(Mandatory = false)]
    [ValidateNotNull]
    [ValidateCount(BootKeyRetriever.BootKeyLength, BootKeyRetriever.BootKeyLength)]
    [AcceptHexString]
    [Alias("NewKey", "New", "NewSysKey")]
    public byte[] NewBootKey
    {
        get;
        set;
    }

    [Parameter]
    public SwitchParameter Force
    {
        get;
        set;
    }

    protected override void BeginProcessing()
    {
        if (!Force.IsPresent)
        {
            // Do not continue with operation until the user enforces it.
            var exception = new ArgumentException(WarningMessage);
            var error = new ErrorRecord(exception, "SetADDBBootKey_ForceRequired", ErrorCategory.InvalidArgument, null);
            this.ThrowTerminatingError(error);
        }

        base.BeginProcessing();
        using (var directoryAgent = new DirectoryAgent(this.DirectoryContext))
        {
            directoryAgent.ChangeBootKey(this.OldBootKey, this.NewBootKey);
        }
        // TODO: Verbosity
        // TODO: Exception handling
    }

    protected override bool ReadOnly
    {
        get
        {
            // We need to modify the PEK List attribute.
            return false;
        }
    }
}
