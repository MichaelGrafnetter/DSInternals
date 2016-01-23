// ---------------------------------------------------------------------------
// <copyright file="BoundCriteria.cs" company="Microsoft">
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
    /// Choices for Cursor.FindRecordsBetween
    /// </summary>
    public enum BoundCriteria
    {
        /// <summary>
        /// Whether the bounds are included.
        /// </summary>
        Inclusive = 0,

        /// <summary>
        /// Whether the bounds are excluded.
        /// </summary>
        Exclusive = 1,
    }
}
