using System.Management.Automation;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "DpapiNgSidKeyIdentifier")]
[Alias("Get-CngDpapiSidKeyIdentifier")]
[OutputType(typeof(ProtectionKeyIdentifier))]
public class GetDpapiNgSidKeyIdentifierCommand : PSCmdlet
{
    [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true
    )]
    [ValidateNotNullOrEmpty]
    [AcceptHexString]
    [Alias("KeyId", "ProtectionKeyIdentifier", "KeyIdentifier")]
    public byte[] Blob
    {
        get;
        set;
    }

    protected override void ProcessRecord()
    {
        try
        {
            this.WriteObject(new ProtectionKeyIdentifier(this.Blob));
        }
        catch (ArgumentOutOfRangeException ex)
        {
            var error = new ErrorRecord(ex, "GetDpapiNgSidKeyIdentifier_InvalidBlob", ErrorCategory.InvalidData, this.Blob);
            this.WriteError(error);
        }
        catch (ArgumentException ex)
        {
            var error = new ErrorRecord(ex, "GetDpapiNgSidKeyIdentifier_InvalidArgument", ErrorCategory.InvalidArgument, this.Blob);
            this.WriteError(error);
        }
    }
}
