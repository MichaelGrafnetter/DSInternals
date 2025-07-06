using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Database.Isam;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Server2003;
using Microsoft.Isam.Esent.Interop.Vista;
using Windows.Win32;

// The official NuGet package contains this struct as well, but it is not public. 
using NATIVE_UNICODEINDEX = Windows.Win32.Storage.Jet.JET_UNICODEINDEX;

namespace DSInternals.DataStore
{
    public static class IsamInstanceExtensions
    {
        public static IsamSystemParametersExt GetIsamSystemParametersExt(this IsamInstance managedInstance)
        {
            if (managedInstance == null)
            {
                throw new ArgumentNullException(nameof(managedInstance));
            }

            // HACK: Use reflection to get the private field
            JET_INSTANCE nativeInstance = (JET_INSTANCE) typeof(IsamInstance).GetField("instance", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(managedInstance);

            return new IsamSystemParametersExt(nativeInstance);
        }
    }

    /// <summary>
    // The IsamSystemParametersExt adds ESE parameters required by DSInternals to IsamSystemParameters.
    /// </summary>
    public class IsamSystemParametersExt
    {
        private readonly JET_INSTANCE _instance;

        public IsamSystemParametersExt(JET_INSTANCE instance) {
            this._instance = instance;
        }

        /// <summary>
        /// Indicates whether the database should be checked for indexes over Unicode key columns that were built using an older version of the NLS library..
        /// </summary>
        public bool EnableIndexChecking2
        {
            get
            {
                int numericValue = 0;
                Api.JetGetSystemParameter(this._instance, JET_SESID.Nil, JET_param.EnableIndexChecking, ref numericValue, out string ignored, 0);
                return Convert.ToBoolean(numericValue);
            }

            set
            {
                Api.JetSetSystemParameter(this._instance, JET_SESID.Nil, JET_param.EnableIndexChecking, Convert.ToInt32(value), null);
            }
        }

        /// <summary>
        /// Disables or enables all database engine callbacks to application provided functions.
        /// </summary>
        public bool DisableCallbacks
        {
            get
            {
                int numericValue = 0;
                // TODO: Add JET_paramDisableCallbacks to JET_param.
                Api.JetGetSystemParameter(this._instance, JET_SESID.Nil, (JET_param) PInvoke.JET_paramDisableCallbacks, ref numericValue, out string ignored, 0);
                return Convert.ToBoolean(numericValue);
            }
            set
            {
                Api.JetSetSystemParameter(this._instance, JET_SESID.Nil, (JET_param) PInvoke.JET_paramDisableCallbacks, Convert.ToInt32(value), null);
            }
        }

        /// <summary>
        /// Indicates whether transaction log files of an obsolete version will automatically be deleted.
        /// </summary>
        public bool DeleteOldLogs
        {
            get
            {
                int numericValue = 0;
                // TODO: Add JET_paramDeleteOldLogs to JET_param.
                Api.JetGetSystemParameter(this._instance, JET_SESID.Nil, (JET_param)PInvoke.JET_paramDeleteOldLogs, ref numericValue, out string ignored, 0);
                return Convert.ToBoolean(numericValue);
            }
            set
            {
                Api.JetSetSystemParameter(this._instance, JET_SESID.Nil, (JET_param)PInvoke.JET_paramDeleteOldLogs, Convert.ToInt32(value), null);
            }
        }

        /// <summary>
        /// Gets or sets the engine format version.
        /// </summary>
        public int EngineFormatVersion
        {
            get
            {
                int result = 0;
                // TODO: Add JET_paramEngineFormatVersion to JET_param.
                Api.JetGetSystemParameter(this._instance, JET_SESID.Nil, (JET_param)194, ref result, out string ignored, 0);
                return result;
            }
            set
            {
                Api.JetSetSystemParameter(this._instance, JET_SESID.Nil, (JET_param)194, value, null);
            }
        }

        /// <summary>
        /// Gets or sets the full path to the folder where crash recovery should look for the databases referenced in the transaction log.
        /// </summary>
        public string AlternateDatabaseRecoveryPath
        {
            get
            {
                int ignored = 0;
                string result;
                Api.JetGetSystemParameter(this._instance, JET_SESID.Nil, Server2003Param.AlternateDatabaseRecoveryPath, ref ignored, out result, 1024);
                return result;
            }

            set
            {
                Api.JetSetSystemParameter(this._instance, JET_SESID.Nil, Server2003Param.AlternateDatabaseRecoveryPath, 0, value);
            }
        }

        /// <summary>
        /// Gets or sets the options for legacy file names.
        /// </summary>
        public LegacyFileNames LegacyFileNames
        {
            get
            {
                int result = 0;
                Api.JetGetSystemParameter(this._instance, JET_SESID.Nil, VistaParam.LegacyFileNames, ref result, out string ignored, 0);
                return (LegacyFileNames)result;
            }
            set
            {
                Api.JetSetSystemParameter(this._instance, JET_SESID.Nil, VistaParam.LegacyFileNames, (int)value, null);
            }
        }

        /// <summary>
        /// Gets or sets the default Unicode parameters used by any index over a Unicode key column.
        /// </summary>
        internal NATIVE_UNICODEINDEX UnicodeIndexDefault
        {
            get
            {
                NATIVE_UNICODEINDEX result = default;
                var resultHandle = GCHandle.Alloc(result, GCHandleType.Pinned);
                try
                {
                    // TODO: Add JET_paramUnicodeIndexDefault to JET_param.
                    IntPtr resultPointer = resultHandle.AddrOfPinnedObject();
                    Api.JetGetSystemParameter(this._instance, JET_SESID.Nil, (JET_param) PInvoke.JET_paramUnicodeIndexDefault, ref resultPointer, out string ignored, Marshal.SizeOf<NATIVE_UNICODEINDEX>());
                }
                finally
                {
                    resultHandle.Free();
                }

                return result;
            }
            set
            {
                var nativeoptionsHandle = GCHandle.Alloc(value, GCHandleType.Pinned);
                try
                {
                    Api.JetSetSystemParameter(this._instance, JET_SESID.Nil, (JET_param) PInvoke.JET_paramUnicodeIndexDefault, nativeoptionsHandle.AddrOfPinnedObject(), null);
                }
                finally
                {
                    nativeoptionsHandle.Free();
                }
            }
        }
    }
}
