namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common.Data;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, "ADSIAccount")]
    [OutputType(typeof(DSAccount), typeof(DSUser), typeof(DSComputer))]
    public class GetADSIAccountCommand : ADSICommandBase
    {
        [Parameter(Mandatory = false)]
        [Alias("Property", "PropertySets", "PropertySet")]
        [PSDefaultValue(Value = "All")]
        public AccountPropertySets Properties
        {
            get;
            set;
        } = AccountPropertySets.All;

        protected override void ProcessRecord()
        {
            foreach(var account in this.Client.GetAccounts(this.Properties))
            {
                this.WriteObject(account);
            }
        }
    }
}
