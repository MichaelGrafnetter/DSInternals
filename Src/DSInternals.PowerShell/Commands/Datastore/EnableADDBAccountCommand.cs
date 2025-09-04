using System.Management.Automation;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Enable, "ADDBAccount")]
    [OutputType("None")]
    /// <summary>
    /// Implements the Enable-ADDBAccount PowerShell cmdlet.
    /// </summary>
    public class EnableADDBAccountCommand : ADDBAccountStatusCommandBase
    {
        protected override bool Enabled
        {
            get
            {
                return true;
            }
        }
    }
}