//-----------------------------------------------------------------------
// <copyright file="Windows8Api.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    using System;
    using Microsoft.Isam.Esent.Interop.Vista;
    using Win81 = Microsoft.Isam.Esent.Interop.Windows81;

    /// <summary>
    /// Api calls introduced in Windows 8.
    /// </summary>
    public static class Windows8Api
    {
        #region Init / Term

        /// <summary>
        /// Prepares an instance for termination. Can also be used to resume a previous quiescing.
        /// </summary>
        /// <param name="instance">The (running) instance to use.</param>
        /// <param name="grbit">The options to stop or resume the instance.</param>
        public static void JetStopServiceInstance2(
            JET_INSTANCE instance,
            StopServiceGrbit grbit)
        {
            Api.Check(Api.Impl.JetStopServiceInstance2(instance, grbit));
        }

        #endregion

        #region Transactions
        /// <summary>
        /// Causes a session to enter a transaction or create a new save point in an existing
        /// transaction.
        /// </summary>
        /// <param name="sesid">The session to begin the transaction for.</param>
        /// <param name="userTransactionId">An optional identifier supplied by the user for identifying the transaction.</param>
        /// <param name="grbit">Transaction options.</param>
        /// <remarks>Introduced in Windows 8.</remarks>
        public static void JetBeginTransaction3(JET_SESID sesid, long userTransactionId, BeginTransactionGrbit grbit)
        {
            Api.Check(Api.Impl.JetBeginTransaction3(sesid, userTransactionId, grbit));
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
        public static void JetCommitTransaction2(
            JET_SESID sesid,
            CommitTransactionGrbit grbit,
            TimeSpan durableCommit,
            out JET_COMMIT_ID commitId)
        {
            Api.Check(Api.Impl.JetCommitTransaction2(sesid, grbit, durableCommit, out commitId));
        }

        #endregion

        /// <summary>
        /// Gets extended information about an error.
        /// </summary>
        /// <param name="error">The error code about which to retrieve information.</param>
        /// <param name="errinfo">Information about the specified error code.</param>
        public static void JetGetErrorInfo(
            JET_err error,
            out JET_ERRINFOBASIC errinfo)
        {
            Api.Check(Api.Impl.JetGetErrorInfo(error, out errinfo));
        }

        /// <summary>
        /// Resizes a currently open database. Windows 8: Only supports growing a database file.
        /// Windows 8.1: When <see cref="InstanceParameters.EnableShrinkDatabase"/> is set to
        /// <see cref="Win81.ShrinkDatabaseGrbit.On"/>, and if the
        /// file system supports Sparse Files, then space may be freed up in the middle of the
        /// file.
        /// </summary>
        /// <remarks>
        /// Many APIs return the logical size of the file, not how many bytes it takes up on disk.
        /// Win32's GetCompressedFileSize returns the correct on-disk size.
        /// <see cref="Api.JetGetDatabaseInfo(JET_SESID, JET_DBID, out int, JET_DbInfo)"/>
        /// returns the on-disk size when used with
        /// <see cref="Win81.Windows81DbInfo.FilesizeOnDisk"/>
        /// </remarks>
        /// <param name="sesid">The session to use.</param>
        /// <param name="dbid">The database to grow.</param>
        /// <param name="desiredPages">The desired size of the database, in pages.</param>
        /// <param name="actualPages">The size of the database, in pages, after the call. </param>
        /// <param name="grbit">Resize options.</param>
        public static void JetResizeDatabase(
            JET_SESID sesid, 
            JET_DBID dbid, 
            int desiredPages, 
            out int actualPages,
            ResizeDatabaseGrbit grbit)
        {
            Api.Check(Api.Impl.JetResizeDatabase(sesid, dbid, desiredPages, out actualPages, grbit));
        }

        #region DDL
        /// <summary>
        /// Creates indexes over data in an ESE database.
        /// </summary>
        /// <remarks>
        /// When creating multiple indexes (i.e. with numIndexCreates
        /// greater than 1) this method MUST be called
        /// outside of any transactions and with exclusive access to the
        /// table. The JET_TABLEID returned by "Api.JetCreateTable"
        /// will have exlusive access or the table can be opened for
        /// exclusive access by passing <see cref="OpenTableGrbit.DenyRead"/>
        /// to <see cref="Api.JetOpenTable"/>.
        /// </remarks>
        /// <param name="sesid">The session to use.</param>
        /// <param name="tableid">The table to create the index on.</param>
        /// <param name="indexcreates">Array of objects describing the indexes to be created.</param>
        /// <param name="numIndexCreates">Number of index description objects.</param>
        public static void JetCreateIndex4(
            JET_SESID sesid,
            JET_TABLEID tableid,
            JET_INDEXCREATE[] indexcreates,
            int numIndexCreates)
        {
            Api.Check(Api.Impl.JetCreateIndex4(sesid, tableid, indexcreates, numIndexCreates));            
        }

        /// <summary>
        /// Creates a temporary table with a single index. A temporary table
        /// stores and retrieves records just like an ordinary table created
        /// using JetCreateTableColumnIndex. However, temporary tables are
        /// much faster than ordinary tables due to their volatile nature.
        /// They can also be used to very quickly sort and perform duplicate
        /// removal on record sets when accessed in a purely sequential manner.
        /// Also see
        /// <seealso cref="Api.JetOpenTempTable"/>,
        /// "Api.JetOpenTempTable2",
        /// <seealso cref="Api.JetOpenTempTable3"/>.
        /// <seealso cref="VistaApi.JetOpenTemporaryTable"/>.
        /// </summary>
        /// <remarks>
        /// Use <see cref="VistaApi.JetOpenTemporaryTable"/>
        /// for earlier versions of Esent.
        /// </remarks>
        /// <param name="sesid">The session to use.</param>
        /// <param name="temporarytable">
        /// Description of the temporary table to create on input. After a
        /// successful call, the structure contains the handle to the temporary
        /// table and column identifications. Use <see cref="Api.JetCloseTable"/>
        /// to free the temporary table when finished.
        /// </param>
        public static void JetOpenTemporaryTable2(JET_SESID sesid, JET_OPENTEMPORARYTABLE temporarytable)
        {
            Api.Check(Api.Impl.JetOpenTemporaryTable2(sesid, temporarytable));
        }

        /// <summary>
        /// Creates a table, adds columns, and indices on that table.
        /// <seealso cref="Api.JetCreateTableColumnIndex3"/>
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="dbid">The database to which to add the new table.</param>
        /// <param name="tablecreate">Object describing the table to create.</param>
        /// <seealso cref="Api.JetCreateTableColumnIndex3"/>
        public static void JetCreateTableColumnIndex4(
            JET_SESID sesid,
            JET_DBID dbid,
            JET_TABLECREATE tablecreate)
        {
            Api.Check(Api.Impl.JetCreateTableColumnIndex4(sesid, dbid, tablecreate));
        }
        #endregion

        #region Session Parameters

        /// <summary>
        /// Gets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set, see
        /// <see cref="JET_sesparam"/> and <see cref="Windows10.Windows10Sesparam"/>.</param>
        /// <param name="value">A 32-bit integer to retrieve.</param>
        public static void JetGetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            out int value)
        {
            Api.Check(Api.Impl.JetGetSessionParameter(sesid, sesparamid, out value));
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
        public static void JetGetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            byte[] data,
            int length,
            out int actualDataSize)
        {
            Api.Check(Api.Impl.JetGetSessionParameter(sesid, sesparamid, data, length, out actualDataSize));
        }

        /// <summary>
        /// Sets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set.</param>
        /// <param name="value">A 32-bit integer to set.</param>
        public static void JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, int value)
        {
            Api.Check(Api.Impl.JetSetSessionParameter(sesid, sesparamid, value));
        }

        /// <summary>
        /// Sets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set.</param>
        /// <param name="data">Data to set in this session parameter.</param>
        /// <param name="dataSize">Size of the data provided.</param>
        public static void JetSetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            byte[] data,
            int dataSize)
        {
            Api.Check(Api.Impl.JetSetSessionParameter(sesid, sesparamid, data, dataSize));
        }

        #endregion

        #region Misc

        /// <summary>
        /// If the records with the specified key ranges are not in the buffer
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
        /// <returns><c>true</c> if some preread done, <c>false</c> otherwise.</returns>
        public static bool JetTryPrereadIndexRanges(
            JET_SESID sesid,
            JET_TABLEID tableid,
            JET_INDEX_RANGE[] indexRanges,
            int rangeIndex,
            int rangeCount,
            out int rangesPreread,
            JET_COLUMNID[] columnsPreread,
            PrereadIndexRangesGrbit grbit)
        {
            JET_err err = (JET_err)Api.Impl.JetPrereadIndexRanges(sesid, tableid, indexRanges, rangeIndex, rangeCount, out rangesPreread, columnsPreread, grbit);
            return err >= JET_err.Success;
        }

        /// <summary>
        /// If the records with the specified key ranges are not in the buffer
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
        public static void JetPrereadIndexRanges(
            JET_SESID sesid,
            JET_TABLEID tableid,
            JET_INDEX_RANGE[] indexRanges,
            int rangeIndex,
            int rangeCount,
            out int rangesPreread,
            JET_COLUMNID[] columnsPreread,
            PrereadIndexRangesGrbit grbit)
        {
            Api.Check(Api.Impl.JetPrereadIndexRanges(sesid, tableid, indexRanges, rangeIndex, rangeCount, out rangesPreread, columnsPreread, grbit));
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
        public static void PrereadKeyRanges(
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
            Api.Check(Api.Impl.JetPrereadKeyRanges(sesid, tableid, keysStart, keyStartLengths, keysEnd, keyEndLengths, rangeIndex, rangeCount, out rangesPreread, columnsPreread, grbit));
        }

        /// <summary>
        /// Set an array of simple filters for <see cref="Api.JetMove(JET_SESID,JET_TABLEID,int,MoveGrbit)"/>.
        /// </summary>
        /// <param name="sesid">The session to use for the call.</param>
        /// <param name="tableid">The cursor to position.</param>
        /// <param name="filters">Simple record filters.</param>
        /// <param name="grbit">Move options.</param>
        public static void JetSetCursorFilter(JET_SESID sesid, JET_TABLEID tableid, JET_INDEX_COLUMN[] filters, CursorFilterGrbit grbit)
        {
            Api.Check(Api.Impl.JetSetCursorFilter(sesid, tableid, filters, grbit));
        }

        #endregion
    }
}
