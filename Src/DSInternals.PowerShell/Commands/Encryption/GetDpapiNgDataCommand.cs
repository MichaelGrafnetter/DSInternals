using System.Management.Automation;
using System.Security.Cryptography;
using DSInternals.Common.Cryptography;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "DpapiNgData")]
[Alias("Get-CngDpapiData")]
[OutputType(typeof(CngProtectedDataBlob))]
public class GetDpapiNgDataCommand : PSCmdlet
{
    [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true
    )]
    [ValidateNotNullOrEmpty]
    [AcceptBase64String]
    [Alias("CngProtectedDataBlob", "ProtectedBlob")]
    public byte[] Blob
    {
        get;
        set;
    }

    protected override void ProcessRecord()
    {
        try
        {
            this.WriteObject(CngProtectedDataBlob.Decode(this.Blob));
        }
        catch (CryptographicException ex)
        {
            var error = new ErrorRecord(ex, "GetDpapiNgData_InvalidBlob", ErrorCategory.InvalidData, this.Blob);
            this.WriteError(error);
        }
        catch (ArgumentException ex)
        {
            var error = new ErrorRecord(ex, "GetDpapiNgData_InvalidArgument", ErrorCategory.InvalidArgument, this.Blob);
            this.WriteError(error);
        }
    }
}
