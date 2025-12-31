using System.ComponentModel;
using System.Management.Automation;
using System.Security;
using DSInternals.Common;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Interop;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsData.ConvertTo, "NTHash")]
[OutputType(new Type[] { typeof(string) })]
public class ConvertToNTHashCommand : PSCmdlet
{
    #region Parameters

    [Parameter(
        Mandatory = true,
        ValueFromPipeline = true,
        Position = 0,
        HelpMessage = "Provide a password in the form of a SecureString."
    )]
    [Alias("p")]
    [ValidatePasswordLength(0, DSInternals.Common.Cryptography.NTHash.MaxInputLength)]
    // This value is stored as an encrypted string.
    public SecureString Password
    {
        get;
        set;
    }

    #endregion Parameters

    #region Cmdlet Overrides

    protected override void ProcessRecord()
    {
        this.WriteVerbose("Calculating NT hash.");
        try
        {
            byte[] hashBytes = NTHash.ComputeHash(Password);
            string hashHex = hashBytes.ToHex();
            this.WriteObject(hashHex);
        }
        catch (ArgumentException ex)
        {
            ErrorRecord error = new ErrorRecord(ex, "Error1", ErrorCategory.InvalidArgument, this.Password);
            this.WriteError(error);
        }
        catch (Win32Exception ex)
        {
            ErrorCategory category = ((Win32ErrorCode)ex.NativeErrorCode).ToPSCategory();
            ErrorRecord error = new ErrorRecord(ex, "Error2", category, this.Password);
            // Allow the processing to continue on this error:
            this.WriteError(error);
        }
        catch (Exception ex)
        {
            ErrorRecord error = new ErrorRecord(ex, "Error3", ErrorCategory.NotSpecified, this.Password);
            this.WriteError(error);
        }
    }

    #endregion Cmdlet Overrides
}
