// ---------------------------------------------------------------------------
// <copyright file="Columnid.cs" company="Microsoft">
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
    using Microsoft.Isam.Esent.Interop.Vista;

    /// <summary>
    /// Identifies a column in a table
    /// </summary>
    /// <remarks>
    /// A Columnid contains the name of a column and its internal identifier.
    /// A Columnid also encodes the type of the column which is used for conversions to and from CLR objects.
    /// Retrieving an column by columnid is more efficient than retrieving a column by name, as the name to
    /// columnid and type lookup can be expensive
    /// </remarks>
    public class Columnid
    {
        /// <summary>
        /// The columnid
        /// </summary>
        private readonly JET_COLUMNID columnid;

        /// <summary>
        /// The column type of this column.
        /// </summary>
        private readonly JET_coltyp coltyp;

        /// <summary>
        /// The name
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The CLR type that closest represents the data stored in the database.
        /// </summary>
        private readonly Type type;

        /// <summary>
        /// Whether the column contains ASCII data (text columns only).
        /// </summary>
        private readonly bool isAscii;

        /// <summary>
        /// Initializes a new instance of the <see cref="Columnid"/> class.
        /// </summary>
        /// <param name="columnbase">The column identifier.</param>
        internal Columnid(JET_COLUMNBASE columnbase)
            : this(columnbase.szBaseColumnName, columnbase.columnid, columnbase.coltyp, columnbase.cp == JET_CP.ASCII)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Columnid"/> class.
        /// </summary>
        /// <param name="name">The column name.</param>
        /// <param name="columnid">The column identifier.</param>
        /// <param name="coltyp">The column type.</param>
        /// <param name="isAscii">If it's a text column, whether the data is ASCII or Unicode.</param>
        internal Columnid(string name, JET_COLUMNID columnid, JET_coltyp coltyp, bool isAscii)
        {
            this.name = name;
            this.columnid = columnid;
            this.coltyp = coltyp;
            this.isAscii = isAscii;

            switch (coltyp)
            {
                case JET_coltyp.Nil:
                    throw new ArgumentOutOfRangeException("columnid.Type", "Nil is not valid");
                case JET_coltyp.Bit:
                    this.type = typeof(bool);
                    break;
                case JET_coltyp.UnsignedByte:
                    this.type = typeof(byte);
                    break;
                case JET_coltyp.Short:
                    this.type = typeof(short);
                    break;
                case JET_coltyp.Long:
                    this.type = typeof(int);
                    break;
                case JET_coltyp.Currency:
                    this.type = typeof(long);
                    break;
                case JET_coltyp.IEEESingle:
                    this.type = typeof(float);
                    break;
                case JET_coltyp.IEEEDouble:
                    this.type = typeof(double);
                    break;
                case JET_coltyp.DateTime:
                    this.type = typeof(DateTime);
                    break;
                case JET_coltyp.Binary:
                    this.type = typeof(byte[]);
                    break;
                case JET_coltyp.Text:
                    this.type = typeof(string);
                    break;
                case JET_coltyp.LongBinary:
                    this.type = typeof(byte[]);
                    break;
                case JET_coltyp.LongText:
                    this.type = typeof(string);
                    break;
                case VistaColtyp.UnsignedLong:
                    this.type = typeof(uint);
                    break;
                case VistaColtyp.LongLong:
                    this.type = typeof(long);
                    break;
                case VistaColtyp.GUID:
                    this.type = typeof(Guid);
                    break;
                case VistaColtyp.UnsignedShort:
                    this.type = typeof(ushort);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("columnid.Coltyp", "unknown type");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the column contains ASCII data (text columns only).
        /// </summary>
        public bool IsAscii
        {
            get
            {
                return this.isAscii;
            }
        }

        /// <summary>
        /// Gets the name of the column
        /// </summary>
        /// <remarks>
        /// A column name is only unique in the context of a specific table.
        /// </remarks>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the type of the column.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// Gets the interop columnid.
        /// </summary>
        /// <value>
        /// The interop columnid.
        /// </value>
        internal JET_COLUMNID InteropColumnid
        {
            get
            {
                return this.columnid;
            }
        }

        /// <summary>
        /// Gets the underlying ESE <see cref="JET_coltyp"/> of the column.
        /// </summary>
        internal JET_coltyp Coltyp
        {
            get
            {
                return this.coltyp;
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
            return string.Format("Columnid({0}, {1}, {2}, IsAscii={3})", this.Name, this.type.Name, this.coltyp, this.isAscii);
        }
    }
}