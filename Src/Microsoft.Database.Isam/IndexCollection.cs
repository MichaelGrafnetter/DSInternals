// ---------------------------------------------------------------------------
// <copyright file="IndexCollection.cs" company="Microsoft">
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
    /// A class containing information about the indices on a particular table.
    /// It is usually used for metadata discovery.
    /// </summary>
    public class IndexCollection : DictionaryBase, IEnumerable
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
        /// The cached index definition
        /// </summary>
        private string cachedIndexDefinition = null;

        /// <summary>
        /// The index definition
        /// </summary>
        private IndexDefinition indexDefinition = null;

        /// <summary>
        /// The index update identifier
        /// </summary>
        private long indexUpdateID = 0;

        /// <summary>
        /// The read only
        /// </summary>
        private bool readOnly = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexCollection"/> class. 
        /// </summary>
        public IndexCollection()
        {
            this.database = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexCollection"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="tableName">Name of the table.</param>
        internal IndexCollection(IsamDatabase database, string tableName)
        {
            this.database = database;
            this.tableName = tableName;
            this.readOnly = true;
        }

        /// <summary>
        /// Gets the names.
        /// </summary>
        /// <value>
        /// The names.
        /// </value>
        /// <exception cref="System.InvalidOperationException">the names of the indices in this index collection cannot be enumerated in this manner when accessing the table definition of an existing table</exception>
        public ICollection Names
        {
            get
            {
                if (this.database != null)
                {
                    // CONSIDER:  should we provide an ICollection of our index names to avoid this wart?
                    throw new InvalidOperationException(
                        "the names of the indices in this index collection cannot be enumerated in this manner when accessing the table definition of an existing table");
                }
                else
                {
                    return this.Dictionary.Keys;
                }
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
        /// Fetches the Index Definition for the specified index name.
        /// </summary>
        /// <value>
        /// The <see cref="IndexDefinition"/> corresponding to <paramref name="indexName"/>.
        /// </value>
        /// <param name="indexName">Name of the index.</param>
        /// <returns>An <see cref="IndexDefinition"/> object.</returns>
        public IndexDefinition this[string indexName]
        {
            get
            {
                if (this.database != null)
                {
                    lock (this.database.IsamSession)
                    {
                        JET_SESID sesid = this.database.IsamSession.Sesid;
                        if (this.cachedIndexDefinition != indexName.ToLower(CultureInfo.InvariantCulture)
                            || this.indexUpdateID != DatabaseCommon.SchemaUpdateID)
                        {
                            JET_INDEXLIST indexList;
                            Api.JetGetIndexInfo(
                                sesid,
                                this.database.Dbid,
                                this.tableName,
                                indexName,
                                out indexList,
                                JET_IdxInfo.List);
                            try
                            {
                                this.indexDefinition = IndexDefinition.Load(this.database, this.tableName, indexList);
                            }
                            finally
                            {
                                Api.JetCloseTable(sesid, indexList.tableid);
                            }

                            this.cachedIndexDefinition = indexName.ToLower(CultureInfo.InvariantCulture);
                            this.indexUpdateID = DatabaseCommon.SchemaUpdateID;
                        }

                        return this.indexDefinition;
                    }
                }
                else
                {
                    return (IndexDefinition)Dictionary[indexName.ToLower(CultureInfo.InvariantCulture)];
                }
            }

            set
            {
                this.Dictionary[indexName.ToLower(CultureInfo.InvariantCulture)] = value;
            }
        }

        /// <summary>
        /// Fetches an enumerator containing all the indices on this table.
        /// </summary>
        /// <returns>An enumerator containing all the indices on this table.</returns>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public new IndexEnumerator GetEnumerator()
        {
            if (this.database != null)
            {
                return new IndexEnumerator(this.database, this.tableName);
            }
            else
            {
                return new IndexEnumerator(this.Dictionary.GetEnumerator());
            }
        }

        /// <summary>
        /// Adds the specified index definition.
        /// </summary>
        /// <param name="indexDefinition">The index definition.</param>
        public void Add(IndexDefinition indexDefinition)
        {
            this.Dictionary.Add(indexDefinition.Name.ToLower(CultureInfo.InvariantCulture), indexDefinition);
        }

        /// <summary>
        /// Determines if the table contains an index with the given name.
        /// </summary>
        /// <param name="indexName">Name of the index.</param>
        /// <returns>Whether the collection contains an index of name <paramref name="indexName"/>.</returns>
        public bool Contains(string indexName)
        {
            if (this.database != null)
            {
                lock (this.database.IsamSession)
                {
                    bool exists = false;

                    try
                    {
                        int density;

                        Api.JetGetIndexInfo(
                            this.database.IsamSession.Sesid,
                            this.database.Dbid,
                            this.tableName,
                            indexName,
                            out density,
                            JET_IdxInfo.SpaceAlloc);
                        exists = true;
                    }
                    catch (EsentIndexNotFoundException)
                    {
                    }

                    return exists;
                }
            }
            else
            {
                return Dictionary.Contains(indexName.ToLower(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Removes the specified index name.
        /// </summary>
        /// <param name="indexName">Name of the index.</param>
        public void Remove(string indexName)
        {
            Dictionary.Remove(indexName.ToLower(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Fetches an enumerator containing all the indices on this table
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
        /// value must be of type IndexDefinition;value
        /// or
        /// key must match value.Name;key
        /// </exception>
        protected override void OnValidate(object key, object value)
        {
            if (!(key is string))
            {
                throw new ArgumentException("key must be of type System.String", "key");
            }

            if (!(value is IndexDefinition))
            {
                throw new ArgumentException("value must be of type IndexDefinition", "value");
            }

            if (((string)key).ToLower(CultureInfo.InvariantCulture)
                != ((IndexDefinition)value).Name.ToLower(CultureInfo.InvariantCulture))
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
