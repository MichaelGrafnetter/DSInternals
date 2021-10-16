//-----------------------------------------------------------------------
// <copyright file="jet_iopriority.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Vista
{
    using System;

    /// <summary>
    /// Values for use with <see cref="VistaParam.IOPriority"/>.
    /// </summary>
    [Flags]
    public enum JET_IOPriority
    {
        /// <summary>
        /// This is the default I/O priority level.
        /// </summary>
        Normal = 0x0,

        /// <summary>
        /// Subsequent I/Os issued will be issued at Low priority.
        /// </summary>
        Low = 0x1,

        /// <summary>
        /// Subsequent I/Os issued for checkpoint advancement will be issued at Low priority.
        /// </summary>
        /// <remarks>
        /// Available on Windows 8.1 and later.
        /// </remarks>
        LowForCheckpoint = 0x2,

        /// <summary>
        /// Subsequent I/Os issued for scavenging buffers will be issued at Low priority.
        /// </summary>
        /// <remarks>
        /// Available on Windows 8.1 and later.
        /// </remarks>
        LowForScavenge = 0x4,
    }
}