namespace DSInternals.PowerShell.Commands
{
    using DSInternals.SAM;
    using DSInternals.SAM.Interop;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, "SamPasswordPolicy")]
    [OutputType(typeof(SamDomainPasswordInformation))]
    public class GetSamPasswordPolicyCommand : SamCommandBase
    {
        #region Parameters
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Domain
        {
            get;
            set;
        }
        #endregion Parameters

        #region Cmdlet Overrides
        protected override void ProcessRecord()
        {
            this.WriteVerbose(string.Format("Opening domain {0}.", this.Domain));

            if (this.Domain.Contains("."))
            {
                // This is not a hard check, because dots are actually allowed in NetBIOS names, although not recommended.
                this.WriteWarning("The domain name supplied appears to be a DNS name instead of NetBIOS name.");
            }
            using(var samDomain = this.SamServer.OpenDomain(this.Domain, SamDomainAccessMask.ReadPasswordParameters))
            {
                var policy = samDomain.GetPasswordPolicy();
                this.WriteObject(policy);
            }
        }
        #endregion Cmdlet Overrides
    }
}
