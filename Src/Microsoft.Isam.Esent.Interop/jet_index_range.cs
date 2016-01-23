//-----------------------------------------------------------------------
// <copyright file="jet_index_range.cs" company="Microsoft Corporation">
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
    /// The native version of the <see cref="JET_INDEX_RANGE"/> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    internal struct NATIVE_INDEX_RANGE
    {
        /// <summary>
        /// The column values for the start of the index.
        /// </summary>
        public IntPtr rgStartColumns;

        /// <summary>
        /// Number of column values for the start of the index.
        /// </summary>
        public uint cStartColumns;

        /// <summary>
        /// The column values for the end of the index.
        /// </summary>
        public IntPtr rgEndColumns;

        /// <summary>
        /// Number of column values for the end of the index.
        /// </summary>
        public uint cEndColumns;
    }

    /// <summary>
    /// Contains definition for <see cref="Windows8Api.JetPrereadIndexRanges"/>.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1300:ElementMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    public class JET_INDEX_RANGE
    {
        /// <summary>
        /// Gets or sets the column values for the start of the index.
        /// </summary>
        public JET_INDEX_COLUMN[] startColumns { get; set; }

        /// <summary>
        /// Gets or sets the column values for the end of the index.
        /// </summary>
        public JET_INDEX_COLUMN[] endColumns { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="JET_INDEX_RANGE"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="JET_INDEX_COLUMN"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "JET_INDEX_RANGE");
        }

        /// <summary>
        /// Gets the NATIVE_indexcolumn structure that represents the object.
        /// </summary>
        /// <param name="handles">GC handle collection to add any pinned handles.</param>
        /// <returns>The NATIVE_indexcolumn structure.</returns>
        internal NATIVE_INDEX_RANGE GetNativeIndexRange(ref GCHandleCollection handles)
        {
            NATIVE_INDEX_RANGE indexRange = new NATIVE_INDEX_RANGE();
            NATIVE_INDEX_COLUMN[] nativeColumns;
            if (this.startColumns != null)
            {
                nativeColumns = new NATIVE_INDEX_COLUMN[this.startColumns.Length];
                for (int i = 0; i < this.startColumns.Length; i++)
                {
                    nativeColumns[i] = this.startColumns[i].GetNativeIndexColumn(ref handles);
                }

                indexRange.rgStartColumns = handles.Add(nativeColumns);
                indexRange.cStartColumns = (uint)this.startColumns.Length;
            }

            if (this.endColumns != null)
            {
                nativeColumns = new NATIVE_INDEX_COLUMN[this.endColumns.Length];
                for (int i = 0; i < this.endColumns.Length; i++)
                {
                    nativeColumns[i] = this.endColumns[i].GetNativeIndexColumn(ref handles);
                }

                indexRange.rgEndColumns = handles.Add(nativeColumns);
                indexRange.cEndColumns = (uint)this.endColumns.Length;
            }

            return indexRange;
        }
    }
}
