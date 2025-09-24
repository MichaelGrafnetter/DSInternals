using System.Management.Automation;
using DSInternals.SAM;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "LsaPolicyInformation")]
[OutputType(typeof(LsaPolicyInformation))]
public class GetLsaPolicyInformationCommand : LsaPolicyCommandBase
{
    #region Cmdlet Overrides
    protected override void ProcessRecord()
    {
        var policyInfo = new LsaPolicyInformation()
        {
            DnsDomain = this.LsaPolicy.QueryDnsDomainInformation(),
            Domain = this.LsaPolicy.QueryAccountDomainInformation(),
            LocalDomain = this.LsaPolicy.QueryLocalAccountDomainInformation(),
            MachineAccountSid = this.LsaPolicy.QueryMachineAccountInformation()
        };

        this.WriteObject(policyInfo);
    }

    protected override LsaPolicyAccessMask RequiredAccessMask => LsaPolicyAccessMask.ViewLocalInformation;
    #endregion Cmdlet Overrides
}
