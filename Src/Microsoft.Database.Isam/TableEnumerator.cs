// ---------------------------------------------------------------------------
// <copyright file="TableEnumerator.cs" company="Microsoft">
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

    using Microsoft.Isam.Esent.Interop;

    /// <summary>
    /// Enumerates the tables in a given database.
    /// </summary>
    public class TableEnumerator : IEnumerator
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly IsamDatabase database;

        /// <summary>
        /// The enumerator
        /// </summary>
        private readonly IDictionaryEnumerator enumerator;

        /// <summary>
        /// The object list
        /// </summary>
        private JET_OBJECTLIST objectList;

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
        /// The table definition
        /// </summary>
        private TableDefinition tableDefinition = null;

        /// <summary>
        /// The table update identifier
        /// </summary>
        private long tableUpdateID = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableEnumerator"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        internal TableEnumerator(IsamDatabase database)
        {
            this.database = database;
            this.enumerator = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableEnumerator"/> class.
        /// </summary>
        /// <param name="enumerator">The enumerator.</param>
        internal TableEnumerator(IDictionaryEnumerator enumerator)
        {
            this.database = null;
            this.enumerator = enumerator;
        }

        /// <summary>
        /// Finalizes an instance of the TableEnumerator class
        /// </summary>
        ~TableEnumerator()
        {
            this.Reset();
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>The current element in the collection.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// after last table in database
        /// or
        /// before first table in database
        /// </exception>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public TableDefinition Current
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
                                throw new InvalidOperationException("after last table in database");
                            }
                            else
                            {
                                throw new InvalidOperationException("before first table in database");
                            }
                        }

                        if (this.tableDefinition == null || this.tableUpdateID != DatabaseCommon.SchemaUpdateID)
                        {
                            this.tableDefinition = TableDefinition.Load(this.database, this.objectList);
                            this.tableUpdateID = DatabaseCommon.SchemaUpdateID;
                        }

                        return this.tableDefinition;
                    }
                }
                else
                {
                    return (TableDefinition)this.enumerator.Value;
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
                            Api.JetCloseTable(this.database.IsamSession.Sesid, this.objectList.tableid);
                        }
                    }

                    this.cleanup = false;
                    this.moved = false;
                    this.current = false;
                    this.tableDefinition = null;
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
                    // filter out anything that is not a normal table
                    ObjectInfoFlags flags = 0;
                    do
                    {
                        this.MoveNext_();

                        if (this.current)
                        {
                            flags = (ObjectInfoFlags)Api.RetrieveColumnAsUInt32(
                                    this.database.IsamSession.Sesid,
                                    this.objectList.tableid,
                                    this.objectList.columnidflags);
                        }
                    }
                    while (this.current && (flags & (ObjectInfoFlags.System | ObjectInfoFlags.TableTemplate)) != 0);

                    return this.current;
                }
            }
            else
            {
                return this.enumerator.MoveNext();
            }
        }

        /// <summary>
        /// Moves the next_.
        /// </summary>
        internal void MoveNext_()
        {
            if (!this.moved)
            {
                Api.JetGetObjectInfo(this.database.IsamSession.Sesid, this.database.Dbid, out this.objectList);
                this.cleanup = true;
                this.current = Api.TryMoveFirst(this.database.IsamSession.Sesid, this.objectList.tableid);
            }
            else if (this.current)
            {
                this.current = Api.TryMoveNext(this.database.IsamSession.Sesid, this.objectList.tableid);
                if (!this.current)
                {
                    Api.JetCloseTable(this.database.IsamSession.Sesid, this.objectList.tableid);
                    this.cleanup = false;
                }
            }

            this.moved = true;
            this.tableDefinition = null;
        }
    }
}
