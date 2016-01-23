// ---------------------------------------------------------------------------
// <copyright file="ColumnCollection.cs" company="Microsoft">
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
    using Microsoft.Isam.Esent.Interop;

    /// <summary>
    /// Contains the columns defined in the table.
    /// </summary>
    public class ColumnCollection : DictionaryBase, IEnumerable
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly IsamDatabase database;

        /// <summary>
        /// The table name
        /// </summary>
        private readonly string tableName;

        /// <summary>
        /// The cached column definition
        /// </summary>
        private string cachedColumnDefinition = null;

        /// <summary>
        /// The column definition
        /// </summary>
        private ColumnDefinition columnDefinition = null;

        /// <summary>
        /// The column update identifier
        /// </summary>
        private long columnUpdateID = 0;

        /// <summary>
        /// Whether the collection is read-only.
        /// </summary>
        private bool readOnly = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnCollection"/> class. 
        /// </summary>
        public ColumnCollection()
        {
            this.database = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnCollection"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="tableName">Name of the table.</param>
        internal ColumnCollection(IsamDatabase database, string tableName)
        {
            this.database = database;
            this.tableName = tableName;
            this.readOnly = true;
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
        /// Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, false.</returns>
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
        /// <exception cref="System.InvalidOperationException">the names of the columns in this column collection cannot be enumerated in this manner when accessing the table definition of an existing table</exception>
        public ICollection Names
        {
            get
            {
                if (this.database != null)
                {
                    // CONSIDER: should we provide an ICollection of our column names to avoid this wart?
                    throw new InvalidOperationException(
                        "the names of the columns in this column collection cannot be enumerated in this manner when accessing the table definition of an existing table");
                }
                else
                {
                    return this.Dictionary.Keys;
                }
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
        /// Fetches the Column Definition for the specified column name.
        /// </summary>
        /// <value>
        /// The <see cref="ColumnDefinition"/> for the specifed column name.
        /// </value>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The <see cref="ColumnDefinition"/> for the specified column name.</returns>
        public ColumnDefinition this[string columnName]
        {
            get
            {
                if (this.database != null)
                {
                    lock (this.database.IsamSession)
                    {
                        if (this.cachedColumnDefinition != columnName.ToLower(CultureInfo.InvariantCulture)
                            || this.columnUpdateID != DatabaseCommon.SchemaUpdateID)
                        {
                            JET_COLUMNBASE columnBase;
                            Api.JetGetColumnInfo(
                                this.database.IsamSession.Sesid,
                                this.database.Dbid,
                                this.tableName,
                                columnName,
                                out columnBase);
                            this.columnDefinition = ColumnDefinition.Load(this.database, this.tableName, columnBase);
                            this.cachedColumnDefinition = columnName.ToLower(CultureInfo.InvariantCulture);
                            this.columnUpdateID = DatabaseCommon.SchemaUpdateID;
                        }

                        return this.columnDefinition;
                    }
                }
                else
                {
                    return (ColumnDefinition)this.Dictionary[columnName.ToLower(CultureInfo.InvariantCulture)];
                }
            }
        }

        /// <summary>
        /// Fetches the Column Definition for the specified columnid.
        /// </summary>
        /// <value>
        /// The <see cref="ColumnDefinition"/> for the specifed column identifier
        /// </value>
        /// <param name="columnid">The columnid.</param>
        /// <returns>The <see cref="ColumnDefinition"/> for the specified column identifier.</returns>
        public ColumnDefinition this[Columnid columnid]
        {
            get
            {
                if (this.database != null)
                {
                    lock (this.database.IsamSession)
                    {
                        if (this.cachedColumnDefinition != columnid.Name.ToLower(CultureInfo.InvariantCulture)
                            || this.columnUpdateID != DatabaseCommon.SchemaUpdateID)
                        {
                            JET_COLUMNBASE columnBase;
                            Api.JetGetColumnInfo(
                                this.database.IsamSession.Sesid,
                                this.database.Dbid,
                                this.tableName,
                                columnid.Name,
                                out columnBase);
                            this.columnDefinition = ColumnDefinition.Load(this.database, this.tableName, columnBase);
                            this.cachedColumnDefinition = columnid.Name.ToLower(CultureInfo.InvariantCulture);
                            this.columnUpdateID = DatabaseCommon.SchemaUpdateID;
                        }

                        return this.columnDefinition;
                    }
                }
                else
                {
                    return (ColumnDefinition)this.Dictionary[columnid.Name.ToLower(CultureInfo.InvariantCulture)];
                }
            }
        }

        /// <summary>
        /// Fetches an enumerator containing all the columns in this table.
        /// </summary>
        /// <returns>An enumerator containing all the columns in this table.</returns>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public new ColumnEnumerator GetEnumerator()
        {
            if (this.database != null)
            {
                return new ColumnEnumerator(this.database, this.tableName);
            }
            else
            {
                return new ColumnEnumerator(Dictionary.GetEnumerator());
            }
        }

        /// <summary>
        /// Adds the specified column definition.
        /// </summary>
        /// <param name="columnDefinition">The column definition.</param>
        public void Add(ColumnDefinition columnDefinition)
        {
            Dictionary.Add(columnDefinition.Name.ToLower(CultureInfo.InvariantCulture), columnDefinition);
        }

        /// <summary>
        /// Determines if the table contains a column with the given name
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>Whether the specified column exists in the collection.</returns>
        public bool Contains(string columnName)
        {
            if (this.database != null)
            {
                lock (this.database.IsamSession)
                {
                    bool exists = false;

                    try
                    {
                        JET_COLUMNBASE columnBase;
                        Api.JetGetColumnInfo(
                            this.database.IsamSession.Sesid,
                            this.database.Dbid,
                            this.tableName,
                            columnName,
                            out columnBase);
                        exists = true;
                    }
                    catch (EsentColumnNotFoundException)
                    {
                    }

                    return exists;
                }
            }
            else
            {
                return Dictionary.Contains(columnName.ToLower(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Determines if the table contains a column with the given columnid
        /// </summary>
        /// <param name="columnid">The columnid.</param>
        /// <returns>Whether the specified column exists in the collection.</returns>
        public bool Contains(Columnid columnid)
        {
            if (this.database != null)
            {
                lock (this.database.IsamSession)
                {
                    bool exists = false;

                    try
                    {
                        JET_COLUMNBASE columnBase;
                        Api.JetGetColumnInfo(
                            this.database.IsamSession.Sesid,
                            this.database.Dbid,
                            this.tableName,
                            columnid.Name,
                            out columnBase);
                        exists = true;
                    }
                    catch (EsentColumnNotFoundException)
                    {
                    }

                    return exists;
                }
            }
            else
            {
                return Dictionary.Contains(columnid.Name.ToLower(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Removes the specified column name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        public void Remove(string columnName)
        {
            Dictionary.Remove(columnName.ToLower(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Removes the specified columnid.
        /// </summary>
        /// <param name="columnid">The columnid.</param>
        public void Remove(Columnid columnid)
        {
            Dictionary.Remove(columnid.Name.ToLower(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Fetches an enumerator containing all the columns in this table
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
        /// value must be of type ColumnDefinition;value
        /// or
        /// key must match value.Name;key
        /// </exception>
        protected override void OnValidate(object key, object value)
        {
            if (!(key is string))
            {
                throw new ArgumentException("key must be of type System.String", "key");
            }

            if (!(value is ColumnDefinition))
            {
                throw new ArgumentException("value must be of type ColumnDefinition", "value");
            }

            if (((string)key).ToLower(CultureInfo.InvariantCulture) != ((ColumnDefinition)value).Name.ToLower(CultureInfo.InvariantCulture))
            {
                throw new ArgumentException("key must match value.Name", "key");
            }
        }

        /// <summary>
        /// Checks the read only.
        /// </summary>
        /// <exception cref="System.NotSupportedException">this index collection cannot be changed</exception>
        private void CheckReadOnly()
        {
            if (this.readOnly)
            {
                throw new NotSupportedException("this index collection cannot be changed");
            }
        }
    }
}
