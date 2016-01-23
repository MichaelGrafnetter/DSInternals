// ---------------------------------------------------------------------------
// <copyright file="ConditionalColumnCollection.cs" company="Microsoft">
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
    /// Holds the conditional columns associated with a <see cref="IndexDefinition"/>.
    /// </summary>
    public class ConditionalColumnCollection : CollectionBase
    {
        /// <summary>
        /// The read only
        /// </summary>
        private bool readOnly = false;

        /// <summary>
        /// Gets or sets a value indicating whether [read only].
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
        /// <returns>
        /// Returns the element at the specified index.
        /// </returns>
        public ConditionalColumn this[int index]
        {
            get
            {
                return (ConditionalColumn)List[index];
            }

            set
            {
                this.CheckReadOnly();
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Adds the specified conditional column.
        /// </summary>
        /// <param name="conditionalColumn">The conditional column.</param>
        public void Add(ConditionalColumn conditionalColumn)
        {
            List.Add(conditionalColumn);
        }

        /// <summary>
        /// Returns the index of the specified column.
        /// </summary>
        /// <param name="conditionalColumn">The conditional column.</param>
        /// <returns>
        /// The index of the specified column.
        /// </returns>
        public int IndexOf(ConditionalColumn conditionalColumn)
        {
            return List.IndexOf(conditionalColumn);
        }

        /// <summary>
        /// Inserts the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="conditionalColumn">The conditional column.</param>
        public void Insert(int index, ConditionalColumn conditionalColumn)
        {
            List.Insert(index, conditionalColumn);
        }

        /// <summary>
        /// Removes the specified conditional column.
        /// </summary>
        /// <param name="conditionalColumn">The conditional column.</param>
        public void Remove(ConditionalColumn conditionalColumn)
        {
            List.Remove(conditionalColumn);
        }

        /// <summary>
        /// Returns whether the specified column exists in the collection.
        /// </summary>
        /// <param name="conditionalColumn">The conditional column.</param>
        /// <returns>Whether the specified column exists in the collection.</returns>
        public bool Contains(ConditionalColumn conditionalColumn)
        {
            return List.Contains(conditionalColumn);
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
        /// <exception cref="System.ArgumentException">value must be of type ConditionalColumn;value</exception>
        protected override void OnValidate(object value)
        {
            if (!(value is ConditionalColumn))
            {
                throw new ArgumentException("value must be of type ConditionalColumn", "value");
            }
        }

        /// <summary>
        /// Checks the read only.
        /// </summary>
        /// <exception cref="System.NotSupportedException">this conditional column collection cannot be changed</exception>
        private void CheckReadOnly()
        {
            if (this.ReadOnly)
            {
                throw new NotSupportedException("this conditional column collection cannot be changed");
            }
        }
    }
}
