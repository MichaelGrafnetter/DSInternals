using System.ComponentModel;
using System.Management.Automation;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Interop;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Remove, "DpapiNgNamedDescriptor", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
[Alias("Remove-CngDpapiNamedDescriptor")]
[OutputType("None")]
public class RemoveDpapiNgNamedDescriptorCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
    [ValidateNotNullOrEmpty]
    [Alias("Key")]
    public string Name
    {
        get;
        set;
    }

    [Parameter]
    public SwitchParameter Machine
    {
        get;
        set;
    }

    protected override void ProcessRecord()
    {
        if (!this.ShouldProcess(this.Name, "Remove DPAPI-NG named descriptor"))
        {
            return;
        }

        bool deleted = DpapiNg.DeleteDescriptor(this.Name, this.Machine.IsPresent);
        if (!deleted)
        {
            var exception = new Win32Exception((int)Win32ErrorCode.FILE_NOT_FOUND);
            var error = new ErrorRecord(exception, "RemoveDpapiNgNamedDescriptor_NotFound", ErrorCategory.ObjectNotFound, this.Name);
            this.WriteError(error);
        }
    }
}
