namespace DSInternals.PowerShell.Commands
{
    using DSInternals.SAM;
    using DSInternals.SAM.Interop;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, "LsaPolicyInformation")]
    [OutputType(typeof(LsaPolicyInformation))]
    public class GetLsaPolicyInformationCommand : PSCmdlet
    {
        #region Parameters
        [Parameter(Mandatory = false, Position = 1)]
        [ValidateNotNullOrEmpty]
        [Alias("Computer", "Machine", "MachineName", "System", "SystemName")]
        public string ComputerName
        {
            get;
            set;
        }
        #endregion Parameters

        #region Cmdlet Overrides
        protected override void ProcessRecord()
        {
            using (var policy = new LsaPolicy(this.ComputerName, LsaPolicyAccessMask.ViewLocalInformation))
            {
                var policyInfo = new LsaPolicyInformation()
                {
                    DnsDomain = policy.QueryDnsDomainInformation(),
                    Domain = policy.QueryAccountDomainInformation(),
                    LocalDomain = policy.QueryLocalAccountDomainInformation(),
                    MachineAccountSid = policy.QueryMachineAccountInformation()
                };
                
                this.WriteObject(policyInfo);
            }
        }
        #endregion Cmdlet Overrides
    }
}