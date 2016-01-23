// ---------------------------------------------------------------------------
// <copyright file="ColumnFlags.cs" company="Microsoft">
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
    using Microsoft.Isam.Esent.Interop.Server2003;

    /// <summary>
    /// Column flags enumeration
    /// </summary>
    [Flags]
    public enum ColumnFlags
    {
        /// <summary>
        /// Default options.
        /// </summary>
        None = ColumndefGrbit.None,

        /// <summary>
        /// The column will be fixed. It will always use the same amount of space in a row,
        /// regardless of how much data is being stored in the column. Fixed
        /// cannot be used with Tagged. This bit cannot be used with long values (i.e. Text
        /// and Binary longer than 255 bytes).
        /// </summary>
        Fixed = ColumndefGrbit.ColumnFixed,

        /// <summary>
        /// A variable sized column, cannot be bigger than 255 bytes.
        /// </summary>
        Variable = ColumndefGrbit.None,

        /// <summary>
        /// Sparse columns take no space in the record unless set (unlike Fixed
        /// or Variable columns) and can be up to 2GB in length. Can't be used with
        /// <see cref="Fixed"/>.
        /// </summary>
        Sparse = ColumndefGrbit.ColumnTagged,

        /// <summary>
        /// This column cannot be set to NULL
        /// </summary>
        NonNull = ColumndefGrbit.ColumnNotNULL,

        /// <summary>
        /// This column will contain a version number maintained by the ISAM
        /// that will be incremented on every update of the record.
        /// This option can only be applied to integer columns.
        /// This option can't be used with <see cref="AutoIncrement"/>,
        /// <see cref="EscrowUpdate"/>, or <see cref="Sparse"/>.
        /// </summary>
        Version = ColumndefGrbit.ColumnVersion,

        /// <summary>
        /// The column will automatically be incremented. The number is an increasing number, and
        /// is guaranteed to be unique within a table. The numbers, however, might not be continuous.
        /// For example, if five rows are inserted into a table, the "autoincrement" column could
        /// contain the values { 1, 2, 6, 7, 8 }. This bit can only be used on columns of type
        /// integer types (int and long).
        /// </summary>
        AutoIncrement = ColumndefGrbit.ColumnAutoincrement,

        /// <summary>
        /// This column can be updated by the application (read-only flag, returned
        /// by GetInformation-style calls only).
        /// </summary>
        Updatable = ObjectInfoGrbit.Updatable,

        /// <summary>
        /// The column can be multi-valued.
        /// A multi-valued column can have zero, one, or more values
        /// associated with it. The various values in a multi-valued column are identified by a number
        /// called the itagSequence member.
        /// Multi-valued columns must be tagged columns; that is, they cannot be fixed-length or
        /// variable-length columns.
        /// All multi-
        /// valued columns are also sparse columns.
        /// </summary>
        MultiValued = ColumndefGrbit.ColumnMultiValued,

        /// <summary>
        ///  Specifies that a column is an escrow update column. An escrow update column can be
        ///  updated concurrently by different sessions with JetEscrowUpdate and will maintain
        ///  transactional consistency. An escrow update column must also meet the following conditions:
        ///  An escrow update column can be created only when the table is empty.
        ///  An escrow update column must be of type JET_coltypLong.
        ///  An escrow update column must have a default value.
        ///  JET_bitColumnEscrowUpdate cannot be used in conjunction with <see cref="Sparse"/>,
        ///  <see cref="Version"/>, or <see cref="AutoIncrement"/>.
        /// </summary>
        EscrowUpdate = ColumndefGrbit.ColumnEscrowUpdate,

        /// <summary>
        /// When the escrow-update column reaches a value of zero, the callback function will be invoked.
        /// </summary>
        Finalize = ColumndefGrbit.ColumnFinalize,

        /// <summary>
        /// The default value for a column will be provided by a callback function. A column that
        /// has a user-defined default must be a tagged column.
        /// </summary>
        UserDefinedDefault = ColumndefGrbit.ColumnUserDefinedDefault,

        /// <summary>
        /// This is a finalizable column (delete record if escrow value equals 0).
        /// </summary>
        DeleteOnZero = Server2003Grbits.ColumnDeleteOnZero
    }
}
