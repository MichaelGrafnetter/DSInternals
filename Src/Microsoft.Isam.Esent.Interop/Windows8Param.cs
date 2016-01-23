//-----------------------------------------------------------------------
// <copyright file="Windows8Param.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    /// <summary>
    /// System parameters that were introduced in Windows 8.
    /// </summary>
    public static class Windows8Param
    {
        /// <summary>
        /// Per-instance property for relative cache priorities (default = 100).
        /// </summary>
        public const JET_param CachePriority = (JET_param)177;

        /// <summary>
        /// Percentage of version store that can be used by oldest transaction
        /// before <see cref="JET_err.VersionStoreOutOfMemory"/> (default = 100).
        /// </summary>
        public const JET_param MaxTransactionSize = (JET_param)178;

        /// <summary>
        /// Maximum number of I/O operations dispatched for a given purpose.
        /// </summary>
        public const JET_param PrereadIOMax = (JET_param)179;

        /// <summary>
        /// Database Maintenance serialization is enabled for databases sharing
        /// the same disk.
        /// </summary>
        public const JET_param EnableDBScanSerialization = (JET_param)180;

        /// <summary>
        /// The threshold for what is considered a hung IO that should be acted upon.
        /// </summary>
        public const JET_param HungIOThreshold = (JET_param)181;

        /// <summary>
        /// A set of actions to be taken on IOs that appear hung.
        /// </summary>
        public const JET_param HungIOActions = (JET_param)182;

        /// <summary>
        /// Smallest amount of data that should be compressed with xpress compression.
        /// </summary>
        public const JET_param MinDataForXpress = (JET_param)183;

        /// <summary>
        /// Friendly name for this instance of the process.
        /// </summary>
        public const JET_param ProcessFriendlyName = (JET_param)186;

        /// <summary>
        /// Callback for when log is flushed.
        /// </summary>
        public const JET_param DurableCommitCallback = (JET_param)187;
    }
}
