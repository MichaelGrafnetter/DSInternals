// ---------------------------------------------------------------------------
// <copyright file="Position.cs" company="Microsoft">
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
    /// Describes an (approximate) ordinal position in a table
    /// </summary>
    /// <remarks>
    /// A Position can be viewed as a non-simplified fractional position
    /// </remarks>
    public class Position
    {
        /// <summary>
        /// The entry
        /// </summary>
        private readonly int entry;

        /// <summary>
        /// The total entries
        /// </summary>
        private readonly int totalEntries;

        /// <summary>
        /// Initializes a new instance of the <see cref="Position" /> class.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="totalEntries">The total number of entries.</param>
        public Position(int entry, int totalEntries)
        {
            this.entry = entry;
            this.totalEntries = totalEntries;

            // FUTURE-2005/21/02-BrettSh - Sorry state of Assert()s in the managed code base (see %eseroot%\src\interop\error.h for more)
            // Debug.Assert( entry <= totalEntries, "Cannot have a fractional position > 1" );
        }

        /// <summary>
        /// Gets the position in the table.
        /// </summary>
        public int Entry
        {
            get
            {
                return this.entry;
            }
        }

        /// <summary>
        /// Gets the total number of records in the table.
        /// </summary>
        public int TotalEntries
        {
            get
            {
                return this.totalEntries;
            }
        }
    }
}
