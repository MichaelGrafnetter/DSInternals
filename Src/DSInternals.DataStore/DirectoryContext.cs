namespace DSInternals.DataStore
{
    using System;
    using System.IO;
    using Microsoft.Database.Isam;
    using Microsoft.Isam.Esent.Interop;

    public class DirectoryContext : IDisposable
    {
        private const string JetInstanceName = "DSInternals";

        private IsamInstance instance;
        private IsamSession session;
        private IsamDatabase database;
        private bool isDBAttached = false;

        /// <summary>
        /// Creates a new Active Directory database context.
        /// </summary>
        /// <param name="dbPath">dbFilePath must point to the DIT file on the local computer.</param>
        /// <param name="logPath">The path should point to a writeable folder on the local computer, where ESE log files will be created. If not specified, then temp folder will be used.</param>
        public DirectoryContext(string dbFilePath, bool readOnly, string logDirectoryPath = null)
        {
            if (!File.Exists(dbFilePath))
            {
                // TODO: Extract as resource
                throw new FileNotFoundException("The specified database file does not exist.", dbFilePath);
            }

            this.DSADatabaseFile = dbFilePath;
            ValidateDatabaseState(this.DSADatabaseFile);

            this.DSAWorkingDirectory = Path.GetDirectoryName(this.DSADatabaseFile);
            string checkpointDirectoryPath = this.DSAWorkingDirectory;
            string tempDirectoryPath = this.DSAWorkingDirectory;

            this.DatabaseLogFilesPath = logDirectoryPath;
            if (this.DatabaseLogFilesPath != null)
            {
                if (!Directory.Exists(this.DatabaseLogFilesPath))
                {
                    // TODO: Extract as resource
                    throw new FileNotFoundException("The specified log directory does not exist.", this.DatabaseLogFilesPath);
                }
            }
            else
            {
                this.DatabaseLogFilesPath = this.DSAWorkingDirectory;
            }

            // TODO: Exception handling?
            // HACK: IsamInstance constructor throws AccessDenied Exception when the path does not end with a backslash.
            this.instance = new IsamInstance(AddPathSeparator(checkpointDirectoryPath), AddPathSeparator(this.DatabaseLogFilesPath), AddPathSeparator(tempDirectoryPath), ADConstants.EseBaseName, JetInstanceName, readOnly, ADConstants.PageSize);
            try
            {
                var isamParameters = this.instance.IsamSystemParameters;

                // Set the size of the transaction log files to AD defaults.
                isamParameters.LogFileSize = ADConstants.EseLogFileSize;

                // Delete the log files that are not matching (generation wise) during soft recovery.
                isamParameters.DeleteOutOfRangeLogs = true;

                // Check the database for indexes over Unicode key columns that were built using an older version of the NLS library.
                isamParameters.EnableIndexChecking = true;

                // Automatically clean up indexes over Unicode key columns as necessary to avoid database format changes caused by changes to the NLS library.
                isamParameters.EnableIndexCleanup = true;

                // Retain only transaction log files that are younger than the current checkpoint.
                isamParameters.CircularLog = true;

                // Disable all database engine callbacks to application provided functions. This enables us to open Win2016 DBs on non-DC systems.
                isamParameters.DisableCallbacks = true;

                // TODO: Configure additional ISAM parameters
                // this.instance.IsamSystemParameters.EnableOnlineDefrag = false;

                this.session = this.instance.CreateSession();
                this.session.AttachDatabase(this.DSADatabaseFile);
                this.isDBAttached = true;
                this.database = this.session.OpenDatabase(this.DSADatabaseFile);
                this.Schema = new DirectorySchema(this.database);
                this.SecurityDescriptorRersolver = new SecurityDescriptorRersolver(this.database);
                this.DistinguishedNameResolver = new DistinguishedNameResolver(this.database, this.Schema);
                this.LinkResolver = new LinkResolver(this.database, this.Schema);
                this.DomainController = new DomainController(this);
            }
            catch (EsentErrorException e)
            {
                // EsentUnicodeTranslationFailException - This typically happens while opening a Windows Server 2003 DIT on a newer system.
                // EsentSecondaryIndexCorruptedException - This typically happens when opening a Windows Server 2012 R2 DIT on Windows 7.
                this.Dispose();
                throw new InvalidDatabaseStateException("There was a problem reading the database, which probably comes from a different OS. Try defragmenting it first by running the 'esentutl /d ntds.dit' command.", this.DSADatabaseFile, e);
            }
            catch
            {
                // Free resources if anything failed
                this.Dispose();
                throw;
            }
        }

        public string DSAWorkingDirectory
        {
            get;
            private set;
        }

        public string DSADatabaseFile
        {
            get;
            private set;
        }

        public string DatabaseLogFilesPath
        {
            get;
            private set;
        }

        public DirectorySchema Schema
        {
            get;
            private set;
        }

        public LinkResolver LinkResolver
        {
            get;
            private set;
        }

        public DomainController DomainController
        {
            get;
            private set;
        }

        public DistinguishedNameResolver DistinguishedNameResolver
        {
            get;
            private set;
        }

        public SecurityDescriptorRersolver SecurityDescriptorRersolver
        {
            get;
            private set;
        }

        public Cursor OpenDataTable()
        {
            return this.database.OpenCursor(ADConstants.DataTableName);
        }

        public Cursor OpenLinkTable()
        {
            return this.database.OpenCursor(ADConstants.LinkTableName);
        }

        public Cursor OpenSystemTable()
        {
            return this.database.OpenCursor(ADConstants.SystemTableName);
        }

        public IsamTransaction BeginTransaction()
        {
            return new IsamTransaction(this.session);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                // Do nothing
                return;
            }

            if(this.LinkResolver != null)
            {
                this.LinkResolver.Dispose();
                this.LinkResolver = null;
            }

            if (this.SecurityDescriptorRersolver != null)
            {
                this.SecurityDescriptorRersolver.Dispose();
                this.SecurityDescriptorRersolver = null;
            }

            if (this.DistinguishedNameResolver != null)
            {
                this.DistinguishedNameResolver.Dispose();
                this.DistinguishedNameResolver = null;
            }

            if (this.DomainController != null)
            {
                this.DomainController.Dispose();
                this.DomainController = null;
            }

            if (this.database != null)
            {
                this.database.Dispose();
                this.database = null;
            }

            if (this.session != null)
            {
                if (this.isDBAttached)
                {
                    this.session.DetachDatabase(this.DSADatabaseFile);
                    this.isDBAttached = false;
                }

                this.session.Dispose();
                this.session = null;
            }

            if (this.instance != null)
            {
                this.instance.Dispose();
                this.instance = null;
            }
        }

        private static string AddPathSeparator(string path)
        {
            // TODO: Newer version of ISAM should implemet this
            if (string.IsNullOrEmpty(path) || path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                // No need to add path separator
                return path;
            }
            else
            {
                return path + Path.DirectorySeparatorChar;
            }
        }

        private static void ValidateDatabaseState(string dbFilePath)
        {
            // Retrieve info about the DB (Win Version, Page Size, State,...)
            JET_DBINFOMISC dbInfo;
            Api.JetGetDatabaseFileInfo(dbFilePath, out dbInfo, JET_DbInfo.Misc);

            if (dbInfo.dbstate != JET_dbstate.CleanShutdown)
            {
                // Database might be inconsistent
                // TODO: Extract message as a recource
                throw new InvalidDatabaseStateException("The database is not in a clean state. Try to recover it first by running the 'esentutl /r edb /d' command.", dbFilePath);
            }
        }
    }
}
