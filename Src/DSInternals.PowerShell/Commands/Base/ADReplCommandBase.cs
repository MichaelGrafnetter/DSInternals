using System;
using System.Management.Automation;
using System.Net;
using DSInternals.Replication;

namespace DSInternals.PowerShell.Commands
{
    public abstract class ADReplCommandBase : PSCmdletEx, IDisposable
    {

        #region Parameters
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [Alias("Host", "DomainController", "DC")]
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

        protected DirectoryReplicationClient ReplicationClient
        {
            get;
            private set;
        }

        #region Cmdlet Overrides

        protected override void BeginProcessing()
        {
            NetworkCredential netCredential = null;
            if(this.Credential != null)
            {
                // Convert PSCredential to NetworkCredential
                netCredential = this.Credential.GetNetworkCredential();
            }

            this.ReplicationClient = new DirectoryReplicationClient(this.Server, netCredential);
        }

        #endregion Cmdlet Overrides

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.ReplicationClient != null)
            {
                this.ReplicationClient.Dispose();
                this.ReplicationClient = null;
            }
        }
    }
}
