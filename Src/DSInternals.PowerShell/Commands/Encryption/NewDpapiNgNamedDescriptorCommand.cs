using System.Management.Automation;
using DSInternals.Common.Cryptography;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.New, "DpapiNgNamedDescriptor")]
[Alias("New-CngDpapiNamedDescriptor")]
[OutputType("None")]
public class NewDpapiNgNamedDescriptorCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
    [ValidateNotNullOrEmpty]
    public string Name
    {
        get;
        set;
    }

    [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
    [ValidateNotNullOrEmpty]
    public string Descriptor
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
        try
        {
            DpapiNg.RegisterDescriptor(this.Name, this.Descriptor, this.Machine.IsPresent);
        }
        catch (Exception ex)
        {
            var error = new ErrorRecord(ex, "NewDpapiNgNamedDescriptor_Failed", ErrorCategory.WriteError, this.Name);
            this.WriteError(error);
        }
    }
}
