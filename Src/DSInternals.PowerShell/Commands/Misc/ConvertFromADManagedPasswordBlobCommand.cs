
using System.Management.Automation;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;
[Cmdlet(VerbsData.ConvertFrom, "ADManagedPasswordBlob")]
[OutputType(typeof(ManagedPassword))]
public class ConvertFromADManagedPasswordBlobCommand : PSCmdlet
{
    #region Parameters

    [Parameter(
        Mandatory = true,
        Position = 0
    )]
    [Alias("msDS-ManagedPassword", "ManagedPassword", "ManagedPasswordBlob")]
    public byte[] Blob
    {
        get;
        set;
    }

    #endregion Parameters

    #region Cmdlet Overrides

    protected override void ProcessRecord()
    {
        this.WriteObject(ManagedPassword.Parse(this.Blob));
    }

    #endregion Cmdlet Overrides
}
