// ---------------------------------------------------------------------------
// <copyright file="TableCollection.cs" company="Microsoft">
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
    /// Manages a collection of tables for a database
    /// </summary>
    public class TableCollection : DictionaryBase, IEnumerable
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly IsamDatabase database;

        /// <summary>
        /// The read only
        /// </summary>
        private readonly bool readOnly = false;

        /// <summary>
        /// The cached table definition
        /// </summary>
        private string cachedTableDefinition = null;

        /// <summary>
        /// The table definition
        /// </summary>
        private TableDefinition tableDefinition = null;

        /// <summary>
        /// The table update identifier
        /// </summary>
        private long tableUpdateID = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableCollection"/> class. 
        /// </summary>
        public TableCollection()
        {
            this.database = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableCollection"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        internal TableCollection(IsamDatabase database)
        {
            this.database = database;
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
        /// Gets the names of the tables.
        /// </summary>
        /// <value>
        /// The names.
        /// </value>
        /// <exception cref="System.InvalidOperationException">the names of the tables in this table collection cannot be enumerated in this manner when accessing the table collection of a database</exception>
        public ICollection Names
        {
            get
            {
                if (this.database != null)
                {
                    // CONSIDER:  should we provide an ICollection of our table names to avoid this wart?
                    throw new InvalidOperationException("the names of the tables in this table collection cannot be enumerated in this manner when accessing the table collection of a database");
                }
                else
                {
                    return this.Dictionary.Keys;
                }
            }
        }

        /// <summary>
        /// Fetches the Table Definition for the specified table name
        /// </summary>
        /// <value>
        /// The <see cref="TableDefinition"/>.
        /// </value>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>A <see cref="TableDefinition"/> object representing the specified table.</returns>
        public TableDefinition this[string tableName]
        {
            get
            {
                if (this.database != null)
                {
                    lock (this.database.IsamSession)
                    {
                        if (this.cachedTableDefinition != tableName.ToLower(CultureInfo.InvariantCulture)
                            || this.tableUpdateID != DatabaseCommon.SchemaUpdateID)
                        {
                            JET_OBJECTINFO objectInfo;
                            Api.JetGetObjectInfo(
                                this.database.IsamSession.Sesid,
                                this.database.Dbid,
                                JET_objtyp.Table,
                                tableName,
                                out objectInfo);
                            this.tableDefinition = TableDefinition.Load(this.database, tableName, objectInfo);
                            this.cachedTableDefinition = tableName.ToLower(CultureInfo.InvariantCulture);
                            this.tableUpdateID = DatabaseCommon.SchemaUpdateID;
                        }

                        return this.tableDefinition;
                    }
                }
                else
                {
                    return (TableDefinition)Dictionary[tableName.ToLower(CultureInfo.InvariantCulture)];
                }
            }

            set
            {
                this.Dictionary[tableName.ToLower(CultureInfo.InvariantCulture)] = value;
            }
        }

        /// <summary>
        /// Determines if the database contains a table with the given name
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>Whether the named table exists in this database.</returns>
        public bool Contains(string tableName)
        {
            if (this.database != null)
            {
                lock (this.database.IsamSession)
                {
                    bool exists = false;

                    try
                    {
                        JET_OBJECTINFO objectInfo;
                        Api.JetGetObjectInfo(
                            this.database.IsamSession.Sesid,
                            this.database.Dbid,
                            JET_objtyp.Table,
                            tableName,
                            out objectInfo);
                        exists = true;
                    }
                    catch (EsentObjectNotFoundException)
                    {
                    }

                    return exists;
                }
            }
            else
            {
                return Dictionary.Contains(tableName.ToLower(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Fetches an enumerator containing all the tables in this database.
        /// </summary>
        /// <returns>An enumerator containing all the tables in this database.</returns>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public new TableEnumerator GetEnumerator()
        {
            if (this.database != null)
            {
                return new TableEnumerator(this.database);
            }
            else
            {
                return new TableEnumerator(this.Dictionary.GetEnumerator());
            }
        }

        /// <summary>
        /// Fetches an enumerator containing all the tables in this database
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
        /// Adds the specified table definition.
        /// </summary>
        /// <param name="tableDefinition">The table definition.</param>
        internal void Add(TableDefinition tableDefinition)
        {
            this.Dictionary.Add(tableDefinition.Name.ToLower(CultureInfo.InvariantCulture), tableDefinition);
        }

        /// <summary>
        /// Removes the specified table name.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        internal void Remove(string tableName)
        {
            Dictionary.Remove(tableName.ToLower(CultureInfo.InvariantCulture));
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
        /// value must be of type TableDefinition;value
        /// or
        /// key must match value.Name;key
        /// </exception>
        protected override void OnValidate(object key, object value)
        {
            if (!(key is string))
            {
                throw new ArgumentException("key must be of type System.String", "key");
            }

            if (!(value is TableDefinition))
            {
                throw new ArgumentException("value must be of type TableDefinition", "value");
            }

            if (((string)key).ToLower(CultureInfo.InvariantCulture) != ((TableDefinition)value).Name.ToLower(CultureInfo.InvariantCulture))
            {
                throw new ArgumentException("key must match value.Name", "key");
            }
        }

        /// <summary>
        /// Checks the read only.
        /// </summary>
        /// <exception cref="System.NotSupportedException">this table collection cannot be changed</exception>
        private void CheckReadOnly()
        {
            if (this.readOnly)
            {
                throw new NotSupportedException("this table collection cannot be changed");
            }
        }
    }
}
