// --------------------------------------------------------------------------------------------------------------------
// <copyright file="jet_pfndurablecommitcallback.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
// <summary>
//   Callback for JET_param JET_paramEmitLogDataCallback.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    using System;

    /// <summary>
    /// Callback for JET_paramDurableCommitCallback.
    /// </summary>
    /// <param name="instance">Instance to use.</param>
    /// <param name="pCommitIdSeen">Commit-id that has just been flushed.</param>
    /// <param name="grbit">Reserved currently.</param>
    /// <returns>An error code.</returns>
    public delegate JET_err JET_PFNDURABLECOMMITCALLBACK(
        JET_INSTANCE instance,
        JET_COMMIT_ID pCommitIdSeen,
        DurableCommitCallbackGrbit grbit);

    /// <summary>
    /// Callback for JET_paramDurableCommitCallback.
    /// </summary>
    /// <param name="instance">Instance to use.</param>
    /// <param name="pCommitIdSeen">Commit-id that has just been flushed.</param>
    /// <param name="grbit">Reserved currently.</param>
    /// <returns>An error code.</returns>
    internal delegate JET_err NATIVE_JET_PFNDURABLECOMMITCALLBACK(
        IntPtr instance,
        ref NATIVE_COMMIT_ID pCommitIdSeen,
        uint grbit);
}
