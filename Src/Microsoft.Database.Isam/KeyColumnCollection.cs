// ---------------------------------------------------------------------------
// <copyright file="KeyColumnCollection.cs" company="Microsoft">
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
    using System.Collections;

    /// <summary>
    /// A class used by <see cref="IndexDefinition"/> to represent which
    /// columns are used by a particular index.
    /// </summary>
    public class KeyColumnCollection : CollectionBase
    {
        /// <summary>
        /// The read only
        /// </summary>
        private bool readOnly = false;

        /// <summary>
        /// Gets or sets a value indicating whether the collection is Read-Only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read only]; otherwise, <c>false</c>.
        /// </value>
        public bool ReadOnly
        {
            get
            {
                return this.readOnly;
            }

            set
            {
                this.CheckReadOnly();
                this.readOnly = value;
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The element at the specified index.</returns>
        public KeyColumn this[int index]
        {
            get
            {
                return (KeyColumn)this.List[index];
            }

            set
            {
                this.CheckReadOnly();
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Adds the specified key column.
        /// </summary>
        /// <param name="keyColumn">The key column.</param>
        public void Add(KeyColumn keyColumn)
        {
            List.Add(keyColumn);
        }

        /// <summary>
        /// Gets the index of the specified <see cref="KeyColumn"/>.
        /// </summary>
        /// <param name="keyColumn">The key column.</param>
        /// <returns>An index to be used.</returns>
        public int IndexOf(KeyColumn keyColumn)
        {
            return List.IndexOf(keyColumn);
        }

        /// <summary>
        /// Inserts the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="keyColumn">The key column.</param>
        public void Insert(int index, KeyColumn keyColumn)
        {
            List.Insert(index, keyColumn);
        }

        /// <summary>
        /// Removes the specified key column.
        /// </summary>
        /// <param name="keyColumn">The key column.</param>
        public void Remove(KeyColumn keyColumn)
        {
            List.Remove(keyColumn);
        }

        /// <summary>
        /// Determines whether the specified <see cref="KeyColumn"/> is present in this collection.
        /// </summary>
        /// <param name="keyColumn">The key column.</param>
        /// <returns>Whether the specified <see cref="KeyColumn"/> is present in this collection.</returns>
        public bool Contains(KeyColumn keyColumn)
        {
            return List.Contains(keyColumn);
        }

        /// <summary>
        /// Performs additional custom processes when clearing the contents of the <see cref="T:System.Collections.CollectionBase" /> instance.
        /// </summary>
        protected override void OnClear()
        {
            this.CheckReadOnly();
        }

        /// <summary>
        /// Performs additional custom processes before inserting a new element into the <see cref="T:System.Collections.CollectionBase" /> instance.
        /// </summary>
        /// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
        /// <param name="value">The new value of the element at <paramref name="index" />.</param>
        protected override void OnInsert(int index, object value)
        {
            this.CheckReadOnly();
        }

        /// <summary>
        /// Performs additional custom processes when removing an element from the <see cref="T:System.Collections.CollectionBase" /> instance.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
        /// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
        protected override void OnRemove(int index, object value)
        {
            this.CheckReadOnly();
        }

        /// <summary>
        /// Performs additional custom processes before setting a value in the <see cref="T:System.Collections.CollectionBase" /> instance.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
        /// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
        /// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
        protected override void OnSet(int index, object oldValue, object newValue)
        {
            this.CheckReadOnly();
        }

        /// <summary>
        /// Performs additional custom processes when validating a value.
        /// </summary>
        /// <param name="value">The object to validate.</param>
        /// <exception cref="System.ArgumentException">value must be of type KeyColumn;value</exception>
        protected override void OnValidate(object value)
        {
            if (!(value is KeyColumn))
            {
                throw new ArgumentException("value must be of type KeyColumn", "value");
            }
        }

        /// <summary>
        /// Checks the read only.
        /// </summary>
        /// <exception cref="System.NotSupportedException">this key column collection cannot be changed</exception>
        private void CheckReadOnly()
        {
            if (this.ReadOnly)
            {
                throw new NotSupportedException("this key column collection cannot be changed");
            }
        }
    }
}
