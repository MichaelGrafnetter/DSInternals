// ---------------------------------------------------------------------------
// <copyright file="TableDefinition.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

// ---------------------------------------------------------------------
// <summary>
// </summary>
// ---------------------------------------------------------------------

namespace Microsoft.Database.Isam
{
    using Microsoft.Isam.Esent.Interop;

    /// <summary>
    /// A Table Definition contains the schema for a single table.  It can be
    /// used to explore the schema for an existing table, modify the schema
    /// for an existing table, and to create the definition for a new table.
    /// </summary>
    public partial class TableDefinition
    {
        /// <summary>
        /// The database
        /// </summary>
        private IsamDatabase database = null;

        /// <summary>
        /// The name
        /// </summary>
        private string name = null;

        /// <summary>
        /// The table type
        /// </summary>
        private TableType tableType = TableType.None;

        /// <summary>
        /// The column collection
        /// </summary>
        private ColumnCollection columnCollection = null;

        /// <summary>
        /// The index collection
        /// </summary>
        private IndexCollection indexCollection = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableDefinition"/> class. 
        /// For use when defining a new table.
        /// </summary>
        /// <param name="name">the name of the table to be defined</param>
        public TableDefinition(string name)
        {
            this.name = name;
            this.tableType = TableType.Base;
            this.columnCollection = new ColumnCollection();
            this.indexCollection = new IndexCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableDefinition" /> class.
        /// For use when defining a new table.
        /// </summary>
        /// <param name="name">the name of the table to be defined</param>
        /// <param name="tableType">Type of the table.</param>
        public TableDefinition(string name, TableType tableType)
        {
            this.name = name;
            this.tableType = tableType;
            this.columnCollection = new ColumnCollection();
            this.indexCollection = new IndexCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableDefinition"/> class.
        /// </summary>
        internal TableDefinition()
        {
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the type of the table.
        /// </summary>
        public TableType Type
        {
            get
            {
                return this.tableType;
            }
        }

        /// <summary>
        /// Gets a collection containing the table's columns.
        /// </summary>
        public ColumnCollection Columns
        {
            get
            {
                return this.columnCollection;
            }
        }

        /// <summary>
        /// Gets a collection containing the tables indices.
        /// </summary>
        public IndexCollection Indices
        {
            get
            {
                return this.indexCollection;
            }
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        internal IsamSession IsamSession
        {
            get
            {
                return this.database.IsamSession;
            }
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        internal IsamDatabase Database
        {
            get
            {
                return this.database;
            }
        }

        /// <summary>
        /// Creates a single column with the specified definition in the table
        /// underlying this table definition
        /// </summary>
        /// <param name="columnDefinition">The column definition.</param>
        /// <returns>The <see cref="Columnid"/> object corresponding to the
        /// newly-added column.</returns>
        /// <remarks>
        /// It is currently not possible to add an AutoIncrement column to a
        /// table that is being used by a Cursor.  All such Cursors must be
        /// disposed before the column can be successfully added.
        /// </remarks>
        public Columnid AddColumn(ColumnDefinition columnDefinition)
        {
            lock (this.database.IsamSession)
            {
                using (IsamTransaction trx = new IsamTransaction(this.database.IsamSession))
                {
                    OpenTableGrbit grbit = OpenTableGrbit.None;

                    // if we are trying to add an auto-inc column then we must
                    // be able to open the table for exclusive access.  if we can't
                    // then we will not be able to add the column
                    if ((columnDefinition.Flags & ColumnFlags.AutoIncrement) != 0)
                    {
                        grbit = grbit | OpenTableGrbit.DenyRead;
                    }

                    // open the table with the appropriate access
                    JET_TABLEID tableid;
                    Api.JetOpenTable(this.database.IsamSession.Sesid, this.database.Dbid, this.name, null, 0, grbit, out tableid);

                    // add the new column to the table
                    JET_COLUMNDEF columndef = new JET_COLUMNDEF();
                    columndef.coltyp = DatabaseCommon.ColtypFromColumnDefinition(columnDefinition);
                    columndef.cp = JET_CP.Unicode;
                    columndef.cbMax = columnDefinition.MaxLength;
                    columndef.grbit = Converter.ColumndefGrbitFromColumnFlags(columnDefinition.Flags);
                    byte[] defaultValueBytes = Converter.BytesFromObject(
                        columndef.coltyp,
                        false /* ASCII */,
                        columnDefinition.DefaultValue);
                    int defaultValueBytesLength = (defaultValueBytes == null) ? 0 : defaultValueBytes.Length;
                    JET_COLUMNID jetColumnid;
                    Api.JetAddColumn(
                        this.database.IsamSession.Sesid,
                        tableid,
                        columnDefinition.Name,
                        columndef,
                        defaultValueBytes,
                        defaultValueBytesLength,
                        out jetColumnid);

                    // commit our change
                    Api.JetCloseTable(this.database.IsamSession.Sesid, tableid);
                    trx.Commit();
                    DatabaseCommon.SchemaUpdateID++;

                    // return the columnid for the new column
                    return new Columnid(
                        columnDefinition.Name,
                        jetColumnid,
                        columndef.coltyp,
                        columndef.cp == JET_CP.ASCII);
                }
            }
        }

        /// <summary>
        /// Deletes a single column from the table underlying this table
        /// definition
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <remarks>
        /// It is not possible to delete a column that is currently referenced
        /// by an index over this table.
        /// </remarks>
        public void DropColumn(string columnName)
        {
            lock (this.database.IsamSession)
            {
                using (IsamTransaction trx = new IsamTransaction(this.database.IsamSession))
                {
                    // open the table
                    JET_TABLEID tableid;
                    Api.JetOpenTable(
                        this.database.IsamSession.Sesid,
                        this.database.Dbid,
                        this.name,
                        null,
                        0,
                        OpenTableGrbit.None,
                        out tableid);

                    // delete the column from the table
                    Api.JetDeleteColumn(this.database.IsamSession.Sesid, tableid, columnName);

                    // commit our change
                    Api.JetCloseTable(this.database.IsamSession.Sesid, tableid);
                    trx.Commit();
                    DatabaseCommon.SchemaUpdateID++;
                }
            }
        }

        /// <summary>
        /// Creates a single index with the specified definition in the table
        /// underlying this table definition
        /// </summary>
        /// <param name="indexDefinition">The index definition.</param>
        public void CreateIndex(IndexDefinition indexDefinition)
        {
            lock (this.database.IsamSession)
            {
                using (IsamTransaction trx = new IsamTransaction(this.database.IsamSession))
                {
                    // open the table
                    JET_TABLEID tableid;
                    Api.JetOpenTable(
                        this.database.IsamSession.Sesid,
                        this.database.Dbid,
                        this.name,
                        null,
                        0,
                        OpenTableGrbit.None,
                        out tableid);

                    // add the new index to the table
                    JET_INDEXCREATE[] indexcreates = new JET_INDEXCREATE[1] { new JET_INDEXCREATE() };
                    indexcreates[0].szIndexName = indexDefinition.Name;
                    indexcreates[0].szKey = DatabaseCommon.IndexKeyFromIndexDefinition(indexDefinition);
                    indexcreates[0].cbKey = indexcreates[0].szKey.Length;
                    indexcreates[0].grbit = DatabaseCommon.GrbitFromIndexDefinition(indexDefinition);
                    indexcreates[0].ulDensity = indexDefinition.Density;
                    indexcreates[0].pidxUnicode = new JET_UNICODEINDEX();
                    indexcreates[0].pidxUnicode.lcid = indexDefinition.CultureInfo.LCID;
                    indexcreates[0].pidxUnicode.dwMapFlags = Converter.MapFlagsFromUnicodeIndexFlags(Converter.UnicodeFlagsFromCompareOptions(indexDefinition.CompareOptions));
                    indexcreates[0].rgconditionalcolumn = DatabaseCommon.ConditionalColumnsFromIndexDefinition(indexDefinition);
                    indexcreates[0].cConditionalColumn = indexcreates[0].rgconditionalcolumn.Length;
                    indexcreates[0].cbKeyMost = indexDefinition.MaxKeyLength;
                    Api.JetCreateIndex2(this.database.IsamSession.Sesid, tableid, indexcreates, indexcreates.Length);

                    // commit our change
                    Api.JetCloseTable(this.database.IsamSession.Sesid, tableid);
                    trx.Commit();
                    DatabaseCommon.SchemaUpdateID++;
                }
            }
        }

        /// <summary>
        /// Deletes a single index from the table underlying this table
        /// </summary>
        /// <param name="name">The name.</param>
        /// <remarks>
        /// It is currently not possible to delete an index that is being used
        /// by a Cursor as its CurrentIndex.  All such Cursors must either be
        /// disposed or set to a different index before the index can be
        /// successfully deleted.
        /// </remarks>
        public void DropIndex(string name)
        {
            lock (this.database.IsamSession)
            {
                using (IsamTransaction trx = new IsamTransaction(this.database.IsamSession))
                {
                    // open the table
                    JET_TABLEID tableid;
                    Api.JetOpenTable(
                        this.database.IsamSession.Sesid,
                        this.database.Dbid,
                        this.name,
                        null,
                        0,
                        OpenTableGrbit.None,
                        out tableid);

                    // delete the index from the table
                    Api.JetDeleteIndex(this.database.IsamSession.Sesid, tableid, name);

                    // commit our change
                    Api.JetCloseTable(this.database.IsamSession.Sesid, tableid);
                    trx.Commit();
                    DatabaseCommon.SchemaUpdateID++;
                }
            }
        }

        /// <summary>
        /// Loads the specified table specified in the <see cref="JET_OBJECTLIST"/> object.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="objectList">The object list.</param>
        /// <returns>A <see cref="TableDefinition"/> corresponding to the table specified in <paramref name="objectList"/>.</returns>
        internal static TableDefinition Load(IsamDatabase database, JET_OBJECTLIST objectList)
        {
            lock (database.IsamSession)
            {
                // save the database
                TableDefinition tableDefinition = new TableDefinition();
                tableDefinition.database = database;

                // load info for the table
                tableDefinition.name = Api.RetrieveColumnAsString(
                    database.IsamSession.Sesid,
                    objectList.tableid,
                    objectList.columnidobjectname);

                // create our column and index collections
                tableDefinition.columnCollection = new ColumnCollection(database, tableDefinition.name);
                tableDefinition.indexCollection = new IndexCollection(database, tableDefinition.name);

                return tableDefinition;
            }
        }

        /// <summary>
        /// Loads the specified table specified in the <see cref="JET_OBJECTINFO"/> object.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="objectInfo">The object information.</param>
        /// <returns>A <see cref="TableDefinition"/> corresponding to the table specified in <paramref name="objectInfo"/>.</returns>
        internal static TableDefinition Load(IsamDatabase database, string tableName, JET_OBJECTINFO objectInfo)
        {
            lock (database.IsamSession)
            {
                // save the database
                TableDefinition tableDefinition = new TableDefinition();
                tableDefinition.database = database;

                // load info for the table
                tableDefinition.name = tableName;

                // create our column and index collections
                tableDefinition.columnCollection = new ColumnCollection(database, tableDefinition.name);
                tableDefinition.indexCollection = new IndexCollection(database, tableDefinition.name);

                return tableDefinition;
            }
        }
    }
}
