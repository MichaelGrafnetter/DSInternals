//-----------------------------------------------------------------------
// <copyright file="jet_errinfobasic.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The native version of the JET_ERRINFOBASIC structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    internal struct NATIVE_ERRINFOBASIC
    {
        /// <summary>
        /// The number of elements in the error hierarchy.
        /// </summary>
        public const int HierarchySize = 8;

        /// <summary>
        /// The length of the source file where the error occurred.
        /// </summary>
        public const int SourceFileLength = 64;

        /// <summary>
        /// Size of the structure.
        /// </summary>
        public uint cbStruct;

        /// <summary>
        /// The error value for the requested info level.
        /// </summary>
        public JET_err errValue;

        /// <summary>
        /// The most specific category of the error.
        /// </summary>
        public JET_ERRCAT errcatMostSpecific;

        /// <summary>
        /// Hierarchy of error categories. Position 0 is the highest level in the hierarchy, and the rest are JET_errcatUnknown.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HierarchySize)]
        public byte[] rgCategoricalHierarchy;

        /// <summary>
        /// The source file line for the requested info level.
        /// </summary>
        public uint lSourceLine;

        /// <summary>
        /// The source file name for the requested info level.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SourceFileLength)]
        public string rgszSourceFile;
    }

    /// <summary>
    /// Contains the information about an error.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1300:ElementMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [Serializable]
    public sealed class JET_ERRINFOBASIC : IContentEquatable<JET_ERRINFOBASIC>, IDeepCloneable<JET_ERRINFOBASIC>
    {
        /// <summary>
        /// The error value for the requested info level.
        /// </summary>
        private JET_err errorValue;

        /// <summary>
        /// The most specific category of the error.
        /// </summary>
        private JET_ERRCAT errorcatMostSpecific;

        /// <summary>
        /// Hierarchy of error categories. Position 0 is the highest level in the hierarchy, and the rest are JET_errcatUnknown.
        /// </summary>
        private JET_ERRCAT[] arrayCategoricalHierarchy;

        /// <summary>
        /// The source file line for the requested info level.
        /// </summary>
        private int sourceLine;

        /// <summary>
        /// The source file name for the requested info level.
        /// </summary>
        private string sourceFile;

        /// <summary>
        /// Initializes a new instance of the JET_ERRINFOBASIC class.
        /// </summary>
        public JET_ERRINFOBASIC()
        {
            this.rgCategoricalHierarchy = new JET_ERRCAT[NATIVE_ERRINFOBASIC.HierarchySize];
        }

        /// <summary>
        /// Gets or sets the error value for the requested info level.
        /// </summary>
        public JET_err errValue
        {
            [DebuggerStepThrough]
            get { return this.errorValue; }
            set { this.errorValue = value; }
        }

        /// <summary>
        /// Gets or sets the category of the error.
        /// </summary>
        public JET_ERRCAT errcat
        {
            [DebuggerStepThrough]
            get { return this.errorcatMostSpecific; }
            set { this.errorcatMostSpecific = value; }
        }

        /// <summary>
        /// Gets or sets the hierarchy of errors. Position 0 is the highest level in the hierarchy, and the rest are JET_errcatUnknown.
        /// </summary>
        public JET_ERRCAT[] rgCategoricalHierarchy
        {
            [DebuggerStepThrough]
            get { return this.arrayCategoricalHierarchy; }
            set { this.arrayCategoricalHierarchy = value; }
        }

        /// <summary>
        /// Gets or sets the source file line for the requested info level.
        /// </summary>
        public int lSourceLine
        {
            [DebuggerStepThrough]
            get { return this.sourceLine; }
            set { this.sourceLine = value; }
        }

        /// <summary>
        /// Gets or sets the source file name for the requested info level.
        /// </summary>
        public string rgszSourceFile
        {
            [DebuggerStepThrough]
            get { return this.sourceFile; }
            set { this.sourceFile = value; }
        }

        /// <summary>
        /// Returns a deep copy of the object.
        /// </summary>
        /// <returns>A deep copy of the object.</returns>
        public JET_ERRINFOBASIC DeepClone()
        {
            JET_ERRINFOBASIC result = (JET_ERRINFOBASIC)this.MemberwiseClone();

            return result;
        }

        /// <summary>
        /// Generate a string representation of the instance.
        /// </summary>
        /// <returns>The structure as a string.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "JET_ERRINFOBASIC({0}:{1}:{2}:{3})", this.errValue, this.errcat, this.rgszSourceFile, this.lSourceLine);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to another instance.
        /// </summary>
        /// <param name="other">An instance to compare with this instance.</param>
        /// <returns>True if the two instances are equal.</returns>
        public bool ContentEquals(JET_ERRINFOBASIC other)
        {
            if (null == other)
            {
                return false;
            }

            return this.errValue == other.errValue
                && this.errcat == other.errcat
                && this.lSourceLine == other.lSourceLine
                && this.rgszSourceFile == other.rgszSourceFile
                && Util.ArrayStructEquals(this.rgCategoricalHierarchy, other.rgCategoricalHierarchy, this.rgCategoricalHierarchy == null ? 0 : this.rgCategoricalHierarchy.Length);
        }

        /// <summary>
        /// Gets the native (interop) version of this object.
        /// </summary>
        /// <returns>The native (interop) version of this object.</returns>
        internal NATIVE_ERRINFOBASIC GetNativeErrInfo()
        {
            var native = new NATIVE_ERRINFOBASIC();
            native.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_ERRINFOBASIC)));

            native.errValue = this.errValue;
            native.errcatMostSpecific = this.errcat;
            native.rgCategoricalHierarchy = new byte[NATIVE_ERRINFOBASIC.HierarchySize];

            if (this.rgCategoricalHierarchy != null)
            {
                for (int i = 0; i < this.rgCategoricalHierarchy.Length; ++i)
                {
                    native.rgCategoricalHierarchy[i] = (byte)this.rgCategoricalHierarchy[i];
                }
            }

            native.lSourceLine = (uint)this.lSourceLine;
            native.rgszSourceFile = this.rgszSourceFile;

            return native;
        }

        /// <summary>
        /// Sets the fields of the object from a native JET_ERRINFOBASIC struct.
        /// </summary>
        /// <param name="value">
        /// The native errinfobasic to set the values from.
        /// </param>
        internal void SetFromNative(
            ref NATIVE_ERRINFOBASIC value)
        {
            unchecked
            {
                this.errValue = value.errValue;
                this.errcat = value.errcatMostSpecific;
                if (value.rgCategoricalHierarchy != null)
                {
                    for (int i = 0; i < value.rgCategoricalHierarchy.Length; ++i)
                    {
                        this.rgCategoricalHierarchy[i] = (JET_ERRCAT)value.rgCategoricalHierarchy[i];
                    }
                }

                this.lSourceLine = (int)value.lSourceLine;
                this.rgszSourceFile = value.rgszSourceFile;
            }
        }
    }
}
