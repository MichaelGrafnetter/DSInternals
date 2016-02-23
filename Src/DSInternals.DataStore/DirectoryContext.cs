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
        private string attachedDatabasePath;

        private static readonly Version Win8Version = new Version(6, 2);
        private static readonly Version Win81Version = new Version(6, 3);

        /// <summary>
        ///
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

            ValidateDatabaseState(dbFilePath);

            string dbDirectoryPath = Path.GetDirectoryName(dbFilePath);
            string checkpointDirectoryPath = dbDirectoryPath;
            string tempDirectoryPath = dbDirectoryPath;
            if (logDirectoryPath != null)
            {
                if (!Directory.Exists(logDirectoryPath))
                {
                    // TODO: Extract as resource
                    throw new FileNotFoundException("The specified log directory does not exist.", logDirectoryPath);
                }
            }
            else
            {
                logDirectoryPath = dbDirectoryPath;
            }
            // TODO: Exception handling?
            // HACK: IsamInstance constructor throws AccessDenied Exception when the path does not end with a backslash.
            this.instance = new IsamInstance(AddPathSeparator(checkpointDirectoryPath), AddPathSeparator(logDirectoryPath), AddPathSeparator(tempDirectoryPath), ADConstants.EseBaseName, JetInstanceName, readOnly, ADConstants.PageSize);
            try
            {
                var isamParameters = this.instance.IsamSystemParameters;
                // TODO: Add param explanations
                isamParameters.LogFileSize = ADConstants.EseLogFileSize;
                isamParameters.DeleteOutOfRangeLogs = true;
                isamParameters.EnableIndexChecking = 1;
                isamParameters.EnableIndexCleanup = true;
                isamParameters.CircularLog = true;
                // TODO: Configure additional ISAM parameters
                // this.instance.IsamSystemParameters.EnableOnlineDefrag = false;
                // JET_paramDeleteOldLogs =  1
                this.session = this.instance.CreateSession();
                this.session.AttachDatabase(dbFilePath);
                this.attachedDatabasePath = dbFilePath;
                this.database = this.session.OpenDatabase(dbFilePath);
                this.Schema = new DirectorySchema(this.database);
                this.SecurityDescriptorRersolver = new SecurityDescriptorRersolver(this.database);
                this.DistinguishedNameResolver = new DistinguishedNameResolver(this.database, this.Schema);
                this.LinkResolver = new LinkResolver(this.database, this.Schema);
                this.DomainController = new DomainController(this);
            }
            catch
            {
                // Free resources if anything failed
                this.Dispose();
                throw;
            }
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
                if (this.attachedDatabasePath != null)
                {
                    this.session.DetachDatabase(this.attachedDatabasePath);
                    this.attachedDatabasePath = null;
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
            
            // Get current Windows version and trim the build number
            var currentWinVer = Environment.OSVersion.Version;
            var currentWinVerTrimmed = new Version(currentWinVer.Major, currentWinVer.Minor);

            // Get the version of Windows that has last modified this DIT file
            var dbVersion = new Version(dbInfo.dwMajorVersion, dbInfo.dwMinorVersion);

            // Now compare those 2 versions
            if(dbVersion != currentWinVerTrimmed)
            {
                //HACK: There is a bug in Esentutl on Windows 6.3. It incorrectly puts version 6.2 into the ntds.dit file.
                if(!(dbVersion == Win8Version && currentWinVerTrimmed == Win81Version))
                {
                    // TODO: Extract message as a recource
                    throw new InvalidDatabaseStateException(String.Format("The database comes from a different Windows version ({0}). Try defragmenting it first by running the 'esentutl /d ntds.dit' command.", dbVersion), dbFilePath);
                }
            }
        }
    }
}