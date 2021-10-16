//-----------------------------------------------------------------------
// <copyright file="Windows8JetApi.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using Microsoft.Isam.Esent.Interop.Vista;
    using Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// Windows8 calls to the ESENT interop layer. These calls take the managed types (e.g. JET_SESID) and
    /// return errors.
    /// </summary>
    internal sealed partial class JetApi : IJetApi
    {
        #region Transactions
        /// <summary>
        /// Causes a session to enter a transaction or create a new save point in an existing
        /// transaction.
        /// </summary>
        /// <param name="sesid">The session to begin the transaction for.</param>
        /// <param name="userTransactionId">An optional identifier supplied by the user for identifying the transaction.</param>
        /// <param name="grbit">Transaction options.</param>
        /// <returns>An error if the call fails.</returns>
        public int JetBeginTransaction3(JET_SESID sesid, long userTransactionId, BeginTransactionGrbit grbit)
        {
            TraceFunctionCall();
            return Err(NativeMethods.JetBeginTransaction3(sesid.Value, userTransactionId, unchecked((uint)grbit)));
        }

        /// <summary>
        /// Commits the changes made to the state of the database during the current save point
        /// and migrates them to the previous save point. If the outermost save point is committed
        /// then the changes made during that save point will be committed to the state of the
        /// database and the session will exit the transaction.
        /// </summary>
        /// <param name="sesid">The session to commit the transaction for.</param>
        /// <param name="grbit">Commit options.</param>
        /// <param name="durableCommit">Duration to commit lazy transaction.</param>
        /// <param name="commitId">Commit-id associated with this commit record.</param>
        /// <returns>An error if the call fails.</returns>
        public int JetCommitTransaction2(JET_SESID sesid, CommitTransactionGrbit grbit, TimeSpan durableCommit, out JET_COMMIT_ID commitId)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetCommitTransaction2");
            int err;
            uint cmsecDurableCommit = (uint)durableCommit.TotalMilliseconds;

            NATIVE_COMMIT_ID nativeCommitId = new NATIVE_COMMIT_ID();
            unsafe
            {
                err = Err(NativeMethods.JetCommitTransaction2(sesid.Value, unchecked((uint)grbit), cmsecDurableCommit, ref nativeCommitId));
            }

            commitId = new JET_COMMIT_ID(nativeCommitId);

            return err;
        }

        #endregion

        /// <summary>
        /// Gets extended information about an error.
        /// </summary>
        /// <param name="error">The error code about which to retrieve information.</param>
        /// <param name="errinfo">Information about the specified error code.</param>
        /// <returns>An error code.</returns>
        public int JetGetErrorInfo(
            JET_err error,
            out JET_ERRINFOBASIC errinfo)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetGetErrorInfo");

            NATIVE_ERRINFOBASIC nativeErrinfobasic = new NATIVE_ERRINFOBASIC();
            errinfo = new JET_ERRINFOBASIC();

            nativeErrinfobasic.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_ERRINFOBASIC)));
            int nativeErr = (int)error;

            int err = Implementation.NativeMethods.JetGetErrorInfoW(
                ref nativeErr,
                ref nativeErrinfobasic,
                nativeErrinfobasic.cbStruct,
                (uint)JET_ErrorInfo.SpecificErr,
                (uint)ErrorInfoGrbit.None);
            errinfo.SetFromNative(ref nativeErrinfobasic);

            return err;
        }

        /// <summary>
        /// Resizes a currently open database.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="dbid">The database to grow.</param>
        /// <param name="desiredPages">The desired size of the database, in pages.</param>
        /// <param name="actualPages">The size of the database, in pages, after the call. </param>
        /// <param name="grbit">Resize options.</param>
        /// <returns>An error code.</returns>
        public int JetResizeDatabase(
            JET_SESID sesid,
            JET_DBID dbid,
            int desiredPages,
            out int actualPages,
            ResizeDatabaseGrbit grbit)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetResizeDatabase");
            CheckNotNegative(desiredPages, "desiredPages");

            uint actualPagesNative = 0;
            int err = Err(NativeMethods.JetResizeDatabase(
                        sesid.Value, dbid.Value, checked((uint)desiredPages), out actualPagesNative, (uint)grbit));
            actualPages = checked((int)actualPagesNative);
            return err;
        }

        /// <summary>
        /// Creates indexes over data in an ESE database.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="tableid">The table to create the index on.</param>
        /// <param name="indexcreates">Array of objects describing the indexes to be created.</param>
        /// <param name="numIndexCreates">Number of index description objects.</param>
        /// <remarks>
        /// <para>
        /// <see cref="Api.JetCreateIndex2"/> and <see cref="Microsoft.Isam.Esent.Interop.Windows8.Windows8Api.JetCreateIndex4"/>
        /// are very similar, and appear to take the same arguments. The difference is in the
        /// implementation. JetCreateIndex2 uses LCIDs for Unicode indices (e.g. 1033), while
        /// JetCreateIndex4 uses Locale Names (e.g. "en-US" or "de-DE". LCIDs are older, and not as well
        /// supported in newer version of windows.
        /// </para>
        /// </remarks>
        /// <returns>An error code.</returns>
        /// <seealso cref="Api.JetCreateIndex"/>
        /// <seealso cref="Api.JetCreateIndex2"/>
        public int JetCreateIndex4(
            JET_SESID sesid,
            JET_TABLEID tableid,
            JET_INDEXCREATE[] indexcreates,
            int numIndexCreates)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetCreateIndex4");
            CheckNotNull(indexcreates, "indexcreates");
            CheckNotNegative(numIndexCreates, "numIndexCreates");
            if (numIndexCreates > indexcreates.Length)
            {
                throw new ArgumentOutOfRangeException(
                    "numIndexCreates", numIndexCreates, "numIndexCreates is larger than the number of indexes passed in");
            }

            return CreateIndexes3(sesid, tableid, indexcreates, numIndexCreates);
        }

        /// <summary>
        /// Creates a temporary table with a single index. A temporary table
        /// stores and retrieves records just like an ordinary table created
        /// using JetCreateTableColumnIndex. However, temporary tables are
        /// much faster than ordinary tables due to their volatile nature.
        /// They can also be used to very quickly sort and perform duplicate
        /// removal on record sets when accessed in a purely sequential manner.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="temporarytable">
        /// Description of the temporary table to create on input. After a
        /// successful call, the structure contains the handle to the temporary
        /// table and column identifications.
        /// </param>
        /// <returns>An error code.</returns>
        public int JetOpenTemporaryTable2(JET_SESID sesid, JET_OPENTEMPORARYTABLE temporarytable)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetOpenTemporaryTable2");
            CheckNotNull(temporarytable, "temporarytable");

            NATIVE_OPENTEMPORARYTABLE2 nativetemporarytable = temporarytable.GetNativeOpenTemporaryTable2();
            var nativecolumnids = new uint[nativetemporarytable.ccolumn];
            NATIVE_COLUMNDEF[] nativecolumndefs = GetNativecolumndefs(temporarytable.prgcolumndef, temporarytable.ccolumn);
            unsafe
            {
                using (var gchandlecollection = new GCHandleCollection())
                {
                    // Pin memory
                    nativetemporarytable.prgcolumndef = (NATIVE_COLUMNDEF*)gchandlecollection.Add(nativecolumndefs);
                    nativetemporarytable.rgcolumnid = (uint*)gchandlecollection.Add(nativecolumnids);
                    if (null != temporarytable.pidxunicode)
                    {
                        NATIVE_UNICODEINDEX2 unicode = temporarytable.pidxunicode.GetNativeUnicodeIndex2();
                        unicode.szLocaleName = gchandlecollection.Add(Util.ConvertToNullTerminatedUnicodeByteArray(temporarytable.pidxunicode.GetEffectiveLocaleName()));
                        nativetemporarytable.pidxunicode = (NATIVE_UNICODEINDEX2*)gchandlecollection.Add(unicode);
                    }

                    // Call the interop method
                    int err = Err(NativeMethods.JetOpenTemporaryTable2(sesid.Value, ref nativetemporarytable));

                    // Convert the return values
                    SetColumnids(temporarytable.prgcolumndef, temporarytable.prgcolumnid, nativecolumnids, temporarytable.ccolumn);
                    temporarytable.tableid = new JET_TABLEID { Value = nativetemporarytable.tableid };

                    return err;
                }
            }
        }

        /// <summary>
        /// Creates a table, adds columns, and indices on that table.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="dbid">The database to which to add the new table.</param>
        /// <param name="tablecreate">Object describing the table to create.</param>
        /// <returns>An error if the call fails.</returns>
        public int JetCreateTableColumnIndex4(
            JET_SESID sesid,
            JET_DBID dbid,
            JET_TABLECREATE tablecreate)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetCreateTableColumnIndex4");
            CheckNotNull(tablecreate, "tablecreate");

            return CreateTableColumnIndex4(sesid, dbid, tablecreate);
        }

        #region Session Parameters

        /// <summary>
        /// Gets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set, see
        /// <see cref="JET_sesparam"/> and <see cref="Windows10.Windows10Sesparam"/>.</param>
        /// <param name="value">A 32-bit integer to retrieve.</param>
        /// <returns>An error if the call fails.</returns>
        public int JetGetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            out int value)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetGetSessionParameter");
            int err;

            int actualDataSize;
            err = NativeMethods.JetGetSessionParameter(
                sesid.Value,
                (uint)sesparamid,
                out value,
                sizeof(int),
                out actualDataSize);

            if (err >= (int)JET_err.Success)
            {
                if (actualDataSize != sizeof(int))
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "Bad return value. Unexpected data size returned. Expected {0}, but received {1}.",
                            sizeof(int),
                            actualDataSize),
                        "sesparamid");
                }
            }

            return Err(err);
        }

        /// <summary>
        /// Gets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set, see
        /// <see cref="JET_sesparam"/> and <see cref="Windows10.Windows10Sesparam"/>.</param>
        /// <param name="data">A byte array to retrieve.</param>
        /// <param name="length">AThe length of the data array.</param>
        /// <param name="actualDataSize">The actual size of the data field.</param>
        /// <returns>An error if the call fails.</returns>
        public int JetGetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            byte[] data,
            int length,
            out int actualDataSize)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetGetSessionParameter");
            CheckDataSize(data, length, "length");

            int err;

            err = NativeMethods.JetGetSessionParameter(
                sesid.Value,
                (uint)sesparamid,
                data,
                length,
                out actualDataSize);

            return Err(err);
        }

        /// <summary>
        /// Sets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set.</param>
        /// <param name="valueToSet">A 32-bit integer to set.</param>
        /// <returns>An error if the call fails.</returns>
        public int JetSetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            int valueToSet)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetSetSessionParameter");
            int err;

            err = NativeMethods.JetSetSessionParameter(sesid.Value, (uint)sesparamid, ref valueToSet, sizeof(int));

            return Err(err);
        }

        /// <summary>
        /// Sets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set.</param>
        /// <param name="data">Data to set in this session parameter.</param>
        /// <param name="dataSize">Size of the data provided.</param>
        /// <returns>An error if the call fails.</returns>
        public int JetSetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            byte[] data,
            int dataSize)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetSetSessionParameter");
            CheckNotNegative(dataSize, "dataSize");
            CheckDataSize(data, dataSize, "dataSize");

            int err;

            err = NativeMethods.JetSetSessionParameter(sesid.Value, (uint)sesparamid, data, dataSize);

            return Err(err);
        }

        #endregion

        #region JetGetIndexInfo overloads
        /// <summary>
        /// Retrieves information about indexes on a table.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="dbid">The database to use.</param>
        /// <param name="tablename">The name of the table to retrieve index information about.</param>
        /// <param name="indexname">The name of the index to retrieve information about.</param>
        /// <param name="result">Filled in with information about indexes on the table.</param>
        /// <param name="infoLevel">The type of information to retrieve.</param>
        /// <returns>An error if the call fails.</returns>
        public int JetGetIndexInfo(
            JET_SESID sesid,
            JET_DBID dbid,
            string tablename,
            string indexname,
            out JET_INDEXCREATE result,
            JET_IdxInfo infoLevel)
        {
            TraceFunctionCall();
            CheckNotNull(tablename, "tablename");
            int err;

            switch (infoLevel)
            {
                case Microsoft.Isam.Esent.Interop.Windows7.Windows7IdxInfo.CreateIndex:
                case Microsoft.Isam.Esent.Interop.Windows7.Windows7IdxInfo.CreateIndex2:
                case Microsoft.Isam.Esent.Interop.Windows8.Windows8IdxInfo.InfoCreateIndex3:
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid value JET_IdxInfo for this JET_INDEXCREATE overload."));
            }

            if (this.Capabilities.SupportsWindows8Features)
            {
                {
                    int bufferSize = 10 * Marshal.SizeOf(typeof(NATIVE_INDEXCREATE3));
                    IntPtr unmanagedBuffer = Marshal.AllocHGlobal(bufferSize);
                    try
                    {
                        // var nativeIndexcreate = new NATIVE_INDEXCREATE3();
                        // nativeIndexcreate.cbStruct = checked((uint)bufferSize);
                        infoLevel = Windows8IdxInfo.InfoCreateIndex3;

                        err = Err(NativeMethods.JetGetIndexInfoW(
                            sesid.Value,
                            dbid.Value,
                            tablename,
                            indexname,
                            unmanagedBuffer,
                            (uint)bufferSize,
                            (uint)infoLevel));

                        NATIVE_INDEXCREATE3 nativeIndexcreate = (NATIVE_INDEXCREATE3)Marshal.PtrToStructure(unmanagedBuffer, typeof(NATIVE_INDEXCREATE3));

                        result = new JET_INDEXCREATE();
                        result.SetAllFromNativeIndexCreate(ref nativeIndexcreate);
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(unmanagedBuffer);
                    }
                }
            }
            else
            {
                result = null;
                err = Err((int)JET_err.FeatureNotAvailable);
            }

            return err;
        }

        /// <summary>
        /// Retrieves information about indexes on a table.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="tableid">The table to retrieve index information about.</param>
        /// <param name="indexname">The name of the index to retrieve information about.</param>
        /// <param name="result">Filled in with information about indexes on the table.</param>
        /// <param name="infoLevel">The type of information to retrieve.</param>
        /// <returns>An error if the call fails.</returns>
        public int JetGetTableIndexInfo(
            JET_SESID sesid,
            JET_TABLEID tableid,
            string indexname,
            out JET_INDEXCREATE result,
            JET_IdxInfo infoLevel)
        {
            TraceFunctionCall();
            int err;

            switch (infoLevel)
            {
                case Microsoft.Isam.Esent.Interop.Windows7.Windows7IdxInfo.CreateIndex:
                case Microsoft.Isam.Esent.Interop.Windows7.Windows7IdxInfo.CreateIndex2:
                case Microsoft.Isam.Esent.Interop.Windows8.Windows8IdxInfo.InfoCreateIndex3:
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid value JET_IdxInfo for this JET_INDEXCREATE overload."));
            }

            if (this.Capabilities.SupportsWindows8Features)
            {
                {
                    int bufferSize = 10 * Marshal.SizeOf(typeof(NATIVE_INDEXCREATE3));
                    IntPtr unmanagedBuffer = Marshal.AllocHGlobal(bufferSize);
                    try
                    {
                        // var nativeIndexcreate = new NATIVE_INDEXCREATE3();
                        // nativeIndexcreate.cbStruct = checked((uint)bufferSize);
                        infoLevel = Windows8IdxInfo.InfoCreateIndex3;

                        err = Err(NativeMethods.JetGetTableIndexInfoW(
                            sesid.Value,
                            tableid.Value,
                            indexname,
                            unmanagedBuffer,
                            (uint)bufferSize,
                            (uint)infoLevel));

                        NATIVE_INDEXCREATE3 nativeIndexcreate = (NATIVE_INDEXCREATE3)Marshal.PtrToStructure(unmanagedBuffer, typeof(NATIVE_INDEXCREATE3));

                        result = new JET_INDEXCREATE();
                        result.SetAllFromNativeIndexCreate(ref nativeIndexcreate);
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(unmanagedBuffer);
                    }
                }
            }
            else
            {
                result = null;
                err = Err((int)JET_err.FeatureNotAvailable);
            }

            return err;
        }
        #endregion

        #region prereading
        /// <summary>
        /// If the records with the specified key rangess are not in the buffer
        /// cache, then start asynchronous reads to bring the records into the
        /// database buffer cache.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="tableid">The table to issue the prereads against.</param>
        /// <param name="indexRanges">The key ranges to preread.</param>
        /// <param name="rangeIndex">The index of the first key range in the array to read.</param>
        /// <param name="rangeCount">The maximum number of key ranges to preread.</param>
        /// <param name="rangesPreread">Returns the number of keys actually preread.</param>
        /// <param name="columnsPreread">List of column ids for long value columns to preread.</param>
        /// <param name="grbit">Preread options. Used to specify the direction of the preread.</param>
        /// <returns>
        /// An error if the call fails.
        /// </returns>
        public int JetPrereadIndexRanges(
            JET_SESID sesid,
            JET_TABLEID tableid,
            JET_INDEX_RANGE[] indexRanges,
            int rangeIndex,
            int rangeCount,
            out int rangesPreread,
            JET_COLUMNID[] columnsPreread,
            PrereadIndexRangesGrbit grbit)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetPrereadIndexRanges");
            CheckNotNull(indexRanges, "indexRanges");
            CheckDataSize(indexRanges, rangeIndex, "rangeIndex", rangeCount, "rangeCount");

            var handles = new GCHandleCollection();
            try
            {
                NATIVE_INDEX_RANGE[] nativeRanges = new NATIVE_INDEX_RANGE[rangeCount];
                for (int i = 0; i < rangeCount; i++)
                {
                    nativeRanges[i] = indexRanges[i + rangeIndex].GetNativeIndexRange(ref handles);
                }

                if (columnsPreread != null)
                {
                    var nativecolumnids = new uint[columnsPreread.Length];
                    for (int i = 0; i < columnsPreread.Length; i++)
                    {
                         nativecolumnids[i] = (uint)columnsPreread[i].Value;
                    }

                    return Err(NativeMethods.JetPrereadIndexRanges(sesid.Value, tableid.Value, nativeRanges, (uint)rangeCount, out rangesPreread, nativecolumnids, (uint)columnsPreread.Length, checked((uint)grbit)));
                }
                else
                {
                    return Err(NativeMethods.JetPrereadIndexRanges(sesid.Value, tableid.Value, nativeRanges, (uint)rangeCount, out rangesPreread, null, (uint)0, checked((uint)grbit)));
                }
            }
            finally
            {
                handles.Dispose();
            }
        }

        /// <summary>
        /// If the records with the specified key ranges are not in the
        /// buffer cache then start asynchronous reads to bring the records
        /// into the database buffer cache.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="tableid">The table to issue the prereads against.</param>
        /// <param name="keysStart">The start of key ranges to preread.</param>
        /// <param name="keyStartLengths">The lengths of the start keys to preread.</param>
        /// <param name="keysEnd">The end of key rangess to preread.</param>
        /// <param name="keyEndLengths">The lengths of the end keys to preread.</param>
        /// <param name="rangeIndex">The index of the first key range in the array to read.</param>
        /// <param name="rangeCount">The maximum number of key ranges to preread.</param>
        /// <param name="rangesPreread">Returns the number of keys actually preread.</param>
        /// <param name="columnsPreread">List of column ids for long value columns to preread.</param>
        /// <param name="grbit">Preread options. Used to specify the direction of the preread.</param>
        /// <returns>An error or warning.</returns>
        public int JetPrereadKeyRanges(
            JET_SESID sesid,
            JET_TABLEID tableid,
            byte[][] keysStart,
            int[] keyStartLengths,
            byte[][] keysEnd,
            int[] keyEndLengths,
            int rangeIndex,
            int rangeCount,
            out int rangesPreread,
            JET_COLUMNID[] columnsPreread,
            PrereadIndexRangesGrbit grbit)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetPrereadKeyRanges");
            CheckDataSize(keysStart, rangeIndex, "rangeIndex", rangeCount, "rangeCount");
            CheckDataSize(keyStartLengths, rangeIndex, "rangeIndex", rangeCount, "rangeCount");
            CheckNotNull(keysStart, "keysStart");
            if (keysEnd != null)
            {
                CheckNotNull(keyEndLengths, "keyEndLengths");
                CheckDataSize(keysEnd, rangeIndex, "rangeIndex", rangeCount, "rangeCount");
            }

            if (keyEndLengths != null)
            {
                CheckNotNull(keysEnd, "keysEnd");
                CheckDataSize(keyEndLengths, rangeIndex, "rangeIndex", rangeCount, "rangeCount");
            }

            grbit = grbit | PrereadIndexRangesGrbit.NormalizedKey;

            using (var handles = new GCHandleCollection())
            {
                NATIVE_INDEX_COLUMN[] startColumn;
                NATIVE_INDEX_COLUMN[] endColumn;
                NATIVE_INDEX_RANGE[] ranges = new NATIVE_INDEX_RANGE[rangeCount];
                for (int i = 0; i < rangeCount; i++)
                {
                    startColumn = new NATIVE_INDEX_COLUMN[1];
                    startColumn[0].pvData = handles.Add(keysStart[i + rangeIndex]);
                    startColumn[0].cbData = (uint)keyStartLengths[i + rangeIndex];
                    ranges[i].rgStartColumns = handles.Add(startColumn);
                    ranges[i].cStartColumns = 1;
                    if (keysEnd != null)
                    {
                        endColumn = new NATIVE_INDEX_COLUMN[1];
                        endColumn[0].pvData = handles.Add(keysEnd[i + rangeIndex]);
                        endColumn[0].cbData = (uint)keyEndLengths[i + rangeIndex];
                        ranges[i].rgEndColumns = handles.Add(endColumn);
                        ranges[i].cEndColumns = 1;
                    }
                }

                if (columnsPreread != null)
                {
                    var nativecolumnids = new uint[columnsPreread.Length];
                    for (int i = 0; i < columnsPreread.Length; i++)
                    {
                        nativecolumnids[i] = (uint)columnsPreread[i].Value;
                    }

                    return Err(NativeMethods.JetPrereadIndexRanges(sesid.Value, tableid.Value, ranges, (uint)rangeCount, out rangesPreread, nativecolumnids, (uint)columnsPreread.Length, checked((uint)grbit)));
                }
                else
                {
                    return Err(NativeMethods.JetPrereadIndexRanges(sesid.Value, tableid.Value, ranges, (uint)rangeCount, out rangesPreread, null, (uint)0, checked((uint)grbit)));
                }
            }
        }

        /// <summary>
        /// Set an array of simple filters for <see cref="Api.JetMove(JET_SESID,JET_TABLEID,int,MoveGrbit)"/>
        /// </summary>
        /// <param name="sesid">The session to use for the call.</param>
        /// <param name="tableid">The cursor to position.</param>
        /// <param name="filters">Simple record filters.</param>
        /// <param name="grbit">Move options.</param>
        /// <returns>An error if the call fails.</returns>
        public int JetSetCursorFilter(JET_SESID sesid, JET_TABLEID tableid, JET_INDEX_COLUMN[] filters, CursorFilterGrbit grbit)
        {
            TraceFunctionCall();
            this.CheckSupportsWindows8Features("JetSetCursorFilter");

            if (filters == null || filters.Length == 0)
            {
                return Err(NativeMethods.JetSetCursorFilter(sesid.Value, tableid.Value, null, 0, checked((uint)grbit)));
            }

            var handles = new GCHandleCollection();
            try
            {
                NATIVE_INDEX_COLUMN[] nativeFilters = new NATIVE_INDEX_COLUMN[filters.Length];

                for (int i = 0; i < filters.Length; i++)
                {
                    nativeFilters[i] = filters[i].GetNativeIndexColumn(ref handles);
                }

                return Err(NativeMethods.JetSetCursorFilter(sesid.Value, tableid.Value, nativeFilters, (uint)filters.Length, checked((uint)grbit)));
            }
            finally
            {
                handles.Dispose();
            }
        }
        #endregion

        #region Private utility functions
        /// <summary>
        /// Make native indexcreate structures from the managed ones.
        /// </summary>
        /// <param name="managedIndexCreates">Index create structures to convert.</param>
        /// <param name="handles">The handle collection used to pin the data.</param>
        /// <returns>Pinned native versions of the index creates.</returns>
        private static unsafe NATIVE_INDEXCREATE3[] GetNativeIndexCreate3s(
            IList<JET_INDEXCREATE> managedIndexCreates,
            ref GCHandleCollection handles)
        {
            NATIVE_INDEXCREATE3[] nativeIndices = null;

            if (managedIndexCreates != null && managedIndexCreates.Count > 0)
            {
                nativeIndices = new NATIVE_INDEXCREATE3[managedIndexCreates.Count];

                for (int i = 0; i < managedIndexCreates.Count; ++i)
                {
                    nativeIndices[i] = managedIndexCreates[i].GetNativeIndexcreate3();

                    if (null != managedIndexCreates[i].pidxUnicode)
                    {
                        NATIVE_UNICODEINDEX2 unicode = managedIndexCreates[i].pidxUnicode.GetNativeUnicodeIndex2();
                        unicode.szLocaleName = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(managedIndexCreates[i].pidxUnicode.GetEffectiveLocaleName()));
                        nativeIndices[i].pidxUnicode = (NATIVE_UNICODEINDEX2*)handles.Add(unicode);
                        nativeIndices[i].grbit |= (uint)VistaGrbits.IndexUnicode;
                    }

                    nativeIndices[i].szKey = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(managedIndexCreates[i].szKey));
                    nativeIndices[i].szIndexName = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(managedIndexCreates[i].szIndexName));
                    nativeIndices[i].rgconditionalcolumn = GetNativeConditionalColumns(managedIndexCreates[i].rgconditionalcolumn, true, ref handles);

                    // Convert pSpaceHints.
                    if (managedIndexCreates[i].pSpaceHints != null)
                    {
                        NATIVE_SPACEHINTS nativeSpaceHints = managedIndexCreates[i].pSpaceHints.GetNativeSpaceHints();

                        nativeIndices[i].pSpaceHints = handles.Add(nativeSpaceHints);
                    }
                }
            }

            return nativeIndices;
        }

        /// <summary>
        /// Creates indexes over data in an ESE database.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="tableid">The table to create the index on.</param>
        /// <param name="indexcreates">Array of objects describing the indexes to be created.</param>
        /// <param name="numIndexCreates">Number of index description objects.</param>
        /// <returns>An error code.</returns>
        private static int CreateIndexes3(JET_SESID sesid, JET_TABLEID tableid, IList<JET_INDEXCREATE> indexcreates, int numIndexCreates)
        {
            // pin the memory
            var handles = new GCHandleCollection();
            try
            {
                NATIVE_INDEXCREATE3[] nativeIndexcreates = GetNativeIndexCreate3s(indexcreates, ref handles);
                return Err(NativeMethods.JetCreateIndex4W(sesid.Value, tableid.Value, nativeIndexcreates, checked((uint)numIndexCreates)));
            }
            finally
            {
                handles.Dispose();
            }
        }

        /// <summary>
        /// Creates a table, adds columns, and indices on that table.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="dbid">The database to which to add the new table.</param>
        /// <param name="tablecreate">Object describing the table to create.</param>
        /// <returns>An error if the call fails.</returns>
        private static int CreateTableColumnIndex4(
            JET_SESID sesid,
            JET_DBID dbid,
            JET_TABLECREATE tablecreate)
        {
            NATIVE_TABLECREATE4 nativeTableCreate = tablecreate.GetNativeTableCreate4();

            unsafe
            {
                var handles = new GCHandleCollection();
                try
                {
                    // Convert/pin the column definitions.
                    nativeTableCreate.rgcolumncreate = (NATIVE_COLUMNCREATE*)GetNativeColumnCreates(tablecreate.rgcolumncreate, true, ref handles);

                    // Convert/pin the index definitions.
                    NATIVE_INDEXCREATE3[] nativeIndexCreates = GetNativeIndexCreate3s(tablecreate.rgindexcreate, ref handles);
                    nativeTableCreate.rgindexcreate = handles.Add(nativeIndexCreates);

                    // Convert/pin the space hints.
                    if (tablecreate.pSeqSpacehints != null)
                    {
                        NATIVE_SPACEHINTS nativeSpaceHints = tablecreate.pSeqSpacehints.GetNativeSpaceHints();
                        nativeTableCreate.pSeqSpacehints = (NATIVE_SPACEHINTS*)handles.Add(nativeSpaceHints);
                    }

                    if (tablecreate.pLVSpacehints != null)
                    {
                        NATIVE_SPACEHINTS nativeSpaceHints = tablecreate.pLVSpacehints.GetNativeSpaceHints();
                        nativeTableCreate.pLVSpacehints = (NATIVE_SPACEHINTS*)handles.Add(nativeSpaceHints);
                    }

                    int err = NativeMethods.JetCreateTableColumnIndex4W(sesid.Value, dbid.Value, ref nativeTableCreate);

                    // Modified fields.
                    tablecreate.tableid = new JET_TABLEID
                    {
                        Value = nativeTableCreate.tableid
                    };

                    tablecreate.cCreated = checked((int)nativeTableCreate.cCreated);

                    if (tablecreate.rgcolumncreate != null)
                    {
                        for (int i = 0; i < tablecreate.rgcolumncreate.Length; ++i)
                        {
                            tablecreate.rgcolumncreate[i].SetFromNativeColumnCreate(ref nativeTableCreate.rgcolumncreate[i]);
                        }
                    }

                    if (tablecreate.rgindexcreate != null)
                    {
                        for (int i = 0; i < tablecreate.rgindexcreate.Length; ++i)
                        {
                            tablecreate.rgindexcreate[i].SetFromNativeIndexCreate(ref nativeIndexCreates[i]);
                        }
                    }

                    return Err(err);
                }
                finally
                {
                    handles.Dispose();
                }
            }
        }

        // Do not add new public functions here, go above private functions above ...
        #endregion
    }
}
