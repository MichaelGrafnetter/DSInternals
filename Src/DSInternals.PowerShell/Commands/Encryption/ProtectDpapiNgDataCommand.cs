using System.Management.Automation;
using System.Security.Cryptography;
using System.Text;
using DSInternals.Common.Cryptography;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsSecurity.Protect, "DpapiNgData", DefaultParameterSetName = DescriptorParameterSetName)]
[Alias("Protect-CngDpapiData")]
[OutputType(typeof(string))]
public class ProtectDpapiNgDataCommand : PSCmdlet
{
    private const string DescriptorParameterSetName = "Descriptor";

    private const string NamedDescriptorParameterSetName = "NamedDescriptor";

    [Parameter(
        Mandatory = true,
        Position = 0,
        ParameterSetName = DescriptorParameterSetName
    )]
    [ValidateNotNullOrEmpty]
    public string? Descriptor
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        Position = 0,
        ParameterSetName = NamedDescriptorParameterSetName
    )]
    [ValidateNotNullOrEmpty]
    public string? NamedDescriptor
    {
        get;
        set;
    }

    [Parameter(ParameterSetName = NamedDescriptorParameterSetName)]
    public SwitchParameter Machine
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        Position = 1,
        ValueFromPipeline = true,
        ParameterSetName = DescriptorParameterSetName
    )]
    [Parameter(
        Mandatory = true,
        Position = 1,
        ValueFromPipeline = true,
        ParameterSetName = NamedDescriptorParameterSetName
    )]
    [ValidateNotNull]
    public string Cleartext
    {
        get;
        set;
    }

    [Parameter]
    [ValidateNotNull]
    public Encoding Encoding
    {
        get;
        set;
    } = Encoding.Unicode;

    protected override void ProcessRecord()
    {
        try
        {
            byte[] protectedBlob = this.ParameterSetName == NamedDescriptorParameterSetName
                ? DpapiNg.ProtectSecret(this.NamedDescriptor, this.Machine.IsPresent, this.Cleartext, this.Encoding)
                : DpapiNg.ProtectSecret(this.Descriptor, this.Cleartext, this.Encoding);

            this.WriteObject(Convert.ToBase64String(protectedBlob));
        }
        catch (CryptographicException ex)
        {
            var error = new ErrorRecord(ex, "ProtectDpapiNgData_CryptographicError", ErrorCategory.SecurityError, this.Descriptor ?? this.NamedDescriptor);
            this.WriteError(error);
        }
        catch (ArgumentException ex)
        {
            var error = new ErrorRecord(ex, "ProtectDpapiNgData_InvalidArgument", ErrorCategory.InvalidArgument, this.Descriptor ?? this.NamedDescriptor);
            this.WriteError(error);
        }
    }
}
