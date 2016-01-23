//-----------------------------------------------------------------------
// <copyright file="Windows10NativeMethods.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Implementation
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Isam.Esent.Interop.Windows10;

    /// <summary>
    /// Native interop for Windows10 functions in ese.dll.
    /// </summary>
    internal static partial class NativeMethods
    {
        #region Sessions

        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetSetSessionParameter(
            IntPtr sesid,
            uint sesparamid,
            ref NATIVE_OPERATIONCONTEXT data,
            int dataSize);

        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetGetSessionParameter(
            IntPtr sesid,
            uint sesparamid,
            out NATIVE_OPERATIONCONTEXT data,
            int dataSize,
            out int actualDataSize);

        [DllImport(EsentDll, ExactSpelling = true)]
        public static unsafe extern int JetGetThreadStats(JET_THREADSTATS2* pvResult, uint cbMax);

        #endregion
    }
}
