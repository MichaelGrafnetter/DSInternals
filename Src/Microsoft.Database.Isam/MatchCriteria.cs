// ---------------------------------------------------------------------------
// <copyright file="MatchCriteria.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

// ---------------------------------------------------------------------
// <summary>
// </summary>
// ---------------------------------------------------------------------

namespace Microsoft.Database.Isam
{
    using Microsoft.Isam.Esent.Interop;

    /// <summary>
    /// Choices for Cursor.FindRecords
    /// </summary>
    public enum MatchCriteria
    {
        /// <summary>
        /// The cursor will be positioned at the index entry closest to the
        /// start of the index that exactly matches the search key.
        /// </summary>
        EqualTo = SeekGrbit.SeekEQ,

        /// <summary>
        /// The cursor will be positioned at the index entry closest to the
        /// end of the index that is less than an index entry that would
        /// exactly match the search criteria.
        /// </summary>
        LessThan = SeekGrbit.SeekLT,

        /// <summary>
        /// The cursor will be positioned at the index entry closest to the
        /// end of the index that is less than or equal to an index entry
        /// that would exactly match the search criteria.
        /// </summary>
        LessThanOrEqualTo = SeekGrbit.SeekLE,

        /// <summary>
        /// The cursor will be positioned at the index entry closest to the
        /// start of the index that is greater than or equal to an index
        /// entry that would exactly match the search criteria.
        /// </summary>
        GreaterThanOrEqualTo = SeekGrbit.SeekGE,

        /// <summary>
        /// The cursor will be positioned at the index entry closest to the
        /// start of the index that is greater than an index entry that
        /// would exactly match the search criteria.
        /// </summary>
        GreaterThan = SeekGrbit.SeekGT,
    }
}
