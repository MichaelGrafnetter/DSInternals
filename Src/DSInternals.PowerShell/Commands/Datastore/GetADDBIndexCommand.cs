using System;
using System.Management.Automation;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "ADDBIndex")]
    // TODO: output type
    [OutputType("None")]
    /// <summary>
    /// Implements the Get-ADDBIndex PowerShell cmdlet.
    /// </summary>
    public class GetADDBIndexCommand : ADDBCommandBase
    {
        protected override void ProcessRecord()
        {
            throw new NotImplementedException();
        }
    }
}