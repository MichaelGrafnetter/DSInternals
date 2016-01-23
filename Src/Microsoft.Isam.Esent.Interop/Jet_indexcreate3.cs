//-----------------------------------------------------------------------
// <copyright file="jet_indexcreate3.cs" company="Microsoft Corporation">
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
    using Microsoft.Isam.Esent.Interop.Vista;

    /// <summary>
    /// The native version of the JET_INDEXCREATE3 structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    internal unsafe struct NATIVE_INDEXCREATE3
    {
        /// <summary>
        /// Size of the structure.
        /// </summary>
        public uint cbStruct;

        /// <summary>
        /// Name of the index.
        /// </summary>
        public IntPtr szIndexName;

        /// <summary>
        /// Index key description.
        /// </summary>
        public IntPtr szKey;

        /// <summary>
        /// Size of index key description.
        /// </summary>
        public uint cbKey;

        /// <summary>
        /// Index options.
        /// </summary>
        public uint grbit;

        /// <summary>
        /// Index density.
        /// </summary>
        public uint ulDensity;

        /// <summary>
        /// Pointer to unicode sort options.
        /// </summary>
        public NATIVE_UNICODEINDEX2* pidxUnicode;

        /// <summary>
        /// Maximum size of column data to index. This can also be
        /// a pointer to a JET_TUPLELIMITS structure.
        /// </summary>
        public IntPtr cbVarSegMac;

        /// <summary>
        /// Pointer to array of conditional columns.
        /// </summary>
        public IntPtr rgconditionalcolumn;

        /// <summary>
        /// Count of conditional columns.
        /// </summary>
        public uint cConditionalColumn;

        /// <summary>
        /// Returned error from index creation.
        /// </summary>
        public int err;

        /// <summary>
        /// Maximum size of the key.
        /// </summary>
        public uint cbKeyMost;

        /// <summary>
        /// A <see cref="NATIVE_SPACEHINTS"/> pointer.
        /// </summary>
        public IntPtr pSpaceHints;
    }

    /// <summary>
    /// Contains the information needed to create an index over data in an ESE database.
    /// </summary>
    public sealed partial class JET_INDEXCREATE : IContentEquatable<JET_INDEXCREATE>, IDeepCloneable<JET_INDEXCREATE>
    {
        /// <summary>
        /// Gets the native (interop) version of this object, except for
        /// <see cref="szIndexName"/> and <see cref="szKey"/>.
        /// </summary>
        /// <remarks>The cbKey holds the length of the key in bytes, and does not need to be adjusted.</remarks>
        /// <returns>The native (interop) version of this object.</returns>
        internal NATIVE_INDEXCREATE3 GetNativeIndexcreate3()
        {
            this.CheckMembersAreValid();
            var native = new NATIVE_INDEXCREATE3();

            native.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_INDEXCREATE3)));

            // szIndexName and szKey are converted at pinvoke time.
            //
            // native.szIndexName = this.szIndexName;
            // native.szKey = this.szKey;
            native.cbKey = checked((uint)this.cbKey * sizeof(char));
            native.grbit = unchecked((uint)this.grbit);
            native.ulDensity = checked((uint)this.ulDensity);

            native.cbVarSegMac = new IntPtr(this.cbVarSegMac);

            native.cConditionalColumn = checked((uint)this.cConditionalColumn);

            if (0 != this.cbKeyMost)
            {
                native.cbKeyMost = checked((uint)this.cbKeyMost);
                native.grbit |= unchecked((uint)VistaGrbits.IndexKeyMost);
            }

            return native;
        }
        
        /// <summary>
        /// Sets only the output fields of the object from a NATIVE_INDEXCREATE3 struct,
        /// specifically <see cref="err"/>.
        /// </summary>
        /// <param name="value">
        /// The native indexcreate to set the values from.
        /// </param>
        internal void SetFromNativeIndexCreate(NATIVE_INDEXCREATE3 value)
        {
            this.err = (JET_err)value.err;
        }        
    }
}
