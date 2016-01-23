// ---------------------------------------------------------------------------
// <copyright file="ColumnEnumerator.cs" company="Microsoft">
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
    /// Enumerates the columns in a given table.
    /// </summary>
    public class ColumnEnumerator : IEnumerator
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
        /// The column list
        /// </summary>
        private JET_COLUMNLIST columnList;

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
        /// The column definition
        /// </summary>
        private ColumnDefinition columnDefinition = null;

        /// <summary>
        /// The column update identifier
        /// </summary>
        private long columnUpdateID = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnEnumerator"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="tableName">Name of the table.</param>
        internal ColumnEnumerator(IsamDatabase database, string tableName)
        {
            this.database = database;
            this.tableName = tableName;
            this.enumerator = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnEnumerator"/> class.
        /// </summary>
        /// <param name="enumerator">The enumerator.</param>
        internal ColumnEnumerator(IDictionaryEnumerator enumerator)
        {
            this.database = null;
            this.enumerator = enumerator;
        }

        /// <summary>
        /// Finalizes an instance of the ColumnEnumerator class
        /// </summary>
        ~ColumnEnumerator()
        {
            this.Reset();
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>The current element in the collection.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// after last column in table
        /// or
        /// before first column in table
        /// </exception>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public ColumnDefinition Current
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
                                throw new InvalidOperationException("after last column in table");
                            }
                            else
                            {
                                throw new InvalidOperationException("before first column in table");
                            }
                        }

                        if (this.columnDefinition == null || this.columnUpdateID != DatabaseCommon.SchemaUpdateID)
                        {
                            this.columnDefinition = ColumnDefinition.Load(this.database, this.tableName, this.columnList);
                            this.columnUpdateID = DatabaseCommon.SchemaUpdateID;
                        }

                        return this.columnDefinition;
                    }
                }
                else
                {
                    return (ColumnDefinition)this.enumerator.Value;
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
                            Api.JetCloseTable(this.database.IsamSession.Sesid, this.columnList.tableid);
                        }
                    }

                    this.cleanup = false;
                    this.moved = false;
                    this.current = false;
                    this.columnDefinition = null;
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
                    JET_SESID sesid = this.database.IsamSession.Sesid;
                    if (!this.moved)
                    {
                        Api.JetGetColumnInfo(sesid, this.database.Dbid, this.tableName, null, out this.columnList);
                        this.cleanup = true;
                        this.current = Api.TryMoveFirst(sesid, this.columnList.tableid);
                    }
                    else if (this.current)
                    {
                        this.current = Api.TryMoveNext(sesid, this.columnList.tableid);
                        if (!this.current)
                        {
                            Api.JetCloseTable(sesid, this.columnList.tableid);
                            this.cleanup = false;
                        }
                    }

                    this.moved = true;
                    this.columnDefinition = null;
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
