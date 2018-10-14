namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using DSInternals.DataStore;

    public abstract class ADDBObjectCommandBase : ADDBCommandBase
    {
        protected const string parameterSetByGuid = "ByGuid";
        protected const string parameterSetByDN = "ByDN";

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = parameterSetByDN)]
        [ValidateNotNullOrEmpty]
        [Alias("dn")]
        public string DistinguishedName
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = parameterSetByGuid)]
        [ValidateNotNullOrEmpty]
        [Alias("Guid")]
        public Guid ObjectGuid
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
                ErrorRecord error = new ErrorRecord(ex, "TableOpenError", ErrorCategory.OpenError, null);
                // Terminate on this error:
                this.ThrowTerminatingError(error);
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