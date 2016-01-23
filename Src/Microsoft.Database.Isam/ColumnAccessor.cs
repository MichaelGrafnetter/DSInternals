// ---------------------------------------------------------------------------
// <copyright file="ColumnAccessor.cs" company="Microsoft">
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
    /// Wraps a JET_TABLEID with indexed accessors
    /// </summary>
    public class ColumnAccessor : IEnumerable
    {
        /// <summary>
        /// The underlying session.
        /// </summary>
        private readonly IsamSession isamSession;

        /// <summary>
        /// The underlying tableid.
        /// </summary>
        private readonly JET_TABLEID tableid;

        /// <summary>
        /// The grbits to use for retrieving the column.
        /// </summary>
        private readonly RetrieveColumnGrbit grbit;

        /// <summary>
        /// The underlying cursor.
        /// </summary>
        private Cursor cursor;

        /// <summary>
        /// The Update Identifier.
        /// </summary>
        private long updateID = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnAccessor" /> class.
        /// </summary>
        /// <param name="cursor">The cursor.</param>
        /// <param name="isamSession">The session.</param>
        /// <param name="tableid">The tableid.</param>
        /// <param name="grbit">The grbit.</param>
        internal ColumnAccessor(Cursor cursor, IsamSession isamSession, JET_TABLEID tableid, RetrieveColumnGrbit grbit)
        {
            this.cursor = cursor;
            this.isamSession = isamSession;
            this.tableid = tableid;
            this.grbit = grbit;
        }

        /// <summary>
        /// Gets the update identifier.
        /// </summary>
        internal long UpdateID
        {
            get
            {
                return this.updateID;
            }
        }

        /// <summary>
        /// Accessor for itag 1 of a column, converted to/from an object
        /// </summary>
        /// <value>
        /// The <see cref="System.Object"/>.
        /// </value>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The value stored within.</returns>
        public object this[string columnName]
        {
            get
            {
                return this.RetrieveColumn(columnName, 0);
            }

            set
            {
                this.SetColumn(columnName, 0, value);
            }
        }

        /// <summary>
        /// Accessor for any itag of a column, converted to/from an object
        /// </summary>
        /// <value>
        /// The <see cref="System.Object"/>.
        /// </value>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value stored within.</returns>
        /// <remarks>
        /// The index argument is 0-based, we add 1 to get the itag
        /// </remarks>
        public object this[string columnName, int index]
        {
            get
            {
                return this.RetrieveColumn(columnName, index);
            }

            set
            {
                this.SetColumn(columnName, index, value);
            }
        }

        /// <summary>
        /// Accessor for itag 1 of a column, converted to/from an object
        /// </summary>
        /// <value>
        /// The <see cref="System.Object"/>.
        /// </value>
        /// <param name="columnid">The columnid.</param>
        /// <returns>The value stored within.</returns>
        public object this[Columnid columnid]
        {
            get
            {
                return this.RetrieveColumn(columnid.InteropColumnid, columnid.Coltyp, columnid.IsAscii, 0);
            }

            set
            {
                this.SetColumn(columnid.InteropColumnid, columnid.Coltyp, columnid.IsAscii, 0, value);
            }
        }

        /// <summary>
        /// Accessor for any itag of a column, converted to/from an object
        /// </summary>
        /// <value>
        /// The <see cref="System.Object"/>.
        /// </value>
        /// <param name="columnid">The columnid.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value stored within.</returns>
        /// <remarks>
        /// The index argument is 0-based, we add 1 to get the itag
        /// </remarks>
        public object this[Columnid columnid, int index]
        {
            get
            {
                return this.RetrieveColumn(columnid.InteropColumnid, columnid.Coltyp, columnid.IsAscii, index);
            }

            set
            {
                this.SetColumn(columnid.InteropColumnid, columnid.Coltyp, columnid.IsAscii, index, value);
            }
        }

        /// <summary>
        /// Calculates the size of the specified multivalue stored in the specified column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="index">The index.</param>
        /// <returns>Returns the size of the specified multivalue stored in the specified column.</returns>
        public long SizeOf(string columnName, int index)
        {
            lock (this.isamSession)
            {
                this.cursor.CheckDisposed();

                using (IsamTransaction trx = new IsamTransaction(this.isamSession, true))
                {
                    return this.SizeOf(this.cursor.TableDefinition.Columns[columnName].Columnid, index);
                }
            }
        }

        /// <summary>
        /// Calculates the size of the specified column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>Returns the size of the specified column.</returns>
        public long SizeOf(string columnName)
        {
            return this.SizeOf(columnName, 0);
        }

        /// <summary>
        /// Calculates the size of the specified multivalue stored in the specified column.
        /// </summary>
        /// <param name="columnid">The columnid.</param>
        /// <param name="index">The index.</param>
        /// <returns>Returns the size of the specified multivalue stored in the specified column.</returns>
        public long SizeOf(Columnid columnid, int index)
        {
            lock (this.isamSession)
            {
                this.cursor.CheckDisposed();

                if ((this.grbit & RetrieveColumnGrbit.RetrieveCopy) == 0)
                {
                    this.cursor.CheckRecord();
                }

                int itagSequence = index + 1;

                int? size = Api.RetrieveColumnSize(
                    this.isamSession.Sesid,
                    this.tableid,
                    columnid.InteropColumnid,
                    itagSequence,
                    RetrieveColumnGrbit.None);
                return size.GetValueOrDefault();
            }
        }

        /// <summary>
        /// Calculates the size of the specified column.
        /// </summary>
        /// <param name="columnid">The columnid.</param>
        /// <returns>Returns the size of the specified column.</returns>
        public long SizeOf(Columnid columnid)
        {
            return this.SizeOf(columnid, 0);
        }

        /// <summary>
        /// Fetches an enumerator containing all the field values in the record.
        /// </summary>
        /// <returns>
        /// Returns an enumerator containing all the field values in the record.
        /// </returns>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public RecordEnumerator GetEnumerator()
        {
            // return the enumerator for the field collection
            return this.cursor.GetFields(this.grbit).GetEnumerator();
        }

        /// <summary>
        /// Fetches an enumerator containing all the field values in the record
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <remarks>
        /// This is the standard version that will work with other CLR
        /// languages.
        /// </remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

        /// <summary>
        /// Retrieves the column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value stored within.</returns>
        private object RetrieveColumn(string columnName, int index)
        {
            lock (this.isamSession)
            {
                this.cursor.CheckDisposed();

                ColumnDefinition columnDefinition = this.cursor.TableDefinition.Columns[columnName];
                return this.RetrieveColumn(columnDefinition, index);
            }
        }

        /// <summary>
        /// Retrieves the column.
        /// </summary>
        /// <param name="columnDefinition">The column definition.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value stored within.</returns>
        private object RetrieveColumn(ColumnDefinition columnDefinition, int index)
        {
            lock (this.isamSession)
            {
                this.cursor.CheckDisposed();

                using (IsamTransaction trx = new IsamTransaction(this.isamSession, true))
                {
                    Columnid columnid = columnDefinition.Columnid;
                    return this.RetrieveColumn(columnid.InteropColumnid, columnid.Coltyp, columnid.IsAscii, index);
                }
            }
        }

        /// <summary>
        /// Sets the column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="index">The index.</param>
        /// <param name="obj">The object.</param>
        private void SetColumn(string columnName, int index, object obj)
        {
            lock (this.isamSession)
            {
                this.cursor.CheckDisposed();

                // Note that this overload doesn't start a transaction, but assumes the
                // implementation does the transaction.
                ColumnDefinition columnDefinition = this.cursor.TableDefinition.Columns[columnName];
                this.SetColumn(columnDefinition, index, obj);
            }
        }

        /// <summary>
        /// Sets the column.
        /// </summary>
        /// <param name="columnDefinition">The column definition.</param>
        /// <param name="index">The index.</param>
        /// <param name="obj">The object.</param>
        private void SetColumn(ColumnDefinition columnDefinition, int index, object obj)
        {
            lock (this.isamSession)
            {
                this.cursor.CheckDisposed();

                using (IsamTransaction trx = new IsamTransaction(this.isamSession, true))
                {
                    Columnid columnid = columnDefinition.Columnid;
                    this.SetColumn(columnid.InteropColumnid, columnid.Coltyp, columnid.IsAscii, index, obj);
                    trx.Commit();
                }
            }
        }

        /// <summary>
        /// Retrieves the column.
        /// </summary>
        /// <param name="columnid">The columnid.</param>
        /// <param name="coltyp">The coltyp.</param>
        /// <param name="isAscii">Whether a textual column is Ascii.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value stored within.</returns>
        /// <remarks>
        /// Itags start at 1, so we add 1 to the index
        /// </remarks>
        private object RetrieveColumn(JET_COLUMNID columnid, JET_coltyp coltyp, bool isAscii, int index)
        {
            lock (this.isamSession)
            {
                this.cursor.CheckDisposed();

                if ((this.grbit & RetrieveColumnGrbit.RetrieveCopy) == 0)
                {
                    this.cursor.CheckRecord();
                }

                JET_RETINFO retinfo = new JET_RETINFO();
                retinfo.ibLongValue = 0;
                retinfo.itagSequence = index + 1;
                byte[] bytes = Api.RetrieveColumn(this.isamSession.Sesid, this.tableid, columnid, this.grbit, retinfo);

                object obj = Converter.ObjectFromBytes(coltyp, isAscii, bytes);
                return obj;
            }
        }

        /// <summary>
        /// Sets the column.
        /// </summary>
        /// <param name="columnid">The columnid.</param>
        /// <param name="coltyp">The coltyp.</param>
        /// <param name="isAscii">Whether a textual column is Ascii.</param>
        /// <param name="index">The index.</param>
        /// <param name="obj">The object.</param>
        /// <exception cref="System.InvalidOperationException">You may only update fields through Cursor.EditRecord.</exception>
        /// <remarks>
        /// Itags start at 1, so we add 1 to the index
        /// </remarks>
        private void SetColumn(JET_COLUMNID columnid, JET_coltyp coltyp, bool isAscii, int index, object obj)
        {
            lock (this.isamSession)
            {
                this.cursor.CheckDisposed();

                if ((this.grbit & RetrieveColumnGrbit.RetrieveCopy) == 0)
                {
                    this.cursor.CheckRecord();
                    throw new InvalidOperationException("You may only update fields through Cursor.EditRecord.");
                }

                // if this is a Sort or PreSortTemporary TT and we are setting
                // an LV column then always attempt to use intrinsic LVs
                SetColumnGrbit grbitSet = SetColumnGrbit.None;
                if ((this.cursor.TableDefinition.Type == TableType.Sort
                     || this.cursor.TableDefinition.Type == TableType.PreSortTemporary)
                    && (coltyp == JET_coltyp.LongText || coltyp == JET_coltyp.LongBinary))
                {
                    grbitSet = grbitSet | SetColumnGrbit.IntrinsicLV;
                }

                JET_SETINFO setinfo = new JET_SETINFO();
                setinfo.ibLongValue = 0;
                setinfo.itagSequence = index + 1;

                byte[] bytes = Converter.BytesFromObject(coltyp, isAscii, obj);
                int bytesLength = bytes == null ? 0 : bytes.Length;

                Api.JetSetColumn(this.isamSession.Sesid, this.tableid, columnid, bytes, bytesLength, grbitSet, setinfo);

                this.updateID++;
            }
        }
    }
}
