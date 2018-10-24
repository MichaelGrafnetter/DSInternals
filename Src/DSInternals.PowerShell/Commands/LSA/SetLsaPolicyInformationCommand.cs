namespace DSInternals.PowerShell.Commands
{
    using DSInternals.SAM;
    using DSInternals.SAM.Interop;
    using System;
    using System.Management.Automation;
    using System.Security.Principal;

    [Cmdlet(VerbsCommon.Set, "LsaPolicyInformation")]
    [OutputType("None")]
    public class SetLsaPolicyInformationCommand : PSCmdlet
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

        [Parameter(Mandatory = false)]
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
            using (var policy = new LsaPolicy(this.ComputerName, LsaPolicyAccessMask.ViewLocalInformation | LsaPolicyAccessMask.TrustAdmin))
            {
                var newInfo = new LsaDnsDomainInformation()
                {
                    DnsDomainName = this.DnsDomainName,
                    DnsForestName = this.DnsForestName,
                    Guid = this.DomainGuid,
                    Name = this.DomainName,
                    Sid = this.DomainSid
                };

                policy.SetDnsDomainInformation(newInfo);
            }
        }
        #endregion Cmdlet Overrides
    }
}