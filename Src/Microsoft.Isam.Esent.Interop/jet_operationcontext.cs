// <copyright file="jet_operationcontext.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows10
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>
    /// A structure that can be used with <see cref="Microsoft.Isam.Esent.Interop.Windows10.Windows10Sesparam.OperationContext"/> to set a client context on a session.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.OrderingRules",
        "SA1202:ElementsMustBeOrderedByAccess",
        Justification = "Ordering matches the rest of the codebase.")]
    internal struct NATIVE_OPERATIONCONTEXT
    {
        /// <summary>
        /// User ID this operation context belongs to.
        /// </summary>
        public int ulUserID;

        /// <summary>
        /// An operation ID identifying this operation.
        /// </summary>
        public byte nOperationID;

        /// <summary>
        /// Type of this operation.
        /// </summary>
        public byte nOperationType;

        /// <summary>
        /// The client type that this operation context belongs to.
        /// </summary>
        public byte nClientType;

        /// <summary>
        /// Flags for additional context that an application might want to store.
        /// </summary>
        public byte fFlags;
    }

    /// <summary>
    /// A type that can be used with <see cref="Windows10Sesparam.OperationContext"/> to set a client context on a session.
    /// </summary>
    public struct JET_OPERATIONCONTEXT : IEquatable<JET_OPERATIONCONTEXT>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="JET_OPERATIONCONTEXT"/>
        /// struct.
        /// </summary>
        /// <param name="native">The native <see cref="NATIVE_OPERATIONCONTEXT"/>
        /// object to be based upon.</param>
        internal JET_OPERATIONCONTEXT(ref NATIVE_OPERATIONCONTEXT native)
            : this()
        {
            this.UserID = native.ulUserID;
            this.OperationID = native.nOperationID;
            this.OperationType = native.nOperationType;
            this.ClientType = native.nClientType;

            this.Flags = native.fFlags;
        }
        #endregion

        /// <summary>
        /// Gets or sets the user ID this operation context belongs to.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets an operation ID identifying this operation.
        /// </summary>
        public byte OperationID { get; set; }

        /// <summary>
        /// Gets or sets the type of this operation.
        /// </summary>
        public byte OperationType { get; set; }

        /// <summary>
        /// Gets or sets the client type that this operation context belongs to.
        /// </summary>
        public byte ClientType { get; set; }

        /// <summary>
        /// Gets or sets the flags for additional context that an application might want to store.
        /// </summary>
        public byte Flags { get; set; }

        #region operators
        /// <summary>
        /// Determines whether two specified instances of <see cref="JET_OPERATIONCONTEXT"/>
        /// are equal.
        /// </summary>
        /// <param name="lhs">The first instance to compare.</param>
        /// <param name="rhs">The second instance to compare.</param>
        /// <returns>True if the two instances are equal.</returns>
        public static bool operator ==(JET_OPERATIONCONTEXT lhs, JET_OPERATIONCONTEXT rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="JET_OPERATIONCONTEXT"/>
        /// are not equal.
        /// </summary>
        /// <param name="lhs">The first instance to compare.</param>
        /// <param name="rhs">The second instance to compare.</param>
        /// <returns>True if the two instances are not equal.</returns>
        public static bool operator !=(JET_OPERATIONCONTEXT lhs, JET_OPERATIONCONTEXT rhs)
        {
            return !(lhs == rhs);
        }
        #endregion

        #region IEquatable and related required functions.
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

            return this.Equals((JET_OPERATIONCONTEXT)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.UserID.GetHashCode()
                   ^ this.OperationID.GetHashCode()
                   ^ this.OperationType.GetHashCode()
                   ^ this.ClientType.GetHashCode()
                   ^ this.Flags.GetHashCode();
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to another instance.
        /// </summary>
        /// <param name="other">An instance to compare with this instance.</param>
        /// <returns>True if the two instances are equal.</returns>
        public bool Equals(JET_OPERATIONCONTEXT other)
        {
            bool contentEquals = 
                this.UserID == other.UserID &&
                this.OperationID == other.OperationID &&
                this.OperationType == other.OperationType &&
                this.ClientType == other.ClientType &&
                this.Flags == other.Flags;
            return contentEquals;
        }

        #endregion

        /// <summary>
        /// Generate a string representation of the instance.
        /// </summary>
        /// <returns>The structure as a string.</returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "JET_OPERATIONCONTEXT({0}:{1}:{2}:{3}:0x{4:x2})",
                this.UserID,
                this.OperationID,
                this.OperationType,
                this.ClientType,
                this.Flags);
        }

        /// <summary>
        /// Gets the native (interop) version of this object.
        /// </summary>
        /// <returns>
        /// The native (interop) version of this object.
        /// </returns>
        internal NATIVE_OPERATIONCONTEXT GetNativeOperationContext()
        {
            var native = new NATIVE_OPERATIONCONTEXT();
            native.ulUserID = this.UserID;
            native.nOperationID = this.OperationID;
            native.nOperationType = this.OperationType;
            native.nClientType = this.ClientType;
            native.fFlags = this.Flags;
            return native;
        }
    }
}
