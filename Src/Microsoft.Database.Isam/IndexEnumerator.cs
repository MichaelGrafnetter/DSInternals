// ---------------------------------------------------------------------------
// <copyright file="IndexEnumerator.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation.  All rights reserved.
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

    using Microsoft.Isam.Esent.Interop;

    /// <summary>
    /// Enumerates the indices on a given table.
    /// </summary>
    public class IndexEnumerator : IEnumerator
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
        /// The enumerator
        /// </summary>
        private readonly IDictionaryEnumerator enumerator;

        /// <summary>
        /// The index list
        /// </summary>
        private JET_INDEXLIST indexList;

        /// <summary>
        /// The cleanup
        /// </summary>
        private bool cleanup = false;

        /// <summary>
        /// The moved
        /// </summary>
        private bool moved = false;

        /// <summary>
        /// The current
        /// </summary>
        private bool current = false;

        /// <summary>
        /// The index definition
        /// </summary>
        private IndexDefinition indexDefinition = null;

        /// <summary>
        /// The index update identifier
        /// </summary>
        private long indexUpdateID = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexEnumerator" /> class.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="tableName">Name of the table.</param>
        internal IndexEnumerator(IsamDatabase database, string tableName)
        {
            this.database = database;
            this.tableName = tableName;
            this.enumerator = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexEnumerator"/> class.
        /// </summary>
        /// <param name="enumerator">The enumerator.</param>
        internal IndexEnumerator(IDictionaryEnumerator enumerator)
        {
            this.database = null;
            this.enumerator = enumerator;
        }

        /// <summary>
        /// Finalizes an instance of the IndexEnumerator class
        /// </summary>
        ~IndexEnumerator()
        {
            this.Reset();
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>The current element in the collection.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// after last index on table
        /// or
        /// before first index on table
        /// </exception>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public IndexDefinition Current
        {
            get
            {
                if (this.database != null)
                {
                    lock (this.database.IsamSession)
                    {
                        if (!this.current)
                        {
                            if (this.moved)
                            {
                                throw new InvalidOperationException("after last index on table");
                            }
                            else
                            {
                                throw new InvalidOperationException("before first index on table");
                            }
                        }

                        if (this.indexDefinition == null || this.indexUpdateID != DatabaseCommon.SchemaUpdateID)
                        {
                            this.indexDefinition = IndexDefinition.Load(this.database, this.tableName, this.indexList);
                            this.indexUpdateID = DatabaseCommon.SchemaUpdateID;
                        }

                        return this.indexDefinition;
                    }
                }
                else
                {
                    return (IndexDefinition)this.enumerator.Value;
                }
            }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>The current element in the collection.</returns>
        /// <remarks>
        /// This is the standard version that will work with other CLR
        /// languages.
        /// </remarks>
        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            if (this.database != null)
            {
                lock (this.database.IsamSession)
                {
                    if (this.cleanup)
                    {
                        if (!this.database.IsamSession.Disposed)
                        {
                            // BUGBUG:  we will try to close an already closed tableid
                            // if it was already closed due to a rollback.  this could
                            // cause us to crash in ESENT due to lack of full validation
                            // in small config.  we should use cursor LS to detect when
                            // our cursor gets closed and thus avoid closing it again
                            Api.JetCloseTable(this.database.IsamSession.Sesid, this.indexList.tableid);
                        }
                    }

                    this.cleanup = false;
                    this.moved = false;
                    this.current = false;
                    this.indexDefinition = null;
                }
            }
            else
            {
                this.enumerator.Reset();
            }
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        public bool MoveNext()
        {
            if (this.database != null)
            {
                lock (this.database.IsamSession)
                {
                    if (!this.moved)
                    {
                        Api.JetGetIndexInfo(
                            this.database.IsamSession.Sesid,
                            this.database.Dbid,
                            this.tableName,
                            null,
                            out this.indexList,
                            JET_IdxInfo.List);
                        this.cleanup = true;
                        this.current = Api.TryMoveFirst(this.database.IsamSession.Sesid, this.indexList.tableid);
                    }
                    else if (this.current)
                    {
                        this.current = Api.TryMoveNext(this.database.IsamSession.Sesid, this.indexList.tableid);
                        if (!this.current)
                        {
                            Api.JetCloseTable(this.database.IsamSession.Sesid, this.indexList.tableid);
                            this.cleanup = false;
                        }
                    }

                    this.moved = true;
                    this.indexDefinition = null;
                    return this.current;
                }
            }
            else
            {
                return this.enumerator.MoveNext();
            }
        }
    }
}
