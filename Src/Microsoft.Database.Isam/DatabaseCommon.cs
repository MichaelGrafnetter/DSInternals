// ---------------------------------------------------------------------------
// <copyright file="DatabaseCommon.cs" company="Microsoft">
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
    using Microsoft.Isam.Esent.Interop.Vista;

    /// <summary>
    /// A Database is a file used by the ISAM to store data.  It is organized
    /// into tables which are in turn comprised of columns and indices and
    /// contain data in the form of records.  The database's schema can be
    /// enumerated and manipulated by this object.  Also, the database's
    /// tables can be opened for access by this object.
    /// <para>
    /// DatabaseCommon is the common root class for all types of databases.
    /// Currently, there are two types:  Database which provides access to
    /// ordinary databases and TemporaryDatabase which provides access to
    /// temporary databases.
    /// </para>
    /// </summary>
    public abstract class DatabaseCommon : IDisposable
    {
        /// <summary>
        /// The schema update identifier
        /// </summary>
        private static long schemaUpdateID = 0;

        /// <summary>
        /// The session
        /// </summary>
        private readonly IsamSession isamSession;

        /// <summary>
        /// The cleanup
        /// </summary>
        private bool cleanup = false;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseCommon"/> class.
        /// </summary>
        /// <param name="isamSession">The session.</param>
        internal DatabaseCommon(IsamSession isamSession)
        {
            lock (isamSession)
            {
                this.isamSession = isamSession;
                this.cleanup = true;
            }
        }

        /// <summary>
        /// Finalizes an instance of the DatabaseCommon class
        /// </summary>
        ~DatabaseCommon()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the session that created this database
        /// </summary>
        public IsamSession IsamSession
        {
            get
            {
                return this.isamSession;
            }
        }

        /// <summary>
        /// Gets a collection of tables in the database.
        /// </summary>
        /// <returns>a collection of tables in the database</returns>
        public abstract TableCollection Tables { get; }

        /// <summary>
        /// Gets or sets the schema update identifier.
        /// </summary>
        /// <value>
        /// The schema update identifier.
        /// </value>
        internal static long SchemaUpdateID
        {
            get
            {
                return schemaUpdateID;
            }

            set
            {
                schemaUpdateID = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [disposed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [disposed]; otherwise, <c>false</c>.
        /// </value>
        internal virtual bool Disposed
        {
            get
            {
                return this.disposed || this.isamSession.Disposed;
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
        /// Creates a single table with the specified definition in the database
        /// </summary>
        /// <param name="tableDefinition">The table definition.</param>
        public abstract void CreateTable(TableDefinition tableDefinition);

        /// <summary>
        /// Deletes a single table in the database
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <remarks>
        /// It is currently not possible to delete a table that is being used
        /// by a Cursor.  All such Cursors must be disposed before the
        /// table can be successfully deleted.
        /// </remarks>
        public abstract void DropTable(string tableName);

        /// <summary>
        /// Determines if a given table exists in the database
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>
        /// true if the table was found, false otherwise
        /// </returns>
        public abstract bool Exists(string tableName);

        /// <summary>
        /// Opens a cursor over the specified table.
        /// </summary>
        /// <param name="tableName">the name of the table to be opened</param>
        /// <returns>a cursor over the specified table in this database</returns>
        public abstract Cursor OpenCursor(string tableName);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.Dispose();
        }

        /// <summary>
        /// Converts <see cref="ColumnDefinition"/> to a <see cref="JET_coltyp"/>.
        /// </summary>
        /// <param name="columnDefinition">The column definition.</param>
        /// <returns>The <see cref="JET_coltyp"/> equivalent of <paramref name="columnDefinition"/>.</returns>
        /// <exception cref="System.ArgumentException">Cannot map this type to a native column type</exception>
        internal static JET_coltyp ColtypFromColumnDefinition(ColumnDefinition columnDefinition)
        {
            if (columnDefinition.Type == typeof(bool))
            {
                return JET_coltyp.Bit;
            }
            else if (columnDefinition.Type == typeof(byte))
            {
                return JET_coltyp.UnsignedByte;
            }
            else if (columnDefinition.Type == typeof(char))
            {
                return VistaColtyp.UnsignedShort;
            }
            else if (columnDefinition.Type == typeof(System.DateTime))
            {
                return JET_coltyp.DateTime;
            }
            else if (columnDefinition.Type == typeof(double))
            {
                return JET_coltyp.IEEEDouble;
            }
            else if (columnDefinition.Type == typeof(short))
            {
                return JET_coltyp.Short;
            }
            else if (columnDefinition.Type == typeof(int))
            {
                return JET_coltyp.Long;
            }
            else if (columnDefinition.Type == typeof(long))
            {
                return VistaColtyp.LongLong;
            }
            else if (columnDefinition.Type == typeof(float))
            {
                return JET_coltyp.IEEESingle;
            }
            else if (columnDefinition.Type == typeof(string))
            {
                if (columnDefinition.MaxLength > 0 && columnDefinition.MaxLength <= 255)
                {
                    return JET_coltyp.Text;
                }
                else
                {
                    return JET_coltyp.LongText;
                }
            }
            else if (columnDefinition.Type == typeof(ushort))
            {
                return VistaColtyp.UnsignedShort;
            }
            else if (columnDefinition.Type == typeof(uint))
            {
                return VistaColtyp.UnsignedLong;
            }
            else if (columnDefinition.Type == typeof(byte[]))
            {
                if (columnDefinition.MaxLength > 0 && columnDefinition.MaxLength <= 255)
                {
                    return JET_coltyp.Binary;
                }
                else
                {
                    return JET_coltyp.LongBinary;
                }
            }
            else if (columnDefinition.Type == typeof(System.Guid))
            {
                return VistaColtyp.GUID;
            }
            else
            {
                throw new ArgumentException("Cannot map this type to a native column type");
            }
        }

        /// <summary>
        /// Converts a <see cref="IndexDefinition"/> to a double-null-terminated string usable by the
        /// C API to create an index.
        /// </summary>
        /// <param name="indexDefinition">The index definition.</param>
        /// <returns>A double-null-terminated string usable by the C API.</returns>
        internal static string IndexKeyFromIndexDefinition(IndexDefinition indexDefinition)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyColumn keyColumn in indexDefinition.KeyColumns)
            {
                sb.AppendFormat("{0}{1}\0", keyColumn.IsAscending ? "+" : "-", keyColumn.Name);
            }

            // Index keys need to be double-null terminated. The following allows us to use 'string.Length'
            // directly instead of having to add 1/2 for the terminating NULLs.
            sb.Append('\0');
            return sb.ToString();
        }

        /// <summary>
        /// Converts a <see cref="IndexDefinition"/> to an array of <see cref="JET_CONDITIONALCOLUMN"/> objects.
        /// </summary>
        /// <param name="indexDefinition">The index definition.</param>
        /// <returns>An array of <see cref="JET_CONDITIONALCOLUMN"/> objects.</returns>
        internal static JET_CONDITIONALCOLUMN[] ConditionalColumnsFromIndexDefinition(IndexDefinition indexDefinition)
        {
            JET_CONDITIONALCOLUMN[] conditionalColumns = new JET_CONDITIONALCOLUMN[indexDefinition.ConditionalColumns.Count];
            for (int col = 0; col < conditionalColumns.Length; ++col)
            {
                conditionalColumns[col] = new JET_CONDITIONALCOLUMN();
            }

            int i = 0;
            foreach (ConditionalColumn conditionalColumn in indexDefinition.ConditionalColumns)
            {
                conditionalColumns[i].szColumnName = conditionalColumn.Name;
                conditionalColumns[i].grbit = conditionalColumn.MustBeNull
                                                  ? ConditionalColumnGrbit.ColumnMustBeNull
                                                  : ConditionalColumnGrbit.ColumnMustBeNonNull;
                i++;
            }

            return conditionalColumns;
        }

        /// <summary>
        /// Retrieves the <see cref="CreateIndexGrbit"/> options from an <see cref="IndexDefinition"/> object.
        /// </summary>
        /// <param name="indexDefinition">The index definition.</param>
        /// <returns>The <see cref="CreateIndexGrbit"/> options.</returns>
        internal static CreateIndexGrbit GrbitFromIndexDefinition(IndexDefinition indexDefinition)
        {
            CreateIndexGrbit grbit = CreateIndexGrbit.None;

            // We always provide unicode normalization configuration,
            // but ManagedEsent will set the grbit automatically.
            // grbit = grbit | VistaGrbits.IndexUnicode;
            // We always provide a max key length, but ManagedEsent will
            // set the grbit automatically.
            // grbit = grbit | VistaGrbits.IndexKeyMost;

            // we always do cross product indexing of multi-values
            grbit = grbit | VistaGrbits.IndexCrossProduct;

            if ((indexDefinition.Flags & IndexFlags.Unique) != 0)
            {
                grbit = grbit | CreateIndexGrbit.IndexUnique;
            }

            if ((indexDefinition.Flags & IndexFlags.Primary) != 0)
            {
                grbit = grbit | CreateIndexGrbit.IndexPrimary;
            }

            if ((indexDefinition.Flags & IndexFlags.DisallowNull) != 0)
            {
                grbit = grbit | CreateIndexGrbit.IndexDisallowNull;
            }

            if ((indexDefinition.Flags & IndexFlags.IgnoreNull) != 0)
            {
                grbit = grbit | CreateIndexGrbit.IndexIgnoreNull;
            }

            if ((indexDefinition.Flags & IndexFlags.IgnoreAnyNull) != 0)
            {
                grbit = grbit | CreateIndexGrbit.IndexIgnoreAnyNull;
            }

            if ((indexDefinition.Flags & IndexFlags.SortNullsHigh) != 0)
            {
                grbit = grbit | CreateIndexGrbit.IndexSortNullsHigh;
            }

            if ((indexDefinition.Flags & IndexFlags.DisallowTruncation) != 0)
            {
                grbit = grbit | VistaGrbits.IndexDisallowTruncation;
            }

            // AllowTruncation is an Isam-only flag, and we require it.
            if ((indexDefinition.Flags & IndexFlags.AllowTruncation) == 0)
            {
                grbit = grbit | VistaGrbits.IndexDisallowTruncation;
            }

            return grbit;
        }

        /// <summary>
        /// Checks the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="e">The e.</param>
        internal static void CheckName(string name, System.Exception e)
        {
            // we accept null names as valid at this level
            if (name == null)
            {
                return;
            }

            // valid names must be between 1 and 64 chars in length
            if (name.Length < 1 || name.Length > 64)
            {
                throw e;
            }

            // valid names must be in the ASCII range and cannot contain any
            // of the following characters:
            // -  control chars (< 0x20)
            // -  !
            // -  .
            // -  [
            // -  ]
            for (int i = 0; i < name.Length - 1; i++)
            {
                if (name[i] < 0x20
                    || name[i] == '!'
                    || name[i] == '.'
                    || name[i] == '['
                    || name[i] == ']'
                    || name[i] > 0xFF)
                {
                    throw e;
                }
            }

            // valid names must not begin with a space
            if (name[0] == ' ')
            {
                throw e;
            }
        }

        /// <summary>
        /// Converts a major/minor/build number of ESENT to a single number for comparison.
        /// </summary>
        /// <param name="major">The major.</param>
        /// <param name="minor">The minor.</param>
        /// <param name="build">The build.</param>
        /// <param name="update">The update.</param>
        /// <returns>An integer value equivalent of the version specified.</returns>
        /// <remarks>Returns a different value from <see cref="ESEVersion"/></remarks>
        /// <seealso cref="ESEVersion"/>
        internal static long ESENTVersion(int major, int minor, int build, int update)
        {
            long version = ((long)major << 28)
                + (((long)minor << 24) & 0x0F)
                + ((long)build << 8)
                + ((long)update);

            return version;
        }

        /// <summary>
        /// Converts a major/minor/build number of ESE to a single number for comparison.
        /// </summary>
        /// <param name="major">The major.</param>
        /// <param name="minor">The minor.</param>
        /// <param name="build">The build.</param>
        /// <param name="update">The update.</param>
        /// <returns>An integer value equivalent of the version specified.</returns>
        /// <remarks>Returns a different value from <see cref="ESENTVersion"/></remarks>
        /// <seealso cref="ESENTVersion"/>
        internal static long ESEVersion(int major, int minor, int build, int update)
        {
            long version = (((long)major & 0x1F) << 27) +
                (((long)minor & 0x1F) << 22) +
                (((long)build & 0x3FFF) << 8) +
                ((long)update & 0xFF);

            return version;
        }

        /// <summary>
        /// Checks the engine version.
        /// </summary>
        /// <param name="isamSession">The session.</param>
        /// <param name="versionESENT">The minimum ESENT version required.</param>
        /// <param name="versionESE">The minimum ESE version required.</param>
        /// <returns>Whether the current engine is greater than or equal to the required version.</returns>
        internal static bool CheckEngineVersion(
            IsamSession isamSession,
            long versionESENT,
            long versionESE)
        {
#if (ESENT)
            long version = versionESENT;
#else
            long version = versionESE;
#endif

            uint engineVersion;
            Api.JetGetVersion(isamSession.Sesid, out engineVersion);
            return engineVersion >= version;
        }

        /// <summary>
        /// Checks the engine version.
        /// </summary>
        /// <param name="isamSession">The session.</param>
        /// <param name="versionESENT">The version esent.</param>
        /// <param name="versionESE">The version ese.</param>
        /// <param name="e">The e.</param>
        internal static void CheckEngineVersion(
            IsamSession isamSession,
            long versionESENT,
            long versionESE,
            System.Exception e)
        {
            if (!CheckEngineVersion(isamSession, versionESENT, versionESE))
            {
                throw e;
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
            lock (this.isamSession)
            {
                if (this.Disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }
            }
        }
    }
}
