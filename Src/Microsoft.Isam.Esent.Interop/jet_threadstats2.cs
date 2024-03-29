﻿//-----------------------------------------------------------------------
// <copyright file="jet_threadstats2.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows10
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Contains cumulative statistics on the work performed by the database
    /// engine on the current thread. This information is returned via
    /// JetGetThreadStats.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.NamingRules",
        "SA1300:ElementMustBeginWithUpperCaseLetter",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    [Serializable]
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules",
        "SA1305:FieldNamesMustNotUseHungarianNotation",
        Justification = "This should match the unmanaged API, which isn't capitalized.")]
    public struct JET_THREADSTATS2 : IEquatable<JET_THREADSTATS2>
    {
        /// <summary>
        /// The size of a JET_THREADSTATS2 structure.
        /// </summary>
        internal static readonly int Size = Marshal.SizeOf(typeof(JET_THREADSTATS2));

        /// <summary>
        /// Size of the structure. This is used for interop.
        /// </summary>
        private readonly int cbStruct;

        /// <summary>
        /// Number of pages visited.
        /// </summary>
        private int pagesReferenced;

        /// <summary>
        /// Number of pages read from disk.
        /// </summary>
        private int pagesRead;

        /// <summary>
        /// Number of pages preread.
        /// </summary>
        private int pagesPreread;

        /// <summary>
        /// Number of pages dirtied.
        /// </summary>
        private int pagesDirtied;

        /// <summary>
        /// Pages redirtied.
        /// </summary>
        private int pagesRedirtied;

        /// <summary>
        /// Number of log records generated.
        /// </summary>
        private int numLogRecords;

        /// <summary>
        /// Number of bytes logged.
        /// </summary>
        private int loggedBytes;

        /// <summary>
        /// Elapsed time for pages cache missed in microseconds.
        /// </summary>
        private long usecsCacheMisses;

        /// <summary>
        /// Number of pages cache missed.
        /// </summary>
        private int pagesCacheMisses;

        /// <summary>
        /// Gets the total number of database pages visited by the database
        /// engine on the current thread.
        /// </summary>
        public int cPageReferenced
        {
            [DebuggerStepThrough]
            get { return this.pagesReferenced; }
            internal set { this.pagesReferenced = value; }
        }

        /// <summary>
        /// Gets the total number of database pages fetched from disk by the
        /// database engine on the current thread.
        /// </summary>
        public int cPageRead
        {
            [DebuggerStepThrough]
            get { return this.pagesRead; }
            internal set { this.pagesRead = value; }
        }

        /// <summary>
        /// Gets the total number of database pages prefetched from disk by
        /// the database engine on the current thread.
        /// </summary>
        public int cPagePreread
        {
            [DebuggerStepThrough]
            get { return this.pagesPreread; }
            internal set { this.pagesPreread = value; }
        }

        /// <summary>
        /// Gets the total number of database pages, with no unwritten changes,
        /// that have been modified by the database engine on the current thread.
        /// </summary>
        public int cPageDirtied
        {
            [DebuggerStepThrough]
            get { return this.pagesDirtied; }
            internal set { this.pagesDirtied = value; }
        }

        /// <summary>
        /// Gets the total number of database pages, with unwritten changes, that
        /// have been modified by the database engine on the current thread.
        /// </summary>
        public int cPageRedirtied
        {
            [DebuggerStepThrough]
            get { return this.pagesRedirtied; }
            internal set { this.pagesRedirtied = value; }
        }

        /// <summary>
        /// Gets the total number of transaction log records that have been
        /// generated by the database engine on the current thread.
        /// </summary>
        public int cLogRecord
        {
            [DebuggerStepThrough]
            get { return this.numLogRecords; }
            internal set { this.numLogRecords = value; }
        }

        /// <summary>
        /// Gets the total size, in bytes, of transaction log records that
        /// have been generated by the database engine on the current thread.
        /// </summary>
        public int cbLogRecord
        {
            [DebuggerStepThrough]
            get { return this.loggedBytes; }
            internal set { this.loggedBytes = value; }
        }

        /// <summary>
        /// Gets the cumulative latency, in microseconds, of cache misses experienced by
        /// this thread.
        /// </summary>
        public long cusecPageCacheMiss
        {
            [DebuggerStepThrough]
            get { return this.usecsCacheMisses; }
            internal set { this.usecsCacheMisses = value; }
        }

        /// <summary>
        /// Gets the cumulative count of cache misses experienced by the thread.
        /// </summary>
        public int cPageCacheMiss
        {
            [DebuggerStepThrough]
            get { return this.pagesCacheMisses; }
            internal set { this.pagesCacheMisses = value; }
        }

        /// <summary>
        /// Create a new <see cref="JET_THREADSTATS2"/> struct with the specified
        /// valued.
        /// </summary>
        /// <param name="cPageReferenced">
        /// Number of pages visited.
        /// </param>
        /// <param name="cPageRead">
        /// Number of pages read.
        /// </param>
        /// <param name="cPagePreread">
        /// Number of pages preread.
        /// </param>
        /// <param name="cPageDirtied">
        /// TNumber of pages dirtied.
        /// </param>
        /// <param name="cPageRedirtied">
        /// Number of pages redirtied.
        /// </param>
        /// <param name="cLogRecord">
        /// Number of log records generated.
        /// </param>
        /// <param name="cbLogRecord">
        /// Bytes of log records written.
        /// </param>
        /// <param name="cusecPageCacheMiss">
        /// Elapsed time for pages cache missed in microseconds.
        /// </param>
        /// <param name="cPageCacheMiss">
        /// Number of pages cache missed.
        /// </param>
        /// <returns>
        /// A new <see cref="JET_THREADSTATS2"/> struct with the specified values.
        /// </returns>
        public static JET_THREADSTATS2 Create(
            int cPageReferenced,
            int cPageRead,
            int cPagePreread,
            int cPageDirtied,
            int cPageRedirtied,
            int cLogRecord,
            int cbLogRecord,
            long cusecPageCacheMiss,
            int cPageCacheMiss)
        {
            return new JET_THREADSTATS2
            {
                cPageReferenced = cPageReferenced,
                cPageRead = cPageRead,
                cPagePreread = cPagePreread,
                cPageDirtied = cPageDirtied,
                cPageRedirtied = cPageRedirtied,
                cLogRecord = cLogRecord,
                cbLogRecord = cbLogRecord,
                cusecPageCacheMiss = cusecPageCacheMiss,
                cPageCacheMiss = cPageCacheMiss,
            };
        }

        /// <summary>
        /// Add the stats in two JET_THREADSTATS2 structures.
        /// </summary>
        /// <param name="t1">The first JET_THREADSTATS2.</param>
        /// <param name="t2">The second JET_THREADSTATS2.</param>
        /// <returns>A JET_THREADSTATS2 containing the result of adding the stats in t1 and t2.</returns>
        public static JET_THREADSTATS2 Add(JET_THREADSTATS2 t1, JET_THREADSTATS2 t2)
        {
            unchecked
            {
                return new JET_THREADSTATS2
                {
                    cPageReferenced = t1.cPageReferenced + t2.cPageReferenced,
                    cPageRead = t1.cPageRead + t2.cPageRead,
                    cPagePreread = t1.cPagePreread + t2.cPagePreread,
                    cPageDirtied = t1.cPageDirtied + t2.cPageDirtied,
                    cPageRedirtied = t1.cPageRedirtied + t2.cPageRedirtied,
                    cLogRecord = t1.cLogRecord + t2.cLogRecord,
                    cbLogRecord = t1.cbLogRecord + t2.cbLogRecord,
                    cusecPageCacheMiss = t1.cusecPageCacheMiss + t2.cusecPageCacheMiss,
                    cPageCacheMiss = t1.cPageCacheMiss + t2.cPageCacheMiss,
                };
            }
        }

        /// <summary>
        /// Add the stats in two JET_THREADSTATS2 structures.
        /// </summary>
        /// <param name="t1">The first JET_THREADSTATS2.</param>
        /// <param name="t2">The second JET_THREADSTATS2.</param>
        /// <returns>A JET_THREADSTATS2 containing the result of adding the stats in t1 and t2.</returns>
        public static JET_THREADSTATS2 operator +(JET_THREADSTATS2 t1, JET_THREADSTATS2 t2)
        {
            return Add(t1, t2);
        }

        /// <summary>
        /// Calculate the difference in stats between two JET_THREADSTATS2 structures.
        /// </summary>
        /// <param name="t1">The first JET_THREADSTATS2.</param>
        /// <param name="t2">The second JET_THREADSTATS2.</param>
        /// <returns>A JET_THREADSTATS2 containing the difference in stats between t1 and t2.</returns>
        public static JET_THREADSTATS2 Subtract(JET_THREADSTATS2 t1, JET_THREADSTATS2 t2)
        {
            unchecked
            {
                return new JET_THREADSTATS2
                {
                    cPageReferenced = t1.cPageReferenced - t2.cPageReferenced,
                    cPageRead = t1.cPageRead - t2.cPageRead,
                    cPagePreread = t1.cPagePreread - t2.cPagePreread,
                    cPageDirtied = t1.cPageDirtied - t2.cPageDirtied,
                    cPageRedirtied = t1.cPageRedirtied - t2.cPageRedirtied,
                    cLogRecord = t1.cLogRecord - t2.cLogRecord,
                    cbLogRecord = t1.cbLogRecord - t2.cbLogRecord,
                    cusecPageCacheMiss = t1.cusecPageCacheMiss - t2.cusecPageCacheMiss,
                    cPageCacheMiss = t1.cPageCacheMiss - t2.cPageCacheMiss,
                };
            }
        }

        /// <summary>
        /// Calculate the difference in stats between two JET_THREADSTATS2 structures.
        /// </summary>
        /// <param name="t1">The first JET_THREADSTATS2.</param>
        /// <param name="t2">The second JET_THREADSTATS2.</param>
        /// <returns>A JET_THREADSTATS2 containing the difference in stats between t1 and t2.</returns>
        public static JET_THREADSTATS2 operator -(JET_THREADSTATS2 t1, JET_THREADSTATS2 t2)
        {
            return Subtract(t1, t2);
        }

        /// <summary>
        /// Determines whether two specified instances of JET_THREADSTATS2
        /// are equal.
        /// </summary>
        /// <param name="lhs">The first instance to compare.</param>
        /// <param name="rhs">The second instance to compare.</param>
        /// <returns>True if the two instances are equal.</returns>
        public static bool operator ==(JET_THREADSTATS2 lhs, JET_THREADSTATS2 rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Determines whether two specified instances of JET_THREADSTATS2
        /// are not equal.
        /// </summary>
        /// <param name="lhs">The first instance to compare.</param>
        /// <param name="rhs">The second instance to compare.</param>
        /// <returns>True if the two instances are not equal.</returns>
        public static bool operator !=(JET_THREADSTATS2 lhs, JET_THREADSTATS2 rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Gets a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override string ToString()
        {
            // String.Concat is faster than using a StringBuilder.
            // use Int32.ToString instead of passing the Int32 to 
            // String.Format (which requires boxing).
            return string.Concat(
                this.cPageReferenced.ToString("N0", CultureInfo.InvariantCulture),
                " page reference",
                GetPluralS(this.cPageReferenced),
                ", ",
                this.cPageRead.ToString("N0", CultureInfo.InvariantCulture),
                " page",
                GetPluralS(this.cPageRead),
                " read, ",
                this.cPagePreread.ToString("N0", CultureInfo.InvariantCulture),
                " page",
                GetPluralS(this.cPagePreread),
                " preread, ",
                this.cPageDirtied.ToString("N0", CultureInfo.InvariantCulture),
                " page",
                GetPluralS(this.cPageDirtied),
                " dirtied, ",
                this.cPageRedirtied.ToString("N0", CultureInfo.InvariantCulture),
                " page",
                GetPluralS(this.cPageRedirtied),
                " redirtied, ",
                this.cLogRecord.ToString("N0", CultureInfo.InvariantCulture),
                " log record",
                GetPluralS(this.cLogRecord),
                ", ",
                this.cbLogRecord.ToString("N0", CultureInfo.InvariantCulture),
                " byte",
                GetPluralS(this.cbLogRecord),
                " logged",
                ", ",
                this.cusecPageCacheMiss.ToString("N0", CultureInfo.InvariantCulture),
                " page cache miss latency (us)",
                ", ",
                this.cPageCacheMiss.ToString("N0", CultureInfo.InvariantCulture),
                " page cache miss count");
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

            return this.Equals((JET_THREADSTATS2)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.cPageReferenced
                   ^ this.cPageRead << 1
                   ^ this.cPagePreread << 2
                   ^ this.cPageDirtied << 3
                   ^ this.cPageRedirtied << 4
                   ^ this.cLogRecord << 5
                   ^ this.cbLogRecord << 6
                   ^ this.cusecPageCacheMiss.GetHashCode() << 7
                   ^ this.cPageCacheMiss << 8;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal
        /// to another instance.
        /// </summary>
        /// <param name="other">An instance to compare with this instance.</param>
        /// <returns>True if the two instances are equal.</returns>
        public bool Equals(JET_THREADSTATS2 other)
        {
            return this.cPageCacheMiss == other.cPageCacheMiss
                   && this.cusecPageCacheMiss == other.cusecPageCacheMiss
                   && this.cbLogRecord == other.cbLogRecord
                   && this.cLogRecord == other.cLogRecord
                   && this.cPageDirtied == other.cPageDirtied
                   && this.cPagePreread == other.cPagePreread
                   && this.cPageRead == other.cPageRead
                   && this.cPageRedirtied == other.cPageRedirtied
                   && this.cPageReferenced == other.cPageReferenced;
        }

        /// <summary>
        /// Get the plural suffix ('s') for the given number.
        /// </summary>
        /// <param name="n">The number.</param>
        /// <returns>The letter 's' if n is greater than 1.</returns>
        private static string GetPluralS(int n)
        {
            return n == 1 ? string.Empty : "s";
        }
    }
}
