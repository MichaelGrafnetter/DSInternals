using System.Management.Automation;
using System.Security;
using DSInternals.Common.Cryptography;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsData.ConvertTo, "OrgIdHash", DefaultParameterSetName = "FromHash")]
[OutputType(new Type[] { typeof(string) })]
public class ConvertToOrgIdHashCommand : PSCmdlet
{
    #region Parameters

    [Parameter(
        ParameterSetName = "FromPassword",
        Mandatory = true,
        ValueFromPipeline = true,
        Position = 0,
        HelpMessage = "Provide a password in the form of a SecureString."
    )]
    [Alias("p")]
    [ValidatePasswordLength(0, DSInternals.Common.Cryptography.NTHash.MaxInputLength)]
    public SecureString Password
    {
        get;
        set;
    }

    [Parameter(
        ParameterSetName = "FromHash",
        Mandatory = true,
        ValueFromPipeline = true,
        Position = 0,
        HelpMessage = "Provide a 16-byte NT Hash of user's password in hexadecimal format."
    )]
    [ValidateNotNull]
    [ValidateCount(DSInternals.Common.Cryptography.NTHash.HashSize, DSInternals.Common.Cryptography.NTHash.HashSize)]
    [AcceptHexString]
    [Alias("h")]
    public byte[] NTHash
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = false,
        ValueFromPipeline = true,
        Position = 1,
        HelpMessage = "Provide a 10-byte salt in hexadecimal format."
    )]
    [ValidateNotNull]
    [AcceptHexString]
    [ValidateCount(OrgIdHash.SaltSize, OrgIdHash.SaltSize)]
    [Alias("s")]
    public byte[] Salt
    {
        get;
        set;
    }

    #endregion Parameters

    #region Cmdlet Overrides

    protected override void ProcessRecord()
    {
        this.WriteVerbose("Calculating OrgId hash.");
        try
        {
            string orgIdHash;
            // TODO: Switch by parametersetname
            if (this.NTHash != null)
            {
                // Calculate OrgId hash from NT Hash:
                byte[] binaryNTHash = NTHash;
                orgIdHash = OrgIdHash.ComputeFormattedHash(this.NTHash, this.Salt);
            }
            else
            {
                // Calculate OrgId hash from password:
                orgIdHash = OrgIdHash.ComputeFormattedHash(this.Password, this.Salt);
            }
            this.WriteObject(orgIdHash);
        }
        catch (Exception ex)
        {
            ErrorRecord error = new ErrorRecord(ex, "Error1", ErrorCategory.NotSpecified, this.Password);
            this.WriteError(error);
        }
    }

    #endregion Cmdlet Overrides
}
