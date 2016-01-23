//-----------------------------------------------------------------------
// <copyright file="jet_opentemporarytable2.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Vista
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The native version of the JET_OPENTEMPORARYTABLE2 structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    internal unsafe struct NATIVE_OPENTEMPORARYTABLE2
    {
        /// <summary>
        /// Size of the structure.
        /// </summary>
        public uint cbStruct;

        /// <summary>
        /// Columns to create.
        /// </summary>
        public NATIVE_COLUMNDEF* prgcolumndef;

        /// <summary>
        /// Number of entries in prgcolumndef.
        /// </summary>
        public uint ccolumn;

        /// <summary>
        /// Optional pointer to unicode index information.
        /// </summary>
        public NATIVE_UNICODEINDEX2* pidxunicode;

        /// <summary>
        /// Table options.
        /// </summary>
        public uint grbit;

        /// <summary>
        /// Pointer to array of returned columnids. This
        /// should have at least ccolumn entries.
        /// </summary>
        public uint* rgcolumnid;

        /// <summary>
        /// Maximum key size.
        /// </summary>
        public uint cbKeyMost;

        /// <summary>
        /// Maximum amount of data used to construct a key.
        /// </summary>
        public uint cbVarSegMac;

        /// <summary>
        /// Returns the tableid of the new table.
        /// </summary>
        public IntPtr tableid;
    }
    
    /// <summary>
    /// A collection of parameters for the JetOpenTemporaryTable method.
    /// </summary>
    public partial class JET_OPENTEMPORARYTABLE
    {
        /// <summary>
        /// Returns the unmanaged opentemporarytable that represents this managed class.
        /// </summary>
        /// <returns>
        /// A native (interop) version of the JET_OPENTEMPORARYTABLE.
        /// </returns>
        internal NATIVE_OPENTEMPORARYTABLE2 GetNativeOpenTemporaryTable2()
        {
            this.CheckDataSize();
            var openTemporaryTable = new NATIVE_OPENTEMPORARYTABLE2();
            openTemporaryTable.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_OPENTEMPORARYTABLE2)));
            openTemporaryTable.ccolumn = checked((uint)this.ccolumn);
            openTemporaryTable.grbit = (uint)this.grbit;
            openTemporaryTable.cbKeyMost = checked((uint)this.cbKeyMost);
            openTemporaryTable.cbVarSegMac = checked((uint)this.cbVarSegMac);
            return openTemporaryTable;
        }
    }
}
