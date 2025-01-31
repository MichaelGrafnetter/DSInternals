namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using DSInternals.DataStore;

    public abstract class ADDBObjectCommandBase : ADDBCommandBase
    {
        protected const string ParameterSetByGuid = "ByGuid";
        protected const string ParameterSetByDN = "ByDN";

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = ParameterSetByDN)]
        [ValidateNotNullOrEmpty]
        [Alias("dn")]
        public string DistinguishedName
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = ParameterSetByGuid)]
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
