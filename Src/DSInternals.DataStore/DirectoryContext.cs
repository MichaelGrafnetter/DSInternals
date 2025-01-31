namespace DSInternals.DataStore
{
    using System;
    using System.IO;
    using Microsoft.Database.Isam;
    using Microsoft.Isam.Esent.Interop;

    public class DirectoryContext : IDisposable
    {
        private const string JetInstanceNameFormat = "DSInternals-{0:D}";

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
                 throw new FileNotFoundException("The specified database file does not exist.", dbFilePath);
            }

            this.DSADatabaseFile = dbFilePath;

            // Retrieve info about the DB (Win Version, Page Size, State,...)
            JET_DBINFOMISC dbInfo;
            Api.JetGetDatabaseFileInfo(dbFilePath, out dbInfo, JET_DbInfo.Misc);

            if (dbInfo.dbstate != JET_dbstate.CleanShutdown)
            {
                // Database might be inconsistent
                throw new InvalidDatabaseStateException("The database is not in a clean state. Try to recover it first by running the 'esentutl /r edb /d' command.", dbFilePath);
            }

            this.PageSize = dbInfo.cbPageSize;

            this.DSAWorkingDirectory = Path.GetDirectoryName(this.DSADatabaseFile);
            string checkpointDirectoryPath = this.DSAWorkingDirectory;
            string tempDatabasePath = Path.Combine(this.DSAWorkingDirectory, ADConstants.EseTempDatabaseName);

            this.DatabaseLogFilesPath = logDirectoryPath;
            if (this.DatabaseLogFilesPath != null)
            {
                if (!Directory.Exists(this.DatabaseLogFilesPath))
                {
                    throw new FileNotFoundException("The specified log directory does not exist.", this.DatabaseLogFilesPath);
                }
            }
            else
            {
                // Use the default location if an alternate log directory is not provided.
                this.DatabaseLogFilesPath = this.DSAWorkingDirectory;
            }

            // Achieve instance name uniquness by appending a GUID to "DSInternals-"
            string jetInstanceName = String.Format(JetInstanceNameFormat, Guid.NewGuid());

            // Note: IsamInstance constructor throws AccessDenied Exception when the path does not end with a backslash.
            this.instance = new IsamInstance(AddPathSeparator(checkpointDirectoryPath), AddPathSeparator(this.DatabaseLogFilesPath), tempDatabasePath, ADConstants.EseBaseName, jetInstanceName, readOnly, this.PageSize);

            try
            {
                var isamParameters = this.instance.IsamSystemParameters;

                // Set the size of the transaction log files to AD defaults.
                isamParameters.LogFileSize = ADConstants.EseLogFileSize;

                // Delete the log files that are not matching (generation wise) during soft recovery.
                isamParameters.DeleteOutOfRangeLogs = true;

                // Check the database for indexes over Unicode key columns that were built using an older version of the NLS library.
                isamParameters.EnableIndexChecking2 = true;

                // Automatically clean up indexes over Unicode key columns as necessary to avoid database format changes caused by changes to the NLS library.
                isamParameters.EnableIndexCleanup = true;

                // Retain only transaction log files that are younger than the current checkpoint.
                isamParameters.CircularLog = true;

                // Disable all database engine callbacks to application provided functions. This enables us to open Win2016 DBs on non-DC systems.
                isamParameters.DisableCallbacks = true;

                // Increase the limit of maximum open tables.
                isamParameters.MaxOpenTables = ADConstants.EseMaxOpenTables;

                // Enable backwards compatibility with the file naming conventions of earlier releases of the database engine.
                isamParameters.LegacyFileNames = ADConstants.EseLegacyFileNames;

                // Set EN-US to be used by any index over a Unicode key column.
                isamParameters.UnicodeIndexDefault = new JET_UNICODEINDEX()
                {
                    lcid = ADConstants.EseIndexDefaultLocale,
                    dwMapFlags = ADConstants.EseIndexDefaultCompareOptions
                };

                // Force crash recovery to look for the database referenced in the transaction log in the specified folder.
                isamParameters.AlternateDatabaseRecoveryPath = this.DSAWorkingDirectory;

                if (!readOnly)
                {
                    // Delete obsolete log files.
                    isamParameters.DeleteOldLogs = true;

                    if (EsentVersion.SupportsWindows10Features)
                    {
                        try
                        {
                            // Required for Windows Server 2022 compatibility, as it limits the transaction log file format to 8920.
                            // Note: Usage of JET_efvUsePersistedFormat still causes minor DB format upgrade.
                            isamParameters.EngineFormatVersion = 0x40000002; // JET_efvUsePersistedFormat: Instructs the engine to use the minimal Engine Format Version of all loaded log and DB files.
                        }
                        catch (EsentInvalidParameterException)
                        {
                            // JET_efvUsePersistedFormat should be supported since Windows Server 2016.
                            // Just continue even if it is not supported on the current Windows build.
                        }
                    }
                }

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
            catch (EsentFileAccessDeniedException e)
            {
                // This exception was probably thrown by the OpenDatabase method
                // HACK: Do not dispose the IsamSession object. Its Dispose method would throw an exception, which is a bug in ISAM.
                GC.SuppressFinalize(this.session);
                this.session = null;

                // Free resources if anything failed
                this.Dispose();

                throw;
            }
            catch (EsentErrorException e)
            {
                // Free resources if anything failed
                this.Dispose();

                // EsentUnicodeTranslationFailException - This typically happens while opening a Windows Server 2003 DIT on a newer system.
                // EsentSecondaryIndexCorruptedException - This typically happens when opening a Windows Server 2012 R2 DIT on Windows 7.
                throw new InvalidDatabaseStateException("There was a problem reading the database, which probably comes from a different OS. Try defragmenting it first by running the 'esentutl /d ntds.dit' command.", this.DSADatabaseFile, e);
            }
            catch
            {
                // Free resources if anything failed
                this.Dispose();
                throw;
            }
        }

        public int PageSize
        {
            get;
            private set;
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
    }
}
