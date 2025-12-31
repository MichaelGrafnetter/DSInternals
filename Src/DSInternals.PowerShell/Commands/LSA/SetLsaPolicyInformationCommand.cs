using System.Management.Automation;
using System.Security.Principal;
using DSInternals.SAM;

namespace DSInternals.PowerShell.Commands;
[Cmdlet(VerbsCommon.Set, "LsaPolicyInformation")]
[OutputType("None")]
public class SetLsaPolicyInformationCommand : LsaPolicyCommandBase
{
    #region Parameters
    [Parameter(Mandatory = true)]
    [ValidateNotNullOrEmpty]
    [Alias("NetBIOSDomainName", "Workgroup")]
    public string DomainName
    {
        get;
        set;
    }

    [Parameter(Mandatory = true)]
    [AllowNull]
    public string DnsDomainName
    {
        get;
        set;
    }

    [Parameter(Mandatory = true)]
    [AllowNull]
    public string DnsForestName
    {
        get;
        set;
    }

    [Parameter(Mandatory = true)]
    [AllowNull]
    public Guid? DomainGuid
    {
        get;
        set;
    }

    [Parameter(Mandatory = true)]
    [AllowNull]
    public SecurityIdentifier DomainSid
    {
        get;
        set;
    }
    #endregion Parameters

    #region Cmdlet Overrides
    protected override void ProcessRecord()
    {
        var newInfo = new LsaDnsDomainInformation()
        {
            DnsDomainName = this.DnsDomainName,
            DnsForestName = this.DnsForestName,
            Guid = this.DomainGuid,
            Name = this.DomainName,
            Sid = this.DomainSid
        };

        this.LsaPolicy.SetDnsDomainInformation(newInfo);
    }

    protected override LsaPolicyAccessMask RequiredAccessMask => LsaPolicyAccessMask.ViewLocalInformation |
                                                                 LsaPolicyAccessMask.TrustAdmin;
    #endregion Cmdlet Overrides
}
