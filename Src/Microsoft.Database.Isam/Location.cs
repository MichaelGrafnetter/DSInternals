// ---------------------------------------------------------------------------
// <copyright file="Location.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

// ---------------------------------------------------------------------
// <summary>
// </summary>
// ---------------------------------------------------------------------

namespace Microsoft.Database.Isam
{
    using System;

    /// <summary>
    /// A secondary/primary key location that precisely identifies a given record
    /// </summary>
    public class Location
    {
        /// <summary>
        /// The index name
        /// </summary>
        private readonly string indexName;

        /// <summary>
        /// The primary bookmark
        /// </summary>
        private readonly byte[] primaryBookmark;

        /// <summary>
        /// The secondary bookmark
        /// </summary>
        private readonly byte[] secondaryBookmark;

        /// <summary>
        /// Initializes a new instance of the <see cref="Location" /> class.
        /// </summary>
        /// <param name="indexName">Name of the index.</param>
        /// <param name="primaryBookmark">The primary bookmark.</param>
        /// <param name="secondaryBookmark">The secondary bookmark.</param>
        internal Location(string indexName, byte[] primaryBookmark, byte[] secondaryBookmark)
        {
            this.indexName = indexName;
            this.primaryBookmark = primaryBookmark;
            this.secondaryBookmark = secondaryBookmark;
        }

        /// <summary>
        /// Gets the name of the index.
        /// </summary>
        /// <value>
        /// The name of the index.
        /// </value>
        internal string IndexName
        {
            get
            {
                return this.indexName;
            }
        }

        /// <summary>
        /// Gets the primary bookmark.
        /// </summary>
        /// <value>
        /// The primary bookmark.
        /// </value>
        internal byte[] PrimaryBookmark
        {
            get
            {
                return this.primaryBookmark;
            }
        }

        /// <summary>
        /// Gets the secondary bookmark.
        /// </summary>
        /// <value>
        /// The secondary bookmark.
        /// </value>
        internal byte[] SecondaryBookmark
        {
            get
            {
                return this.secondaryBookmark;
            }
        }
    }
}
