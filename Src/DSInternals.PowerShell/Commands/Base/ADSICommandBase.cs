namespace DSInternals.PowerShell.Commands
{
    using DSInternals.ADSI;
    using System;
    using System.Management.Automation;
    using System.Net;

    public abstract class ADSICommandBase : PSCmdlet, IDisposable
    {
        #region Parameters
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        [Alias("Host", "DomainController", "DC", "ComputerName")]
        public string Server
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        public PSCredential Credential
        {
            get;
            set;
        }

        #endregion Parameters
        protected AdsiClient Client
        {
            get;
            private set;
        }

        #region Cmdlet Overrides

        protected override void BeginProcessing()
        {
            // TODO: Debug output
            // TODO: Exception handling
            NetworkCredential netCredential = null;
            if(this.Credential != null)
            {
                // Convert PSCredential to NetworkCredential
                netCredential = this.Credential.GetNetworkCredential();
            }
            this.Client = new AdsiClient(this.Server, netCredential);
        }

        #endregion Cmdlet Overrides

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.Client != null)
            {
                this.Client.Dispose();
                this.Client = null;
            }
        }
    }
}
