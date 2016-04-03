// ---------------------------------------------------------------------------
// <copyright file="ColumnDefinition.cs" company="Microsoft">
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
    using System.Text;

    using Microsoft.Isam.Esent.Interop;
    using Microsoft.Isam.Esent.Interop.Server2003;

    /// <summary>
    /// A Column Definition contains the schema for a single column.  It can be
    /// used to explore the schema for an existing column and to create the
    /// definition for a new column.
    /// </summary>
    public class ColumnDefinition
    {
        /// <summary>
        /// The columnid
        /// </summary>
        private Columnid columnid;

        /// <summary>
        /// The name
        /// </summary>
        private string name = null;

        /// <summary>
        /// The type
        /// </summary>
        private Type type = null;

        /// <summary>
        /// The flags
        /// </summary>
        private ColumnFlags flags = ColumnFlags.None;

        /// <summary>
        /// The maximum length
        /// </summary>
        private int maxLength = 0;

        /// <summary>
        /// The default value
        /// </summary>
        private object defaultValue = null;

        /// <summary>
        /// The read only
        /// </summary>
        private bool readOnly = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDefinition"/> class. 
        /// For use when defining a new column.
        /// </summary>
        /// <param name="name">
        /// the name of the column to be defined
        /// </param>
        /// <param name="type">
        /// the type of the column to be defined
        /// </param>
        /// <param name="flags">
        /// the flags for the column to be defined
        /// </param>
        public ColumnDefinition(string name, Type type, ColumnFlags flags)
        {
            this.name = name;
            this.type = type;
            this.flags = flags;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDefinition"/> class. 
        /// For use when defining a new column.
        /// </summary>
        /// <param name="name">
        /// the name of the column to be defined
        /// </param>
        public ColumnDefinition(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDefinition"/> class.
        /// </summary>
        internal ColumnDefinition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDefinition"/> class.
        /// </summary>
        /// <param name="columnid">The columnid.</param>
        internal ColumnDefinition(Columnid columnid)
        {
            this.columnid = columnid;
            this.name = columnid.Name;
            this.type = columnid.Type;
        }

        /// <summary>
        /// Gets a value indicating whether this is a text column, whether it stores Ascii data. If false, text columns store Unicode data.
        /// </summary>
        public bool IsAscii { get; private set; }

        /// <summary>
        /// Gets the column ID of the column
        /// </summary>
        /// <remarks>
        /// The column ID is undefined if this column definition will be used
        /// to define a new column
        /// </remarks>
        public Columnid Columnid
        {
            get
            {
                return this.columnid;
            }
        }

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets or sets the type of the column.
        /// </summary>
        public Type Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.CheckReadOnly();
                this.type = value;
            }
        }

        /// <summary>
        /// Gets or sets the column's flags.
        /// </summary>
        public ColumnFlags Flags
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
        /// Gets or sets the max length of the column in bytes.
        /// </summary>
        /// <remarks>
        /// The max length of a fixed column need not be specified when
        /// defining a new column.
        /// <para>
        /// A max length of zero for a variable length column is the same as
        /// giving that column the largest possible max length.
        /// </para>
        /// </remarks>
        public int MaxLength
        {
            get
            {
                return this.maxLength;
            }

            set
            {
                this.CheckReadOnly();
                this.maxLength = value;
            }
        }

        /// <summary>
        /// Gets or sets the default value of the column.
        /// </summary>
        /// <remarks>
        /// If the field corresponding to this column in a given record is
        /// never set then the value of that field will be the default value of
        /// the column.
        /// <para>
        /// The size of the default value is currently limited to 255 bytes
        /// by the ISAM.  It is also not possible to specify a zero length
        /// default value.
        /// </para>
        /// </remarks>
        public object DefaultValue
        {
            get
            {
                return this.defaultValue;
            }

            set
            {
                this.CheckReadOnly();
                this.defaultValue = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this column definition cannot be changed.
        /// </summary>
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
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("ColumnDefinition({0}, {1})", this.type.Name, this.Name);
        }

        /// <summary>
        /// Creates a <see cref="ColumnDefinition"/> object representing the column passed in by <paramref name="columnList"/>.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="columnList">The <see cref="JET_COLUMNLIST"/> object that represents the row in
        /// the temptable for this particular column.</param>
        /// <returns>
        /// A <see cref="ColumnDefinition"/> object based on the current row in the temptable
        /// represented by <paramref name="columnList"/>.
        /// </returns>
        internal static ColumnDefinition Load(IsamDatabase database, string tableName, JET_COLUMNLIST columnList)
        {
            lock (database.IsamSession)
            {
                using (IsamTransaction trx = new IsamTransaction(database.IsamSession))
                {
                    // load info for the column
                    ColumnDefinition columnDefinition = new ColumnDefinition();

                    JET_SESID sesid = database.IsamSession.Sesid;

                    // As of Sep 2015, JetGetColumnInfoW is only called for Win8+. Even though Unicode should have
                    // worked in Win7, it wasn't reliable until Win8.
                    Encoding encodingOfTextColumns = EsentVersion.SupportsWindows8Features ? Encoding.Unicode : LibraryHelpers.EncodingASCII;

                    string columnName = Api.RetrieveColumnAsString(
                        database.IsamSession.Sesid,
                        columnList.tableid,
                        columnList.columnidcolumnname,
                        encodingOfTextColumns);
                    JET_COLUMNBASE columnbase;
                    Api.JetGetColumnInfo(database.IsamSession.Sesid, database.Dbid, tableName, columnName, out columnbase);
                    columnDefinition.columnid = new Columnid(columnbase);
                    columnDefinition.name = columnDefinition.columnid.Name;
                    columnDefinition.type = columnDefinition.columnid.Type;

                    ColumndefGrbit grbitColumn = (ColumndefGrbit)Api.RetrieveColumnAsUInt32(sesid, columnList.tableid, columnList.columnidgrbit).GetValueOrDefault();
                    columnDefinition.flags = ColumnFlagsFromGrbits(grbitColumn);

                    columnDefinition.maxLength = Api.RetrieveColumnAsInt32(sesid, columnList.tableid, columnList.columnidcbMax).GetValueOrDefault();

                    columnDefinition.IsAscii = columnbase.cp == JET_CP.ASCII;
                    byte[] defaultValueBytes = Api.RetrieveColumn(sesid, columnList.tableid, columnList.columnidDefault);

                    Columnid isamColumnid = columnDefinition.columnid;
                    columnDefinition.defaultValue = Converter.ObjectFromBytes(
                        isamColumnid.Coltyp,
                        columnDefinition.IsAscii,
                        defaultValueBytes);

                    columnDefinition.ReadOnly = true;

                    return columnDefinition;
                }
            }
        }

        /// <summary>
        /// Creates a <see cref="ColumnDefinition"/> object representing the column passed in by <paramref name="columnBase"/>.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="columnBase">The <see cref="JET_COLUMNBASE"/> object that represents this particular column.</param>
        /// <returns>
        /// A <see cref="ColumnDefinition"/> object based on the <paramref name="columnBase"/> object.
        /// </returns>
        internal static ColumnDefinition Load(IsamDatabase database, string tableName, JET_COLUMNBASE columnBase)
        {
            lock (database.IsamSession)
            {
                using (IsamTransaction trx = new IsamTransaction(database.IsamSession))
                {
                    JET_SESID sesid = database.IsamSession.Sesid;

                    // load info for the column
                    ColumnDefinition columnDefinition = new ColumnDefinition();

                    columnDefinition.columnid = new Columnid(columnBase);
                    columnDefinition.name = columnDefinition.columnid.Name;
                    columnDefinition.type = columnDefinition.columnid.Type;

                    columnDefinition.flags = ColumnFlagsFromGrbits(columnBase.grbit);

                    columnDefinition.maxLength = (int)columnBase.cbMax;
                    columnDefinition.IsAscii = columnBase.cp == JET_CP.ASCII;

                    // there is currently no efficient means to retrieve the
                    // default value of a specific column from JET.  so, we are
                    // going to reach into the catalog and fetch it directly
                    JET_TABLEID tableidCatalog;
                    Api.JetOpenTable(
                        sesid,
                        database.Dbid,
                        "MSysObjects",
                        null,
                        0,
                        OpenTableGrbit.ReadOnly,
                        out tableidCatalog);

                    Api.JetSetCurrentIndex(sesid, tableidCatalog, "RootObjects");
                    Api.MakeKey(sesid, tableidCatalog, true, MakeKeyGrbit.NewKey);
                    Api.MakeKey(
                        sesid,
                        tableidCatalog,
                        Converter.BytesFromObject(JET_coltyp.Text, true, columnBase.szBaseTableName),
                        MakeKeyGrbit.None);
                    Api.JetSeek(sesid, tableidCatalog, SeekGrbit.SeekEQ);
                    JET_COLUMNBASE columnbaseCatalog;
                    Api.JetGetTableColumnInfo(sesid, tableidCatalog, "ObjidTable", out columnbaseCatalog);
                    uint objidTable = Api.RetrieveColumnAsUInt32(sesid, tableidCatalog, columnbaseCatalog.columnid).GetValueOrDefault();

                    Api.JetSetCurrentIndex(sesid, tableidCatalog, "Name");
                    Api.MakeKey(sesid, tableidCatalog, objidTable, MakeKeyGrbit.NewKey);
                    Api.MakeKey(sesid, tableidCatalog, (short)2, MakeKeyGrbit.None);
                    Api.MakeKey(sesid, tableidCatalog, columnBase.szBaseColumnName, Encoding.ASCII, MakeKeyGrbit.None);
                    Api.JetSeek(sesid, tableidCatalog, SeekGrbit.SeekEQ);

                    Api.JetGetTableColumnInfo(sesid, tableidCatalog, "DefaultValue", out columnbaseCatalog);
                    byte[] defaultValueBytes = Api.RetrieveColumn(sesid, tableidCatalog, columnbaseCatalog.columnid);

                    Columnid isamColumnid = columnDefinition.columnid;
                    columnDefinition.defaultValue = Converter.ObjectFromBytes(
                        isamColumnid.Coltyp,
                        columnDefinition.IsAscii,
                        defaultValueBytes);

                    columnDefinition.ReadOnly = true;

                    return columnDefinition;
                }
            }
        }

        /// <summary>
        /// Converts <see cref="ColumndefGrbit"/> to <see cref="ColumnFlags"/>.
        /// </summary>
        /// <param name="grbitColumn">The grbit to convert.</param>
        /// <returns>A <see cref="ColumnFlags"/> value equivalent to <paramref name="grbitColumn"/>.</returns>
        private static ColumnFlags ColumnFlagsFromGrbits(ColumndefGrbit grbitColumn)
        {
            ColumnFlags flags = ColumnFlags.None;

            if ((grbitColumn & ColumndefGrbit.ColumnFixed) != 0)
            {
                flags = flags | ColumnFlags.Fixed;
            }

            if ((grbitColumn & (ColumndefGrbit.ColumnFixed | ColumndefGrbit.ColumnTagged)) == 0)
            {
                flags = flags | ColumnFlags.Variable;
            }

            if ((grbitColumn & ColumndefGrbit.ColumnTagged) != 0)
            {
                flags = flags | ColumnFlags.Sparse;
            }

            if ((grbitColumn & ColumndefGrbit.ColumnNotNULL) != 0)
            {
                flags = flags | ColumnFlags.NonNull;
            }

            if ((grbitColumn & ColumndefGrbit.ColumnVersion) != 0)
            {
                flags = flags | ColumnFlags.Version;
            }

            if ((grbitColumn & ColumndefGrbit.ColumnAutoincrement) != 0)
            {
                flags = flags | ColumnFlags.AutoIncrement;
            }

            if ((grbitColumn & ColumndefGrbit.ColumnUpdatable) != 0)
            {
                flags = flags | ColumnFlags.Updatable;
            }

            if ((grbitColumn & ColumndefGrbit.ColumnMultiValued) != 0)
            {
                flags = flags | ColumnFlags.MultiValued;
            }

            if ((grbitColumn & ColumndefGrbit.ColumnEscrowUpdate) != 0)
            {
                flags = flags | ColumnFlags.EscrowUpdate;
            }

            if ((grbitColumn & ColumndefGrbit.ColumnFinalize) != 0)
            {
                flags = flags | ColumnFlags.Finalize;
            }

            if ((grbitColumn & ColumndefGrbit.ColumnUserDefinedDefault) != 0)
            {
                flags = flags | ColumnFlags.UserDefinedDefault;
            }

            if ((grbitColumn & Server2003Grbits.ColumnDeleteOnZero) != 0)
            {
                flags = flags | ColumnFlags.DeleteOnZero;
            }

            return flags;
        }

        /// <summary>
        /// Checks the read only status.
        /// </summary>
        /// <exception cref="System.NotSupportedException">this column definition cannot be changed</exception>
        private void CheckReadOnly()
        {
            if (this.readOnly)
            {
                throw new NotSupportedException("this column definition cannot be changed");
            }
        }
    }
}
