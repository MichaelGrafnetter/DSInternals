//-----------------------------------------------------------------------
// <copyright file="DatabaseConfig.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Database.Isam.Config
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Reflection;
    using System.Threading;
    using Microsoft.Database.Isam.Win32;
    using Microsoft.Isam.Esent.Interop;
    using Microsoft.Isam.Esent.Interop.Vista;
    using Microsoft.Isam.Esent.Interop.Windows10;

    /// <summary>
    /// Represents a subset of JET_param definitions required for config classes.
    /// </summary>
    internal struct ParamDef
    {
        /// <summary>
        /// JET_param Id.
        /// </summary>
        public int Id;

        /// <summary>
        /// Can be set after global init?
        /// </summary>
        public bool StaticAfterGlobalInit;

        /// <summary>
        /// Managed type of the parameter.
        /// </summary>
        public Type ParamType;

        /// <summary>
        /// Initializes a new instance of the ParamDef struct.
        /// </summary>
        /// <param name="id"><see cref="JET_param"/> Id.</param>
        /// <param name="staticAfterGlobalInit">Can be set after global init?</param>
        /// <param name="paramType">Managed type of the param.</param>
        public ParamDef(int id, bool staticAfterGlobalInit, Type paramType)
        {
            this.Id = id;
            this.StaticAfterGlobalInit = staticAfterGlobalInit;
            this.ParamType = paramType;
        }
    }

    /// <summary>
    /// A class that contains all engine wide Ese parameters.
    /// </summary>
    public sealed partial class DatabaseConfig : ConfigSetBase
    {
        /// <summary>
        /// A table of JET_param definitions used by config classes.
        /// </summary>
        internal static readonly ParamDef[] ParamTable;

        /// <summary>
        /// Passed to JetGetSystemParameter for retrieving string parameters.
        /// </summary>
        private const int MaxStringParamSize = 260;

        /// <summary>
        /// A per-process lock to synchronize calls to SetGlobalParams()
        /// </summary>
        private static readonly Mutex GlobalParamsMutex;

        /// <summary>
        /// Initializes static members of the <see cref="DatabaseConfig" /> class.
        /// Used to initialize the param table.
        /// </summary>
        static DatabaseConfig()
        {
            // Create a per-process named mutex. C# locks are per app-domain, which could potentially screw up Ese global params that are process wide.
            DatabaseConfig.GlobalParamsMutex = new Mutex(false, "Microsoft.Isam.Esent.Interop.DatabaseConfig.globalParamsMutex_" + NativeMethods.GetCurrentProcessId());

            DatabaseConfig.ParamTable = new ParamDef[DatabaseConfig.ParamMaxValueInvalid];
            FillParamTable();
            UnpublishedFillParamTable();
        }

        /// <summary>
        /// Gets or sets a string identifier that uniquely identifies an instance of Database.
        /// </summary>
        public string Identifier
        {
            get { return this.GetEngineParam<string>(DatabaseParams.Identifier); }
            set { this.SetEngineParam(DatabaseParams.Identifier, value); }
        }

        /// <summary>
        /// Gets or sets a user-friendly name that is used to identify an instance of Database in system diagnostics (event log, etc).
        /// </summary>
        public string DisplayName
        {
            get { return this.GetEngineParam<string>(DatabaseParams.DisplayName); }
            set { this.SetEngineParam(DatabaseParams.DisplayName, value); }
        }

        /// <summary>
        /// Gets or sets flags used to select optional Engine behaviour at initialization. See <see cref ="CreateInstanceGrbit"/> and <see cref="Api.JetCreateInstance2"/>
        /// </summary>
        public CreateInstanceGrbit EngineFlags
        {
            get { return this.GetEngineParam<CreateInstanceGrbit>(DatabaseParams.EngineFlags); }
            set { this.SetEngineParam(DatabaseParams.EngineFlags, value); }
        }

        /// <summary>
        /// Gets or sets flags used to select optional behaviour while initializing an instance in the Engine and recovering the database from the log stream. See <see cref ="InitGrbit"/> and <see cref="Api.JetInit2"/>
        /// </summary>
        public InitGrbit DatabaseRecoveryFlags
        {
            get { return this.GetEngineParam<InitGrbit>(DatabaseParams.DatabaseRecoveryFlags); }
            set { this.SetEngineParam(DatabaseParams.DatabaseRecoveryFlags, value); }
        }

        /// <summary>
        /// Gets or sets a path to use for creating or opening the database file.
        /// </summary>
        public string DatabaseFilename
        {
            get { return this.GetEngineParam<string>(DatabaseParams.DatabaseFilename); }
            set { this.SetEngineParam(DatabaseParams.DatabaseFilename, value); }
        }

        /// <summary>
        /// Gets or sets flags used to select optional behaviour while creating a new database file. See <see cref ="CreateDatabaseGrbit"/> and <see cref="Api.JetCreateDatabase2"/>
        /// </summary>
        public CreateDatabaseGrbit DatabaseCreationFlags
        {
            get { return this.GetEngineParam<CreateDatabaseGrbit>(DatabaseParams.DatabaseCreationFlags); }
            set { this.SetEngineParam(DatabaseParams.DatabaseCreationFlags, value); }
        }

        /// <summary>
        /// Gets or sets flags used to select optional behaviour while attaching a database file to the Engine. See <see cref ="AttachDatabaseGrbit"/> and <see cref="Api.JetAttachDatabase2"/>
        /// </summary>
        public AttachDatabaseGrbit DatabaseAttachFlags
        {
            get { return this.GetEngineParam<AttachDatabaseGrbit>(DatabaseParams.DatabaseAttachFlags); }
            set { this.SetEngineParam(DatabaseParams.DatabaseAttachFlags, value); }
        }

        /// <summary>
        /// Gets or sets flags used to select optional behaviour while terminating an instance in the Engine. See <see cref ="TermGrbit"/> and <see cref="Api.JetTerm2"/>
        /// </summary>
        public TermGrbit DatabaseStopFlags
        {
            get { return this.GetEngineParam<TermGrbit>(DatabaseParams.DatabaseStopFlags); }
            set { this.SetEngineParam(DatabaseParams.DatabaseStopFlags, value); }
        }

        /// <summary>
        /// Gets or sets the maximum size of the database in pages. Zero means there is no maximum. See maxPages parameter for <see cref ="Api.JetCreateDatabase2"/> or <see cref="Api.JetAttachDatabase2"/>
        /// </summary>
        public int DatabaseMaxPages
        {
            get { return this.GetEngineParam<int>(DatabaseParams.DatabaseMaxPages); }
            set { this.SetEngineParam(DatabaseParams.DatabaseMaxPages, value); }
        }

        /// <summary>
        /// Sets global parameters before an Ese instance is created.
        /// </summary>
        public void SetGlobalParams()
        {
            // Lock this section using a process-wide lock, so in case of a race between two threads, a config set is either completely set or completely over-written
            DatabaseConfig.GlobalParamsMutex.WaitOne();
            try
            {
                for (int i = 0; i < DatabaseConfig.ParamTable.Length; i++)
                {
                    // This check ensures that only global params that must be set before global init are set here
                    // This protects instance params from being set before instance init. Setting them here would change the defaults for these params
                    // for all future instances. That would be really bad because the settings contained in the current config set would 'spill over'
                    // into future config sets.
                    if (DatabaseConfig.ParamTable[i].StaticAfterGlobalInit)
                    {
                        object valueToSet;
                        if (this.ParamStore.TryGetValue(i, out valueToSet))
                        {
                            object currValue;
                            DatabaseConfig.TryGetParamFromInstance(JET_INSTANCE.Nil, i, out currValue);
                            if (!currValue.Equals(valueToSet))
                            {
                                DatabaseConfig.SetParamOnInstance(JET_INSTANCE.Nil, i, valueToSet);
                            }
                        }
                    }
                }
            }
            finally
            {
                DatabaseConfig.GlobalParamsMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Sets instance specific parameters after an Ese instance has been created.
        /// </summary>
        /// <param name="instance">The instance handle to use.</param>
        public void SetInstanceParams(JET_INSTANCE instance)
        {
            Action<DatabaseConfig, JET_INSTANCE, int> setParamIfExists = (engineConfig, inst, param) =>
            {
                object value;
                if (engineConfig.ParamStore.TryGetValue(param, out value))
                {
                    DatabaseConfig.SetParamOnInstance(inst, param, value);
                }
            };

            // First, set parameters that require to be set in a particular order
#if !ESENT && !MANAGEDESENT_ON_WSA
            setParamIfExists(this, instance, (int)Windows10Param.ConfigStoreSpec);
#endif
            setParamIfExists(this, instance, (int)VistaParam.EnableAdvanced);
            setParamIfExists(this, instance, (int)VistaParam.Configuration);

            for (int i = 0; i < DatabaseConfig.ParamTable.Length; i++)
            {
                if (!DatabaseConfig.ParamTable[i].StaticAfterGlobalInit
#if !ESENT && !MANAGEDESENT_ON_WSA
                    && i != (int)Windows10Param.ConfigStoreSpec
#endif
                    && i != (int)VistaParam.EnableAdvanced
                    && i != (int)VistaParam.Configuration)
                {
                    setParamIfExists(this, instance, i);
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="DatabaseConfig"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="DatabaseConfig"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "DatabaseConfig( {0} ) [Id: {1}]", this.DisplayName, this.Identifier);
        }

        /// <summary>
        /// Get a <see cref="JET_param"/> from a particular instance.
        /// </summary>
        /// <param name="instance">The instance handle to use.</param>
        /// <param name="param">The param id.</param>
        /// <param name="value">The param value.</param>
        /// <returns>True for all instance params. False otherwise.</returns>
        internal static bool TryGetParamFromInstance(JET_INSTANCE instance, int param, out object value)
        {
            value = null;
            if (param >= DatabaseConfig.ParamMaxValueInvalid)
            {
                return false;
            }

            JET_wrn wrn;
            var paramDef = DatabaseConfig.ParamTable[param];
            if (paramDef.ParamType == typeof(string))
            {
                string strValue;
                var intValue = new IntPtr(0);
                wrn = Api.JetGetSystemParameter(instance, JET_SESID.Nil, (JET_param)param, ref intValue, out strValue, DatabaseConfig.MaxStringParamSize);
                value = strValue;
            }
            else
            {
                string strValue;
                var intValue = new IntPtr(0);
                wrn = Api.JetGetSystemParameter(instance, JET_SESID.Nil, (JET_param)param, ref intValue, out strValue, 0);
                if (paramDef.ParamType == typeof(int))
                {
                    value = intValue.ToInt32();
                }
                else if (paramDef.ParamType == typeof(bool))
                {
                    value = Convert.ToBoolean(intValue.ToInt32());
                }
                else if (paramDef.ParamType.IsEnum)
                {
                    value = Enum.ToObject(paramDef.ParamType, intValue.ToInt32());
                }
                else if (paramDef.ParamType == typeof(IntPtr))
                {
                    value = intValue;
                }
                else
                {
                    throw new NotSupportedException(string.Format("Can't get param {0}. Type {1} isn't supported.", paramDef.Id, paramDef.ParamType));
                }
            }

            Contract.Ensures(wrn == JET_wrn.Success);
            return true;
        }

        /// <summary>
        /// Sets a <see cref="JET_param"/> on a particular instance.
        /// </summary>
        /// <param name="instance">The instance handle to use.</param>
        /// <param name="param">The param id.</param>
        /// <param name="value">The param value.</param>
        internal static void SetParamOnInstance(JET_INSTANCE instance, int param, object value)
        {
            Contract.Requires(param < DatabaseConfig.ParamMaxValueInvalid);
            JET_wrn wrn;
            var paramDef = DatabaseConfig.ParamTable[param];
            Contract.Requires(paramDef.ParamType == value.GetType());

            if (paramDef.ParamType == typeof(string))
            {
                wrn = Api.JetSetSystemParameter(instance, JET_SESID.Nil, (JET_param)param, 0, (string)value);
            }
            else
            {
                IntPtr intValue;
                if (paramDef.ParamType == typeof(int) || paramDef.ParamType.IsEnum)
                {
                    intValue = new IntPtr((int)value);
                }
                else if (paramDef.ParamType == typeof(bool))
                {
                    intValue = new IntPtr(Convert.ToInt32((bool)value));
                }
                else if (paramDef.ParamType == typeof(IntPtr))
                {
                    intValue = (IntPtr)value;
                }
                else
                {
                    throw new NotSupportedException(string.Format("Can't set param {0}. Type {1} isn't supported.", paramDef.Id, paramDef.ParamType));
                }

                wrn = Api.JetSetSystemParameter(instance, JET_SESID.Nil, (JET_param)param, intValue, null);
            }

            Contract.Ensures(wrn == JET_wrn.Success);
        }

        /// <summary>
        /// Partial method for filling the param table from auto-generated code.
        /// </summary>
        static partial void FillParamTable();

        /// <summary>
        /// Partial method for filling the param table from auto-generated code.
        /// </summary>
        static partial void UnpublishedFillParamTable();

        /// <summary>
        /// Helper method to get a custom defined engine param.
        /// </summary>
        /// <typeparam name="T">The type of the engine param.</typeparam>
        /// <param name="param">The engine param to get.</param>
        /// <returns>The engine param value.</returns>
        private T GetEngineParam<T>(DatabaseParams param)
        {
            // Engine params are always accessed from the config store dictionary (even if the config set is live)
            return ConfigSetBase.GetValueOrDefault<T>(this.ParamStore.TryGetValue, (int)param);
        }

        /// <summary>
        /// Helper method to set a custom defined engine param.
        /// </summary>
        /// <param name="param">The engine param to set.</param>
        /// <param name="value">The engine param value.</param>
        private void SetEngineParam(DatabaseParams param, object value)
        {
            // Engine params can't be set once the config set goes live
            if (this.SetParamDelegate == null)
            {
                this.ParamStore[(int)param] = value;
            }
            else
            {
                throw new EsentAlreadyInitializedException();
            }
        }
    }
}
