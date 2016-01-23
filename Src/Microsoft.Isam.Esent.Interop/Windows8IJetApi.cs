//-----------------------------------------------------------------------
// <copyright file="Windows8IJetApi.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Implementation
{
    using System;
    using Microsoft.Isam.Esent.Interop.Vista;
    using Microsoft.Isam.Esent.Interop.Windows8;
 
    /// <summary>
    /// This interface describes all the Windows8 methods which have a
    /// P/Invoke implementation. Concrete instances of this interface provide
    /// methods that call ESENT.
    /// </summary>
    internal partial interface IJetApi
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
        int JetBeginTransaction3(JET_SESID sesid, long userTransactionId, BeginTransactionGrbit grbit);

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
        int JetCommitTransaction2(
            JET_SESID sesid,
            CommitTransactionGrbit grbit,
            TimeSpan durableCommit,
            out JET_COMMIT_ID commitId);

        #endregion

        /// <summary>
        /// Gets extended information about an error.
        /// </summary>
        /// <param name="error">The error code about which to retrieve information.</param>
        /// <param name="errinfo">Information about the specified error code.</param>
        /// <returns>An error code.</returns>
        int JetGetErrorInfo(
            JET_err error,
            out JET_ERRINFOBASIC errinfo);

        /// <summary>
        /// Resizes a currently open database.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="dbid">The database to grow.</param>
        /// <param name="desiredPages">The desired size of the database, in pages.</param>
        /// <param name="actualPages">The size of the database, in pages, after the call. </param>
        /// <param name="grbit">Resize options.</param>
        /// <returns>An error code.</returns>
        int JetResizeDatabase(
            JET_SESID sesid, 
            JET_DBID dbid, 
            int desiredPages, 
            out int actualPages,
            ResizeDatabaseGrbit grbit);

        #region DDL
        /// <summary>
        /// Creates indexes over data in an ESE database.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="tableid">The table to create the index on.</param>
        /// <param name="indexcreates">Array of objects describing the indexes to be created.</param>
        /// <param name="numIndexCreates">Number of index description objects.</param>
        /// <returns>An error code.</returns>
        int JetCreateIndex4(
            JET_SESID sesid,
            JET_TABLEID tableid,
            JET_INDEXCREATE[] indexcreates,
            int numIndexCreates);

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
        int JetOpenTemporaryTable2(JET_SESID sesid, JET_OPENTEMPORARYTABLE temporarytable);

        /// <summary>
        /// Creates a table, adds columns, and indices on that table.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="dbid">The database to which to add the new table.</param>
        /// <param name="tablecreate">Object describing the table to create.</param>
        /// <returns>An error if the call fails.</returns>
        int JetCreateTableColumnIndex4(
            JET_SESID sesid,
            JET_DBID dbid,
            JET_TABLECREATE tablecreate);
        #endregion

        #region Session Parameters
        /// <summary>
        /// Gets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set, see
        /// <see cref="JET_sesparam"/> and <see cref="Windows10.Windows10Sesparam"/>.</param>
        /// <param name="value">A 32-bit integer to retrieve.</param>
        /// <returns>An error if the call fails.</returns>
        int JetGetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, out int value);

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
        int JetGetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            byte[] data,
            int length,
            out int actualDataSize);

        /// <summary>
        /// Sets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set.</param>
        /// <param name="value">A 32-bit integer to set.</param>
        /// <returns>An error if the call fails.</returns>
        int JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, int value);

        /// <summary>
        /// Sets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set.</param>
        /// <param name="data">Data to set in this session parameter.</param>
        /// <param name="dataSize">Size of the data provided.</param>
        /// <returns>An error if the call fails.</returns>
        int JetSetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            byte[] data,
            int dataSize);

        #endregion

        #region Misc

        /// <summary>
        /// If the records with the specified key ranges are not in the buffer
        /// cache, then start asynchronous reads to bring the records into the
        /// database buffer cache.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="tableid">The table to issue the prereads against.</param>
        /// <param name="indexRanges">The key rangess to preread.</param>
        /// <param name="rangeIndex">The index of the first key range in the array to read.</param>
        /// <param name="rangeCount">The maximum number of key ranges to preread.</param>
        /// <param name="rangesPreread">Returns the number of keys actually preread.</param>
        /// <param name="columnsPreread">List of column ids for long value columns to preread.</param>
        /// <param name="grbit">Preread options. Used to specify the direction of the preread.</param>
        /// <returns>
        /// An error if the call fails.
        /// </returns>
        int JetPrereadIndexRanges(
            JET_SESID sesid,
            JET_TABLEID tableid,
            JET_INDEX_RANGE[] indexRanges,
            int rangeIndex,
            int rangeCount,
            out int rangesPreread,
            JET_COLUMNID[] columnsPreread,
            PrereadIndexRangesGrbit grbit);

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
        int JetPrereadKeyRanges(
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
            PrereadIndexRangesGrbit grbit);

        /// <summary>
        /// Set an array of simple filters for <see cref="Api.JetMove(JET_SESID,JET_TABLEID,int,MoveGrbit)"/>
        /// </summary>
        /// <param name="sesid">The session to use for the call.</param>
        /// <param name="tableid">The cursor to position.</param>
        /// <param name="filters">Simple record filters.</param>
        /// <param name="grbit">Move options.</param>
        /// <returns>An error if the call fails.</returns>
        int JetSetCursorFilter(
            JET_SESID sesid,
            JET_TABLEID tableid,
            JET_INDEX_COLUMN[] filters,
            CursorFilterGrbit grbit);

        #endregion
    }
}
