// ---------------------------------------------------------------------------
// <copyright file="TableType.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

// ---------------------------------------------------------------------
// <summary>
// </summary>
// ---------------------------------------------------------------------

namespace Microsoft.Database.Isam
{
    /// <summary>
    /// Table types enumeration
    /// </summary>
    public enum TableType
    {
        /// <summary>
        /// The empty value.
        /// </summary>
        None,

        /// <summary>
        /// An ordinary database table.
        /// </summary>
        Base,

        /// <summary>
        /// A temporary table that can only be used to sort rows.  A sort is used
        /// in two phases:  insert phase and extraction phase.  In the insert
        /// phase, rows can only be inserted into the table.  In the extraction
        /// phase, rows can only be extracted in order from first to last.  Once
        /// all rows have been extracted, the sort can only be dropped.  Temporary
        /// tables are volatile.
        /// </summary>
        Sort,

        /// <summary>
        /// A temporary table that is initially created by using a sort.  This
        /// table is used in two phases:  insert phase and main phase.  In
        /// the insert phase, rows can only be inserted into the table.  When
        /// the insert phase is ended by performing any non-insert operation,
        /// the rows created during the insert phase are sorted and pushed into
        /// the temporary table.  The table then enters the main phase where any
        /// operation is allowed.  Temporary tables are volatile.
        /// </summary>
        PreSortTemporary,

        /// <summary>
        /// An ordinary temporary table.  Any operation is allowed at any time.
        /// Temporary tables are volatile.
        /// </summary>
        Temporary,
    }
}
