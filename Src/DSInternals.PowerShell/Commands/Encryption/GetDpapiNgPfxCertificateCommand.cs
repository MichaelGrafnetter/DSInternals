using System.Management.Automation;
using System.Security.Cryptography;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "DpapiNgPfxCertificate")]
[Alias("Get-CngDpapiPfxCertificate")]
[OutputType(typeof(PfxProtectedPassword))]
public class GetDpapiNgPfxCertificateCommand : PSCmdletEx
{
    [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true
    )]
    [ValidateNotNullOrEmpty]
    [Alias("FilePath", "FullName", "PfxPath")]
    public string Path
    {
        get;
        set;
    }

    protected override void ProcessRecord()
    {
        string? resolvedPath = null;

        try
        {
            resolvedPath = this.ResolveFilePath(this.Path);
            PfxProtectedPassword password = PfxProtectedPassword.Load(resolvedPath);
            this.WriteObject(password);
        }
        catch (SessionStateException ex)
        {
            this.WriteError(new ErrorRecord(ex.ErrorRecord, ex));
        }
        catch (UnauthorizedAccessException ex)
        {
            var error = new ErrorRecord(ex, "GetDpapiNgPfxCertificate_AccessDenied", ErrorCategory.PermissionDenied, resolvedPath ?? this.Path);
            this.WriteError(error);
        }
        catch (IOException ex)
        {
            var error = new ErrorRecord(ex, "GetDpapiNgPfxCertificate_ReadError", ErrorCategory.ReadError, resolvedPath ?? this.Path);
            this.WriteError(error);
        }
        catch (CryptographicException ex)
        {
            var error = new ErrorRecord(ex, "GetDpapiNgPfxCertificate_InvalidPfx", ErrorCategory.InvalidData, resolvedPath ?? this.Path);
            this.WriteError(error);
        }
        catch (ArgumentException ex)
        {
            var error = new ErrorRecord(ex, "GetDpapiNgPfxCertificate_InvalidArgument", ErrorCategory.InvalidArgument, resolvedPath ?? this.Path);
            this.WriteError(error);
        }
        catch (Exception ex)
        {
            var error = new ErrorRecord(ex, "GetDpapiNgPfxCertificate_Error", ErrorCategory.NotSpecified, resolvedPath ?? this.Path);
            this.WriteError(error);
        }
    }
}
