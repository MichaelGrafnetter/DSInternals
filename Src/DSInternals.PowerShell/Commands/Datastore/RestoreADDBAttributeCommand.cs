using System;
using System.Management.Automation;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsData.Restore, "ADDBAttribute")]
    [OutputType("None")]
    public class RestoreADDBAttributeCommand : ADDBObjectCommandBase
    {
        public string[] Property
        {
            get;
            set;
        }

        public int VerInc
        {
            get;
            set;
        }

        public SwitchParameter SubTree
        {
            get;
            set;
        }

        [Parameter]
        public SwitchParameter Force
        {
            get;
            set;
        }

        protected override bool ReadOnly
        {
            get
            {
                return false;
            }
        }

        protected override void ProcessRecord()
        {
            throw new NotImplementedException();

            if (!Force.IsPresent)
            {
                // Do not continue with operation until the user enforces it.
                var exception = new ArgumentException(WarningMessage);
                var error = new ErrorRecord(exception, "RestoreADDBAttribute_ForceRequired", ErrorCategory.InvalidArgument, null);
                this.ThrowTerminatingError(error);
            }
        }
    }
}
