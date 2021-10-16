//-----------------------------------------------------------------------
// <copyright file="jet_index_column.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using Microsoft.Isam.Esent.Interop.Implementation;

    /// <summary>
    /// Comparison operation for filter defined as <see cref="JET_INDEX_COLUMN"/>.
    /// </summary>
    public enum JetRelop
    {
        /// <summary>
        /// Accept only rows which have column value equal to the given value.
        /// </summary>
        Equals = 0,

        /// <summary>
        /// Accept only rows which have columns whose prefix matches the given value.
        /// </summary>
        PrefixEquals,

        /// <summary>
        /// Accept only rows which have column value not equal to the given value.
        /// </summary>
        NotEquals,

        /// <summary>
        /// Accept only rows which have column value less than or equal a given value.
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// Accept only rows which have column value less than a given value.
        /// </summary>
        LessThan,

        /// <summary>
        /// Accept only rows which have column value greater than or equal a given value.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Accept only rows which have column value greater than a given value.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Accept only rows which have column value AND'ed with a given bitmask yielding zero.
        /// </summary>
        BitmaskEqualsZero,

        /// <summary>
        /// Accept only rows which have column value AND'ed with a given bitmask yielding non-zero.
        /// </summary>
        BitmaskNotEqualsZero,
    }

    /// <summary>
    /// The native version of the <see cref="JET_INDEX_COLUMN"/> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    internal struct NATIVE_INDEX_COLUMN
    {
        /// <summary>
        /// The column identifier for the column to check.
        /// </summary>
        public uint columnid;

        /// <summary>
        /// The comparison operation.
        /// </summary>
        public uint relop;

        /// <summary>
        /// A pointer to a value to compare.
        /// </summary>
        public IntPtr pvData;

        /// <summary>
        /// The size of value beginning at pvData, in bytes.
        /// </summary>
        public uint cbData;

        /// <summary>
        /// Options regarding this column value.
        /// </summary>
        public uint grbit;
    }

    /// <summary>
    /// Contains filter definition for <see cref="Windows8Api.JetPrereadIndexRanges"/> and <see cref="Windows8Api.JetSetCursorFilter"/>.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1300:ElementMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    public class JET_INDEX_COLUMN
    {
        /// <summary>
        /// Gets or sets the column identifier for the column to retrieve.
        /// </summary>
        public JET_COLUMNID columnid { get; set; }

        /// <summary>
        /// Gets or sets the filter comparison operation.
        /// </summary>
        public JetRelop relop { get; set; }

        /// <summary>
        /// Gets or sets the value to compare the column with.
        /// </summary>
        public byte[] pvData { get; set; }

        /// <summary>
        /// Gets or sets the option for this column comparison.
        /// </summary>
        public JetIndexColumnGrbit grbit { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="JET_INDEX_COLUMN"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="JET_INDEX_COLUMN"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "JET_INDEX_COLUMN(0x{0:x})", this.columnid);
        }

        /// <summary>
        /// Gets the NATIVE_indexcolumn structure that represents the object.
        /// </summary>
        /// <param name="handles">GC handle collection to add any pinned handles.</param>
        /// <returns>The NATIVE_indexcolumn structure.</returns>
        internal NATIVE_INDEX_COLUMN GetNativeIndexColumn(ref GCHandleCollection handles)
        {
            NATIVE_INDEX_COLUMN indexColumn = new NATIVE_INDEX_COLUMN();
            indexColumn.columnid = this.columnid.Value;
            indexColumn.relop = (uint)this.relop;
            indexColumn.grbit = (uint)this.grbit;
            if (this.pvData != null)
            {
                indexColumn.pvData = handles.Add(this.pvData);
                indexColumn.cbData = (uint)this.pvData.Length;
            }

            return indexColumn;
        }
    }
}
