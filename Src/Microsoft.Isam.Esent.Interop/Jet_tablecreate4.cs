//-----------------------------------------------------------------------
// <copyright file="jet_tablecreate4.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using Microsoft.Isam.Esent.Interop.Implementation;

    /// <summary>
    /// The native version of the <see cref="JET_TABLECREATE"/> structure. This includes callbacks,
    /// space hints, and uses NATIVE_INDEXCREATE4.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    internal unsafe struct NATIVE_TABLECREATE4
    {
        /// <summary>
        /// Size of the structure.
        /// </summary>
        public uint cbStruct;

        /// <summary>
        /// Name of the table to create.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szTableName;

        /// <summary>
        /// Name of the table from which to inherit base DDL.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szTemplateTableName;

        /// <summary>
        /// Initial pages to allocate for table.
        /// </summary>
        public uint ulPages;

        /// <summary>
        /// Table density.
        /// </summary>
        public uint ulDensity;

        /// <summary>
        /// Array of column creation info.
        /// </summary>
        public NATIVE_COLUMNCREATE* rgcolumncreate;

        /// <summary>
        /// Number of columns to create.
        /// </summary>
        public uint cColumns;

        /// <summary>
        /// Array of indices to create, pointer to <see cref="NATIVE_INDEXCREATE3"/>.
        /// </summary>
        public IntPtr rgindexcreate;

        /// <summary>
        /// Number of indices to create.
        /// </summary>
        public uint cIndexes;

        /// <summary>
        /// Callback function to use for the table.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szCallback;

        /// <summary>
        /// Type of the callback function.
        /// </summary>
        public JET_cbtyp cbtyp;

        /// <summary>
        /// Table options.
        /// </summary>
        public uint grbit;

        /// <summary>
        /// Space allocation, maintenance, and usage hints for default sequential index.
        /// </summary>
        public NATIVE_SPACEHINTS* pSeqSpacehints;

        /// <summary>
        /// Space allocation, maintenance, and usage hints for Separated LV tree.
        /// </summary>
        public NATIVE_SPACEHINTS* pLVSpacehints;

        /// <summary>
        /// Heuristic size to separate a intrinsic LV from the primary record.
        /// </summary>
        public uint cbSeparateLV;

        /// <summary>
        /// Returned tableid.
        /// </summary>
        public IntPtr tableid;

        /// <summary>
        /// Count of objects created (columns+table+indexes+callbacks).
        /// </summary>
        public uint cCreated;
    }

    /// <summary>
    /// Contains the information needed to create a table in an ESE database.
    /// </summary>
    public partial class JET_TABLECREATE : IContentEquatable<JET_TABLECREATE>, IDeepCloneable<JET_TABLECREATE>
    {
        /// <summary>
        /// Gets the native (interop) version of this object. The following members are
        /// NOT converted: <see cref="rgcolumncreate"/>, <see cref="rgindexcreate"/>,
        /// <see cref="pSeqSpacehints"/>, and <see cref="pLVSpacehints"/>.
        /// </summary>
        /// <returns>The native (interop) version of this object.</returns>
        internal NATIVE_TABLECREATE4 GetNativeTableCreate4()
        {
            this.CheckMembersAreValid();

            var native = new NATIVE_TABLECREATE4();
            native.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_TABLECREATE4)));
            native.szTableName = this.szTableName;
            native.szTemplateTableName = this.szTemplateTableName;
            native.ulPages = checked((uint)this.ulPages);
            native.ulDensity = checked((uint)this.ulDensity);

            // native.rgcolumncreate is done at pinvoke time.
            native.cColumns = checked((uint)this.cColumns);

            // native.rgindexcreate is done at pinvoke time.
            native.cIndexes = checked((uint)this.cIndexes);
            native.szCallback = this.szCallback;
            native.cbtyp = this.cbtyp;
            native.grbit = checked((uint)this.grbit);

            // native.pSeqSpacehints is done at pinvoke time.
            // native.pLVSpacehints is done at pinvoke time.
            native.cbSeparateLV = checked((uint)this.cbSeparateLV);
            native.tableid = this.tableid.Value;
            native.cCreated = checked((uint)this.cCreated);

            return native;
        }
    }
}
