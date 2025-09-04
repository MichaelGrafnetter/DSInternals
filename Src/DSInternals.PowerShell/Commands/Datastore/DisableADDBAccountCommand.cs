using System;
using System.Management.Automation;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Disable, "ADDBAccount")]
    [OutputType("None")]
    /// <summary>
    /// Implements the Disable-ADDBAccount PowerShell cmdlet.
    /// </summary>
    public class DisableADDBAccountCommand : ADDBAccountStatusCommandBase
    {
        protected override bool Enabled
        {
            get
            {
                return false;
            }
        }
    }
}