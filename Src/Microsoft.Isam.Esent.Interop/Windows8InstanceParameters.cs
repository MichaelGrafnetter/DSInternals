//-----------------------------------------------------------------------
// <copyright file="Windows8InstanceParameters.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Isam.Esent.Interop.Windows7;
    using Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// This class provides static properties to set and get
    /// per-instance ESENT system parameters.
    /// </summary>
    public partial class InstanceParameters
    {
        /// <summary>
        /// Gets or sets the percentage of version store that can be used by oldest transaction
        /// before <see cref="JET_err.VersionStoreOutOfMemory"/> (default = 100).
        /// </summary>
        public int MaxTransactionSize
        {
            get
            {
                return this.GetIntegerParameter(Windows8Param.MaxTransactionSize);
            }

            set
            {
                this.SetIntegerParameter(Windows8Param.MaxTransactionSize, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Database Maintenance should run during recovery.
        /// </summary>
        public int EnableDbScanInRecovery
        {
            get
            {
                return this.GetIntegerParameter(Windows7Param.EnableDbScanInRecovery);
            }

            set
            {
                this.SetIntegerParameter(Windows7Param.EnableDbScanInRecovery, value);
            }
        }          

        /// <summary>
        /// Gets or sets a value indicating whether database Maintenance
        /// serialization is enabled for databases sharing the same disk.
        /// </summary>
        public bool EnableDBScanSerialization
        {
            get
            {
                return this.GetBoolParameter(Windows8Param.EnableDBScanSerialization);
            }

            set
            {
                this.SetBoolParameter(Windows8Param.EnableDBScanSerialization, value);
            }
        }

        /// <summary>
        /// Gets or sets the throttling of the database scan, in milliseconds.
        /// </summary>
        public int DbScanThrottle
        {
            get
            {
                return this.GetIntegerParameter(Windows7Param.DbScanThrottle);
            }

            set
            {
                this.SetIntegerParameter(Windows7Param.DbScanThrottle, value);
            }
        }          

        /// <summary>
        /// Gets or sets the minimum interval to repeat the database scan, in seconds.
        /// </summary>
        public int DbScanIntervalMinSec
        {
            get
            {
                return this.GetIntegerParameter(Windows7Param.DbScanIntervalMinSec);
            }

            set
            {
                this.SetIntegerParameter(Windows7Param.DbScanIntervalMinSec, value);
            }
        }          

        /// <summary>
        /// Gets or sets the maximum interval to allow the database scan to finish, in seconds.
        /// </summary>
        public int DbScanIntervalMaxSec
        {
            get
            {
                return this.GetIntegerParameter(Windows7Param.DbScanIntervalMaxSec);
            }

            set
            {
                this.SetIntegerParameter(Windows7Param.DbScanIntervalMaxSec, value);
            }
        }          

        /// <summary>
        /// Gets or sets the per-instance property for relative cache priorities (default = 100).
        /// </summary>
        public int CachePriority
        {
            get
            {
                return this.GetIntegerParameter(Windows8Param.CachePriority);
            }

            set
            {
                this.SetIntegerParameter(Windows8Param.CachePriority, value);
            }
        }          

        /// <summary>
        /// Gets or sets the maximum number of I/O operations dispatched for a given purpose.
        /// </summary>
        public int PrereadIOMax
        {
            get
            {
                return this.GetIntegerParameter(Windows8Param.PrereadIOMax);
            }

            set
            {
                this.SetIntegerParameter(Windows8Param.PrereadIOMax, value);
            }
        }

        /// <summary>
        /// Gets the callback for log flush.
        /// </summary>
        /// <returns>The delegate that's called for log flush.</returns>
        internal NATIVE_JET_PFNDURABLECOMMITCALLBACK GetDurableCommitCallback()
        {
            NATIVE_JET_PFNDURABLECOMMITCALLBACK pfndurablecommit = null;

            IntPtr rawValue = this.GetIntPtrParameter(Windows8Param.DurableCommitCallback);
            if (rawValue != IntPtr.Zero)
            {
                pfndurablecommit = (NATIVE_JET_PFNDURABLECOMMITCALLBACK)Marshal.GetDelegateForFunctionPointer(rawValue, typeof(NATIVE_JET_PFNDURABLECOMMITCALLBACK));
            }

            return pfndurablecommit;
        }

        /// <summary>
        /// Sets the callback for log flush.
        /// It is the caller's responsibility to hold a reference to the delegate
        /// so that it doesn't get GC'ed.
        /// </summary>
        /// <param name="callback">
        /// The callback.
        /// </param>
        internal void SetDurableCommitCallback(
            NATIVE_JET_PFNDURABLECOMMITCALLBACK callback)
        {
            IntPtr nativeDelegate;
            if (callback != null)
            {
                nativeDelegate = Marshal.GetFunctionPointerForDelegate(callback);
            }
            else
            {
                nativeDelegate = IntPtr.Zero;
            }

            this.SetIntPtrParameter(Windows8Param.DurableCommitCallback, nativeDelegate);
        }

        /// <summary>
        /// Get a system parameter which is an IntPtr.
        /// </summary>
        /// <param name="param">The parameter to get.</param>
        /// <returns>The value of the parameter.</returns>
        private IntPtr GetIntPtrParameter(JET_param param)
        {
            IntPtr value = IntPtr.Zero;
            string ignored;
            Api.JetGetSystemParameter(this.instance, this.sesid, param, ref value, out ignored, 0);
            return value;
        }

        /// <summary>
        /// Set a system parameter which is an IntPtr.
        /// </summary>
        /// <param name="param">The parameter to set.</param>
        /// <param name="value">The value to set.</param>
        private void SetIntPtrParameter(JET_param param, IntPtr value)
        {
            Api.JetSetSystemParameter(this.instance, this.sesid, param, value, null);
        }
    }
}
