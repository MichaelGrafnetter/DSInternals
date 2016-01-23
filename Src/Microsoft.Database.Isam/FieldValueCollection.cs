// ---------------------------------------------------------------------------
// <copyright file="FieldValueCollection.cs" company="Microsoft">
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
    /// A Field Value Collection represents the set of field values that are in
    /// a given field in a given record.  It can be used to efficiently
    /// navigate those field values.
    /// </summary>
    public class FieldValueCollection : CollectionBase
    {
        /// <summary>
        /// The columnid
        /// </summary>
        private readonly Columnid columnid;

        /// <summary>
        /// The read only
        /// </summary>
        private bool readOnly = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldValueCollection"/> class.
        /// </summary>
        /// <param name="columnid">The columnid.</param>
        internal FieldValueCollection(Columnid columnid)
        {
            this.columnid = columnid;
        }

        /// <summary>
        /// Gets the column ID of the column for which these field values were set
        /// in the record
        /// </summary>
        /// <value>
        /// The columnid.
        /// </value>
        public Columnid Columnid
        {
            get
            {
                return this.columnid;
            }
        }

        /// <summary>
        /// Gets the name of the column for which these field values were set in the
        /// record
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return this.columnid.Name;
            }
        }

        /// <summary>
        /// Gets the type of the column for which these field values were set in the
        /// record
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type
        {
            get
            {
                return this.columnid.Type;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this field value collection cannot be changed
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
        /// <returns>The specific element at that position.</returns>
        public object this[int index]
        {
            get
            {
                return this.List[index];
            }

            set
            {
                this.CheckReadOnly();
                this.List[index] = value;
            }
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(object value)
        {
            List.Add(value);
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
        /// <returns>
        /// The index of <paramref name="value" /> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(object value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
        /// <param name="value">The object to insert into the <see cref="T:System.Collections.IList" />.</param>
        public void Insert(int index, object value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.
        /// </summary>
        /// <param name="value">The object to remove from the <see cref="T:System.Collections.IList" />.</param>
        public void Remove(object value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
        /// <returns>
        /// true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.
        /// </returns>
        public bool Contains(object value)
        {
            return List.Contains(value);
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
        /// <exception cref="System.ArgumentException">value must be of type  + Type +  or of type  + typeof(System.DBNull);value</exception>
        protected override void OnValidate(object value)
        {
            if (value.GetType() != Type && value.GetType() != typeof(System.DBNull))
            {
                throw new ArgumentException(
                    "value must be of type " + Type + " or of type " + typeof(System.DBNull),
                    "value");
            }
        }

        /// <summary>
        /// Checks the read only.
        /// </summary>
        /// <exception cref="System.NotSupportedException">this field value collection cannot be changed</exception>
        private void CheckReadOnly()
        {
            if (this.ReadOnly)
            {
                throw new NotSupportedException("this field value collection cannot be changed");
            }
        }
    }
}
