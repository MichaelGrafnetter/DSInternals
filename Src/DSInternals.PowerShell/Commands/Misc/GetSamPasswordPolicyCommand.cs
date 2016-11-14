using DSInternals.Common.Interop;
using DSInternals.SAM.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "SamPasswordPolicy")]
    [OutputType(typeof(SamDomainPasswordInformation))]
    public class GetSamPasswordPolicyCommand : SamCommandBase
    {
        #region Parameters
        [Parameter(
            HelpMessage = @"Specify AD domain.",
            Mandatory = true
        )]
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
                // TODO: Extract as a resource
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
