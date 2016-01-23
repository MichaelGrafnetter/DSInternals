//-----------------------------------------------------------------------
// <copyright file="Win32NativeMethods.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
// <summary>P/Invoke constants for Win32 functions.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Win32
{
    using System;
    using System.ComponentModel;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;

#if !MANAGEDESENT_ON_WSA // None of this can be called in windows store apps.
    /// <summary>
    /// Allocation type options for <see cref="NativeMethods.VirtualAlloc"/>.
    /// </summary>
    [Flags]
    internal enum AllocationType : uint
    {
        /// <summary>
        /// Commit the memory.
        /// </summary>
        MEM_COMMIT = 0x1000,

        /// <summary>
        /// Reserve the memory.
        /// </summary>
        MEM_RESERVE = 0x2000,
    }

    /// <summary>
    /// Memory protection options for <see cref="NativeMethods.VirtualAlloc"/>.
    /// </summary>
    internal enum MemoryProtection : uint
    {
        /// <summary>
        /// Read/write access to the pages.
        /// </summary>
        PAGE_READWRITE = 0x04,
    }

    /// <summary>
    /// Options for <see cref="NativeMethods.VirtualFree"/>.
    /// </summary>
    internal enum FreeType : uint
    {
        /// <summary>
        /// Release the memory. The pages will be in the free state.
        /// </summary>
        MEM_RELEASE = 0x8000,
    }
#endif

    /// <summary>
    /// P/Invoke methods for Win32 functions.
    /// </summary>
    internal static class NativeMethods
    {
#if MANAGEDESENT_ON_CORECLR || MANAGEDESENT_ON_WSA
        /// <summary>
        /// The name of the DLL that holds the Core Memory API set.
        /// </summary>
        private const string WinCoreMemoryDll = "api-ms-win-core-memory-l1-1-1.dll";

        /// <summary>
        /// The name of the DLL that holds the Obsolete Heap API set.
        /// (Might be api-ms-win-core-heap-obsolete-l1-1-0.dll.)
        /// </summary>
        private const string HeapObsolete = "kernelbase";

        /// <summary>
        /// The name of the DLL that holds the Core process/threads API set.
        /// </summary>
        private const string WinCoreProcessThreads = "api-ms-win-core-processthreads-l1-1-1.dll";
#else
        /// <summary>
        /// The name of the DLL that holds the Core Memory API set.
        /// </summary>
        private const string WinCoreMemoryDll = "kernel32.dll";

        /// <summary>
        /// The name of the DLL that holds the Obsolete Heap API set.
        /// </summary>
        private const string HeapObsolete = "kernel32.dll";

        /// <summary>
        /// The name of the DLL that holds the Core process/threads API set.
        /// </summary>
        private const string WinCoreProcessThreads = "kernel32.dll";
#endif // MANAGEDESENT_ON_CORECLR || MANAGEDESENT_ON_WSA

#if !MANAGEDESENT_ON_WSA // None of this can be called in windows store apps.
        /// <summary>
        /// Throw an exception if the given pointer is null (IntPtr.Zero).
        /// </summary>
        /// <param name="ptr">The pointer to check.</param>
        /// <param name="message">The message for the exception.</param>
        public static void ThrowExceptionOnNull(IntPtr ptr, string message)
        {
            if (IntPtr.Zero == ptr)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), message);
            }            
        }

        /// <summary>
        /// Throw an exception if the success code is not true.
        /// </summary>
        /// <param name="success">The success code.</param>
        /// <param name="message">The message for the exception.</param>
        public static void ThrowExceptionOnFailure(bool success, string message)
        {
            if (!success)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), message);
            }
        }

        [DllImport(WinCoreMemoryDll, SetLastError = true)]
        public static extern IntPtr VirtualAlloc(IntPtr plAddress, UIntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport(WinCoreMemoryDll, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool VirtualFree(IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);

        [DllImport(HeapObsolete)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern IntPtr LocalAlloc(int uFlags, UIntPtr sizetdwBytes);

        [DllImport(HeapObsolete)]
        public static extern IntPtr LocalFree(IntPtr hglobal);

#endif // !MANAGEDESENT_ON_WSA

        // Win32 APIs that are white-listed for Windows Store Apps can be safely referenced here.
        [DllImport(WinCoreProcessThreads)]
        public static extern int GetCurrentProcessId();
    }
}
