//-----------------------------------------------------------------------
// <copyright file="jet_signature.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The JET_SIGNATURE structure contains information that uniquely
    /// identifies a database or logfile sequence.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the name of the unmanaged structure.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the name of the unmanaged structure.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the name of the unmanaged structure.")]
    [StructLayout(LayoutKind.Auto)]
    [Serializable]
    public struct JET_SIGNATURE : IEquatable<JET_SIGNATURE>
    {
        /// <summary>
        /// A randomly assigned number.
        /// </summary>
        internal readonly uint ulRandom;

        /// <summary>
        /// The time that the database or first logfile in the sequence was
        /// created.
        /// </summary>
        internal readonly JET_LOGTIME logtimeCreate;

        /// <summary>
        /// NetBIOS name of the computer. This may be null.
        /// </summary>
        private readonly string szComputerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="JET_SIGNATURE"/> struct.
        /// </summary>
        /// <param name="bytes">The serialized representation of a JET_SIGNATURE from ToBytes().</param>
        public JET_SIGNATURE(byte[] bytes)
        {
            var nativeSignature = new NATIVE_SIGNATURE();
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(nativeSignature));
            Marshal.Copy(bytes, 0, ptr, Marshal.SizeOf(nativeSignature));
            for (int i = bytes.Length; i < Marshal.SizeOf(nativeSignature); i++)
            {
                Marshal.WriteByte(ptr, i, 0);
            }

            nativeSignature = (NATIVE_SIGNATURE)Marshal.PtrToStructure(ptr, nativeSignature.GetType());
            Marshal.FreeHGlobal(ptr);

            this.ulRandom = nativeSignature.ulRandom;
            this.logtimeCreate = nativeSignature.logtimeCreate;
            this.szComputerName = nativeSignature.szComputerName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JET_SIGNATURE"/> struct.
        /// </summary>
        /// <param name="random">A random number.</param>
        /// <param name="time">The time for the creation time.</param>
        /// <param name="computerName">The optional computer name.</param>
        internal JET_SIGNATURE(int random, DateTime? time, string computerName)
        {
            this.ulRandom = unchecked((uint)random);
            this.logtimeCreate = time.HasValue ? new JET_LOGTIME(time.Value) : new JET_LOGTIME();
            this.szComputerName = computerName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JET_SIGNATURE"/> struct.
        /// </summary>
        /// <param name="native">A native signature to initialize the members with.</param>
        internal JET_SIGNATURE(NATIVE_SIGNATURE native)
        {
            this.ulRandom = native.ulRandom;
            this.logtimeCreate = native.logtimeCreate;
            this.szComputerName = native.szComputerName;
        }

        /// <summary>
        /// Determines whether two specified instances of JET_SIGNATURE
        /// are equal.
        /// </summary>
        /// <param name="lhs">The first instance to compare.</param>
        /// <param name="rhs">The second instance to compare.</param>
        /// <returns>True if the two instances are equal.</returns>
        public static bool operator ==(JET_SIGNATURE lhs, JET_SIGNATURE rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Determines whether two specified instances of JET_SIGNATURE
        /// are not equal.
        /// </summary>
        /// <param name="lhs">The first instance to compare.</param>
        /// <param name="rhs">The second instance to compare.</param>
        /// <returns>True if the two instances are not equal.</returns>
        public static bool operator !=(JET_SIGNATURE lhs, JET_SIGNATURE rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Generate a string representation of the structure.
        /// </summary>
        /// <returns>The structure as a string.</returns>
        public override string ToString()
        {
            string convertedDate = string.Empty;
            DateTime? createDate = this.logtimeCreate.ToDateTime();
            if (createDate.HasValue)
            {
                // the standard Round-trip date/time pattern
                convertedDate = createDate.Value.ToString("o", CultureInfo.InvariantCulture);
            }

            return string.Format(
                CultureInfo.InvariantCulture,
                "JET_SIGNATURE({0}:{1}:{2})",
                this.ulRandom,
                convertedDate,
                this.szComputerName);
        }

        /// <summary>
        /// Generate the serialized representation of the structure.
        /// </summary>
        /// <returns>The structure as a byte array.</returns>
        public byte[] ToBytes()
        {
            NATIVE_SIGNATURE nativeSignature = this.GetNativeSignature();
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(nativeSignature));
            Marshal.StructureToPtr(nativeSignature, ptr, false);
            byte[] bytes = new byte[Marshal.SizeOf(nativeSignature)];
            Marshal.Copy(ptr, bytes, 0, Marshal.SizeOf(nativeSignature));
            Marshal.FreeHGlobal(ptr);
            return bytes;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to another instance.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>True if the two instances are equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((JET_SIGNATURE)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.ulRandom.GetHashCode()
                   ^ this.logtimeCreate.GetHashCode()
                   ^ (null == this.szComputerName
                          ? -1
                          : this.szComputerName.GetHashCode());
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to another instance.
        /// </summary>
        /// <param name="other">An instance to compare with this instance.</param>
        /// <returns>True if the two instances are equal.</returns>
        public bool Equals(JET_SIGNATURE other)
        {
            bool namesAreEqual = (string.IsNullOrEmpty(this.szComputerName) && string.IsNullOrEmpty(other.szComputerName))
                                 ||
                                 (!string.IsNullOrEmpty(this.szComputerName) && !string.IsNullOrEmpty(other.szComputerName) &&
                                  this.szComputerName == other.szComputerName);
            return namesAreEqual
                   && this.ulRandom == other.ulRandom
                   && this.logtimeCreate == other.logtimeCreate;
        }

        /// <summary>
        /// Converts the structure to the native representation.
        /// </summary>
        /// <returns>The native representation of the signature.</returns>
        internal NATIVE_SIGNATURE GetNativeSignature()
        {
            var native = new NATIVE_SIGNATURE
            {
                ulRandom = this.ulRandom,
                szComputerName = this.szComputerName,
                logtimeCreate = this.logtimeCreate,
            };
            return native;
        }
    }

    /// <summary>
    /// Native (interop) version of the JET_SIGNATURE structure.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the name of the unmanaged structure.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the name of the unmanaged structure.")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    [Serializable]
    internal struct NATIVE_SIGNATURE
    {
        /// <summary>
        /// Size of the szComputerName array.
        /// </summary>
        public const int ComputerNameSize = 16; // JET_MAX_COMPUTER_NAME_LENGTH + 1

        /// <summary>
        /// The size of a NATIVE_SIGNATURE structure.
        /// </summary>
        public static readonly int Size = Marshal.SizeOf(typeof(NATIVE_SIGNATURE));

        /// <summary>
        /// A random number.
        /// </summary>
        public uint ulRandom;

        /// <summary>
        /// Time the database or log sequence was created.
        /// </summary>
        public JET_LOGTIME logtimeCreate;

        /// <summary>
        /// NetBIOS name of the computer.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ComputerNameSize)]
        public string szComputerName;
    }
}