//-----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
// <summary>P/Invoke constants for Win32 functions.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Database.Isam.Win32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// P/Invoke methods for Win32 functions.
    /// </summary>
    internal static class NativeMethods
    {
#if MANAGEDESENT_ON_CORECLR || MANAGEDESENT_ON_WSA
        /// <summary>
        /// The name of the DLL that holds the Core process/threads API set.
        /// </summary>
        private const string WinCoreProcessThreads = "api-ms-win-core-processthreads-l1-1-1.dll";
#else
        /// <summary>
        /// The name of the DLL that holds the Core process/threads API set.
        /// </summary>
        private const string WinCoreProcessThreads = "kernel32.dll";
#endif // MANAGEDESENT_ON_CORECLR || MANAGEDESENT_ON_WSA

        // Win32 APIs that are white-listed for Windows Store Apps can be safely referenced here.
        [DllImport(WinCoreProcessThreads)]
        public static extern int GetCurrentProcessId();
    }
}
