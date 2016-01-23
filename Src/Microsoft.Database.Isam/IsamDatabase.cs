// ---------------------------------------------------------------------------
// <copyright file="IsamDatabase.cs" company="Microsoft">
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

    using Microsoft.Isam.Esent.Interop;

    /// <summary>
    /// A Database is a file used by the ISAM to store data.  It is organized
    /// into tables which are in turn comprised of columns and indices and
    /// contain data in the form of records.  The database's schema can be
    /// enumerated and manipulated by this object.  Also, the database's
    /// tables can be opened for access by this object.
    /// </summary>
    public class IsamDatabase : DatabaseCommon, IDisposable
    {
        /// <summary>
        /// The dbid
        /// </summary>
        private readonly JET_DBID dbid;

        /// <summary>
        /// The table collection
        /// </summary>
        private TableCollection tableCollection = null;

        /// <summary>
        /// The cleanup
        /// </summary>
        private bool cleanup = false;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="IsamDatabase"/> class.
        /// </summary>
        /// <param name="isamSession">The session.</param>
        /// <param name="databaseName">Name of the database.</param>
        internal IsamDatabase(IsamSession isamSession, string databaseName)
            : base(isamSession)
        {
            lock (isamSession)
            {
                Api.JetOpenDatabase(isamSession.Sesid, databaseName, null, out this.dbid, OpenDatabaseGrbit.None);
                this.cleanup = true;
                this.tableCollection = new TableCollection(this);
            }
        }

        /// <summary>
        /// Finalizes an instance of the IsamDatabase class
        /// </summary>
        ~IsamDatabase()
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
        /// Gets the dbid.
        /// </summary>
        /// <value>
        /// The dbid.
        /// </value>
        internal JET_DBID Dbid
        {
            get
            {
                return this.dbid;
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
        public override void CreateTable(TableDefinition tableDefinition)
        {
            lock (this.IsamSession)
            {
                this.CheckDisposed();

                using (IsamTransaction trx = new IsamTransaction(this.IsamSession))
                {
                    // FUTURE-2013/11/15-martinc: Consider using JetCreateTableColumnIndex(). It would be
                    // a bit faster because it's only a single managed/native transition.

                    // Hard-code the initial space and density.
                    JET_TABLEID tableid;
                    Api.JetCreateTable(this.IsamSession.Sesid, this.dbid, tableDefinition.Name, 16, 90, out tableid);

                    foreach (ColumnDefinition columnDefinition in tableDefinition.Columns)
                    {
                        JET_COLUMNDEF columndef = new JET_COLUMNDEF();
                        columndef.coltyp = IsamDatabase.ColtypFromColumnDefinition(columnDefinition);
                        columndef.cp = JET_CP.Unicode;
                        columndef.cbMax = columnDefinition.MaxLength;

                        columndef.grbit = Converter.ColumndefGrbitFromColumnFlags(columnDefinition.Flags);
                        byte[] defaultValueBytes = Converter.BytesFromObject(
                            columndef.coltyp,
                            false /*ASCII */,
                            columnDefinition.DefaultValue);

                        JET_COLUMNID columnid;
                        int defaultValueLength = (defaultValueBytes == null) ? 0 : defaultValueBytes.Length;
                        Api.JetAddColumn(
                            this.IsamSession.Sesid,
                            tableid,
                            columnDefinition.Name,
                            columndef,
                            defaultValueBytes,
                            defaultValueLength,
                            out columnid);
                    }

                    foreach (IndexDefinition indexDefinition in tableDefinition.Indices)
                    {
                        JET_INDEXCREATE[] indexcreates = new JET_INDEXCREATE[1];
                        indexcreates[0] = new JET_INDEXCREATE();

                        indexcreates[0].szIndexName = indexDefinition.Name;
                        indexcreates[0].szKey = IsamDatabase.IndexKeyFromIndexDefinition(indexDefinition);
                        indexcreates[0].cbKey = indexcreates[0].szKey.Length;
                        indexcreates[0].grbit = IsamDatabase.GrbitFromIndexDefinition(indexDefinition);
                        indexcreates[0].ulDensity = indexDefinition.Density;
                        indexcreates[0].pidxUnicode = new JET_UNICODEINDEX();
                        indexcreates[0].pidxUnicode.lcid = indexDefinition.CultureInfo.LCID;
                        indexcreates[0].pidxUnicode.dwMapFlags = (uint)Converter.UnicodeFlagsFromCompareOptions(indexDefinition.CompareOptions);
                        indexcreates[0].rgconditionalcolumn = IsamDatabase.ConditionalColumnsFromIndexDefinition(indexDefinition);
                        indexcreates[0].cConditionalColumn = indexcreates[0].rgconditionalcolumn.Length;
                        indexcreates[0].cbKeyMost = indexDefinition.MaxKeyLength;
                        Api.JetCreateIndex2(this.IsamSession.Sesid, tableid, indexcreates, indexcreates.Length);
                    }

                    // The initially-created tableid is opened exclusively.
                    Api.JetCloseTable(this.IsamSession.Sesid, tableid);
                    trx.Commit();
                    DatabaseCommon.SchemaUpdateID++;
                }
            }
        }

        /// <summary>
        /// Deletes a single table in the database.
        /// </summary>
        /// <param name="tableName">The name of the table to be deleted.</param>
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

                Api.JetDeleteTable(this.IsamSession.Sesid, this.dbid, tableName);
                DatabaseCommon.SchemaUpdateID++;
            }
        }

        /// <summary>
        /// Determines if a given table exists in the database
        /// </summary>
        /// <param name="tableName">The name of the table to evaluate for existence.</param>
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
        /// <param name="exclusive">when true, the table will be opened for exclusive access</param>
        /// <returns>a cursor over the specified table in this database</returns>
        public Cursor OpenCursor(string tableName, bool exclusive)
        {
            lock (this.IsamSession)
            {
                this.CheckDisposed();

                OpenTableGrbit grbit = exclusive ? OpenTableGrbit.DenyRead : OpenTableGrbit.None;
                return new Cursor(this.IsamSession, this, tableName, grbit);
            }
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

                return this.OpenCursor(tableName, false);
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
                        Api.JetCloseDatabase(this.IsamSession.Sesid, this.dbid, CloseDatabaseGrbit.None);
                        base.Dispose(disposing);
                        this.cleanup = false;
                    }

                    this.Disposed = true;
                }
            }
        }

        /// <summary>
        /// Checks the disposed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">
        /// Thrown when the object is already disposed.
        /// </exception>
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
    }
}
