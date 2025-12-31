using System.Management.Automation;
using System.Security.Principal;

namespace DSInternals.PowerShell.Commands;

public abstract class ADReplPrincipalCommandBase : ADReplObjectCommandBase
{
    protected const string ParameterSetByName = "ByName";
    protected const string ParameterSetByUPN = "ByUPN";
    protected const string ParameterSetBySid = "BySID";

    #region Parameters

    [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true,
        ParameterSetName = ParameterSetByName
    )]
    [ValidateNotNullOrEmpty]
    [Alias("Login", "sam", "AccountName", "User")]
    public string SamAccountName
    {
        get;
        set;
    }

    [Parameter(
        Mandatory = false,
        Position = 1,
        ParameterSetName = ParameterSetByName
    )]
    [ValidateNotNullOrEmpty]
    [Alias("AccountDomain", "UserDomain")]
    public string Domain
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
    [Alias("Sid")]
    public SecurityIdentifier ObjectSid
    {
        get;
        set;
    }

    #endregion Parameters

    protected override void BeginProcessing()
    {
        base.BeginProcessing();

        if (ParameterSetName == ParameterSetByName)
        {
            if (string.IsNullOrEmpty(this.Domain))
            {
                // Automatically infer DC's domain name.
                this.Domain = this.ReplicationClient.NetBIOSDomainName;
            }
            else
            {
                // User-provided domain name, so a sanity check is necessary.
                this.ValidateDomainName();
            }
        }
    }

    protected void ValidateDomainName()
    {
        if (this.Domain.Contains("."))
        {
            // This is not a hard check, because dots are actually allowed in NetBIOS names, although not recommended.
            this.WriteWarning("The domain name supplied appears to be a DNS name instead of NetBIOS name.");
        }
    }
}
