
using System.Management.Automation;
using System.Security.Principal;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;
[Cmdlet(VerbsCommon.Get, "ADSIAccount", DefaultParameterSetName = ParameterSetAll)]
[OutputType(typeof(DSAccount), typeof(DSUser), typeof(DSComputer))]
public class GetADSIAccountCommand : ADSICommandBase
{
    #region Constants
    protected const string ParameterSetAll = "All";
    protected const string ParameterSetByName = "ByName";
    protected const string ParameterSetByUPN = "ByUPN";
    protected const string ParameterSetBySid = "BySID";
    protected const string ParameterSetByDN = "ByDN";
    protected const string ParameterSetByGuid = "ByGuid";
    #endregion Constants

    #region Parameters
    [Parameter(Mandatory = false, ParameterSetName = ParameterSetAll)]
    [Alias("AllAccounts", "ReturnAllAccounts")]
    public SwitchParameter All
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = ParameterSetByName
    )]
    [ValidateNotNullOrEmpty]
    [Alias("Login", "SAM", "AccountName", "User")]
    public string SamAccountName
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = ParameterSetByUPN
    )]
    [ValidateNotNullOrEmpty]
    [Alias("UPN")]
    public string UserPrincipalName
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = ParameterSetBySid
    )]
    [ValidateNotNullOrEmpty]
    [Alias("SID")]
    public SecurityIdentifier ObjectSid
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = ParameterSetByDN
    )]
    [ValidateNotNullOrEmpty]
    [Alias("DN")]
    public string DistinguishedName
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = true,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = ParameterSetByGuid
    )]
    [ValidateNotNullOrEmpty]
    [Alias("Guid")]
    public Guid ObjectGuid
    {
        get;
        set;
    }

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
    [Alias("KdsRootKeys", "RootKey", "RootKeys")]
    public KdsRootKey[]? KdsRootKey
    {
        get;
        set;
    }
    #endregion Parameters

    protected override KdsRootKey[]? KdsRootKeysOverride => this.KdsRootKey;

    protected override void ProcessRecord()
    {
        if (this.ParameterSetName == ParameterSetAll)
        {
            this.ReturnAllAccounts();
        }
        else
        {
            this.ReturnSingleAccount();
        }
    }

    private void ReturnAllAccounts()
    {
        foreach (DSAccount account in this.Client.GetAccounts(this.Properties))
        {
            this.WriteObject(account);
        }
    }

    private void ReturnSingleAccount()
    {
        DSAccount account;
        switch (this.ParameterSetName)
        {
            case ParameterSetByDN:
                var dn = new DistinguishedName(this.DistinguishedName);
                account = this.Client.GetAccount(dn, this.Properties);
                break;

            case ParameterSetByName:
                account = this.Client.GetAccount(this.SamAccountName, this.Properties);
                break;

            case ParameterSetByUPN:
                account = this.Client.GetAccountByUpn(this.UserPrincipalName, this.Properties);
                break;

            case ParameterSetByGuid:
                account = this.Client.GetAccount(this.ObjectGuid, this.Properties);
                break;

            case ParameterSetBySid:
                account = this.Client.GetAccount(this.ObjectSid, this.Properties);
                break;

            default:
                // This should never happen:
                throw new PSInvalidOperationException(InvalidParameterSetMessage);
        }

        this.WriteObject(account);
    }
}
