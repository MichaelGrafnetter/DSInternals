//-----------------------------------------------------------------------
// <copyright file="Windows7Param.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows7
{
    /// <summary>
    /// System parameters that have been added to the Windows 7 version of ESENT.
    /// </summary>
    public static class Windows7Param
    {
        /// <summary>
        /// This parameter sets the number of logs that esent will defer database
        /// flushes for. This can be used to increase database recoverability if
        /// failures cause logfiles to be lost.
        /// </summary>
        public const JET_param WaypointLatency = (JET_param)153;

        /// <summary>
        /// Turn on/off automatic sequential B-tree defragmentation tasks (On by 
        /// default, but also requires <see cref="SpaceHintsGrbit"/> flags / <see cref="SpaceHintsGrbit"/>.RetrieveHintTableScan* 
        /// to trigger on any given tables).
        /// </summary>
        public const JET_param DefragmentSequentialBTrees = (JET_param)160;

        /// <summary>
        /// Determine how frequently B-tree density is checked (Note: currently not
        /// implemented).
        /// </summary>
        public const JET_param DefragmentSequentialBTreesDensityCheckFrequency = (JET_param)161;

        /// <summary>
        /// This parameter is used to retrieve the chunk size of long-value
        /// (blob) data. Setting and retrieving data in multiples of this 
        /// size increases efficiency.
        /// </summary>
        public const JET_param LVChunkSizeMost = (JET_param)163;

        /// <summary>
        /// Maximum number of bytes that can be grouped for a coalesced read operation.
        /// </summary>
        public const JET_param MaxCoalesceReadSize = (JET_param)164;

        /// <summary>
        /// Maximum number of bytes that can be grouped for a coalesced write operation.
        /// </summary>
        public const JET_param MaxCoalesceWriteSize = (JET_param)165;

        /// <summary>
        /// Maximum number of bytes that can be gapped for a coalesced read IO operation.
        /// </summary>
        public const JET_param MaxCoalesceReadGapSize = (JET_param)166;

        /// <summary>
        /// Maximum number of bytes that can be gapped for a coalesced write IO operation.
        /// </summary>
        public const JET_param MaxCoalesceWriteGapSize = (JET_param)167;

        /// <summary>
        /// Enable Database Maintenance during recovery.
        /// </summary>
        public const JET_param EnableDbScanInRecovery = (JET_param)169;

        /// <summary>
        /// Throttling of the database scan, in milliseconds.
        /// </summary>
        public const JET_param DbScanThrottle = (JET_param)170;

        /// <summary>
        /// Minimum interval to repeat the database scan, in seconds.
        /// </summary>
        public const JET_param DbScanIntervalMinSec = (JET_param)171;

        /// <summary>
        /// Maximum interval to allow the database scan to finish, in seconds.
        /// </summary>
        public const JET_param DbScanIntervalMaxSec = (JET_param)172;
    }
}