// ---------------------------------------------------------------------------
// <copyright file="Cursor.cs" company="Microsoft">
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
    using System.Runtime.InteropServices;
    using Microsoft.Isam.Esent.Interop;
    using Microsoft.Isam.Esent.Interop.Vista;

    /// <summary>
    /// A cursor represents a location in a specific table and can be used to
    /// read and update the record at that position.
    /// </summary>
    public class Cursor : IDisposable, IEnumerable
    {
        /// <summary>
        /// The session
        /// </summary>
        private readonly IsamSession isamSession;

        /// <summary>
        /// The database
        /// </summary>
        private readonly DatabaseCommon database;

        /// <summary>
        /// The table name
        /// </summary>
        private readonly string tableName;

        /// <summary>
        /// The tableid
        /// </summary>
        private readonly JET_TABLEID tableid;

        /// <summary>
        /// The is sort
        /// </summary>
        private readonly bool isSort = false;

        /// <summary>
        /// The is sort or pre sort
        /// </summary>
        private readonly bool isSortOrPreSort = false;

        /// <summary>
        /// The is temporary table
        /// </summary>
        private readonly bool isTempTable = false;

        /// <summary>
        /// The cleanup
        /// </summary>
        private bool cleanup = false;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// The key start
        /// </summary>
        private byte[] keyStart = null;

        /// <summary>
        /// The grbit seek start
        /// </summary>
        private SeekGrbit grbitSeekStart;

        /// <summary>
        /// The grbit range start
        /// </summary>
        private SetIndexRangeGrbit grbitRangeStart;

        /// <summary>
        /// The key end
        /// </summary>
        private byte[] keyEnd = null;

        /// <summary>
        /// The grbit seek end
        /// </summary>
        private SeekGrbit grbitSeekEnd;

        /// <summary>
        /// The grbit range end
        /// </summary>
        private SetIndexRangeGrbit grbitRangeEnd;

        /// <summary>
        /// The move next
        /// </summary>
        private bool moveNext = false;

        /// <summary>
        /// The move previous
        /// </summary>
        private bool movePrev = false;

        /// <summary>
        /// The out of range
        /// </summary>
        private bool outOfRange = true;

        /// <summary>
        /// The updating
        /// </summary>
        private bool updating = false;

        /// <summary>
        /// The record
        /// </summary>
        private ColumnAccessor record;

        /// <summary>
        /// The edit record
        /// </summary>
        private ColumnAccessor editRecord;

        /// <summary>
        /// The index record
        /// </summary>
        private ColumnAccessor indexRecord;

        /// <summary>
        /// The table definition
        /// </summary>
        private TableDefinition tableDefinition = null;

        /// <summary>
        /// The table update identifier
        /// </summary>
        private long tableUpdateID = 0;

        /// <summary>
        /// The index definition
        /// </summary>
        private IndexDefinition indexDefinition = null;

        /// <summary>
        /// The index update identifier
        /// </summary>
        private long indexUpdateID = 0;

        /// <summary>
        /// The fields
        /// </summary>
        private FieldCollection fields = null;

        /// <summary>
        /// The transaction identifier
        /// </summary>
        private long transactionID = 0;

        /// <summary>
        /// The update identifier
        /// </summary>
        private long updateID = 0;

        /// <summary>
        /// The in insert mode
        /// </summary>
        private bool inInsertMode = false;

        /// <summary>
        /// The in retrieve mode
        /// </summary>
        private bool inRetrieveMode = false;

        /// <summary>
        /// The on before first
        /// </summary>
        private bool onBeforeFirst = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cursor"/> class.
        /// </summary>
        /// <param name="isamSession">The session.</param>
        /// <param name="database">The database.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="grbit">The grbit.</param>
        internal Cursor(IsamSession isamSession, IsamDatabase database, string tableName, OpenTableGrbit grbit)
        {
            lock (isamSession)
            {
                this.isamSession = isamSession;
                this.database = database;
                this.tableName = tableName;
                Api.JetOpenTable(isamSession.Sesid, database.Dbid, tableName, null, 0, grbit, out this.tableid);
                this.cleanup = true;
                this.record = new ColumnAccessor(this, isamSession, this.tableid, RetrieveColumnGrbit.None);
                this.editRecord = new ColumnAccessor(this, isamSession, this.tableid, RetrieveColumnGrbit.RetrieveCopy);
                this.indexRecord = new ColumnAccessor(this, isamSession, this.tableid, RetrieveColumnGrbit.RetrieveFromIndex);

                this.MoveBeforeFirst();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cursor"/> class.
        /// </summary>
        /// <param name="isamSession">The session.</param>
        /// <param name="database">The database.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="tableid">The tableid.</param>
        /// <param name="inInsertMode">if set to <c>true</c> [in insert mode].</param>
        internal Cursor(
            IsamSession isamSession,
            TemporaryDatabase database,
            string tableName,
            JET_TABLEID tableid,
            bool inInsertMode)
        {
            lock (isamSession)
            {
                this.isamSession = isamSession;
                this.database = database;
                this.tableName = tableName;
                this.tableid = tableid;
                this.cleanup = true;
                this.record = new ColumnAccessor(this, isamSession, tableid, RetrieveColumnGrbit.None);
                this.editRecord = new ColumnAccessor(this, isamSession, tableid, RetrieveColumnGrbit.RetrieveCopy);
                this.indexRecord = new ColumnAccessor(this, isamSession, tableid, RetrieveColumnGrbit.RetrieveFromIndex);
                this.isSort = database.Tables[tableName].Type == TableType.Sort;
                this.isSortOrPreSort = database.Tables[tableName].Type == TableType.Sort
                                       || database.Tables[tableName].Type == TableType.PreSortTemporary;
                this.isTempTable = database.Tables[tableName].Type == TableType.Sort
                                   || database.Tables[tableName].Type == TableType.PreSortTemporary
                                   || database.Tables[tableName].Type == TableType.Temporary;
                this.inInsertMode = this.isSortOrPreSort && inInsertMode;
                this.inRetrieveMode = this.isSort && !inInsertMode;
                this.onBeforeFirst = this.isSort && !inInsertMode;

                if (!(this.isSort || (this.isSortOrPreSort && inInsertMode)))
                {
                    this.MoveBeforeFirst();
                }
            }
        }

        /// <summary>
        /// Finalizes an instance of the Cursor class
        /// </summary>
        ~Cursor()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the database that created this cursor
        /// </summary>
        public DatabaseCommon Database
        {
            get
            {
                return this.database;
            }
        }

        /// <summary>
        /// Gets the definition for the table under this cursor
        /// </summary>
        public TableDefinition TableDefinition
        {
            get
            {
                lock (this.isamSession)
                {
                    this.CheckDisposed();

                    if (this.tableDefinition == null || (this.tableUpdateID != DatabaseCommon.SchemaUpdateID && !this.isTempTable))
                    {
                        this.tableDefinition = this.database.Tables[this.tableName];
                        this.tableUpdateID = DatabaseCommon.SchemaUpdateID;
                    }

                    return this.tableDefinition;
                }
            }
        }

        /// <summary>
        /// Gets the definition for the current index of this cursor
        /// </summary>
        public IndexDefinition CurrentIndexDefinition
        {
            get
            {
                lock (this.isamSession)
                {
                    this.CheckDisposed();

                    if (this.indexDefinition == null || (this.indexUpdateID != DatabaseCommon.SchemaUpdateID && !this.isTempTable))
                    {
                        this.indexDefinition = TableDefinition.Indices[this.CurrentIndex];
                        this.indexUpdateID = DatabaseCommon.SchemaUpdateID;
                    }

                    return this.indexDefinition;
                }
            }
        }

        /// <summary>
        /// Gets a collection containing all the field values for the
        /// current record.  If a record is currently being inserted or updated
        /// then the field values will reflect the new data.
        /// </summary>
        /// <remarks>
        /// The field values are cached in the Cursor object.  This cache will
        /// be automatically updated if the Cursor moves to a new record or if
        /// another Session updates the record.  However, this cache will NOT
        /// be updated if another Cursor belonging to the same Session updates
        /// the record.  The cache can be forced to be updated by performing a
        /// Move( 0 ) on the containing Cursor.
        /// <para>
        /// The cache will be reloaded on every call if the Session that opened
        /// this Cursor is not in a transaction.  This is required because
        /// the record data can change at any time when not in a transaction.
        /// If repeated references to Cursor.Fields will be made, they should
        /// all be done inside the same transaction to avoid poor performance.
        /// </para>
        /// </remarks>
        public FieldCollection Fields
        {
            get
            {
                lock (this.isamSession)
                {
                    this.CheckDisposed();

                    if (this.fields == null
                        || this.isamSession.TransactionLevel == 0
                        || this.transactionID != this.isamSession.TransactionID
                        || this.updateID != this.editRecord.UpdateID)
                    {
                        // we always ask for the copy buffer and will only get
                        // it if we are actually doing an insert or update
                        this.fields = this.GetFields(RetrieveColumnGrbit.RetrieveCopy);
                        this.transactionID = this.isamSession.TransactionID;
                        this.updateID = this.editRecord.UpdateID;
                    }

                    return this.fields;
                }
            }
        }

        /// <summary>
        /// Gets the current record for the cursor
        /// </summary>
        /// <remarks>
        /// The field values seen through this column accessor will
        /// represent the original data in the record during an update
        /// operation.
        /// </remarks>
        public ColumnAccessor Record
        {
            get
            {
                this.CheckDisposed();
                return this.record;
            }
        }

        /// <summary>
        /// Gets the current record for the cursor
        /// </summary>
        /// <remarks>
        /// The field values seen through this column accessor will
        /// represent the modified data in the record during an update
        /// operation.
        /// <para>
        /// Only this column accessor may be used to set fields in a record.
        /// </para>
        /// </remarks>
        public ColumnAccessor EditRecord
        {
            get
            {
                this.CheckDisposed();
                return this.editRecord;
            }
        }

        /// <summary>
        /// Gets the current record for the cursor
        /// </summary>
        /// <remarks>
        /// The field values seen through this column accessor will
        /// represent the original data in the record during an update
        /// operation.
        /// <para>
        /// Fetching field values through this column accessor for columns that
        /// are also key columns in the current index may result in improved
        /// performance.  This is because in some cases, the field value may be
        /// computed from the index entry rather that fetched from the record.
        /// </para>
        /// </remarks>
        public ColumnAccessor IndexRecord
        {
            get
            {
                this.CheckDisposed();
                return this.indexRecord;
            }
        }

        /// <summary>
        /// Gets or sets the current index of this cursor.
        /// </summary>
        /// <remarks>
        /// If the table has no primary index then the name of the current
        /// index is an empty string.
        /// </remarks>
        public string CurrentIndex
        {
            get
            {
                lock (this.isamSession)
                {
                    this.CheckDisposed();

                    // if this is a TT then return always return the name of
                    // its one index (or "" if it is an implicit seq index)
                    if (this.isTempTable)
                    {
                        foreach (IndexDefinition indexDefinition in this.TableDefinition.Indices)
                        {
                            return indexDefinition.Name;
                        }

                        return string.Empty;
                    }
                    else
                    {
                        string currentIndex = null;
                        Api.JetGetCurrentIndex(this.isamSession.Sesid, this.tableid, out currentIndex, 255);
                        return currentIndex;
                    }
                }
            }

            set
            {
                this.SetCurrentIndex(value);
            }
        }

        /// <summary>
        /// Gets or sets the key corresponding to the current record for the current index
        /// of the cursor.
        /// </summary>
        public Key Key
        {
            get
            {
                this.CheckDisposed();

                // BUGBUG:  this doesn't always work for multi-valued columns
                // because we have no idea which value we should use
                Key key = new Key();
                foreach (KeyColumn keyColumn in this.CurrentIndexDefinition.KeyColumns)
                {
                    key.Add(this.Record[keyColumn.Columnid]);
                }

                return key;
            }

            set
            {
                this.GotoKey(value);
            }
        }

        /// <summary>
        /// Gets or sets the position of the current record for the current index of the
        /// cursor.
        /// </summary>
        public Position Position
        {
            get
            {
                lock (this.isamSession)
                {
                    this.CheckDisposed();
                    this.CheckRecord();

                    JET_RECPOS recpos;
                    Api.JetGetRecordPosition(this.isamSession.Sesid, this.tableid, out recpos);
                    return new Position((int)recpos.centriesLT, (int)recpos.centriesTotal);
                }
            }

            set
            {
                this.GotoPosition(value);
            }
        }

        /// <summary>
        /// Gets or sets the location of the current record for the current index of the
        /// cursor.
        /// </summary>
        public Location Location
        {
            get
            {
                lock (this.isamSession)
                {
                    this.CheckDisposed();
                    this.CheckRecord();

                    string indexName = this.CurrentIndex;

                    // Unfortunately there isn't an easy exception-free way to tell if we've already set a current
                    // index. JetGetCurrentIndex() will return the name of the Primary index, and is
                    // it worthwhile to then call JetGetIndexInfo() to determine if it's
                    // primary/secondary? We'll just deal with the CLR exception.
                    try
                    {
                        byte[] primaryBookmark;
                        byte[] secondaryBookmark = Api.GetSecondaryBookmark(
                            this.isamSession.Sesid,
                            this.tableid,
                            out primaryBookmark);
                        return new Location(indexName, primaryBookmark, secondaryBookmark);
                    }
                    catch (EsentNoCurrentIndexException)
                    {
                        return new Location(indexName, Api.GetBookmark(this.isamSession.Sesid, this.tableid), null);
                    }
                }
            }

            set
            {
                this.GotoLocation(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [disposed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [disposed]; otherwise, <c>false</c>.
        /// </value>
        internal bool Disposed
        {
            get
            {
                return this.disposed || this.database.Disposed;
            }

            set
            {
                this.disposed = value;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            lock (this)
            {
                this.Dispose(true);
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Move the cursor to the next record on the current index
        /// </summary>
        /// <returns>true if the cursor ends up on a record, false if the cursor is beyond the end of the index</returns>
        public bool MoveNext()
        {
            return this.Move(1);
        }

        /// <summary>
        /// Move the cursor to the previous record on the current index
        /// </summary>
        /// <returns>true if the cursor ends up on a record, false if the cursor is beyond the start of the index</returns>
        public bool MovePrevious()
        {
            return this.Move(-1);
        }

        /// <summary>
        /// Move the cursor by the specified offset on the current index
        /// </summary>
        /// <param name="rows">A signed number of records to skip.  Positive numbers will cause the cursor to move toward the end of the current index.  Negative numbers will cause the cursor to move toward the start of the current index.</param>
        /// <returns>true if the cursor ends up on a record, false if the cursor is beyond the start or end of the index</returns>
        /// <remarks>
        /// Moving by an offset of zero is allowed and can be used to test if
        /// the cursor is currently on a record.  It will also force any cached
        /// data for the current record to be refreshed.
        /// <para>
        /// Large offsets used on large tables may cause this method to take
        /// quite some time to complete.
        /// </para>
        /// </remarks>
        public bool Move(int rows)
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                // if this is a Sort then move prev is illegal
                //
                // NOTE:  this is a hack to work around problems in ESE/ESENT
                // that we cannot fix because we must work downlevel
                if (this.isSort && rows < 0)
                {
                    throw new EsentIllegalOperationException();
                }

                this.OnNavigation();

                // if this is a Sort and we are on a virtual before first and
                // we are moving to the current position then return that
                // no record was found
                //
                // NOTE:  this is a hack to work around problems in ESE/ESENT
                // that we cannot fix because we must work downlevel
                if (this.isSort && this.onBeforeFirst && rows == 0)
                {
                    return false;
                }

                // setup our index range
                if (rows < 0)
                {
                    this.SetLowerLimit();
                }

                if (rows > 0)
                {
                    this.SetUpperLimit();
                }

                // try to move by the desired offset
                bool found = Api.TryMove(this.isamSession.Sesid, this.tableid, unchecked((JET_Move)rows), MoveGrbit.None);
                this.onBeforeFirst = false;

                // clear our field cache because the current record has changed
                this.fields = null;

                return found;
            }
        }

        /// <summary>
        /// Move the cursor to before the first record on the current index
        /// </summary>
        /// <remarks>
        /// This is a logical position, such that calling MoveNext() will leave the cursor
        /// on the first record in the table. Trying to retrieve column values when the cursor
        /// is before the first record is invalid.
        /// </remarks>
        public void MoveBeforeFirst()
        {
            lock (this.isamSession)
            {
                bool prevInRetrieveMode = this.inRetrieveMode;

                this.CheckDisposed();

                // if this is a Sort and we did not just enter retrieve mode
                // then this attempt to move before first is illegal
                //
                // NOTE:  this is a hack to work around problems in ESE/ESENT
                // that we cannot fix because we must work downlevel
                if (this.isSort && prevInRetrieveMode == true)
                {
                    throw new EsentIllegalOperationException();
                }

                this.OnNavigation();

                // if this is a Sort and we did just enter retrieve mode then
                // do nothing because Sorts (unlike normal tables) are
                // positioned on before first by default (rather than on first)
                //
                // NOTE:  this is a hack to work around problems in ESE/ESENT
                // that we cannot fix because we must work downlevel
                if (this.isSort && prevInRetrieveMode == false)
                {
                    this.onBeforeFirst = true;
                }
                else
                {
                    // let's actually move before first
                    //
                    // if an index range is not specified, simply move first
                    if (this.keyStart == null)
                    {
                        Api.TryMoveFirst(this.isamSession.Sesid, this.tableid);
                    }
                    else
                    {
                        // if an index range is specified, seek to its lower limit
                        Api.MakeKey(this.isamSession.Sesid, this.tableid, this.keyStart, MakeKeyGrbit.NormalizedKey);
                        try
                        {
                            Api.JetSeek(this.isamSession.Sesid, this.tableid, this.grbitSeekStart);
                        }
                        catch (EsentRecordNotFoundException)
                        {
                        }
                    }

                    // back up one to account for the MoveBeforeFirst/MoveNext
                    // iterator model of CLR
                    Api.TryMovePrevious(this.isamSession.Sesid, this.tableid);
                }

                // clear our navigation direction
                this.moveNext = false;
                this.movePrev = false;

                // we are initially outside of the current range
                this.outOfRange = true;

                // clear our field cache because the current record has changed
                this.fields = null;
            }
        }

        /// <summary>
        /// Move the cursor to after the last record on the current index
        /// </summary>
        /// <remarks>
        /// This is a logical position, such that calling MovePrevious() will leave the cursor
        /// on the last record in the table. Trying to retrieve column values when the cursor
        /// is after the last record is invalid.
        /// </remarks>
        public void MoveAfterLast()
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();
                this.OnNavigation();

                // if an index range is not specified, simply move last
                if (this.keyEnd == null)
                {
                    Api.TryMoveLast(this.isamSession.Sesid, this.tableid);
                }
                else
                {
                    // if an index range is specified, seek to its upper limit
                    Api.MakeKey(this.isamSession.Sesid, this.tableid, this.keyEnd, MakeKeyGrbit.NormalizedKey);

                    // Ignore the return code. We don't care if TrySeek() returns false.
                    Api.TrySeek(this.isamSession.Sesid, this.tableid, this.grbitSeekEnd);
                }

                // move down one to account for the MoveAfterLast/MovePrevious
                // iterator model of CLR
                Api.TryMoveNext(this.isamSession.Sesid, this.tableid);

                // clear our navigation direction
                this.moveNext = false;
                this.movePrev = false;

                // we are initially outside of the current range
                this.outOfRange = true;

                // clear our field cache because the current record has changed
                this.fields = null;
            }
        }

        /// <summary>
        /// Set the current index of the cursor
        /// </summary>
        /// <param name="indexName">The name of the index on the table for this cursor</param>
        /// <remarks>
        /// This will affect the order records are traversed by the MoveNext,
        /// MovePrevious and Move methods. The cursor will be positioned before
        /// the first record on the index.
        /// <para>
        /// As a side effect, any restriction in effect will be cleared.
        /// </para>
        /// </remarks>
        public void SetCurrentIndex(string indexName)
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                // don't do anything if we are already on the new index
                if (string.Compare(indexName, this.CurrentIndex, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    // if this is a TT then selecting another index will fail
                    if (this.isTempTable)
                    {
                        throw new EsentIndexNotFoundException();
                    }

                    this.OnNavigation();

                    // select the new index
                    Api.JetSetCurrentIndex(this.isamSession.Sesid, this.tableid, indexName);

                    // purge our cached index definition
                    this.indexDefinition = null;

                    // clear our field cache because the current record has changed
                    this.fields = null;

                    // clear our index range
                    this.FindAllRecords();

                    // move before first on the new index
                    this.MoveBeforeFirst();
                }
            }
        }

        /// <summary>
        /// Set the current index of the cursor
        /// </summary>
        /// <param name="indexName">The name of the index on the table for this cursor</param>
        /// <remarks>
        /// This will affect the order records are traversed by the MoveNext,
        /// MovePrevious and Move methods.  The cursor will be positioned on
        /// the new index at the first entry corresponding to the same record
        /// for the entry in the old index.  If no such entry exists because
        /// that record does not have an entry in the new index then the
        /// operation will fail with EsentNoCurrentRecordException.
        /// <para>
        /// As a side effect, any restriction in effect will be cleared.
        /// </para>
        /// </remarks>
        public void MoveToIndex(string indexName)
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                // don't do anything if we are already on the new index
                if (string.Compare(indexName, this.CurrentIndex, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    // if this is a TT then selecting another index will fail
                    if (this.isTempTable)
                    {
                        throw new EsentIndexNotFoundException();
                    }

                    // select the new index and attempt to maintain our
                    // position on this record
                    Api.JetSetCurrentIndex2(this.isamSession.Sesid, this.tableid, indexName, SetCurrentIndexGrbit.NoMove);

                    // purge our cached index definition
                    this.indexDefinition = null;

                    // clear our index range
                    this.FindAllRecords();
                }
            }
        }

        /// <summary>
        /// Position the cursor at the specified key on the current index
        /// </summary>
        /// <param name="key">The full key for the desired record on the current index of the cursor</param>
        /// <remarks>
        /// Only fully qualified keys are allowed.  Partial keys and wildcards
        /// are forbidden.
        /// </remarks>
        /// <returns>true if a record was found, false otherwise</returns>
        public bool GotoKey(Key key)
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                // if this is a Sort then seek is illegal
                //
                // NOTE:  this is a hack to work around problems in ESE/ESENT
                // that we cannot fix because we must work downlevel
                if (this.isSort)
                {
                    throw new EsentIllegalOperationException();
                }

                this.OnNavigation();

                // we only allow fully qualified keys here
                if (key.HasPrefix)
                {
                    throw new ArgumentException("Keys containing prefixes are forbidden", "key");
                }

                if (key.HasWildcard)
                {
                    throw new ArgumentException("Keys containing wildcards are forbidden", "key");
                }

                // compute the key for our seek
                Api.MakeKey(this.isamSession.Sesid, this.tableid, this.MakeKey(key, false), MakeKeyGrbit.NormalizedKey);

                // seek for the record that exactly matches this key and remember
                // if we found it
                bool found = Api.TrySeek(this.isamSession.Sesid, this.tableid, SeekGrbit.SeekEQ);

                // clear our field cache because the current record has changed
                this.fields = null;

                // if we found a record outside the index range then it doesn't count
                found = this.CheckRange() && found;

                return found;
            }
        }

        /// <summary>
        /// Goto the given fractional position on the current index
        /// </summary>
        /// <param name="position">The desired position on the current index of the cursor</param>
        /// <remarks>
        /// As a side effect, any restriction in effect will be cleared.
        /// </remarks>
        public void GotoPosition(Position position)
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();
                this.OnNavigation();

                // clear our index range
                this.FindAllRecords();

                // go to the given fractional position on the current index
                JET_RECPOS recpos = new JET_RECPOS();
                recpos.centriesLT = position.Entry;
                recpos.centriesTotal = position.TotalEntries;
                Api.JetGotoPosition(this.isamSession.Sesid, this.tableid, recpos);

                // clear our field cache because the current record has changed
                this.fields = null;
            }
        }

        /// <summary>
        /// Goto the given location on the current index
        /// </summary>
        /// <param name="location">The desired location on the current index of the cursor</param>
        /// <remarks>
        /// As a side effect, any restriction in effect will be cleared.
        /// </remarks>
        public void GotoLocation(Location location)
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();
                this.OnNavigation();

                // clear our index range
                this.FindAllRecords();

                if (location.SecondaryBookmark != null
                    && string.Compare(location.IndexName, this.CurrentIndex, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    int primarybookmarkLength = location.PrimaryBookmark == null ? 0 : location.PrimaryBookmark.Length;

                    // we are on the same index, so use the secondary index bookmark
                    try
                    {
                        Api.JetGotoSecondaryIndexBookmark(
                            this.isamSession.Sesid,
                            this.tableid,
                            location.SecondaryBookmark,
                            location.SecondaryBookmark.Length,
                            location.PrimaryBookmark,
                            primarybookmarkLength,
                            GotoSecondaryIndexBookmarkGrbit.None);
                    }
                    catch (EsentNoCurrentIndexException)
                    {
                        Api.JetGotoBookmark(
                            this.isamSession.Sesid,
                            this.tableid,
                            location.PrimaryBookmark,
                            location.PrimaryBookmark.Length);
                    }
                }
                else
                {
                    // the index has changed, use the primary bookmark
                    Api.JetGotoBookmark(
                        this.isamSession.Sesid,
                        this.tableid,
                        location.PrimaryBookmark,
                        location.PrimaryBookmark.Length);
                }

                // clear our field cache because the current record has changed
                this.fields = null;

                // if the location is outside the index range then fail
                if (!this.CheckRange())
                {
                    throw new EsentNoCurrentRecordException();
                }
            }
        }

        /// <summary>
        /// Restricts the records that are visible to the cursor to those that
        /// match the given key by the given criteria.  The key may contain
        /// prefix or wildcard key segments which can be used to further
        /// qualify the desired matching records.
        /// </summary>
        /// <param name="criteria">The inequality used to specify which records to find on the current index</param>
        /// <param name="key">The partial or full key used to specify which records to find on the current index</param>
        /// <remarks>
        /// The restriction will remain in effect until explicitly reset or
        /// until implicitly reset by other methods as noted.
        /// <para>
        /// Any previously defined restriction will be cleared.
        /// </para>
        /// <para>
        /// The cursor will be positioned before the first record in the new
        /// restriction.
        /// </para>
        /// </remarks>
        public void FindRecords(MatchCriteria criteria, Key key)
        {
            switch (criteria)
            {
                case MatchCriteria.LessThan:
                this.FindRecordsBetween(Key.Start, BoundCriteria.Inclusive, key, BoundCriteria.Exclusive);
                    break;

                case MatchCriteria.LessThanOrEqualTo:
                    this.FindRecordsBetween(Key.Start, BoundCriteria.Inclusive, key, BoundCriteria.Inclusive);
                    break;

                case MatchCriteria.EqualTo:
                    this.FindRecordsBetween(key, BoundCriteria.Inclusive, key, BoundCriteria.Inclusive);
                    break;

                case MatchCriteria.GreaterThanOrEqualTo:
                    this.FindRecordsBetween(key, BoundCriteria.Inclusive, Key.End, BoundCriteria.Inclusive);
                    break;

                case MatchCriteria.GreaterThan:
                    this.FindRecordsBetween(key, BoundCriteria.Exclusive, Key.End, BoundCriteria.Inclusive);
                    break;
            }
        }

        /// <summary>
        /// Restricts the records that are visible to the cursor to a range of
        /// the current index delineated by the specified keys.
        /// </summary>
        /// <param name="keyStart">The partial or full key used to set the start of the records to find on the current index</param>
        /// <param name="criteriaStart">Indicates if the starting key is inclusive or exclusive</param>
        /// <param name="keyEnd">The partial or full key used to set the end of the records to find on the current index</param>
        /// <param name="criteriaEnd">Indicates if the ending key is inclusive or exclusive</param>
        /// <remarks>
        /// The restriction will remain in effect until explicitly reset or
        /// until implicitly reset by other methods as noted.
        /// <para>
        /// Any previously defined restriction will be cleared.
        /// </para>
        /// <para>
        /// The cursor will be positioned before the first record in the new
        /// restriction.
        /// </para>
        /// </remarks>
        public void FindRecordsBetween(Key keyStart, BoundCriteria criteriaStart, Key keyEnd, BoundCriteria criteriaEnd)
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();
                this.OnNavigation();

                // clear our index range
                this.FindAllRecords();

                // setup the effective index range to be bounded by the specified keys
                this.keyStart = this.MakeKey(keyStart, false);
                this.grbitSeekStart = criteriaStart == BoundCriteria.Inclusive ? SeekGrbit.SeekGE : SeekGrbit.SeekGT;
                this.grbitRangeStart = criteriaStart == BoundCriteria.Inclusive
                                      ? SetIndexRangeGrbit.RangeInclusive
                                      : SetIndexRangeGrbit.None;
                this.keyEnd = this.MakeKey(keyEnd, true);
                this.grbitSeekEnd = criteriaEnd == BoundCriteria.Inclusive ? SeekGrbit.SeekLE : SeekGrbit.SeekLT;
                this.grbitRangeEnd = (criteriaEnd == BoundCriteria.Inclusive
                                     ? SetIndexRangeGrbit.RangeInclusive
                                     : SetIndexRangeGrbit.None) | SetIndexRangeGrbit.RangeUpperLimit;

                // move before first on the new index range
                this.MoveBeforeFirst();
            }
        }

        /// <summary>
        /// Restores the visibility of all records on the current index to the
        /// cursor.
        /// </summary>
        /// <remarks>
        /// The cursor position will remain unchanged.
        /// </remarks>
        public void FindAllRecords()
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                // cancel any index range that is in effect
                if (this.keyStart != null || this.keyEnd != null)
                {
                    // Unfortunately, there doesn't seem to be a way to remove the index range
                    // without throwing an exception.
                    try
                    {
                        Api.JetSetIndexRange(this.isamSession.Sesid, this.tableid, SetIndexRangeGrbit.RangeRemove);
                    }
                    catch (EsentInvalidOperationException)
                    {
                        // This just means there wasn't an index range already set.
                    }
                }

                // setup the effective index range to be the entire index
                this.keyStart = null;
                this.keyEnd = null;

                // clear our navigation direction
                this.moveNext = false;
                this.movePrev = false;

                // we can never be out of range w/o a range
                this.outOfRange = false;
            }
        }

        /// <summary>
        /// Create a new record for insertion into the table.
        /// </summary>
        /// <remarks>
        /// The record will not be inserted until the changes are accepted.
        /// <para>
        /// Changes must be made to the record's fields through EditRecord.
        /// </para>
        /// <para>
        /// It is illegal to insert a new record when already inserting or
        /// updating a record.
        /// </para>
        /// </remarks>
        public void BeginEditForInsert()
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                if (this.updating)
                {
                    throw new InvalidOperationException("It is illegal to insert a new record when already inserting or updating a record.");
                }

                // prepare for the insert
                Api.JetPrepareUpdate(this.isamSession.Sesid, this.tableid, JET_prep.Insert);
                this.updating = true;

                // clear our field cache because we are working on a new record
                this.fields = null;
            }
        }

        /// <summary>
        /// Prepare to update the current record
        /// </summary>
        /// <remarks>
        /// The record will not be updated until the changes are accepted.
        /// <para>
        /// Changes must be made to the record's fields through EditRecord.
        /// </para>
        /// <para>
        /// It is illegal to update a record when already inserting or updating
        /// a record.
        /// </para>
        /// </remarks>
        public void BeginEditForUpdate()
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                if (this.updating)
                {
                    throw new InvalidOperationException("It is illegal to update a record when already inserting or updating a record.");
                }

                Api.JetPrepareUpdate(this.isamSession.Sesid, this.tableid, JET_prep.Replace);
                this.updating = true;
            }
        }

        /// <summary>
        /// Discard any changes made to the current record or the new record
        /// </summary>
        /// <remarks>
        /// It is illegal to cancel an insert or update when not inserting or
        /// updating a record.
        /// </remarks>
        public void RejectChanges()
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                if (!this.updating)
                {
                    throw new InvalidOperationException("It is illegal to cancel an insert or update when not inserting or updating a record.");
                }

                Api.JetPrepareUpdate(this.isamSession.Sesid, this.tableid, JET_prep.Cancel);
                this.updating = false;

                // clear our field cache because our record data has changed
                this.fields = null;
            }
        }

        /// <summary>
        /// Discard the changes made to the current record or the new record
        /// </summary>
        /// <remarks>
        /// It is illegal to accept an insert or update when not inserting or
        /// updating a record.
        /// </remarks>
        public void AcceptChanges()
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                if (!this.updating)
                {
                    throw new InvalidOperationException("It is illegal to accept an insert or update when not inserting or updating a record.");
                }

                Api.JetUpdate(this.isamSession.Sesid, this.tableid);
                this.updating = false;
            }
        }

        /// <summary>
        /// Delete the current record
        /// </summary>
        /// <remarks>
        /// It is illegal to delete a record when inserting or updating a
        /// record.
        /// </remarks>
        public void Delete()
        {
            lock (this.isamSession)
            {
                this.CheckDisposed();

                if (this.updating)
                {
                    throw new InvalidOperationException("It is illegal to delete a record when inserting or updating a record.");
                }

                Api.JetDelete(this.isamSession.Sesid, this.tableid);
                this.updating = false;
            }

            // clear our field cache because our record data has been deleted
            this.fields = null;
        }

        /// <summary>
        /// Fetch an enumerator containing all the records visible to the cursor.
        /// </summary>
        /// <returns>An enumerator containing all the records visible to the cursor.</returns>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public CursorEnumerator GetEnumerator()
        {
            return new CursorEnumerator(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.Dispose();
        }

        /// <summary>
        /// Fetch an enumerator containing all the records visible to the cursor
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <remarks>
        /// This will change the current position of the cursor.
        /// This is the standard version that will work with other CLR
        /// languages.
        /// </remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

        /// <summary>
        /// Checks the disposed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">
        /// Thrown when the object is already disposed.
        /// </exception>
        internal void CheckDisposed()
        {
            lock (this.isamSession)
            {
                if (this.Disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }
            }
        }

        /// <summary>
        /// Checks the record.
        /// </summary>
        /// <exception cref="EsentNoCurrentRecordException">
        /// Thrown when the cursor is not on a record.
        /// </exception>
        internal void CheckRecord()
        {
            lock (this.isamSession)
            {
                if (this.outOfRange)
                {
                    throw new EsentNoCurrentRecordException();
                }
            }
        }

        /// <summary>
        /// Checks the not updating.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">It is illegal to move to a different record when inserting or updating a record.</exception>
        internal void CheckNotUpdating()
        {
            lock (this.isamSession)
            {
                if (this.updating)
                {
                    throw new InvalidOperationException("It is illegal to move to a different record when inserting or updating a record.");
                }
            }
        }

        /// <summary>
        /// Called when [navigation].
        /// </summary>
        internal void OnNavigation()
        {
            lock (this.isamSession)
            {
                this.CheckNotUpdating();

                // if we are a Sort or PreSortTemporary and we are in insert
                // mode and we just moved then we have left insert mode.  if
                // we are a Sort then enter retrieve mode
                if (this.isSortOrPreSort && this.inInsertMode)
                {
                    this.inInsertMode = false;
                    this.inRetrieveMode = this.isSort;
                }
            }
        }

        /// <summary>
        /// Gets the fields of the current row.
        /// </summary>
        /// <param name="grbit">The grbit.</param>
        /// <returns>A <see cref="FieldCollection"/> object to allow retrieval of all fields of the current row.</returns>
        internal FieldCollection GetFields(RetrieveColumnGrbit grbit)
        {
            JET_PFNREALLOC allocator =
                (context, pv, cb) =>
                IntPtr.Zero == pv ? Marshal.AllocHGlobal(new IntPtr(cb)) : Marshal.ReAllocHGlobal(pv, new IntPtr(cb));

            lock (this.isamSession)
            {
                this.CheckDisposed();
                this.CheckRecord();

                EnumerateColumnsGrbit enumerateGrbit = ((grbit & RetrieveColumnGrbit.RetrieveCopy) != 0)
                                                           ? EnumerateColumnsGrbit.EnumerateCopy
                                                           : EnumerateColumnsGrbit.None;

                using (IsamTransaction trx = new IsamTransaction(this.isamSession, true))
                {
                    // enumerate all field values in the current record or copy
                    // buffer
                    JET_ENUMCOLUMN[] jecs;
                    int numColumnValues;
                    Api.JetEnumerateColumns(
                        this.isamSession.Sesid,
                        this.tableid,
                        0, // numColumnids
                        null, // columnids
                        out numColumnValues,
                        out jecs,
                        allocator,
                        IntPtr.Zero, // allocatorContext,
                        int.MaxValue,
                        enumerateGrbit);

                    // create a field collection to contain our field values
                    FieldCollection fields = new FieldCollection();

                    // save the location of the source record for these field values
                    fields.Location = this.Location;

                    // fill the field collection with our field values
                    if (jecs != null && jecs.Length > 0)
                    {
                        foreach (JET_ENUMCOLUMN jec in jecs)
                        {
                            if (jec.rgEnumColumnValue != null && jec.rgEnumColumnValue.Length > 0)
                            {
                                JET_COLUMNBASE columnbase;
                                VistaApi.JetGetTableColumnInfo(
                                    this.isamSession.Sesid,
                                    this.tableid,
                                    jec.columnid,
                                    out columnbase);

                                Columnid columnid = new Columnid(columnbase);
                                bool isAscii = columnbase.cp == JET_CP.ASCII;
                                FieldValueCollection values = new FieldValueCollection(columnid);
                                foreach (JET_ENUMCOLUMNVALUE jecv in jec.rgEnumColumnValue)
                                {
                                    // FUTURE-2013/11/15-martinc: Drat, this is an IntPtr, and ObjectFromBytes() really
                                    // wants a byte[] array. Copying the data to a byte array just to simply
                                    // re-interpret it as an object is inefficient.
                                    // We should write Converter.ObjectFromIntPtr...
                                    byte[] bytesData = new byte[jecv.cbData];
                                    Marshal.Copy(jecv.pvData, bytesData, 0, bytesData.Length);

                                    values.Add(Converter.ObjectFromBytes(columnbase.coltyp, isAscii, bytesData));
                                }

                                values.ReadOnly = true;
                                fields.Add(values);
                            }
                        }
                    }

                    fields.ReadOnly = true;

                    // Return the field collection.
                    return fields;
                }
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (this.isamSession)
            {
                if (!this.Disposed)
                {
                    if (this.cleanup)
                    {
                        if (this.isTempTable)
                        {
                            ((TemporaryDatabase)this.database).ReleaseTempTable(this.tableName, this.inInsertMode);
                        }
                        else
                        {
                            // BUGBUG:  we will try to close an already closed tableid
                            // if it was already closed due to a rollback.  this could
                            // cause us to crash in ESENT due to lack of full validation
                            // in small config.  we should use cursor LS to detect when
                            // our cursor gets closed and thus avoid closing it again
                            Api.JetCloseTable(this.isamSession.Sesid, this.tableid);
                        }

                        this.cleanup = false;
                    }

                    this.Disposed = true;
                }
            }
        }

        /// <summary>
        /// Makes the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="end">if set to <c>true</c> specifies that the key represents the End Limit
        /// (<see cref="MakeKeyGrbit.PartialColumnStartLimit"/>/<see cref="MakeKeyGrbit.PartialColumnEndLimit"/>/
        /// <see cref="MakeKeyGrbit.FullColumnStartLimit"/>/<see cref="MakeKeyGrbit.FullColumnEndLimit"/>).</param>
        /// <returns>The byte value of the key for the index entry
        /// at the current position of a cursor.</returns>
        /// <exception cref="System.ArgumentException">the provided key must have a key segment per key column on the current index or it must contain a prefix or wildcard;key</exception>
        private byte[] MakeKey(Key key, bool end)
        {
            lock (this.isamSession)
            {
                bool firstSegment = true;
                int i = 0;
                foreach (KeySegment segment in key)
                {
                    if (i < this.CurrentIndexDefinition.KeyColumns.Count)
                    {
                        KeyColumn keyColumn = this.CurrentIndexDefinition.KeyColumns[i];

                        byte[] value = null;
                        if (!(segment.Value == null || segment.Value is DBNull))
                        {
                            Columnid isamColumnid = keyColumn.Columnid;
                            value = Converter.BytesFromObject(isamColumnid.Coltyp, false, segment.Value);
                        }

                        MakeKeyGrbit grbit = MakeKeyGrbit.None;
                        if (firstSegment == true)
                        {
                            grbit = grbit | MakeKeyGrbit.NewKey;
                        }

                        if (value != null && value.Length == 0)
                        {
                            grbit = grbit | MakeKeyGrbit.KeyDataZeroLength;
                        }

                        if (segment.Prefix == true)
                        {
                            if (end == false)
                            {
                                grbit = grbit | MakeKeyGrbit.PartialColumnStartLimit;
                            }
                            else
                            {
                                grbit = grbit | MakeKeyGrbit.PartialColumnEndLimit;
                            }
                        }
                        else if (segment.WildcardIsNext() == true)
                        {
                            if (end == false)
                            {
                                grbit = grbit | MakeKeyGrbit.FullColumnStartLimit;
                            }
                            else
                            {
                                grbit = grbit | MakeKeyGrbit.FullColumnEndLimit;
                            }
                        }

                        if (segment.Wildcard == false)
                        {
                            int valueLength = value == null ? 0 : value.Length;
                            Api.JetMakeKey(this.isamSession.Sesid, this.tableid, value, valueLength, grbit);
                            firstSegment = false;
                        }
                    }

                    i++;
                }

                if (i < this.CurrentIndexDefinition.KeyColumns.Count && key.HasPrefix == false && key.HasWildcard == false)
                {
                    throw new ArgumentException(
                        "the provided key must have a key segment per key column on the current index or it must contain a prefix or wildcard",
                        "key");
                }

                if (firstSegment)
                {
                    return null;
                }

                return Api.RetrieveKey(this.isamSession.Sesid, this.tableid, RetrieveKeyGrbit.RetrieveCopy);
            }
        }

        /// <summary>
        /// Sets the lower limit.
        /// </summary>
        private void SetLowerLimit()
        {
            lock (this.isamSession)
            {
                // we need to setup our index range
                if (this.movePrev == false)
                {
                    // only set an index range if one is currently specified
                    if (this.keyStart != null)
                    {
                        // load the key for the start of the current index range
                        Api.MakeKey(this.isamSession.Sesid, this.tableid, this.keyStart, MakeKeyGrbit.NormalizedKey);

                        // limit our backward movement to the defined index range
                        Api.TrySetIndexRange(this.isamSession.Sesid, this.tableid, this.grbitRangeStart);
                    }
                }

                // set our navigation direction
                this.moveNext = false;
                this.movePrev = true;

                // we have moved so we are not out of range
                this.outOfRange = false;
            }
        }

        /// <summary>
        /// Sets the upper limit.
        /// </summary>
        private void SetUpperLimit()
        {
            lock (this.isamSession)
            {
                // we need to setup our index range
                if (this.moveNext == false)
                {
                    // only set an index range if one is currently specified
                    if (this.keyEnd != null)
                    {
                        // load the key for the end of the current index range
                        Api.MakeKey(this.isamSession.Sesid, this.tableid, this.keyEnd, MakeKeyGrbit.NormalizedKey);

                        // limit our foward movement to the defined index range
                        Api.TrySetIndexRange(this.isamSession.Sesid, this.tableid, this.grbitRangeEnd);
                    }
                }

                // set our navigation direction
                this.moveNext = true;
                this.movePrev = false;

                // we have moved so we are not out of range
                this.outOfRange = false;
            }
        }

        /// <summary>
        /// Compares the byte arrays, up to the shorter of the two arrays.
        /// </summary>
        /// <param name="array1">The first array.</param>
        /// <param name="array2">The second array.</param>
        /// <returns>A numerical value indicating which array is greater.</returns>
        private int CompareByteArrays(byte[] array1, byte[] array2)
        {
            int d = array1.Length - array2.Length;

            for (int i = 0; i < (d < 0 ? array1.Length : array2.Length); i++)
            {
                int result = array1[i] - array2[i];

                if (result != 0)
                {
                    return result;
                }
            }

            return d;
        }

        /// <summary>
        /// Checks if the cursor is in the index range specified.
        /// </summary>
        /// <returns>Whether the cursor is currently in the index range.</returns>
        private bool CheckRange()
        {
            lock (this.isamSession)
            {
                byte[] keyCurr = null;

                // clear our navigation direction
                this.moveNext = false;
                this.movePrev = false;

                // fetch the key for the cursor's current location on the index
                if (this.keyStart != null || this.keyEnd != null)
                {
                    try
                    {
                        keyCurr = Api.RetrieveKey(this.isamSession.Sesid, this.tableid, RetrieveKeyGrbit.None);
                    }
                    catch (EsentNoCurrentRecordException)
                    {
                        // return false if we are not on a record
                        return false;
                    }
                }

                // the cursor is currently before the lower limit of the index range
                if (this.keyStart != null)
                {
                    int result = this.CompareByteArrays(keyCurr, this.keyStart);
                    if (result < 0 || (result == 0 && (this.grbitRangeStart & SetIndexRangeGrbit.RangeInclusive) != 0))
                    {
                        // move before the first element on the index range
                        this.MoveBeforeFirst();

                        // return that we are outside the index range
                        return false;
                    }
                }

                // the cursor is currently after the upper limit of the index range
                if (this.keyEnd != null)
                {
                    int result = this.CompareByteArrays(this.keyEnd, keyCurr);
                    if (result == 0 && ((this.grbitRangeEnd & SetIndexRangeGrbit.RangeInclusive) != 0 || result > 0))
                    {
                        // move after the last element on the index range
                        this.MoveAfterLast();

                        // return that we are outside the index range
                        return false;
                    }
                }

                // we have determined that we are not out of range
                this.outOfRange = false;

                // return that we are inside the index range
                return true;
            }
        }
    }
}
