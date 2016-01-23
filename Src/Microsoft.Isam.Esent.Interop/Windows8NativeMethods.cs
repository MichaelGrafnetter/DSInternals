//-----------------------------------------------------------------------
// <copyright file="Windows8NativeMethods.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Implementation
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Isam.Esent.Interop.Vista;
    using Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// Native interop for Windows8 functions in ese.dll.
    /// </summary>
    internal static partial class NativeMethods
    {
        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetGetErrorInfoW(
            ref int error,
            [In, Out] ref Windows8.NATIVE_ERRINFOBASIC pvResult,
            uint cbMax,
            uint InfoLevel,
            uint grbit);

        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetResizeDatabase(
            IntPtr sesid, 
            uint dbid, 
            uint cpg, 
            out uint pcpgActual,
            uint grbit);

        #region DDL
        [DllImport(EsentDll, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int JetCreateIndex4W(
            IntPtr sesid, IntPtr tableid, [In] NATIVE_INDEXCREATE3[] pindexcreate, uint cIndexCreate);

        [DllImport(EsentDll, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int JetCreateTableColumnIndex4W(IntPtr sesid, uint dbid, ref NATIVE_TABLECREATE4 tablecreate3);
        #endregion

        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetOpenTemporaryTable2(IntPtr sesid, [In] [Out] ref NATIVE_OPENTEMPORARYTABLE2 popentemporarytable);

        #region Session Parameters
        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetGetSessionParameter(
            IntPtr sesid,
            uint sesparamid,
            out int data,
            int dataSize,
            out int actualDataSize);

        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetGetSessionParameter(
            IntPtr sesid,
            uint sesparamid,
            [Out] byte[] data,
            int dataSize,
            out int actualDataSize);

        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetSetSessionParameter(
            IntPtr sesid,
            uint sesparamid,
            byte[] data,
            int dataSize);

        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetSetSessionParameter(
            IntPtr sesid,
            uint sesparamid,
            ref int data,
            int dataSize);

        #endregion

        #region Misc
        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetCommitTransaction2(
            IntPtr sesid,
            uint grbit,
            uint cmsecDurableCommit,
            ref NATIVE_COMMIT_ID pCommitId);

        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetPrereadIndexRanges(IntPtr sesid, IntPtr tableid, [In] NATIVE_INDEX_RANGE[] pIndexRanges, uint cIndexRanges, out int pcRangesPreread, uint[] rgcolumnidPreread, uint ccolumnidPreread, uint grbit);

        [DllImport(EsentDll, ExactSpelling = true)]
        public static extern int JetSetCursorFilter(IntPtr sesid, IntPtr tableid, [In] NATIVE_INDEX_COLUMN[] pFilters, uint cFilters, uint grbit);
        #endregion
    }
}
