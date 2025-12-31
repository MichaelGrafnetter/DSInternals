
using System.Management.Automation;

namespace DSInternals.PowerShell.Commands;
[Cmdlet(VerbsCommon.Get, "ADDBDomainController")]
[OutputType(typeof(DSInternals.PowerShell.DomainController))]
public class GetADDBDomainControllerCommand : ADDBCommandBase
{
    protected override void BeginProcessing()
    {
        base.BeginProcessing();
        var dc = this.DirectoryContext.DomainController;
        var dcTransfer = DSInternals.PowerShell.DomainController.Create(dc);
        this.WriteObject(dcTransfer);
    }
}
