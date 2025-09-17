namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using DSInternals.DataStore;

    /// <summary>
    /// Provides a base class for PowerShell cmdlets that operate on Active Directory database files.
    /// </summary>
    public abstract class ADDBCommandBase : PSCmdletEx, IDisposable
    {
        /// <summary>
        /// Gets or sets the DIT database file path.
        /// </summary>
        /// <value>
        /// Path to the DIT file on the local server.
        /// </value>
        /// <remarks>
        /// The DIT must be in a consistent state, that is, the ESE logs must be replayed.
        /// </remarks>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [Alias("Database", "DBPath", "DatabaseFilePath", "DBFilePath")]
        public string DatabasePath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ESE transaction log folder. If not specified, then database folder will be used.
        /// </summary>
        /// <value>
        /// The log path.
        /// </value>
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        [Alias("Log", "TransactionLogPath")]
        public string LogPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the database should be opened in read-only mode.
        /// </summary>
        /// <value>true if the database should be opened read-only; otherwise, false. Default is true.</value>
        protected virtual bool ReadOnly
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the Active Directory database context.
        /// </summary>
        /// <value>The DirectoryContext object representing the opened database.</value>
        protected DirectoryContext DirectoryContext
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes the cmdlet by opening the Active Directory database.
        /// </summary>
        protected override void BeginProcessing()
        {
            // TODO: Debug output
            this.WriteDebug("Opening the Active Directory database.");

            try
            {
                // Resolve possibly relative paths to absolute paths:
                string dbPathResolved = this.ResolveFilePath(this.DatabasePath);
                string logPathResolved = this.ResolveDirectoryPath(this.LogPath);
                this.DirectoryContext = new DirectoryContext(dbPathResolved, this.ReadOnly, logPathResolved);
            }
            catch(SessionStateException ex)
            {
                // This may be DriveNotFoundException, ItemNotFoundException, ProviderNotFoundException, etc.
                // Terminate on this error:
                this.ThrowTerminatingError(new ErrorRecord(ex.ErrorRecord, ex));
            }
            catch (Exception ex)
            {
                ErrorRecord error = new ErrorRecord(ex, "DBContextError", ErrorCategory.OpenError, null);
                // Terminate on this error:
                this.ThrowTerminatingError(error);
            }
        }
        
        /// <summary>
        /// Releases all resources used by the ADDBCommandBase.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the ADDBCommandBase and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.DirectoryContext != null)
            {
                this.DirectoryContext.Dispose();
                this.DirectoryContext = null;
            }
        }
    }
}
