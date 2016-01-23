// --------------------------------------------------------------------------------------------------------------------
// <copyright file="jet_commit_id.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
// <summary>
//   Information context surrounded data emitted from JET_PFNEMITLOGDATA.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Information context surrounded data emitted from JET_PFNEMITLOGDATA.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the name of the unmanaged structure.")]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    internal struct NATIVE_COMMIT_ID
    {
        /// <summary>
        /// Signature for this log sequence.
        /// </summary>
        public NATIVE_SIGNATURE signLog;

        /// <summary>
        /// Reserved value for proper alignment on x86.
        /// </summary>
        public int reserved;

        /// <summary>
        /// Commit-id for this commit transaction.
        /// </summary>
        public long commitId;
    }

    /// <summary>
    /// Information context surrounded data emitted from JET_PFNEMITLOGDATA.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1300:ElementMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    public class JET_COMMIT_ID : IComparable<JET_COMMIT_ID>, IEquatable<JET_COMMIT_ID>
    {
        /// <summary>
        /// Signature for this log sequence.
        /// </summary>
        private readonly JET_SIGNATURE signLog;

        /// <summary>
        /// Commit-id for this commit transaction.
        /// </summary>
        private readonly long commitId;

        /// <summary>
        /// Initializes a new instance of the <see cref="JET_COMMIT_ID"/> class.
        /// </summary>
        /// <param name="native">The native version of the structure.
        /// to use as the data source.</param>
        internal JET_COMMIT_ID(NATIVE_COMMIT_ID native)
        {
            this.signLog = new JET_SIGNATURE(native.signLog);
            this.commitId = native.commitId;
        }

#if MANAGEDESENT_ON_CORECLR
        /// <summary>
        /// Initializes a new instance of the <see cref="JET_COMMIT_ID"/> class. This
        /// is for testing purposes only.
        /// </summary>
        /// <remarks>This is being implemented in new Windows UI only because the Desktop test
        /// code uses reflection to create the object, and the new Windows UI reflection does not
        /// appear to work in this scenario -- Activator.CreateInstance() reflection seems to only
        /// work with exising public constructors.</remarks>
        /// <param name="signature">The log signature for this sequence.</param>
        /// <param name="commitId">The commit identifier for this commit transaction.</param>
        internal JET_COMMIT_ID(JET_SIGNATURE signature, long commitId)
        {
            this.signLog = signature;
            this.commitId = commitId;
        }
#endif // MANAGEDESENT_ON_CORECLR

        /// <summary>
        /// Determine whether one commitid is before another commitid.
        /// </summary>
        /// <param name="lhs">The first commitid to compare.</param>
        /// <param name="rhs">The second commitid to compare.</param>
        /// <returns>True if lhs comes before rhs.</returns>
        public static bool operator <(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }

        /// <summary>
        /// Determine whether one commitid is before another commitid.
        /// </summary>
        /// <param name="lhs">The first commitid to compare.</param>
        /// <param name="rhs">The second commitid to compare.</param>
        /// <returns>True if lhs comes after rhs.</returns>
        public static bool operator >(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }

        /// <summary>
        /// Determine whether one commitid is before another commitid.
        /// </summary>
        /// <param name="lhs">The first commitid to compare.</param>
        /// <param name="rhs">The second commitid to compare.</param>
        /// <returns>True if lhs comes before or equal to rhs.</returns>
        public static bool operator <=(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }

        /// <summary>
        /// Determine whether one commitid is before another commitid.
        /// </summary>
        /// <param name="lhs">The first commitid to compare.</param>
        /// <param name="rhs">The second commitid to compare.</param>
        /// <returns>True if lhs comes after or equal to rhs.</returns>
        public static bool operator >=(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }

        /// <summary>
        /// Determine whether one commitid is is equal to another commitid.
        /// </summary>
        /// <param name="lhs">The first commitid to compare.</param>
        /// <param name="rhs">The second commitid to compare.</param>
        /// <returns>True if lhs comes is equal to rhs.</returns>
        public static bool operator ==(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
        {
            return lhs.CompareTo(rhs) == 0;
        }

        /// <summary>
        /// Determine whether one commitid is not equal to another commitid.
        /// </summary>
        /// <param name="lhs">The first commitid to compare.</param>
        /// <param name="rhs">The second commitid to compare.</param>
        /// <returns>True if lhs comes is not equal to rhs.</returns>
        public static bool operator !=(JET_COMMIT_ID lhs, JET_COMMIT_ID rhs)
        {
            return lhs.CompareTo(rhs) != 0;
        }

        /// <summary>
        /// Generate a string representation of the structure.
        /// </summary>
        /// <returns>The structure as a string.</returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "JET_COMMIT_ID({0}:{1}",
                this.signLog,
                this.commitId);
        }

        /// <summary>
        /// Returns a value comparing this instance with another.
        /// </summary>
        /// <param name="other">An instance to compare with this instance.</param>
        /// <returns>
        /// A signed value representing the relative positions of the instances.
        /// </returns>
        public int CompareTo(JET_COMMIT_ID other)
        {
            if ((object)other == null)
            {
                return this.commitId > 0 ? 1 : 0;
            }

            if (this.signLog != other.signLog)
                {
                throw new ArgumentException("The commit-ids belong to different log-streams");
                }

            return this.commitId.CompareTo(other.commitId);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to another instance.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>true if the two instances are equal.</returns>
        public bool Equals(JET_COMMIT_ID other)
        {
            return this.CompareTo(other) == 0;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to another instance.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>true if the two instances are equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.CompareTo((JET_COMMIT_ID)obj) == 0;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.commitId.GetHashCode()
                   ^ this.signLog.GetHashCode();
        }

        /// <summary>
        /// Converts the class to a NATIVE_COMMIT_ID structure.
        /// </summary>
        /// <returns>A NATIVE_COMMIT_ID structure.</returns>
        internal NATIVE_COMMIT_ID GetNativeCommitId()
        {
            NATIVE_COMMIT_ID native = new NATIVE_COMMIT_ID();

            native.signLog = this.signLog.GetNativeSignature();
            native.commitId = checked((long)this.commitId);

            return native;
        }
    }
}
