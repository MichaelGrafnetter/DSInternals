//-----------------------------------------------------------------------
// <copyright file="LibraryHelpers.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;
    using System.Threading;

    using Microsoft.Isam.Esent.Interop.Implementation;

    /// <summary>
    /// Contains several helper functions that are useful in the test binary.
    /// In particular, it contains functionality that is not available in
    /// reduced-functionality environments (such as CoreClr).
    /// </summary>
    internal static class LibraryHelpers
    {
        /// <summary>Provides a platform-specific character used to separate directory levels in a path string that reflects a hierarchical file system organization.</summary>
        /// <filterpriority>1</filterpriority>
        public static readonly char DirectorySeparatorChar = '\\';

        /// <summary>Provides a platform-specific alternate character used to separate directory levels in a path string that reflects a hierarchical file system organization.</summary>
        /// <filterpriority>1</filterpriority>
        public static readonly char AltDirectorySeparatorChar = '/';

        /// <summary>
        /// Gets an ASCII encoder.
        /// </summary>
        public static Encoding EncodingASCII
        {
            get
            {
#if MANAGEDESENT_ON_CORECLR
                return SlowAsciiEncoding.Encoding;
#else
                return Encoding.ASCII;
#endif
            }
        }

        /// <summary>
        /// Gets a new ASCII encoder. It's preferred to use EncodingASCII, but some applications (e.g. tests)
        /// may want a different Encoding object.
        /// </summary>
        public static Encoding NewEncodingASCII
        {
            get
            {
#if MANAGEDESENT_ON_CORECLR
                return new SlowAsciiEncoding();
#else
                return new ASCIIEncoding();
#endif
            }
        }

        // This should be dead code when running on Core CLR; This is only called by
        // GetIndexInfoFromIndexlist() when called on a pre-Win8 system, and Core CLR
        // is only on Win8 anyway.
#if !MANAGEDESENT_ON_CORECLR
        /// <summary>
        /// Creates a CultureInfo object when given the LCID.
        /// </summary>
        /// <param name="lcid">
        /// The lcid passed in.
        /// </param>
        /// <returns>
        /// A CultureInfo object.
        /// </returns>
        public static CultureInfo CreateCultureInfoByLcid(int lcid)
        {
            return new CultureInfo(lcid);
        }
#endif // !MANAGEDESENT_ON_CORECLR

        /// <summary>
        /// Allocates memory on the native heap.
        /// </summary>
        /// <returns>A pointer to native memory.</returns>
        /// <param name="size">The size of the memory desired.</param>
        public static IntPtr MarshalAllocHGlobal(int size)
        {
#if MANAGEDESENT_ON_CORECLR && !MANAGEDESENT_ON_WSA
            return Win32.NativeMethods.LocalAlloc(0, new UIntPtr((uint)size));
#else
            return Marshal.AllocHGlobal(size);
#endif
        }

        /// <summary>
        /// Frees memory that was allocated on the native heap.
        /// </summary>
        /// <param name="buffer">A pointer to native memory.</param>
        public static void MarshalFreeHGlobal(IntPtr buffer)
        {
#if MANAGEDESENT_ON_CORECLR && !MANAGEDESENT_ON_WSA
            Win32.NativeMethods.LocalFree(buffer);
#else
            Marshal.FreeHGlobal(buffer);
#endif
        }

        /// <summary>Copies the contents of a managed <see cref="T:System.String" /> into unmanaged memory.</summary>
        /// <returns>The address, in unmanaged memory, to where the <paramref name="managedString" /> was copied, or 0 if <paramref name="managedString" /> is null.</returns>
        /// <param name="managedString">A managed string to be copied.</param>
        /// <exception cref="T:System.OutOfMemoryException">The method could not allocate enough native heap memory.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="managedString" /> parameter exceeds the maximum length allowed by the operating system.</exception>
        public static IntPtr MarshalStringToHGlobalUni(string managedString)
        {
#if MANAGEDESENT_ON_CORECLR && !MANAGEDESENT_ON_WSA
            return MyStringToHGlobalUni(managedString);
#else
            return Marshal.StringToHGlobalUni(managedString);
#endif
        }

        /// <summary>
        /// Retrieves the managed ID of the current thread.
        /// </summary>
        /// <returns>The ID of the current thread.</returns>
        public static int GetCurrentManagedThreadId()
        {
#if MANAGEDESENT_ON_CORECLR
            return Environment.CurrentManagedThreadId;
#else
            return Thread.CurrentThread.ManagedThreadId;
#endif
        }

        /// <summary>
        /// Cancels an <see cref="M:System.Threading.Thread.Abort(System.Object)"/> requested for the current thread.
        /// </summary>
        /// <exception cref="T:System.Threading.ThreadStateException">Abort was not invoked on the current thread. </exception><exception cref="T:System.Security.SecurityException">The caller does not have the required security permission for the current thread. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread"/></PermissionSet>
        public static void ThreadResetAbort()
        {
#if MANAGEDESENT_ON_CORECLR
            // Do nothing.
#else
            Thread.ResetAbort();
#endif
        }

        // FUTURE-2013/12/16-martinc. It appears that all of this hacking for running on Core CLR may no longer be necessary.
        // We initially ported to an early version of Core CLR that had a lot of functionality missing. By the time
        // Windows Store Apps came out in Windows 8, many of these functions were added back.
#if MANAGEDESENT_ON_CORECLR && !MANAGEDESENT_ON_WSA
        // System.Runtime.InteropServices.Marshal

        /// <summary>Copies the contents of a managed <see cref="T:System.String" /> into unmanaged memory.</summary>
        /// <returns>The address, in unmanaged memory, to where the <paramref name="managedString" /> was copied, or 0 if <paramref name="managedString" /> is null.</returns>
        /// <param name="managedString">A managed string to be copied.</param>
        /// <exception cref="T:System.OutOfMemoryException">The method could not allocate enough native heap memory.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="managedString" /> parameter exceeds the maximum length allowed by the operating system.</exception>
        [SecurityCritical]
        private static unsafe IntPtr MyStringToHGlobalUni(string managedString)
        {
            if (managedString == null)
            {
                return IntPtr.Zero;
            }

            int charCountWithNull = managedString.Length + 1;
            int byteCount = charCountWithNull * sizeof(char);

            if (byteCount < managedString.Length)
            {
                throw new ArgumentOutOfRangeException("managedString");
            }

            UIntPtr sizetdwBytes = new UIntPtr((uint)byteCount);
            IntPtr rawBuffer = Win32.NativeMethods.LocalAlloc(0, sizetdwBytes);
            if (rawBuffer == IntPtr.Zero)
            {
                throw new OutOfMemoryException();
            }

            fixed (char* sourcePointer = managedString)
            {
                byte* destPointer = (byte*)rawBuffer;
                var unicodeEncoding = new System.Text.UnicodeEncoding();
                int bytesWritten = unicodeEncoding.GetBytes(sourcePointer, charCountWithNull, destPointer, byteCount);
            }

            return rawBuffer;
        }
#endif // MANAGEDESENT_ON_CORECLR && !MANAGEDESENT_ON_WSA

        /// <summary>Returns a <see cref="T:System.DateTime" /> equivalent to the specified OLE Automation Date.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> that represents the same date and time as <paramref name="d" />.</returns>
        /// <param name="d">An OLE Automation Date value. </param>
        /// <exception cref="T:System.ArgumentException">The date is not a valid OLE Automation Date value. </exception>
        /// <filterpriority>1</filterpriority>
        public static DateTime FromOADate(double d)
        {
#if MANAGEDESENT_ON_CORECLR
            return new DateTime(DoubleDateToTicks(d), DateTimeKind.Unspecified);
#else
            return DateTime.FromOADate(d);
#endif
        }

#if MANAGEDESENT_ON_CORECLR
        /// <summary>
        /// Copied from the reflected implementation.
        /// </summary>
        /// <param name="value">The date, as a 64bit integer.</param>
        /// <returns>The date, as a double representation.</returns>
        internal static double TicksToOADate(long value)
        {
            if (value == 0L)
            {
                return 0.0;
            }

            if (value < 864000000000L)
            {
                value += 599264352000000000L;
            }

            if (value < 31241376000000000L)
            {
                throw new OverflowException();
            }

            long num = (value - 599264352000000000L) / 10000L;
            if (num < 0L)
            {
                long num2 = num % 86400000L;
                if (num2 != 0L)
                {
                    num -= (86400000L + num2) * 2L;
                }
            }

            return (double)num / 86400000.0;
        }

        /// <summary>
        /// Copied from the reflected implementation.
        /// </summary>
        /// <param name="value">The date, as a double representation.</param>
        /// <returns>The date, as a 64bit integer.</returns>
        internal static long DoubleDateToTicks(double value)
        {
            if (value >= 2958466.0 || value <= -657435.0)
            {
                throw new ArgumentException("value does not represent a valid date", "value");
            }

            long num = (long)((value * 86400000.0) + ((value >= 0.0) ? 0.5 : -0.5));
            if (num < 0L)
            {
                num -= (num % 86400000L) * 2L;
            }

            num += 59926435200000L;
            if (num < 0L || num >= 315537897600000L)
            {
                throw new ArgumentException("value does not represent a valid date", "value");
            }

            return num * 10000L;
        }
#endif
    }
}
