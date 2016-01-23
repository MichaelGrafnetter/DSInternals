// ---------------------------------------------------------------------------
// <copyright file="IndexDefinition.cs" company="Microsoft">
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
    using System.Globalization;
    using System.Text;

    using Microsoft.Isam.Esent.Interop;
    using Microsoft.Isam.Esent.Interop.Vista;

    /// <summary>
    /// An Index Definition contains the schema for a single index.  It can be
    /// used to explore the schema for an existing index and to create the
    /// definition for a new index.
    /// </summary>
    public class IndexDefinition
    {
        /// <summary>
        /// The name
        /// </summary>
        private string name;

        /// <summary>
        /// The flags
        /// </summary>
        private IndexFlags flags = IndexFlags.None;

        /// <summary>
        /// The density
        /// </summary>
        private int density = 100;

        /// <summary>
        /// The culture information
        /// </summary>
        private CultureInfo cultureInfo = new CultureInfo("en-us");

        /// <summary>
        /// The compare options
        /// </summary>
        private CompareOptions compareOptions = CompareOptions.None;

        /// <summary>
        /// The maximum key length
        /// </summary>
        private int maxKeyLength = 256; // force new index semantics in ESENT (don't truncate source data to max key length prior to normalization)

        /// <summary>
        /// The key column collection
        /// </summary>
        private KeyColumnCollection keyColumnCollection;

        /// <summary>
        /// The conditional column collection
        /// </summary>
        private ConditionalColumnCollection conditionalColumnCollection;

        /// <summary>
        /// The read only
        /// </summary>
        private bool readOnly = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexDefinition"/> class. 
        /// For use when defining a new index.
        /// </summary>
        /// <param name="name">
        /// The name of the index to be defined.
        /// </param>
        public IndexDefinition(string name)
        {
            this.name = name;
            this.keyColumnCollection = new KeyColumnCollection();
            this.conditionalColumnCollection = new ConditionalColumnCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexDefinition"/> class.
        /// </summary>
        internal IndexDefinition()
        {
        }

        /// <summary>
        /// Gets the name of the index.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets or sets the index's flags.
        /// </summary>
        public IndexFlags Flags
        {
            get
            {
                return this.flags;
            }

            set
            {
                this.CheckReadOnly();
                this.flags = value;
            }
        }

        /// <summary>
        /// Gets or sets the ideal density of the index in percent.
        /// </summary>
        /// <remarks>
        /// The ideal density of an index is used to setup a newly created
        /// index into a layout on disk that is appropriate for the typical
        /// workload on that index.  For example, an index that will always be
        /// appended should have a density of 100% and an index that will
        /// experience random insertion should have a lower density to reflect
        /// the average density on which that index would converge at run time.
        /// <para>
        /// The density is primarily tweaked for performance reasons.
        /// </para>
        /// </remarks>
        public int Density
        {
            get
            {
                return this.density;
            }

            set
            {
                this.CheckReadOnly();
                this.density = value;
            }
        }

        /// <summary>
        /// Gets or sets the locale of the index.
        /// </summary>
        /// <remarks>
        /// A locale can be specified for any index but will only affect
        /// indexes with key columns containing string data.
        /// </remarks>
        public CultureInfo CultureInfo
        {
            get
            {
                return this.cultureInfo;
            }

            set
            {
                this.CheckReadOnly();
                this.cultureInfo = value;
            }
        }

        /// <summary>
        /// Gets or sets the locale sensitive collation options for the index.
        /// </summary>
        /// <remarks>
        /// These options can be specified for any index but will only affect
        /// indexes with key columns containing string data.
        /// </remarks>
        public CompareOptions CompareOptions
        {
            get
            {
                return this.compareOptions;
            }

            set
            {
                this.CheckReadOnly();
                this.compareOptions = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum length of a normalized key that will be stored in the
        /// index in bytes.
        /// </summary>
        /// <remarks>
        /// In many ways, this is one of the most critical parameters of an
        /// index.  Key truncation can cause different records to end up with
        /// the same key which makes them indistinguishable to the index.  This
        /// can cause records to be ordered randomly with respect to each other
        /// according to the index's sort order.  This can also cause otherwise
        /// unique index entries to be considered duplicates which can cause
        /// some updates to fail.
        /// <para>
        /// The current limits for the ISAM are as follows:  databases with 2KB
        /// pages can have keys up to 255B in length;  databases with 4KB pages
        /// can have keys up to 1000B in length;  and databases with 8KB pages
        /// can have keys up to 2000B in length.  Note that these lengths are
        /// for normalized keys not raw key data.  Normalized keys are always
        /// slightly longer than the raw data from which they are derived.
        /// </para>
        /// <para>
        /// NOTE:  a max key length of 255 is "special" in that it uses legacy
        /// semantics for building keys.  Those semantics include the truncation
        /// of the source data for normalization at 256 bytes.  This mode should
        /// only be used if you specifically want this behavior for backwards
        /// compatibility because it causes errors during the generation of
        /// index keys under certain conditions.
        /// </para>
        /// </remarks>
        public int MaxKeyLength
        {
            get
            {
                return this.maxKeyLength;
            }

            set
            {
                this.CheckReadOnly();
                this.maxKeyLength = value;
            }
        }

        /// <summary>
        /// Gets a collection of the key columns in this index.
        /// </summary>
        public KeyColumnCollection KeyColumns
        {
            get
            {
                return this.keyColumnCollection;
            }
        }

        /// <summary>
        /// Gets a collection of conditional columns in this index.
        /// </summary>
        public ConditionalColumnCollection ConditionalColumns
        {
            get
            {
                return this.conditionalColumnCollection;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this index definition cannot be changed
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is read only]; otherwise, <c>false</c>.
        /// </value>
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
        /// Creates an <see cref="IndexDefinition"/> object from the specified <paramref name="indexList"/>.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="indexList">The index list.</param>
        /// <returns>An <see cref="IndexDefinition"/> object represting the specified index.</returns>
        internal static IndexDefinition Load(IsamDatabase database, string tableName, JET_INDEXLIST indexList)
        {
            lock (database.IsamSession)
            {
                JET_SESID sesid = database.IsamSession.Sesid;
                using (IsamTransaction trx = new IsamTransaction(database.IsamSession))
                {
                    // load info for the index
                    IndexDefinition indexDefinition = new IndexDefinition();

                    indexDefinition.name = Api.RetrieveColumnAsString(
                        sesid,
                        indexList.tableid,
                        indexList.columnidindexname);

                    CreateIndexGrbit grbitIndex = (CreateIndexGrbit)Api.RetrieveColumnAsUInt32(sesid, indexList.tableid, indexList.columnidgrbitIndex);
                    indexDefinition.flags = IndexFlagsFromGrbits(grbitIndex);

                    Api.JetGetIndexInfo(
                        sesid,
                        database.Dbid,
                        tableName,
                        indexDefinition.name,
                        out indexDefinition.density,
                        JET_IdxInfo.SpaceAlloc);
                    int lcid;
                    Api.JetGetIndexInfo(
                        database.IsamSession.Sesid,
                        database.Dbid,
                        tableName,
                        indexDefinition.name,
                        out lcid,
                        JET_IdxInfo.LCID);
                    indexDefinition.cultureInfo = new CultureInfo(lcid);

                    indexDefinition.compareOptions =
                        Conversions.CompareOptionsFromLCMapFlags(
                            Api.RetrieveColumnAsUInt32(
                                database.IsamSession.Sesid,
                                indexList.tableid,
                                indexList.columnidLCMapFlags).GetValueOrDefault());

                    // CONSIDER:  move this workaround into Isam.Interop
                    try
                    {
                        ushort maxKeyLength;
                        Api.JetGetIndexInfo(
                            database.IsamSession.Sesid,
                            database.Dbid,
                            tableName,
                            indexDefinition.name,
                            out maxKeyLength,
                            JET_IdxInfo.KeyMost);
                        indexDefinition.maxKeyLength = maxKeyLength;
                    }
                    catch (EsentInvalidParameterException)
                    {
                        indexDefinition.maxKeyLength = 255;
                    }
                    catch (EsentColumnNotFoundException)
                    {
                        indexDefinition.maxKeyLength = 255;
                    }

                    // load info for each key column in the index
                    int currentColumn = 0;
                    int totalNumberColumns = Api.RetrieveColumnAsInt32(sesid, indexList.tableid, indexList.columnidcColumn).GetValueOrDefault();

                    indexDefinition.keyColumnCollection = new KeyColumnCollection();
                    do
                    {
                        // load info for this key column
                        IndexKeyGrbit grbitColumn = (IndexKeyGrbit)Api.RetrieveColumnAsUInt32(sesid, indexList.tableid, indexList.columnidgrbitColumn);
                        bool isAscending = (grbitColumn & IndexKeyGrbit.Descending) == 0;
                        string columnName = Api.RetrieveColumnAsString(
                            sesid,
                            indexList.tableid,
                            indexList.columnidcolumnname);
                        JET_COLUMNBASE columnbase;
                        Api.JetGetColumnInfo(sesid, database.Dbid, tableName, columnName, out columnbase);

                        indexDefinition.keyColumnCollection.Add(new KeyColumn(new Columnid(columnbase), isAscending));

                        // move onto the next key column definition, unless it is
                        // the last key column
                        if (currentColumn != totalNumberColumns - 1)
                        {
                            Api.TryMoveNext(sesid, indexList.tableid);
                        }
                    }
                    while (++currentColumn < totalNumberColumns);

                    indexDefinition.keyColumnCollection.ReadOnly = true;

                    // Vista: There is currently no efficient means to retrieve the
                    // conditional columns for an index from JET.  so, we are
                    // going to reach into the catalog and fetch them directly.
                    //
                    // FUTURE: Windows 7 introduced Windows7IdxInfo.CreateIndex and Windows7IdxInfo.CreateIndex2 (and
                    // Win8 has Windows8IdxInfo.CreateIndex3). Consider retrieving the conditional columns with that
                    // API and converting the results. But that does not solve the problem for Vista.
                    indexDefinition.conditionalColumnCollection = new ConditionalColumnCollection();
                    JET_TABLEID tableidCatalog;
                    Api.JetOpenTable(
                        database.IsamSession.Sesid,
                        database.Dbid,
                        "MSysObjects",
                        null,
                        0,
                        OpenTableGrbit.ReadOnly,
                        out tableidCatalog);

                    Api.JetSetCurrentIndex(sesid, tableidCatalog, "RootObjects");

                    Api.MakeKey(sesid, tableidCatalog, true, MakeKeyGrbit.NewKey);
                    Api.MakeKey(sesid, tableidCatalog, tableName, Encoding.ASCII, MakeKeyGrbit.None);
                    Api.JetSeek(sesid, tableidCatalog, SeekGrbit.SeekEQ);

                    JET_COLUMNID columnidTemp = Api.GetTableColumnid(sesid, tableidCatalog, "ObjidTable");
                    int objidTable = Api.RetrieveColumnAsInt32(sesid, tableidCatalog, columnidTemp).GetValueOrDefault();

                    Api.JetSetCurrentIndex(sesid, tableidCatalog, "Name");
                    Api.MakeKey(sesid, tableidCatalog, objidTable, MakeKeyGrbit.NewKey);
                    Api.MakeKey(sesid, tableidCatalog, (short)3, MakeKeyGrbit.None);
                    Api.MakeKey(sesid, tableidCatalog, indexDefinition.name, Encoding.ASCII, MakeKeyGrbit.None);
                    Api.JetSeek(sesid, tableidCatalog, SeekGrbit.SeekEQ);

                    columnidTemp = Api.GetTableColumnid(sesid, tableidCatalog, "Flags");
                    int indexFlagsBytes = Api.RetrieveColumnAsInt32(sesid, tableidCatalog, columnidTemp).GetValueOrDefault();

                    columnidTemp = Api.GetTableColumnid(sesid, tableidCatalog, "ConditionalColumns");
                    byte[] conditionalColumnsBytes = Api.RetrieveColumn(sesid, tableidCatalog, columnidTemp);

                    for (int ib = 0; conditionalColumnsBytes != null && ib < conditionalColumnsBytes.Length; ib += 4)
                    {
                        uint colid;
                        bool mustBeNull;
                        JET_COLUMNBASE columnBase;

                        // fIDXExtendedColumns
                        if ((indexFlagsBytes & 0xffff0000) == 0x00010000)
                        {
                            // fIDXSEGTemplateColumn
                            if ((conditionalColumnsBytes[ib + 0] & 0x80) != 0)
                            {
                                // fCOLUMNIDTemplate
                                colid = 0x80000000 | (uint)(conditionalColumnsBytes[ib + 3] << 8) | (uint)conditionalColumnsBytes[ib + 2];
                            }
                            else
                            {
                                colid = (uint)(conditionalColumnsBytes[ib + 3] << 8) | (uint)conditionalColumnsBytes[ib + 2];
                            }

                            // fIDXSEGMustBeNull
                            if ((conditionalColumnsBytes[ib + 0] & 0x20) != 0)
                            {
                                mustBeNull = true;
                            }
                            else
                            {
                                mustBeNull = false;
                            }
                        }
                        else
                        {
                            // do not load conditional columns from an unknown format
                            continue;
                        }

                        JET_COLUMNID castedColid = JET_COLUMNID.CreateColumnidFromNativeValue(unchecked((int)colid));
                        VistaApi.JetGetColumnInfo(
                            database.IsamSession.Sesid,
                            database.Dbid,
                            tableName,
                            castedColid,
                            out columnBase);

                        indexDefinition.conditionalColumnCollection.Add(new ConditionalColumn(new Columnid(columnBase), mustBeNull));
                    }

                    indexDefinition.conditionalColumnCollection.ReadOnly = true;

                    indexDefinition.ReadOnly = true;

                    return indexDefinition;
                }
            }
        }

        /// <summary>
        /// Converts the <see cref="CreateIndexGrbit"/> enumeration to <see cref="IndexFlags"/>.
        /// </summary>
        /// <param name="grbitIndex">Index of the grbit.</param>
        /// <returns>The <see cref="IndexFlags"/> equivalent to <paramref name="grbitIndex"/>.</returns>
        private static IndexFlags IndexFlagsFromGrbits(CreateIndexGrbit grbitIndex)
        {
            IndexFlags flags = IndexFlags.None;

            if ((grbitIndex & CreateIndexGrbit.IndexUnique) != 0)
            {
                flags = flags | IndexFlags.Unique;
            }

            if ((grbitIndex & CreateIndexGrbit.IndexPrimary) != 0)
            {
                flags = flags | IndexFlags.Primary;
            }

            if ((grbitIndex & CreateIndexGrbit.IndexDisallowNull) != 0)
            {
                flags = flags | IndexFlags.DisallowNull;
            }

            if ((grbitIndex & CreateIndexGrbit.IndexIgnoreNull) != 0)
            {
                flags = flags | IndexFlags.IgnoreNull;
            }

            if ((grbitIndex & CreateIndexGrbit.IndexIgnoreAnyNull) != 0)
            {
                flags = flags | IndexFlags.IgnoreAnyNull;
            }

            if ((grbitIndex & CreateIndexGrbit.IndexSortNullsHigh) != 0)
            {
                flags = flags | IndexFlags.SortNullsHigh;
            }

            if ((grbitIndex & VistaGrbits.IndexDisallowTruncation) != 0)
            {
                flags = flags | IndexFlags.DisallowTruncation;
            }
            else
            {
                flags = flags | IndexFlags.AllowTruncation;
            }

            return flags;
        }

        /// <summary>
        /// Checks the read only.
        /// </summary>
        /// <exception cref="System.NotSupportedException">this index definition cannot be changed</exception>
        private void CheckReadOnly()
        {
            if (this.readOnly)
            {
                throw new NotSupportedException("this index definition cannot be changed");
            }
        }
    }
}
