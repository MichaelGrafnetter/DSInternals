// ---------------------------------------------------------------------------
// <copyright file="FieldCollection.cs" company="Microsoft">
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
    using System.Globalization;

    /// <summary>
    /// A Field Collection represents the set of fields that are in a given
    /// record.  It can be used to efficiently navigate those fields.
    /// </summary>
    public class FieldCollection : DictionaryBase, IEnumerable
    {
        /// <summary>
        /// The location
        /// </summary>
        private Location location;

        /// <summary>
        /// The read only
        /// </summary>
        private bool readOnly = false;

        /// <summary>
        /// Gets a value indicating whether this field collection cannot be changed.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return this.readOnly;
            }
        }

        /// <summary>
        /// Gets the names.
        /// </summary>
        /// <value>
        /// The names.
        /// </value>
        public ICollection Names
        {
            get
            {
                return this.Dictionary.Keys;
            }
        }

        /// <summary>
        /// Gets or sets the location of the record that contained these fields.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public Location Location
        {
            get
            {
                return this.location;
            }

            set
            {
                this.CheckReadOnly();
                this.location = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object has a fixed size.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, false.</returns>
        public bool IsFixedSize
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
        /// The field values for the specified column.
        /// </summary>
        /// <value>
        /// The <see cref="FieldValueCollection"/>.
        /// </value>
        /// <param name="columnName">the name of the column whose field values are desired</param>
        /// <returns>A <see cref="FieldValueCollection"/> object to access values of this column.</returns>
        public FieldValueCollection this[string columnName]
        {
            get
            {
                return (FieldValueCollection)Dictionary[columnName.ToLower(CultureInfo.InvariantCulture)];
            }

            set
            {
                this.Dictionary[columnName.ToLower(CultureInfo.InvariantCulture)] = value;
            }
        }

        /// <summary>
        /// The field values for the specified column
        /// </summary>
        /// <value>
        /// The <see cref="FieldValueCollection"/>.
        /// </value>
        /// <param name="column">the column ID whose field values are desired</param>
        /// <returns>A <see cref="FieldValueCollection"/> object to access values of this column.</returns>
        public FieldValueCollection this[Columnid column]
        {
            get
            {
                return (FieldValueCollection)Dictionary[column.Name.ToLower(CultureInfo.InvariantCulture)];
            }

            set
            {
                this.Dictionary[column.Name.ToLower(CultureInfo.InvariantCulture)] = value;
            }
        }

        /// <summary>
        /// Fetches an enumerator containing all the fields for this record.
        /// </summary>
        /// <returns>An enumerator containing all the fields for this record.</returns>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public new RecordEnumerator GetEnumerator()
        {
            return new RecordEnumerator(Dictionary.GetEnumerator());
        }

        /// <summary>
        /// Adds the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        public void Add(FieldValueCollection values)
        {
            this.Dictionary.Add(values.Name.ToLower(CultureInfo.InvariantCulture), values);
        }

        /// <summary>
        /// Returns whether the specifed column exists in the row.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>Whether the specifed column exists in the row.</returns>
        public bool Contains(string columnName)
        {
            return this.Dictionary.Contains(columnName.ToLower(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Returns whether the specifed column exists in the row.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns>Whether the specifed column exists in the row.</returns>
        public bool Contains(Columnid column)
        {
            return this.Dictionary.Contains(column.Name.ToLower(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Removes the specified column name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        public void Remove(string columnName)
        {
            this.Dictionary.Remove(columnName.ToLower(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Removes the specified column.
        /// </summary>
        /// <param name="column">The column.</param>
        public void Remove(Columnid column)
        {
            this.Dictionary.Remove(column.Name.ToLower(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Fetches an enumerator containing all the fields for this record
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <remarks>
        /// This is the standard version that will work with other CLR
        /// languages.
        /// </remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

        /// <summary>
        /// Performs additional custom processes before clearing the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.
        /// </summary>
        protected override void OnClear()
        {
            this.CheckReadOnly();
        }

        /// <summary>
        /// Performs additional custom processes before inserting a new element into the <see cref="T:System.Collections.DictionaryBase" /> instance.
        /// </summary>
        /// <param name="key">The key of the element to insert.</param>
        /// <param name="value">The value of the element to insert.</param>
        protected override void OnInsert(object key, object value)
        {
            this.CheckReadOnly();
        }

        /// <summary>
        /// Performs additional custom processes before removing an element from the <see cref="T:System.Collections.DictionaryBase" /> instance.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <param name="value">The value of the element to remove.</param>
        protected override void OnRemove(object key, object value)
        {
            this.CheckReadOnly();
        }

        /// <summary>
        /// Performs additional custom processes before setting a value in the <see cref="T:System.Collections.DictionaryBase" /> instance.
        /// </summary>
        /// <param name="key">The key of the element to locate.</param>
        /// <param name="oldValue">The old value of the element associated with <paramref name="key" />.</param>
        /// <param name="newValue">The new value of the element associated with <paramref name="key" />.</param>
        protected override void OnSet(object key, object oldValue, object newValue)
        {
            this.CheckReadOnly();
        }

        /// <summary>
        /// Performs additional custom processes when validating the element with the specified key and value.
        /// </summary>
        /// <param name="key">The key of the element to validate.</param>
        /// <param name="value">The value of the element to validate.</param>
        /// <exception cref="System.ArgumentException">
        /// key must be of type System.String;key
        /// or
        /// value must be of type FieldValueCollection;value
        /// or
        /// key must match value.Name;key
        /// </exception>
        protected override void OnValidate(object key, object value)
        {
            if (!(key is string))
            {
                throw new ArgumentException("key must be of type System.String", "key");
            }

            if (!(value is FieldValueCollection))
            {
                throw new ArgumentException("value must be of type FieldValueCollection", "value");
            }

            if (((string)key).ToLower(CultureInfo.InvariantCulture) != ((FieldValueCollection)value).Name.ToLower(CultureInfo.InvariantCulture))
            {
                throw new ArgumentException("key must match value.Name", "key");
            }
        }

        /// <summary>
        /// Checks the read only.
        /// </summary>
        /// <exception cref="System.NotSupportedException">this field collection cannot be changed</exception>
        private void CheckReadOnly()
        {
            if (this.readOnly)
            {
                throw new NotSupportedException("this field collection cannot be changed");
            }
        }
    }
}
