// ---------------------------------------------------------------------------
// <copyright file="TemporaryDatabase.cs" company="Microsoft">
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
    using System.Globalization;

    using Microsoft.Isam.Esent.Interop;
    using Microsoft.Isam.Esent.Interop.Server2003;
    using Microsoft.Isam.Esent.Interop.Vista;
    using Microsoft.Isam.Esent.Interop.Windows7;

    /// <summary>
    /// A Database is a file used by the ISAM to store data. It is organized
    /// into tables which are in turn comprised of columns and indices and
    /// contain data in the form of records. The database's schema can be
    /// enumerated and manipulated by this object. Also, the database's
    /// tables can be opened for access by this object.
    /// <para>
    /// A <see cref="TemporaryDatabase"/> is used to access the temporary database. There
    /// is one temporary database per instance and its location is configured
    /// by Instance.IsamSystemParameters.TempPath. The temporary database is used
    /// to store temporary tables.
    /// </para>
    /// </summary>
    public class TemporaryDatabase : DatabaseCommon, IDisposable
    {
        /// <summary>
        /// The table collection
        /// </summary>
        private TableCollection tableCollection = null;

        /// <summary>
        /// The temporary table handle collection
        /// </summary>
        private TempTableHandleCollection tempTableHandleCollection = null;

        /// <summary>
        /// The cleanup
        /// </summary>
        private bool cleanup = false;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryDatabase"/> class.
        /// </summary>
        /// <param name="isamSession">The session.</param>
        internal TemporaryDatabase(IsamSession isamSession)
            : base(isamSession)
        {
            lock (isamSession)
            {
                this.cleanup = true;
                this.tableCollection = new TableCollection();
                this.tempTableHandleCollection = new TempTableHandleCollection(false);
            }
        }

        /// <summary>
        /// Finalizes an instance of the TemporaryDatabase class
        /// </summary>
        ~TemporaryDatabase()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets a collection of tables in the database.
        /// </summary>
        /// <returns>a collection of tables in the database</returns>
        public override TableCollection Tables
        {
            get
            {
                this.CheckDisposed();
                return this.tableCollection;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [disposed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [disposed]; otherwise, <c>false</c>.
        /// </value>
        internal override bool Disposed
        {
            get
            {
                return this.disposed || this.IsamSession.Disposed;
            }

            set
            {
                this.disposed = value;
            }
        }

        /// <summary>
        /// Gets the temporary table handles.
        /// </summary>
        /// <value>
        /// The temporary table handles.
        /// </value>
        private TempTableHandleCollection TempTableHandles
        {
            get
            {
                this.CheckDisposed();
                return this.tempTableHandleCollection;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public new void Dispose()
        {
            lock (this)
            {
                this.Dispose(true);
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Creates a single table with the specified definition in the database
        /// </summary>
        /// <param name="tableDefinition">The table definition.</param>
        /// <exception cref="EsentTableDuplicateException">
        /// Thrown when the table definition overlaps with an already existing table.
        /// </exception>
        /// <exception cref="System.ArgumentException">A MaxKeyLength &gt; 255 is not supported for indices over a temporary table on this version of the database engine.;tableDefinition</exception>
        public override void CreateTable(TableDefinition tableDefinition)
        {
            lock (this.IsamSession)
            {
                this.CheckDisposed();

                // validate the table definition for creating a TT
                this.ValidateTableDefinition(tableDefinition);

                // convert the given table definition into an JET_OPENTEMPORARYTABLE
                // struct that we will use to create the TT
                JET_OPENTEMPORARYTABLE openTemporaryTable = this.MakeOpenTemporaryTable(tableDefinition);

                // check if the TT already exists
                if (this.Exists(tableDefinition.Name))
                {
                    throw new EsentTableDuplicateException();
                }

                // do not allow the TT to be created if the session is in a
                // transaction.  we disallow this to sidestep the problem where
                // JET will automatically close (and destroy) the TT if the
                // current level of the transaction is aborted
                if (this.IsamSession.TransactionLevel > 0)
                {
                    // NOTE:  i'm thinking that this requirement is pretty lame,
                    // especially since it only hits us on an abort.  I am going
                    // to allow this for now and see what happens
                    // throw new ArgumentException( "We do not currently allow you to create temp tables while inside of a transaction." );
                }

                // create the TT
                JET_TABLEID tableid = new JET_TABLEID();

                if (DatabaseCommon.CheckEngineVersion(
                    this.IsamSession,
                    DatabaseCommon.ESENTVersion(6, 0, 6000, 0),
                    DatabaseCommon.ESEVersion(8, 0, 685, 0)))
                {
                    VistaApi.JetOpenTemporaryTable(this.IsamSession.Sesid, openTemporaryTable);
                    tableid = openTemporaryTable.tableid;
                }
                else
                {
                    if (openTemporaryTable.cbKeyMost > 255)
                    {
                        throw new ArgumentException("A MaxKeyLength > 255 is not supported for indices over a temporary table on this version of the database engine.", "tableDefinition");
                    }

                    Api.JetOpenTempTable2(
                        this.IsamSession.Sesid,
                        openTemporaryTable.prgcolumndef,
                        openTemporaryTable.prgcolumndef.Length,
                        openTemporaryTable.pidxunicode.lcid,
                        openTemporaryTable.grbit,
                        out tableid,
                        openTemporaryTable.prgcolumnid);
                }

                // re-create the TT's schema to reflect the created TT
                TableDefinition tableDefinitionToCache = MakeTableDefinitionToCache(tableDefinition, openTemporaryTable);

                // cache the TT and its handle
                TempTableHandle tempTableHandle = new TempTableHandle(
                    tableDefinitionToCache.Name,
                    this.IsamSession.Sesid,
                    tableid,
                    tableDefinitionToCache.Type == TableType.Sort || tableDefinitionToCache.Type == TableType.PreSortTemporary);

                this.Tables.Add(tableDefinitionToCache);
                this.TempTableHandles.Add(tempTableHandle);
                this.IsamSession.IsamInstance.TempTableHandles.Add(tempTableHandle);
            }
        }

        /// <summary>
        /// Deletes a single table in the database
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <exception cref="EsentObjectNotFoundException">
        /// Thrown when the specified table can't be found.
        /// </exception>
        /// <exception cref="EsentTableInUseException">
        /// Thrown when the specified table still has cursors open.
        /// </exception>
        /// <remarks>
        /// It is currently not possible to delete a table that is being used
        /// by a Cursor.  All such Cursors must be disposed before the
        /// table can be successfully deleted.
        /// </remarks>
        public override void DropTable(string tableName)
        {
            lock (this.IsamSession)
            {
                this.CheckDisposed();

                if (!this.Exists(tableName))
                {
                    throw new EsentObjectNotFoundException();
                }

                TempTableHandle tempTableHandle = this.TempTableHandles[tableName];

                if (tempTableHandle.CursorCount > 0)
                {
                    throw new EsentTableInUseException();
                }

                this.Tables.Remove(tableName);

                Api.JetCloseTable(this.IsamSession.Sesid, tempTableHandle.Handle);
                this.TempTableHandles.Remove(tempTableHandle.Name);
                this.IsamSession.IsamInstance.TempTableHandles.Remove(tempTableHandle.Guid.ToString());
            }
        }

        /// <summary>
        /// Determines if a given table exists in the database
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>
        /// true if the table was found, false otherwise
        /// </returns>
        public override bool Exists(string tableName)
        {
            this.CheckDisposed();

            return this.Tables.Contains(tableName);
        }

        /// <summary>
        /// Opens a cursor over the specified table.
        /// </summary>
        /// <param name="tableName">the name of the table to be opened</param>
        /// <returns>a cursor over the specified table in this database</returns>
        public override Cursor OpenCursor(string tableName)
        {
            lock (this.IsamSession)
            {
                this.CheckDisposed();

                if (!this.Exists(tableName))
                {
                    throw new EsentObjectNotFoundException();
                }

                TableDefinition tableDefinition = this.Tables[tableName];
                TempTableHandle tempTableHandle = this.TempTableHandles[tableName];
                JET_TABLEID tableid;

                try
                {
                    // if this is a Sort then we must always fail to dup the
                    // cursor
                    //
                    // NOTE:  this is a hack to work around problems in ESE/ESENT
                    // that we cannot fix because we must work downlevel
                    if (tableDefinition.Type == TableType.Sort)
                    {
                        throw new EsentIllegalOperationException();
                    }

                    Api.JetDupCursor(this.IsamSession.Sesid, tempTableHandle.Handle, out tableid, DupCursorGrbit.None);
                    tempTableHandle.InInsertMode = false;
                }
                catch (EsentIllegalOperationException)
                {
                    if (tempTableHandle.CursorCount > 0)
                    {
                        throw new InvalidOperationException("It is not possible to have multiple open cursors on a temporary table that is currently in insert mode.");
                    }
                    else
                    {
                        tableid = tempTableHandle.Handle;
                    }
                }

                Cursor newCursor = new Cursor(this.IsamSession, this, tableName, tableid, tempTableHandle.InInsertMode);

                tempTableHandle.CursorCount++;

                return newCursor;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.Dispose();
        }

        /// <summary>
        /// Releases the temporary table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="inInsertMode">if set to <c>true</c> [in insert mode].</param>
        internal void ReleaseTempTable(string tableName, bool inInsertMode)
        {
            lock (this.IsamSession)
            {
                TempTableHandle tempTableHandle = this.TempTableHandles[tableName];

                tempTableHandle.InInsertMode = inInsertMode;
                tempTableHandle.CursorCount--;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            lock (this.IsamSession)
            {
                if (!this.Disposed)
                {
                    if (this.cleanup)
                    {
                        foreach (TempTableHandle tempTableHandle in this.TempTableHandles)
                        {
                            Api.JetCloseTable(this.IsamSession.Sesid, tempTableHandle.Handle);
                            this.IsamSession.IsamInstance.TempTableHandles.Remove(tempTableHandle.Guid.ToString());
                        }

                        base.Dispose(disposing);
                        this.cleanup = false;
                    }

                    this.Disposed = true;
                }
            }
        }

        /// <summary>
        /// Creates a <see cref="TableDefinition"/> object from a <see cref="JET_OPENTEMPORARYTABLE"/>
        /// object, suitable for caching.
        /// </summary>
        /// <param name="tableDefinition">The table definition.</param>
        /// <param name="openTemporaryTable">The open temporary table.</param>
        /// <returns>A <see cref="TableDefinition"/> object suitable for caching.</returns>
        private static TableDefinition MakeTableDefinitionToCache(
            TableDefinition tableDefinition,
            JET_OPENTEMPORARYTABLE openTemporaryTable)
        {
            // set the new table properties
            TableDefinition tableDefinitionToCache = new TableDefinition(tableDefinition.Name, tableDefinition.Type);

            // add the columns complete with the columnids generated when the
            // TT was created
            //
            // NOTE:  this processing loop has to mirror the loop used to generate
            // the columndefs in MakeOpenTemporaryTable
            int currentColumndef = 0;

            foreach (IndexDefinition indexDefinition in tableDefinition.Indices)
            {
                foreach (KeyColumn keyColumn in indexDefinition.KeyColumns)
                {
                    ColumnDefinition columnDefinition = tableDefinition.Columns[keyColumn.Name];

                    Columnid columnid = new Columnid(
                        columnDefinition.Name,
                        openTemporaryTable.prgcolumnid[currentColumndef],
                        DatabaseCommon.ColtypFromColumnDefinition(columnDefinition),
                        columnDefinition.IsAscii);

                    ColumnDefinition columnDefinitionToCache = new ColumnDefinition(columnid);

                    columnDefinitionToCache.Flags = columnDefinition.Flags;
                    columnDefinitionToCache.MaxLength = columnDefinition.MaxLength;

                    columnDefinitionToCache.ReadOnly = true;

                    tableDefinitionToCache.Columns.Add(columnDefinitionToCache);

                    currentColumndef++;
                }
            }

            // next collect the rest of the columns and put them after the key
            // columns, skipping over the columns we already added
            foreach (ColumnDefinition columnDefinition in tableDefinition.Columns)
            {
                bool alreadyAdded = false;
                foreach (IndexDefinition indexDefinition in tableDefinition.Indices)
                {
                    foreach (KeyColumn keyColumn in indexDefinition.KeyColumns)
                    {
                        if (keyColumn.Name.ToLower(CultureInfo.InvariantCulture) == columnDefinition.Name.ToLower(CultureInfo.InvariantCulture))
                        {
                            alreadyAdded = true;
                        }
                    }
                }

                if (!alreadyAdded)
                {
                    Columnid columnid = new Columnid(
                        columnDefinition.Name,
                        openTemporaryTable.prgcolumnid[currentColumndef],
                        DatabaseCommon.ColtypFromColumnDefinition(columnDefinition),
                        columnDefinition.IsAscii);

                    ColumnDefinition columnDefinitionToCache = new ColumnDefinition(columnid);

                    columnDefinitionToCache.Flags = columnDefinition.Flags;
                    columnDefinitionToCache.MaxLength = columnDefinition.MaxLength;

                    columnDefinitionToCache.ReadOnly = true;

                    tableDefinitionToCache.Columns.Add(columnDefinitionToCache);

                    currentColumndef++;
                }
            }

            tableDefinitionToCache.Columns.ReadOnly = true;

            // add the indices
            foreach (IndexDefinition indexDefinition in tableDefinition.Indices)
            {
                IndexDefinition indexDefinitionToCache = new IndexDefinition(indexDefinition.Name);

                indexDefinitionToCache.Flags = indexDefinition.Flags;
                indexDefinitionToCache.Density = 100;
                indexDefinitionToCache.CultureInfo = indexDefinition.CultureInfo;
                indexDefinitionToCache.CompareOptions = indexDefinition.CompareOptions;
                indexDefinitionToCache.MaxKeyLength = indexDefinition.MaxKeyLength;

                foreach (KeyColumn keyColumn in indexDefinition.KeyColumns)
                {
                    Columnid columnid = tableDefinitionToCache.Columns[keyColumn.Name].Columnid;

                    KeyColumn keyColumnToCache = new KeyColumn(columnid, keyColumn.IsAscending);

                    indexDefinitionToCache.KeyColumns.Add(keyColumnToCache);
                }

                indexDefinitionToCache.KeyColumns.ReadOnly = true;

                indexDefinitionToCache.ReadOnly = true;

                tableDefinitionToCache.Indices.Add(indexDefinitionToCache);
            }

            tableDefinitionToCache.Indices.ReadOnly = true;

            // return the table definition
            return tableDefinitionToCache;
        }

        /// <summary>
        /// Makes the <see cref="JET_OPENTEMPORARYTABLE"/> object to later open it.
        /// </summary>
        /// <param name="tableDefinition">The table definition.</param>
        /// <returns>The newly created <see cref="JET_OPENTEMPORARYTABLE"/> object.</returns>
        private JET_OPENTEMPORARYTABLE MakeOpenTemporaryTable(TableDefinition tableDefinition)
        {
            JET_OPENTEMPORARYTABLE openTemporaryTable = new JET_OPENTEMPORARYTABLE();

            // allocate room for our columns
            int currentColumndef = 0;

            openTemporaryTable.ccolumn = tableDefinition.Columns.Count;
            openTemporaryTable.prgcolumndef = new JET_COLUMNDEF[openTemporaryTable.ccolumn];
            openTemporaryTable.prgcolumnid = new JET_COLUMNID[openTemporaryTable.ccolumn];

            for (int coldef = 0; coldef < openTemporaryTable.ccolumn; ++coldef)
            {
                openTemporaryTable.prgcolumndef[coldef] = new JET_COLUMNDEF();
            }

            // first, collect all the key columns in order and put them as the
            // first columndefs.  we have to do this to guarantee that the TT
            // is sorted properly
            foreach (IndexDefinition indexDefinition in tableDefinition.Indices)
            {
                foreach (KeyColumn keyColumn in indexDefinition.KeyColumns)
                {
                    ColumnDefinition columnDefinition = tableDefinition.Columns[keyColumn.Name];

                    openTemporaryTable.prgcolumndef[currentColumndef].coltyp =
                        DatabaseCommon.ColtypFromColumnDefinition(columnDefinition);
                    openTemporaryTable.prgcolumndef[currentColumndef].cp = JET_CP.Unicode;
                    openTemporaryTable.prgcolumndef[currentColumndef].cbMax = columnDefinition.MaxLength;
                    openTemporaryTable.prgcolumndef[currentColumndef].grbit = (ColumndefGrbit)columnDefinition.Flags
                                                                              | ColumndefGrbit.TTKey
                                                                              | (keyColumn.IsAscending
                                                                                     ? ColumndefGrbit.None
                                                                                     : ColumndefGrbit.TTDescending);
                    currentColumndef++;
                }
            }

            // next collect the rest of the columns and put them after the key
            // columns, skipping over the columns we already added
            foreach (ColumnDefinition columnDefinition in tableDefinition.Columns)
            {
                bool alreadyAdded = false;
                foreach (IndexDefinition indexDefinition in tableDefinition.Indices)
                {
                    foreach (KeyColumn keyColumn in indexDefinition.KeyColumns)
                    {
                        if (keyColumn.Name.ToLower(CultureInfo.InvariantCulture) == columnDefinition.Name.ToLower(CultureInfo.InvariantCulture))
                        {
                            alreadyAdded = true;
                        }
                    }
                }

                if (!alreadyAdded)
                {
                    openTemporaryTable.prgcolumndef[currentColumndef].coltyp = DatabaseCommon.ColtypFromColumnDefinition(columnDefinition);
                    openTemporaryTable.prgcolumndef[currentColumndef].cp = JET_CP.Unicode;
                    openTemporaryTable.prgcolumndef[currentColumndef].cbMax = columnDefinition.MaxLength;
                    openTemporaryTable.prgcolumndef[currentColumndef].grbit = Converter.ColumndefGrbitFromColumnFlags(columnDefinition.Flags);
                    currentColumndef++;
                }
            }

            // set the index flags
            foreach (IndexDefinition indexDefinition in tableDefinition.Indices)
            {
                openTemporaryTable.pidxunicode = new JET_UNICODEINDEX();

                openTemporaryTable.pidxunicode.lcid = indexDefinition.CultureInfo.LCID;
                UnicodeIndexFlags unicodeIndexFlags = Converter.UnicodeFlagsFromCompareOptions(indexDefinition.CompareOptions);
                openTemporaryTable.pidxunicode.dwMapFlags = Converter.MapFlagsFromUnicodeIndexFlags(unicodeIndexFlags);
            }

            // infer the TT mode of operation and set its grbits accordingly
            bool haveColumnWithLongValue = false;
            foreach (ColumnDefinition columnDefinition in tableDefinition.Columns)
            {
                JET_coltyp coltyp = DatabaseCommon.ColtypFromColumnDefinition(columnDefinition);

                if (coltyp == JET_coltyp.LongText || coltyp == JET_coltyp.LongBinary)
                {
                    haveColumnWithLongValue = true;
                }
            }

            bool haveIndexWithSortNullsHigh = false;
            foreach (IndexDefinition indexDefinition in tableDefinition.Indices)
            {
                if ((indexDefinition.Flags & IndexFlags.SortNullsHigh) != 0)
                {
                    haveIndexWithSortNullsHigh = true;
                }
            }

            if (tableDefinition.Type == TableType.Sort)
            {
                foreach (IndexDefinition indexDefinition in tableDefinition.Indices)
                {
                    if ((indexDefinition.Flags & (IndexFlags.Unique | IndexFlags.Primary)) == 0)
                    {
                        // External Sort without duplicate removal
                        openTemporaryTable.grbit = Server2003Grbits.ForwardOnly
                                                   | (haveColumnWithLongValue
                                                          ? Windows7Grbits.IntrinsicLVsOnly
                                                          : TempTableGrbit.None)
                                                   | (haveIndexWithSortNullsHigh
                                                          ? TempTableGrbit.SortNullsHigh
                                                          : TempTableGrbit.None);
                    }
                    else
                    {
                        // External Sort TT with deferred duplicate removal 
                        openTemporaryTable.grbit = TempTableGrbit.Unique
                                                   | (haveColumnWithLongValue
                                                          ? Windows7Grbits.IntrinsicLVsOnly
                                                          : TempTableGrbit.None)
                                                   | (haveIndexWithSortNullsHigh
                                                          ? TempTableGrbit.SortNullsHigh
                                                          : TempTableGrbit.None);
                    }
                }
            }
            else if (tableDefinition.Type == TableType.PreSortTemporary)
            {
                // Pre-sorted B+ Tree TT with deferred duplicate removal 
                openTemporaryTable.grbit = TempTableGrbit.Indexed
                                           | TempTableGrbit.Unique
                                           | TempTableGrbit.Updatable
                                           | TempTableGrbit.Scrollable
                                           | (haveColumnWithLongValue
                                                  ? Windows7Grbits.IntrinsicLVsOnly
                                                  : TempTableGrbit.None)
                                           | (haveIndexWithSortNullsHigh
                                                  ? TempTableGrbit.SortNullsHigh
                                                  : TempTableGrbit.None);
            }
            else if (tableDefinition.Type == TableType.Temporary)
            {
                if (tableDefinition.Indices.Count != 0)
                {
                    // B+ Tree TT with immediate duplicate removal 
                    openTemporaryTable.grbit = TempTableGrbit.Indexed
                                               | TempTableGrbit.Unique
                                               | TempTableGrbit.Updatable
                                               | TempTableGrbit.Scrollable
                                               | TempTableGrbit.ForceMaterialization
                                               | (haveIndexWithSortNullsHigh
                                                      ? TempTableGrbit.SortNullsHigh
                                                      : TempTableGrbit.None);
                }
                else
                {
                    // B+ Tree TT with a sequential index 
                    openTemporaryTable.grbit = TempTableGrbit.Updatable | TempTableGrbit.Scrollable;
                }
            }

            // set the key construction parameters for the TT
            foreach (IndexDefinition indexDefinition in tableDefinition.Indices)
            {
                openTemporaryTable.cbKeyMost = indexDefinition.MaxKeyLength;
                openTemporaryTable.cbVarSegMac = 0;
            }

            // return the constructed JET_OPENTEMPORARYTABLE (whew!)
            return openTemporaryTable;
        }

        /// <summary>
        /// Checks whether this object is disposed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">If the object has already been disposed.</exception>
        private void CheckDisposed()
        {
            lock (this.IsamSession)
            {
                if (this.Disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }
            }
        }

        /// <summary>
        /// Validates the table definition.
        /// </summary>
        /// <param name="tableDefinition">The table definition.</param>
        /// <exception cref="System.ArgumentException">
        /// Illegal name for a temporary table.;tableDefinition
        /// or
        /// Illegal TableType for a temporary table.;tableDefinition
        /// or
        /// Temporary tables must have at least one column.;tableDefinition
        /// or
        /// Illegal name for a column in a temporary table.;tableDefinition
        /// or
        /// Illegal ColumnFlags for a column in a temporary table.;tableDefinition
        /// or
        /// Default values are not supported for temporary table columns.;tableDefinition
        /// or
        /// Temporary tables of type TableType.Sort and TableType.PreSortTemporary must have an index defined.;tableDefinition
        /// or
        /// Temporary tables may only have a single index defined.;tableDefinition
        /// or
        /// Illegal name for an index in a temporary table.;tableDefinition
        /// or
        /// Illegal IndexFlags for an index in a temporary table.;tableDefinition
        /// or
        /// Illegal or unsupported MaxKeyLength for an index in a temporary table.;tableDefinition
        /// or
        /// No KeyColumns for an index in a temporary table.;tableDefinition
        /// or
        /// Too many KeyColumns for an index in a temporary table.;tableDefinition
        /// or
        /// A KeyColumn for an index in the temporary table refers to a column that doesn't exist.;tableDefinition
        /// or
        /// Conditional columns are not supported for temporary table indices.;tableDefinition
        /// or
        /// Temporary tables of type TableType.PreSortTemporary and TableType.Temporary must have a primary index defined.;tableDefinition
        /// </exception>
        private void ValidateTableDefinition(TableDefinition tableDefinition)
        {
            // validate the table's properties
            DatabaseCommon.CheckName(
                tableDefinition.Name,
                new ArgumentException("Illegal name for a temporary table.", "tableDefinition"));
            if (tableDefinition.Name == null)
            {
                throw new ArgumentException("Illegal name for a temporary table.", "tableDefinition");
            }

            if (
                !(tableDefinition.Type == TableType.Sort || tableDefinition.Type == TableType.PreSortTemporary
                  || tableDefinition.Type == TableType.Temporary))
            {
                throw new ArgumentException("Illegal TableType for a temporary table.", "tableDefinition");
            }

            // validate all columns
            if (tableDefinition.Columns.Count == 0)
            {
                throw new ArgumentException("Temporary tables must have at least one column.", "tableDefinition");
            }

            foreach (ColumnDefinition columnDefinition in tableDefinition.Columns)
            {
                DatabaseCommon.CheckName(
                    columnDefinition.Name,
                    new ArgumentException("Illegal name for a column in a temporary table.", "tableDefinition"));
                if (columnDefinition.Name == null)
                {
                    throw new ArgumentException("Illegal name for a column in a temporary table.", "tableDefinition");
                }

                if (tableDefinition.Type == TableType.Sort || tableDefinition.Type == TableType.PreSortTemporary)
                {
                    JET_coltyp coltyp = DatabaseCommon.ColtypFromColumnDefinition(columnDefinition);
                    if (coltyp == JET_coltyp.LongText || coltyp == JET_coltyp.LongBinary)
                    {
                        // timestamp when ESE/ESENT supports JET_bitTTIntrinsicLVsOnly
                        DatabaseCommon.CheckEngineVersion(
                            this.IsamSession,
                            DatabaseCommon.ESENTVersion(6, 1, 6492, 0),
                            DatabaseCommon.ESEVersion(14, 0, 46, 0),
                            new ArgumentException(
                                "LongText and LongBinary columns are not supported for columns in a temporary table of type TableType.Sort or TableType.PreSortTemporary on this version of the database engine.",
                                "tableDefinition"));
                    }
                }

                if (0 != (columnDefinition.Flags
                          & ~(ColumnFlags.Fixed
                              | ColumnFlags.Variable
                              | ColumnFlags.Sparse
                              | ColumnFlags.NonNull
                              | ColumnFlags.MultiValued)))
                {
                    throw new ArgumentException("Illegal ColumnFlags for a column in a temporary table.", "tableDefinition");
                }

                if (columnDefinition.DefaultValue != null)
                {
                    throw new ArgumentException("Default values are not supported for temporary table columns.", "tableDefinition");
                }
            }

            // validate all indices
            if (tableDefinition.Indices.Count == 0
                && (tableDefinition.Type == TableType.Sort || tableDefinition.Type == TableType.PreSortTemporary))
            {
                throw new ArgumentException("Temporary tables of type TableType.Sort and TableType.PreSortTemporary must have an index defined.", "tableDefinition");
            }

            if (tableDefinition.Indices.Count > 1)
            {
                throw new ArgumentException("Temporary tables may only have a single index defined.", "tableDefinition");
            }

            foreach (IndexDefinition indexDefinition in tableDefinition.Indices)
            {
                DatabaseCommon.CheckName(
                    indexDefinition.Name,
                    new ArgumentException("Illegal name for an index in a temporary table.", "tableDefinition"));
                if (indexDefinition.Name == null)
                {
                    throw new ArgumentException("Illegal name for an index in a temporary table.", "tableDefinition");
                }

                if (0 != (indexDefinition.Flags
                          & ~(IndexFlags.Unique
                              | IndexFlags.Primary
                              | IndexFlags.AllowNull
                              | IndexFlags.SortNullsLow
                              | IndexFlags.SortNullsHigh
                              | IndexFlags.AllowTruncation)))
                {
                    throw new ArgumentException("Illegal IndexFlags for an index in a temporary table.", "tableDefinition");
                }

                // Require AllowTruncation.
                if (0 == (indexDefinition.Flags & IndexFlags.AllowTruncation))
                {
                    throw new ArgumentException("Illegal IndexFlags for an index in a temporary table.", "tableDefinition");
                }

                // 255 in XP.
                long keyMost = this.IsamSession.IsamInstance.IsamSystemParameters.KeyMost;

                if (indexDefinition.MaxKeyLength < 255 || indexDefinition.MaxKeyLength > keyMost)
                {
                    throw new ArgumentException("Illegal or unsupported MaxKeyLength for an index in a temporary table.", "tableDefinition");
                }

                // 12 in XP. 16 in Vista.
                int keyColumnMost = 16;

                if (indexDefinition.KeyColumns.Count == 0)
                {
                    throw new ArgumentException("No KeyColumns for an index in a temporary table.", "tableDefinition");
                }

                if (indexDefinition.KeyColumns.Count > keyColumnMost)
                {
                    throw new ArgumentException("Too many KeyColumns for an index in a temporary table.", "tableDefinition");
                }

                foreach (KeyColumn keyColumn in indexDefinition.KeyColumns)
                {
                    if (!tableDefinition.Columns.Contains(keyColumn.Name))
                    {
                        throw new ArgumentException("A KeyColumn for an index in the temporary table refers to a column that doesn't exist.", "tableDefinition");
                    }
                }

                if (indexDefinition.ConditionalColumns.Count != 0)
                {
                    throw new ArgumentException("Conditional columns are not supported for temporary table indices.", "tableDefinition");
                }

                if ((indexDefinition.Flags & IndexFlags.Primary) == 0
                    && (tableDefinition.Type == TableType.PreSortTemporary
                        || tableDefinition.Type == TableType.Temporary))
                {
                    throw new ArgumentException("Temporary tables of type TableType.PreSortTemporary and TableType.Temporary must have a primary index defined.", "tableDefinition");
                }
            }
        }
    }
}
