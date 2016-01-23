// ---------------------------------------------------------------------------
// <copyright file="KeyColumn.cs" company="Microsoft">
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
    /// A Key Column is a segment of an index used to determine the order of
    /// records in a table as seen through that index.  This object can be used
    /// to explore the schema of an existing index and to create the definition
    /// of a new index.
    /// </summary>
    public class KeyColumn
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
        /// The is ascending
        /// </summary>
        private bool isAscending = false;

        /// <summary>
        /// The read only
        /// </summary>
        private bool readOnly = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyColumn"/> class. 
        /// For use when defining a new key column in an
        /// index.
        /// </summary>
        /// <param name="name">
        /// The name of the column in the table to be used for this key column.
        /// </param>
        /// <param name="isAscending">
        /// True if the sort order of this key column is ascending, false if descending.
        /// </param>
        public KeyColumn(string name, bool isAscending)
        {
            this.columnid = null;
            this.name = name;
            this.isAscending = isAscending;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyColumn"/> class.
        /// </summary>
        /// <param name="columnid">The columnid.</param>
        /// <param name="isAscending">if set to <c>true</c> [is ascending].</param>
        internal KeyColumn(Columnid columnid, bool isAscending)
        {
            this.columnid = columnid;
            this.name = columnid.Name;
            this.isAscending = isAscending;
            this.readOnly = true;
        }

        /// <summary>
        /// Gets the column ID of the column used for this key column.
        /// </summary>
        /// <remarks>
        /// The column ID is undefined if this key column will be used to
        /// define a new index
        /// </remarks>
        public Columnid Columnid
        {
            get
            {
                return this.columnid;
            }
        }

        /// <summary>
        /// Gets or sets the name of the column used for this key column.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.CheckReadOnly();
                this.name = value;
            }
        }

        /// <summary>
        /// Gets the type of the column used for this key column.
        /// </summary>
        /// <remarks>
        /// The column type is undefined if this key column will be used to
        /// define a new index.
        /// </remarks>
        public Type Type
        {
            get
            {
                return this.columnid.Type;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the sort order of the key column is ascending.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is ascending]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAscending
        {
            get
            {
                return this.isAscending;
            }

            set
            {
                this.CheckReadOnly();
                this.isAscending = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [is read only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is read only]; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get
            {
                return this.readOnly;
            }
        }

        /// <summary>
        /// Sets a value indicating whether [read only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read only]; otherwise, <c>false</c>.
        /// </value>
        internal bool ReadOnly
        {
            set
            {
                this.readOnly = value;
            }
        }

        /// <summary>
        /// Checks the read only.
        /// </summary>
        /// <exception cref="System.NotSupportedException">this key column definition cannot be changed</exception>
        private void CheckReadOnly()
        {
            if (this.readOnly)
            {
                throw new NotSupportedException("this key column definition cannot be changed");
            }
        }
    }
}
