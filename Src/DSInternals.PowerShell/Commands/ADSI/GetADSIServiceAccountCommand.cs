using System.Management.Automation;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "ADSIServiceAccount")]
[Alias("Get-ADSIGroupManagedServiceAccount", "Get-ADSIDelegatedManagedServiceAccount")]
[OutputType(typeof(GroupManagedServiceAccount))]
public class GetADSIServiceAccountCommand : ADSICommandBase
{
    [Parameter(Mandatory = false)]
    [Alias("EffectiveDate", "PasswordLastSet", "PwdLastSet", "Date", "Time", "d", "t")]
    public DateTime? EffectiveTime
    {
        get;
        set;
    }

    [Parameter(Mandatory = false)]
    [ValidateNotNullOrEmpty]
    [Alias("KdsRootKeys", "RootKey", "RootKeys")]
    public KdsRootKey[]? KdsRootKey
    {
        get;
        set;
    }

    protected override KdsRootKey[]? KdsRootKeysOverride => this.KdsRootKey;

    protected override void ProcessRecord()
    {
        foreach (GroupManagedServiceAccount account in this.Client.GetGroupManagedServiceAccounts(this.EffectiveTime))
        {
            this.WriteObject(account);
        }
    }
}
