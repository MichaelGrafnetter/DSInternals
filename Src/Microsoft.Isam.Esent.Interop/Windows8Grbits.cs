//-----------------------------------------------------------------------
// <copyright file="Windows8Grbits.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    using System;

    /// <summary>
    /// Options for <see cref="Windows8Api.JetGetErrorInfo"/>.
    /// </summary>
    public enum ErrorInfoGrbit
    {
        /// <summary>
        /// No option.
        /// </summary>
        None = 0,
    }

    /// <summary>
    /// Options for <see cref="Windows8Api.JetResizeDatabase"/>.
    /// </summary>
    /// <seealso cref="Windows81.Windows81Grbits.OnlyShrink"/>
    [Flags]
    public enum ResizeDatabaseGrbit
    {
        /// <summary>
        /// No option.
        /// </summary>
        None = 0,

        /// <summary>
        /// Only grow the database. If the resize call would shrink the database, do nothing.
        /// </summary>
        OnlyGrow = 0x1,
    }

    /// <summary>
    /// Options passed to log flush callback.
    /// </summary>
    /// <seealso cref="Microsoft.Isam.Esent.Interop.Windows10.Windows10Grbits.LogUnavailable"/>
    [Flags]
    public enum DurableCommitCallbackGrbit
    {
        /// <summary>
        /// Default options.
        /// </summary>
        None = 0
    }

    /// <summary>
    /// Options for <see cref="Windows8Api.JetPrereadIndexRanges"/>.
    /// </summary>
    [Flags]
    public enum PrereadIndexRangesGrbit
    {
        /// <summary>
        /// Preread forward.
        /// </summary>
        Forward = 0x1,

        /// <summary>
        /// Preread backwards.
        /// </summary>
        Backwards = 0x2,

        /// <summary>
        /// Preread only first page of any long column.
        /// </summary>
        FirstPageOnly = 0x4,

        /// <summary>
        /// Normalized key/bookmark provided instead of column value.
        /// </summary>
        NormalizedKey = 0x8,
    }

    /// <summary>
    /// Options for <see cref="Windows8Api.JetStopServiceInstance2"/>.
    /// </summary>
    [Flags]
    public enum StopServiceGrbit
    {
        /// <summary>
        /// Stops all ESE services for the specified instance.
        /// </summary>
        All = 0x00000000,

        /// <summary>
        /// Stops restartable client specificed background maintenance tasks (B+ Tree Defrag).
        /// </summary>
        BackgroundUserTasks = 0x00000002,

        /// <summary>
        /// Quiesces all dirty caches to disk. Asynchronous. Quiescing is cancelled if the <see cref="Resume"/>
        /// bit is called subsequently.
        /// </summary>
        QuiesceCaches = 0x00000004,

        /// <summary>
        /// Resumes previously issued StopService operations, i.e. "restarts service".  Can be combined
        /// with above grbits to Resume specific services, or with 0x0 Resumes all previous stopped services.
        /// </summary>
        /// <remarks>
        /// Warning: This bit can only be used to resume JET_bitStopServiceBackground and JET_bitStopServiceQuiesceCaches, if you 
        /// did a JET_bitStopServiceAll or JET_bitStopServiceAPI, attempting to use JET_bitStopServiceResume will fail. 
        /// </remarks>
        Resume = int.MinValue, // 0x80000000
    }

    /// <summary>
    /// Options passed while setting cursor filters.
    /// </summary>
    /// <seealso cref="Windows8Api.JetSetCursorFilter"/>
    [Flags]
    public enum CursorFilterGrbit
    {
        /// <summary>
        /// Default options.
        /// </summary>
        None = 0
    }

    /// <summary>
    /// Options for <see cref="JET_INDEX_COLUMN"/>.
    /// </summary>
    [Flags]
    public enum JetIndexColumnGrbit
    {
        /// <summary>
        /// Default options.
        /// </summary>
        None = 0,

        /// <summary>
        /// Zero-length value (non-null).
        /// </summary>
        ZeroLength = 0x1,
    }

    /// <summary>
    /// System parameters that have been introduced in Windows 8.
    /// </summary>
    public static class Windows8Grbits
    {
        /// <summary>
        /// Allows db to remain attached at the end of recovery (for faster
        /// transition to running state).
        /// </summary>
        public const InitGrbit KeepDbAttachedAtEndOfRecovery = (InitGrbit)0x1000;

        /// <summary>
        /// Purge database pages on attach.
        /// </summary>
        public const AttachDatabaseGrbit PurgeCacheOnAttach = (AttachDatabaseGrbit)0x1000;

        /// <summary>
        /// Specifying this flag will change GUID sort order to .Net standard.
        /// </summary>
        public const CreateIndexGrbit IndexDotNetGuid = (CreateIndexGrbit)0x00040000;

        /// <summary>
        /// This option requests that the temporary table sort columns of type
        /// JET_coltypGUID according to .Net Guid sort order.
        /// </summary>        
        public const TempTableGrbit TTDotNetGuid = (TempTableGrbit)0x100;
    }
}
