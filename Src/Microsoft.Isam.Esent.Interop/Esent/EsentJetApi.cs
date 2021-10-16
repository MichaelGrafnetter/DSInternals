//-----------------------------------------------------------------------
// <copyright file="EsentJetApi.cs" company="Microsoft Corporation">
//  Copyright (c) Microsoft Corporation.
// </copyright>
// <summary>
//  JetApi code that is specific to ESENT.
// </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Implementation
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Threading;

    /// <summary>
    /// JetApi code that is specific to ESENT.
    /// </summary>
    internal sealed partial class JetApi
    {
        /// <summary>
        /// Reports the exception to a central authority.
        /// </summary>
        /// <param name="exception">An unhandled exception.</param>
        /// <param name="description">A string description of the scenario.</param>
        internal static void ReportUnhandledException(
            Exception exception,
            string description)
        {
        }

        /// <summary>
        /// Calculates the capabilities of the current Esent version.
        /// </summary>
        private void DetermineCapabilities()
        {
            const int Server2003BuildNumber = 2700;
            const int VistaBuildNumber = 6000;
            const int Windows7BuildNumber = 7000; // includes beta as well as RTM (RTM is 7600)
            const int Windows8BuildNumber = 8000; // includes beta as well as RTM (RTM is 9200)
            const int Windows81BuildNumber = 9300; // includes beta as well as RTM (RTM is 9600)
            const int Windows10BuildNumber = 9900; // includes beta as well as RTM (RTM is 10240)

            // Create new capabilities, set as all false. This will allow
            // us to call into Esent.
            this.Capabilities = new JetCapabilities { ColumnsKeyMost = 12 };

            var version = this.versionOverride;
            if (version == 0)
            {
                version = this.GetVersionFromEsent();
            }

            var buildNumber = (int)((version & 0xFFFFFF) >> 8);

            Trace.WriteLineIf(
                TraceSwitch.TraceVerbose,
                string.Format(CultureInfo.InvariantCulture, "Version = {0}, BuildNumber = {1}", version, buildNumber));

            if (buildNumber >= Server2003BuildNumber)
            {
                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Supports Server 2003 features");
                this.Capabilities.SupportsServer2003Features = true;
            }

            if (buildNumber >= VistaBuildNumber)
            {
                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Supports Vista features");
                this.Capabilities.SupportsVistaFeatures = true;

                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Supports Unicode paths");
                this.Capabilities.SupportsUnicodePaths = true;

                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Supports large keys");
                this.Capabilities.SupportsLargeKeys = true;
                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Supports 16-column keys");
                this.Capabilities.ColumnsKeyMost = 16;
            }

            if (buildNumber >= Windows7BuildNumber)
            {
                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Supports Windows 7 features");
                this.Capabilities.SupportsWindows7Features = true;
            }

            if (buildNumber >= Windows8BuildNumber)
            {
                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Supports Windows 8 features");
                this.Capabilities.SupportsWindows8Features = true;
            }

            if (buildNumber >= Windows81BuildNumber)
            {
                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Supports Windows 8.1 features");
                this.Capabilities.SupportsWindows81Features = true;
            }

            if (buildNumber >= Windows10BuildNumber)
            {
                Trace.WriteLineIf(TraceSwitch.TraceVerbose, "Supports Windows 10 features");
                this.Capabilities.SupportsWindows10Features = true;
            }
        }

        /// <summary>
        /// Create an instance and get the current version of Esent.
        /// </summary>
        /// <returns>The current version of Esent.</returns>
        private uint GetVersionFromEsent()
        {
#if MANAGEDESENT_ON_WSA
            // JetGetVersion isn't available in new Windows user interface, so we'll pretend it's always Win8:
            return 8250 << 8;
#else
            // Create a unique name so that multiple threads can call this simultaneously.
            // This can happen if there are multiple AppDomains.
            string instanceName = string.Format(CultureInfo.InvariantCulture, "GettingEsentVersion{0}", LibraryHelpers.GetCurrentManagedThreadId());
            JET_INSTANCE instance = JET_INSTANCE.Nil;
            RuntimeHelpers.PrepareConstrainedRegions();            
            try
            {
                this.JetCreateInstance(out instance, instanceName);
                this.JetSetSystemParameter(instance, JET_SESID.Nil, JET_param.Recovery, new IntPtr(0), "off");
                this.JetSetSystemParameter(instance, JET_SESID.Nil, JET_param.NoInformationEvent, new IntPtr(1), null);
                this.JetSetSystemParameter(instance, JET_SESID.Nil, JET_param.MaxTemporaryTables, new IntPtr(0), null);
                this.JetSetSystemParameter(instance, JET_SESID.Nil, JET_param.MaxCursors, new IntPtr(16), null);
                this.JetSetSystemParameter(instance, JET_SESID.Nil, JET_param.MaxOpenTables, new IntPtr(16), null);
                this.JetSetSystemParameter(instance, JET_SESID.Nil, JET_param.MaxVerPages, new IntPtr(4), null);
                this.JetSetSystemParameter(instance, JET_SESID.Nil, JET_param.MaxSessions, new IntPtr(1), null);
                this.JetInit(ref instance);

                JET_SESID sesid;
                this.JetBeginSession(instance, out sesid, string.Empty, string.Empty);
                try
                {
                    uint version;
                    this.JetGetVersion(sesid, out version);
                    return version;
                }
                finally
                {
                    this.JetEndSession(sesid, EndSessionGrbit.None);
                }
            }
            finally
            {
                if (JET_INSTANCE.Nil != instance)
                {
                    this.JetTerm(instance);
                }
            }
#endif
        }
    }
}
