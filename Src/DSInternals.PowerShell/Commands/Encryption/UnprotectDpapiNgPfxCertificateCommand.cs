using System.Management.Automation;
using System.Security.Cryptography;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsSecurity.Unprotect, "DpapiNgPfxCertificate", DefaultParameterSetName = PathParameterSetName)]
[Alias("Unprotect-CngDpapiPfxCertificate")]
[OutputType(typeof(PfxProtectedPassword))]
public class UnprotectDpapiNgPfxCertificateCommand : PSCmdletEx
{
    private const string PathParameterSetName = "Path";
    private const string InputObjectParameterSetName = "InputObject";

    [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = PathParameterSetName
    )]
    [ValidateNotNullOrEmpty]
    [Alias("FilePath", "FullName", "PfxPath")]
    public string Path
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true,
        ParameterSetName = InputObjectParameterSetName
    )]
    [ValidateNotNull]
    [Alias("ProtectedPassword")]
    public PfxProtectedPassword InputObject
    {
        get;
        set;
    }

    [Parameter(Mandatory = false)]
    [ValidateNotNullOrEmpty]
    [Alias("KdsRootKeys", "RootKey", "RootKeys")]
    public KdsRootKey[]? KdsRootKey
    {
        get;
        set;
    }

    protected override void ProcessRecord()
    {
        string? resolvedPath = null;

        try
        {
            PfxProtectedPassword password;

            if (this.ParameterSetName == InputObjectParameterSetName)
            {
                password = this.InputObject;
            }
            else
            {
                resolvedPath = this.ResolveFilePath(this.Path);
                password = PfxProtectedPassword.Load(resolvedPath);
            }

            password.Decrypt(this.KdsRootKey);
            this.WriteObject(password);
        }
        catch (SessionStateException ex)
        {
            this.WriteError(new ErrorRecord(ex.ErrorRecord, ex));
        }
        catch (UnauthorizedAccessException ex)
        {
            var error = new ErrorRecord(ex, "UnprotectDpapiNgPfxCertificate_AccessDenied", ErrorCategory.PermissionDenied, resolvedPath ?? this.Path);
            this.WriteError(error);
        }
        catch (IOException ex)
        {
            var error = new ErrorRecord(ex, "UnprotectDpapiNgPfxCertificate_ReadError", ErrorCategory.ReadError, resolvedPath ?? this.Path);
            this.WriteError(error);
        }
        catch (CryptographicException ex)
        {
            var error = new ErrorRecord(ex, "UnprotectDpapiNgPfxCertificate_CryptographicError", ErrorCategory.SecurityError, resolvedPath ?? this.Path);
            this.WriteError(error);
        }
        catch (FormatException ex)
        {
            var error = new ErrorRecord(ex, "UnprotectDpapiNgPfxCertificate_InvalidFormat", ErrorCategory.InvalidData, resolvedPath ?? this.Path);
            this.WriteError(error);
        }
        catch (ArgumentException ex)
        {
            var error = new ErrorRecord(ex, "UnprotectDpapiNgPfxCertificate_InvalidArgument", ErrorCategory.InvalidArgument, resolvedPath ?? this.Path);
            this.WriteError(error);
        }
        catch (InvalidOperationException ex)
        {
            var error = new ErrorRecord(ex, "UnprotectDpapiNgPfxCertificate_InvalidOperation", ErrorCategory.InvalidOperation, resolvedPath ?? this.Path);
            this.WriteError(error);
        }
    }
}
