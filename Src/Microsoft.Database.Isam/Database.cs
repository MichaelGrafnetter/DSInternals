//-----------------------------------------------------------------------
// <copyright file="Database.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Database.Isam
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using Microsoft.Database.Isam.Config;
    using Microsoft.Isam.Esent.Interop;

    /// <summary>
    /// Miscellaneous parameters that are specified during the span of instance initialization and database attach.
    /// They are similar to system parameters (instance-wide scope) but are not part of JET_param*.
    /// This enum shares number space with JET_param* (and we would like to avoid overlapping with JET_sesparam*
    /// just to avoid confusion). So we start at 8192 to give us ample cushion.
    /// </summary>
    internal enum DatabaseParams
    {
        /// <summary>
        /// A string identifier that uniquely identifies an instance of Database
        /// </summary>
        Identifier = 8192,

        /// <summary>
        /// A user-friendly name that is used to identify an instance of Database in system diagnostics (event log etc).
        /// </summary>
        DisplayName,

        /// <summary>
        /// Gets or sets flags used to select optional Engine behaviour at initialization. See <see cref ="CreateInstanceGrbit"/> and <see cref="Api.JetCreateInstance2"/>
        /// </summary>
        EngineFlags,

        /// <summary>
        /// Gets or sets the used to select optional behaviour while initializing the engine and recoverying the database from the log stream. See <see cref ="InitGrbit"/> and <see cref="Api.JetInit2"/>
        /// </summary>
        DatabaseRecoveryFlags,

        /// <summary>
        /// Specifies a path to use for creating or opening the database file.
        /// </summary>
        DatabaseFilename,

        /// <summary>
        /// Gets or sets flags used to select optional behaviour while creating a new database file. See <see cref ="CreateDatabaseGrbit"/> and <see cref="Api.JetCreateDatabase2"/>
        /// </summary>
        DatabaseCreationFlags,

        /// <summary>
        /// Gets or sets flags used to select optional behaviour while attaching a database file to the Engine. See <see cref ="AttachDatabaseGrbit"/> and <see cref="Api.JetAttachDatabase2"/>
        /// </summary>
        DatabaseAttachFlags,

        /// <summary>
        /// Gets or sets the used to select optional behaviour while terminating / shutting the engine. See <see cref ="TermGrbit"/> and <see cref="Api.JetTerm2"/>
        /// </summary>
        DatabaseStopFlags,

        /// <summary>
        /// Gets or sets the maximum size of the database in pages. Zero means there is no maximum. See maxPages parameter for <see cref ="Api.JetCreateDatabase2"/> or <see cref="Api.JetAttachDatabase2"/>
        /// </summary>
        DatabaseMaxPages,
    }

    /// <summary>
    /// A class that encapsulate an online Ese database.
    /// </summary>
    public sealed class Database : IDisposable
    {
        /// <summary>
        /// A variable for generating instance display names.
        /// </summary>
        private static int instanceCounter;

        /// <summary>
        /// True if the instance handle is owned by this engine. Dispose()/Stop() will only call JetTerm() if this is true.
        /// </summary>
        private readonly bool ownsInstance = true;

        /// <summary>
        /// The configuration object associated with the Engine.
        /// </summary>
        private readonly DatabaseConfig config;

        /// <summary>
        /// The instance handle associated with this engine.
        /// </summary>
        private JET_INSTANCE instance = JET_INSTANCE.Nil;

        /// <summary>
        /// Initializes a new instance of the Database class.
        /// </summary>
        /// <param name="databaseFilename">Specifies a path to use for creating or opening a database file to use with the engine.</param>
        public Database(string databaseFilename) : this(databaseFilename, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Database class.
        /// </summary>
        /// <param name="customConfig">A custom config set to use with the engine.</param>
        public Database(IConfigSet customConfig) : this(null, customConfig)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Database class.
        /// </summary>
        /// <param name="databaseFilename">Specifies a path to use for creating or opening a database file to use with the engine.</param>
        /// <param name="customConfig">A custom config set to use with the engine.</param>
        public Database(string databaseFilename, IConfigSet customConfig)
        {
            if (string.IsNullOrEmpty(databaseFilename) && customConfig == null)
            {
                throw new ArgumentException("Must specify a valid databaseFilename or customConfig");
            }

            this.config = new DatabaseConfig();
            if (!string.IsNullOrEmpty(databaseFilename))
            {
                this.config.DatabaseFilename = databaseFilename;
                string systemPath = Path.GetDirectoryName(databaseFilename);
                this.config.SystemPath = systemPath;
                this.config.LogFilePath = systemPath;
                this.config.TempPath = systemPath;
                this.config.AlternateDatabaseRecoveryPath = systemPath;
            }

            if (customConfig != null)
            {
                this.config.Merge(customConfig);    // throw on conflicts
            }

            if (string.IsNullOrEmpty(this.config.Identifier))
            {
                this.config.Identifier = Guid.NewGuid().ToString();
            }

            if (string.IsNullOrEmpty(this.config.DisplayName))
            {
                this.config.DisplayName = string.Format("Database Inst{0:D2}", Interlocked.Increment(ref Database.instanceCounter) - 1);
            }

            this.Start();
        }

        /// <summary>
        /// Initializes a new instance of the Database class.
        /// </summary>
        /// <param name="instance">An initialized instance to be used with Database. The instance should have a database attached and ready to use.</param>
        /// <param name="ownsInstance">True if the instance handle passed into the constructur should be owned by the Database.</param>
        /// <param name="customConfig">A custom config set to use with the engine. The config set should atleast contain the attached database filename.</param>
        /// <remarks>Database will only manage the handle lifetime if ownsInstance is set to true. If its set to false, the caller is responsible for managing the teardown of the instance.</remarks>
        public Database(JET_INSTANCE instance, bool ownsInstance, IConfigSet customConfig)
        {
            this.config = new DatabaseConfig();
            this.config.Merge(customConfig);
            this.instance = instance;
            this.ownsInstance = ownsInstance;

            // Ensure that there is an attached database at a path specified by the config set
            using (var session = new Session(this.instance))
            {
                JET_DBID dbid;
                JET_wrn wrn = Api.JetOpenDatabase(session, this.config.DatabaseFilename, null, out dbid, OpenDatabaseGrbit.ReadOnly);
                Contract.Ensures(wrn == JET_wrn.Success);
                Api.JetCloseDatabase(session, dbid, CloseDatabaseGrbit.None);
            }

            // The config set is live now
            this.config.GetParamDelegate = this.TryGetParam;
            this.config.SetParamDelegate = this.SetParam;
        }

        /// <summary>
        /// Gets the configuration object associated with the Engine.
        /// </summary>
        public DatabaseConfig Config
        {
            get { return this.config; }
        }

        /// <summary>
        /// Gets the instance handle associated with this engine.
        /// </summary>
        public JET_INSTANCE InstanceHandle
        {
            get { return this.instance; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="Database"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="Database"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Database( {0} ) [Id: {1}]", this.config.DisplayName, this.config.Identifier);
        }

        /// <summary>
        /// Terminates the Database gracefully.
        /// All pending updates to the database are flushed to the disk and associated resources are freed.
        /// </summary>
        public void Dispose()
        {
            this.Term();
        }

        /// <summary>
        /// Terminates the Database. The exact behaviour of the termination process depends on the <see cref="TermGrbit"/> passed to the function.
        /// </summary>
        public void Term()
        {
            if (this.ownsInstance && this.instance != JET_INSTANCE.Nil)
            {
                // The TermGrbit.None should be equivalent to TermGrbit.Complete, but to stay consistent
                // with JetInit[2]() mapping to JetInit2() mapping in jetapi, we just translate it.
                Api.JetTerm2(this.instance, this.config.DatabaseStopFlags == TermGrbit.None ? TermGrbit.Complete : this.config.DatabaseStopFlags);
                this.instance = JET_INSTANCE.Nil;
            }
        }

        /// <summary>
        /// Initializes the Database and gets it into a state where it can support application use of the database.
        /// After this method completes, the specified database file is attached and ready for use.
        /// </summary>
        private void Start()
        {
            this.config.SetGlobalParams();
            Api.JetCreateInstance2(out this.instance, this.config.Identifier, this.config.DisplayName, this.config.EngineFlags);
            this.config.SetInstanceParams(this.instance);

            // The config set is live now
            this.config.GetParamDelegate = this.TryGetParam;
            this.config.SetParamDelegate = this.SetParam;

            Api.JetInit2(ref this.instance, this.config.DatabaseRecoveryFlags);

            using (var session = new Session(this.instance))
            {
                try
                {
                    Api.JetAttachDatabase2(session, this.config.DatabaseFilename, this.config.DatabaseMaxPages, this.config.DatabaseAttachFlags);
                }
                catch (EsentFileNotFoundException)
                {
                    JET_DBID dbid;
                    Api.JetCreateDatabase2(session, this.config.DatabaseFilename, this.config.DatabaseMaxPages, out dbid, this.config.DatabaseCreationFlags);
                    Api.JetCloseDatabase(session, dbid, CloseDatabaseGrbit.None);
                }
            }
        }

        /// <summary>
        /// Get a JET_param from the associated instance.
        /// </summary>
        /// <param name="param">The param id.</param>
        /// <param name="value">The param value.</param>
        /// <returns>True for all instance params. False otherwise.</returns>
        private bool TryGetParam(int param, out object value)
        {
            return DatabaseConfig.TryGetParamFromInstance(this.instance, param, out value);
        }

        /// <summary>
        /// Sets a JET_param on the associated instance.
        /// </summary>
        /// <param name="param">The param id.</param>
        /// <param name="value">The param value.</param>
        private void SetParam(int param, object value)
        {
            DatabaseConfig.SetParamOnInstance(this.instance, param, value);
        }
    }
}
