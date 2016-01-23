// ---------------------------------------------------------------------------
// <copyright file="ConditionalColumn.cs" company="Microsoft">
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
    /// A Conditional Column is a column used to determine the visibility of a
    /// record in an index.  This object can be used to explore the schema of
    /// an existing index and to create the definition of a new index.
    /// </summary>
    public class ConditionalColumn
    {
        /// <summary>
        /// The columnid
        /// </summary>
        private readonly Columnid columnid;

        /// <summary>
        /// The name
        /// </summary>
        private string name = null;

        /// <summary>
        /// The must be null
        /// </summary>
        private bool mustBeNull = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalColumn"/> class. 
        /// For use when defining a new conditional
        /// column in an index.
        /// </summary>
        /// <param name="name">
        /// The name of the column in the table to be used for this conditional column.
        /// </param>
        /// <param name="mustBeNull">
        /// True if the column must be null for the record to be visible in the index,
        /// false if the column must be non-null for the record to be visible in the index.
        /// </param>
        public ConditionalColumn(string name, bool mustBeNull)
        {
            this.columnid = null;
            this.name = name;
            this.mustBeNull = mustBeNull;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalColumn"/> class.
        /// </summary>
        /// <param name="columnid">The columnid.</param>
        /// <param name="mustBeNull">if set to <c>true</c> [must be null].</param>
        internal ConditionalColumn(Columnid columnid, bool mustBeNull)
        {
            this.columnid = columnid;
            this.name = columnid.Name;
            this.mustBeNull = mustBeNull;
        }

        /// <summary>
        /// Gets the column ID of the column used for this conditional column
        /// </summary>
        /// <remarks>
        /// The column ID is undefined if this conditional column will be used
        /// to define a new index
        /// </remarks>
        public Columnid Columnid
        {
            get
            {
                return this.columnid;
            }
        }

        /// <summary>
        /// Gets or sets the name of the column used for this conditional column
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets the type of the column used for this conditional column
        /// </summary>
        /// <remarks>
        /// The column type is undefined if this conditional column will be
        /// used to define a new index
        /// </remarks>
        public Type Type
        {
            get
            {
                return this.columnid.Type;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the column must be null for the record to be visible in the index.
        /// </summary>
        public bool MustBeNull
        {
            get
            {
                return this.mustBeNull;
            }

            set
            {
                this.mustBeNull = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the column must be non-null for the record to be visible in the index.
        /// </summary>
        public bool MustBeNonNull
        {
            get
            {
                return !this.mustBeNull;
            }

            set
            {
                this.mustBeNull = !value;
            }
        }
    }
}
