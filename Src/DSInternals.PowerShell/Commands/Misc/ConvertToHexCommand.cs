using System.Management.Automation;
using DSInternals.Common;

namespace DSInternals.PowerShell.Commands;
[Cmdlet(VerbsData.ConvertTo, "Hex")]
[OutputType(new Type[] { typeof(string) })]
public class ConvertToHexCommand : PSCmdlet
{
    #region Parameters

    [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true
    )]
    public byte[] Input
    {
        get;
        set;
    }

    [Parameter]
    public SwitchParameter UpperCase
    {
        get;
        set;
    }

    #endregion Parameters

    #region Cmdlet Overrides

    protected override void ProcessRecord()
    {
        this.WriteObject(this.Input.ToHex(this.UpperCase.IsPresent));
    }

    #endregion Cmdlet Overrides
}
