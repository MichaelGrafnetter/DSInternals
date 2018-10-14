namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using DSInternals.Replication;
    using System.Net;
    using DSInternals.Common.Data;

    public abstract class ADReplCommandBase : PSCmdlet, IDisposable
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

        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        [Alias("Proto", "RPCProtocol", "NCACN")]
        public RpcProtocol Protocol
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
            // TODO: Debug output
            // TODO: Exception handling
            //this.WriteDebug("Opening the Active Directory database.");
            NetworkCredential netCredential = null;
            if(this.Credential != null)
            {
                // Convert PSCredential to NetworkCredential
                netCredential = this.Credential.GetNetworkCredential();
            }
            this.ReplicationClient = new DirectoryReplicationClient(this.Server, this.Protocol, netCredential);
            //try
            //{
            //}
            //catch(SessionStateException ex)
            //{
            //    // This may be DriveNotFoundException, ItemNotFoundException, ProviderNotFoundException, etc.
            //    // Terminate on this error:
            //    this.ThrowTerminatingError(new ErrorRecord(ex.ErrorRecord, ex));
            //}
            //catch (Exception ex)
            //{
            //    ErrorRecord error = new ErrorRecord(ex, "DBContextError", ErrorCategory.OpenError, null);
            //    // Terminate on this error:
            //    this.ThrowTerminatingError(error);
            //}
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