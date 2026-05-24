using System.Management.Automation;
using DSInternals.Common.Cryptography;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "DpapiNgNamedDescriptor")]
[Alias("Get-CngDpapiNamedDescriptor")]
[OutputType(typeof(KeyValuePair<string, string>))]
public class GetDpapiNgNamedDescriptorCommand : PSCmdlet
{
    [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
    [ValidateNotNullOrEmpty]
    [Alias("Key")]
    public string? Name
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
            if (string.IsNullOrEmpty(this.Name))
            {
                this.WriteObject(DpapiNg.ListDescriptors(this.Machine.IsPresent), enumerateCollection: true);
            }
            else
            {
                string descriptor = DpapiNg.QueryDescriptor(this.Name, this.Machine.IsPresent);
                this.WriteObject(new KeyValuePair<string, string>(this.Name, descriptor));
            }
        }
        catch (KeyNotFoundException ex)
        {
            var error = new ErrorRecord(ex, "GetDpapiNgNamedDescriptor_NotFound", ErrorCategory.ObjectNotFound, this.Name);
            this.WriteError(error);
        }
    }
}
