using System.Management.Automation;
using System.Security.Cryptography;
using System.Text;
using DSInternals.Common;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsSecurity.Unprotect, "DpapiNgData", DefaultParameterSetName = OnlineParameterSetName)]
[Alias("Unprotect-CngDpapiData")]
[OutputType(typeof(string))]
public class UnprotectDpapiNgDataCommand : PSCmdlet
{
    private const string OnlineParameterSetName = "Online";

    private const string OfflineParameterSetName = "Offline";

    [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true,
        ParameterSetName = OnlineParameterSetName
    )]
    [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true,
        ParameterSetName = OfflineParameterSetName
    )]
    [ValidateNotNullOrEmpty]
    [AcceptBase64String]
    [Alias("CngProtectedDataBlob", "ProtectedBlob")]
    public byte[] Blob
    {
        get;
        set;
    }

    [Parameter(Mandatory = true, ParameterSetName = OfflineParameterSetName)]
    [ValidateNotNullOrEmpty]
    [Alias("KdsRootKeys", "RootKey", "RootKeys")]
    public KdsRootKey[]? KdsRootKey
    {
        get;
        set;
    }

    [Parameter(Mandatory = false, ParameterSetName = OnlineParameterSetName)]
    [Parameter(Mandatory = false, ParameterSetName = OfflineParameterSetName)]
    [ValidateNotNull]
    [ArgumentCompleter(typeof(EncodingArgumentCompleter))]
    [EncodingTransformation]
    public Encoding? Encoding
    {
        get;
        set;
    }

    protected override void ProcessRecord()
    {
        try
        {
            CngProtectedDataBlob protectedBlob = CngProtectedDataBlob.Decode(this.Blob);

            if (this.KdsRootKey?.Length > 0)
            {
                bool groupKeyCached = protectedBlob.CacheGroupKey(this.KdsRootKey);

                if (!groupKeyCached)
                {
                    throw new ArgumentException("None of the supplied KDS root keys matches the protected data blob.", nameof(this.KdsRootKey));
                }
            }

            if (this.Encoding != null)
            {
                this.WriteObject(protectedBlob.DecryptText(this.Encoding));
            }
            else
            {
                this.WriteObject(protectedBlob.Decrypt().ToHex());
            }
        }
        catch (CryptographicException ex)
        {
            var error = new ErrorRecord(ex, "UnprotectDpapiNgData_CryptographicError", ErrorCategory.SecurityError, this.Blob);
            this.WriteError(error);
        }
        catch (FormatException ex)
        {
            var error = new ErrorRecord(ex, "UnprotectDpapiNgData_InvalidFormat", ErrorCategory.InvalidData, this.Blob);
            this.WriteError(error);
        }
        catch (ArgumentException ex)
        {
            var error = new ErrorRecord(ex, "UnprotectDpapiNgData_InvalidArgument", ErrorCategory.InvalidArgument, this.Blob);
            this.WriteError(error);
        }
        catch (InvalidOperationException ex)
        {
            var error = new ErrorRecord(ex, "UnprotectDpapiNgData_InvalidOperation", ErrorCategory.InvalidOperation, this.Blob);
            this.WriteError(error);
        }
    }
}
