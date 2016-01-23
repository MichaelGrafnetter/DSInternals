// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DurableCommitCallback.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.
// </copyright>
// <summary>
//   Callback for JET_param JET_paramDurableCommitCallback.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Reflection;
#if !MANAGEDESENT_ON_CORECLR
    using System.Runtime.CompilerServices;
#endif

    using Microsoft.Isam.Esent.Interop.Implementation;

    /// <summary>
    /// A class which wraps the callback dealing with durable commits.
    /// </summary>
    public class DurableCommitCallback : EsentResource
    {
        /// <summary>
        /// API call tracing.
        /// </summary>
        private static readonly TraceSwitch TraceSwitch = new TraceSwitch("ESENT DurableCommitCallback", "Wrapper around unmanaged ESENT durable commit callback");

        /// <summary>
        /// Instance associated with this callback.
        /// </summary>
        private JET_INSTANCE instance;

        /// <summary>
        /// Hold a reference to the delegate so that it doesn't get garbage-collected.
        /// </summary>
        private JET_PFNDURABLECOMMITCALLBACK wrappedCallback;

        /// <summary>
        /// Hold a reference to the delegate so that it doesn't get garbage-collected.
        /// </summary>
        [SuppressMessage("Exchange.Performance", "EX0023:DeadVariableDetector", Justification = "Need to hold on to a reference to the callback, so that it does not get garbage collected.")]
        private NATIVE_JET_PFNDURABLECOMMITCALLBACK wrapperCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="DurableCommitCallback"/> class. 
        /// The constructor.
        /// </summary>
        /// <param name="instance">
        /// The instance with which to associate the callback.
        /// </param>
        /// <param name="wrappedCallback">
        /// The managed code callback to call.
        /// </param>
        public DurableCommitCallback(
            JET_INSTANCE instance,
            JET_PFNDURABLECOMMITCALLBACK wrappedCallback)
        {
            this.instance = instance;
            this.wrappedCallback = wrappedCallback;
            this.wrapperCallback = this.NativeDurableCommitCallback;

#if !MANAGEDESENT_ON_WSA // RuntimeHelpers works differently in Windows Store Apps.
            if (this.wrappedCallback != null)
            {
                RuntimeHelpers.PrepareMethod(this.wrappedCallback.Method.MethodHandle);
            }

            RuntimeHelpers.PrepareMethod(typeof(DurableCommitCallback).GetMethod("NativeDurableCommitCallback", BindingFlags.NonPublic | BindingFlags.Instance).MethodHandle);
#endif

            InstanceParameters instanceParameters = new InstanceParameters(this.instance);

            // This might be null.
            instanceParameters.SetDurableCommitCallback(this.wrapperCallback);

            this.ResourceWasAllocated();
        }

        /// <summary>
        /// Generate a string representation of the structure.
        /// </summary>
        /// <returns>The structure as a string.</returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "DurableCommitCallback({0})",
                this.instance.ToString());
        }

        /// <summary>
        /// Terminate the durable commit session.
        /// </summary>
        public void End()
        {
            this.CheckObjectIsNotDisposed();
            this.ReleaseResource();
        }

        /// <summary>
        /// Free the durable commit session.
        /// We do not try to set the instance parameter to null, since the callback is disposed after JetTerm and
        /// the callback cannot be set after JetTerm.
        /// </summary>
        protected override void ReleaseResource()
        {
            this.instance = JET_INSTANCE.Nil;
            this.wrappedCallback = null;
            this.wrapperCallback = null;
            this.ResourceWasReleased();
        }

        /// <summary>
        /// The proxy callback function to call the user-defined managed delegate.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <param name="commitIdSeen">
        /// The commit-id flushed.
        /// </param>
        /// <param name="grbit">
        /// Reserved currently.
        /// </param>
        /// <returns>
        /// An error code.
        /// </returns>
        private JET_err NativeDurableCommitCallback(
            IntPtr instance,
            ref NATIVE_COMMIT_ID commitIdSeen,
            uint grbit)
        {
            RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                JET_INSTANCE jetInstance = new JET_INSTANCE()
                {
                    Value = instance
                };

                if (this.instance != jetInstance)
                {
                    // We assume it's only called on one instance at a time. The only thing
                    // we really care about is serialization of the byte array.
                    //
                    // It would be nice to throw an error, but we're going back to real
                    // code, which doesn't deal with managed exceptions well.
                    return JET_err.CallbackFailed;
                }

                JET_COMMIT_ID commitId = new JET_COMMIT_ID(commitIdSeen);

                return this.wrappedCallback(jetInstance, commitId, (DurableCommitCallbackGrbit)grbit);
            }
            catch (Exception ex)
            {
                Trace.WriteLineIf(
                    TraceSwitch.TraceWarning, string.Format(CultureInfo.InvariantCulture, "Caught Exception {0}", ex));

                JetApi.ReportUnhandledException(ex, "Unhandled exception during NativeDurableCommitCallback");

                // This should never be executed, but the compiler doesn't know it.
                return JET_err.CallbackFailed;
            }
        }
    }
}
