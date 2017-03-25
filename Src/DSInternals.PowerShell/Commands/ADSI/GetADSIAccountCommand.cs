namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common.Data;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, "ADSIAccount")]
    [OutputType(typeof(DSAccount))]
    public class GetADSIAccountCommand : ADSICommandBase
    {

        protected override void ProcessRecord()
        {
            foreach(var account in this.Client.GetAccounts())
            {
                this.WriteObject(account);
            }
        }
    }
}