using System;
using System.Management.Automation;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "ADDBObject")]
    // TODO: output type
    [OutputType("None")]
    public class GetADDBObjectCommand : ADDBObjectCommandBase
    {
        protected override void ProcessRecord()
        {
            throw new NotImplementedException();
        }
    }
}