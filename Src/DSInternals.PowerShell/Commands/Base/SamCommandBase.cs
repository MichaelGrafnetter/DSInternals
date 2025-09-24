using DSInternals.Common.Interop;
using DSInternals.SAM;
using DSInternals.SAM.Interop;
using System;
using System.ComponentModel;
using System.Management.Automation;
using System.Net;
namespace DSInternals.PowerShell.Commands
{
    public abstract class SamCommandBase : PSCmdletEx, IDisposable
    {
        // TODO: Safe Critical everywhere?
        private const string DefaultServer = "localhost";
        private string server;

        protected SamServer SamServer
        {
            get;
            private set;
        }

        #region Parameters
        [Parameter(
            HelpMessage = "Specify the user account credentials to use to perform this task. The default credentials are the credentials of the currently logged on user.",
            Mandatory = false,
            ValueFromPipeline = false
        )]
        [ValidateNotNullOrEmpty]
        [Credential]
        public PSCredential Credential
        {
            get;
            set;
        }

        [Parameter(
            HelpMessage = "Specify the name of a SAM server.",
            Mandatory = false,
            ValueFromPipeline = false
        )]
        [Alias("ComputerName", "Computer")]
        [PSDefaultValue(Value = DefaultServer)]
        [ValidateNotNullOrEmpty]
        public string Server
        {
            get
            {
                return this.server ?? DefaultServer;
            }
            set
            {
                this.server = value;
            }
        }

        #endregion Parameters

        #region Cmdlet Overrides

        protected override void BeginProcessing()
        {
            /* Connect to the specified SAM server: */
            WriteDebug(string.Format("Connecting to SAM server {0}.", this.Server));
            try
            {
                NetworkCredential netCred = this.Credential?.GetNetworkCredential();
                this.SamServer = new SamServer(this.Server, SamServerAccessMask.LookupDomain | SamServerAccessMask.EnumerateDomains, netCred);
            }
            catch (Win32Exception ex)
            {
                ErrorCategory category = ((Win32ErrorCode)ex.NativeErrorCode).ToPSCategory();
                ErrorRecord error = new ErrorRecord(ex, "WinAPIErrorConnect", category, this.Server);
                // Terminate on this error:
                this.ThrowTerminatingError(error);
            }
        }
        #endregion Cmdlet Overrides

        public void Dispose()
        {
            this.Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.SamServer != null)
            {
                this.SamServer.Dispose();
                this.SamServer = null;
            }
        }
    }
}
