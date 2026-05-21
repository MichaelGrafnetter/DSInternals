
using System.Management.Automation;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;
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

    [Parameter(Mandatory = false)]
    [ValidateNotNullOrEmpty]
    [Alias("KdsRootKey", "RootKey", "RootKeys")]
    public KdsRootKey[]? KdsRootKeys
    {
        get;
        set;
    }

    protected override KdsRootKey[]? KdsRootKeysOverride => this.KdsRootKeys;

    protected override void ProcessRecord()
    {
        foreach (DSAccount account in this.Client.GetAccounts(this.Properties))
        {
            this.WriteObject(account);
        }
    }
}
