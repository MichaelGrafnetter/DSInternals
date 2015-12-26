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
        }
    }
}