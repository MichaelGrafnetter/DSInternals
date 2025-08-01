using System;
using System.Management.Automation;
using DSInternals.Common.Kerberos;
using DSInternals.DataStore;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "ADDBTrust")]
    [OutputType(typeof(TrustedDomain))]
    public class GetADDBTrustCommand : ADDBCommandBase
    {
        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        [ValidateCount(BootKeyRetriever.BootKeyLength, BootKeyRetriever.BootKeyLength)]
        [AcceptHexString]
        [Alias("key", "SysKey", "SystemKey")]
        public byte[] BootKey
        {
            get;
            set;
        }

        protected DirectoryAgent DirectoryAgent
        {
            get;
            private set;
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            try
            {
                this.DirectoryAgent = new DirectoryAgent(this.DirectoryContext);
            }
            catch (Exception ex)
            {
                ErrorRecord error = new ErrorRecord(ex, "Trust_TableOpenError", ErrorCategory.OpenError, null);
                // Terminate on this error:
                this.ThrowTerminatingError(error);
            }

            if (this.BootKey == null)
            {
                this.WriteWarning("Trust passwords cannot be decrypted as no system key has been provided.");
            }
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            foreach (var trust in this.DirectoryAgent.GetTrusts(this.BootKey))
            {
                this.WriteObject(trust);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.DirectoryAgent != null)
            {
                this.DirectoryAgent.Dispose();
                this.DirectoryAgent = null;
            }

            base.Dispose(disposing);
        }
    }
}
