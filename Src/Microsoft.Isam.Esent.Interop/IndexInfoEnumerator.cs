//-----------------------------------------------------------------------
// <copyright file="IndexInfoEnumerator.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// Base class for enumerators that return IndexInfo objects. Subclasses differ
    /// by how they open the table.
    /// </summary>
    internal abstract class IndexInfoEnumerator : TableEnumerator<IndexInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexInfoEnumerator"/> class.
        /// </summary>
        /// <param name="sesid">
        /// The session to use.
        /// </param>
        protected IndexInfoEnumerator(JET_SESID sesid) : base(sesid)
        {
        }

        /// <summary>
        /// Gets or sets the indexlist used to retrieve data.
        /// </summary>
        protected JET_INDEXLIST Indexlist { get; set; }

        /// <summary>
        /// Gets the entry the cursor is currently positioned on.
        /// </summary>
        /// <returns>The entry the cursor is currently positioned on.</returns>
        protected override IndexInfo GetCurrent()
        {
            return this.GetIndexInfoFromIndexlist(this.Sesid, this.Indexlist);
        }

        /// <summary>
        /// Retrieves information about indexes on a table.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="indexname">The name of the index.</param>
        /// <param name="result">Filled in with information about indexes on the table.</param>
        /// <param name="infoLevel">The type of information to retrieve.</param>
        protected abstract void GetIndexInfo(
                JET_SESID sesid,
                string indexname,
                out string result,
                JET_IdxInfo infoLevel);

        /// <summary>
        /// Create an array of IndexSegment objects from the data in the current JET_INDEXLIST entry.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="indexlist">The indexlist to take the data from.</param>
        /// <returns>An array of IndexSegment objects containing the information for the current index.</returns>
        private static IndexSegment[] GetIndexSegmentsFromIndexlist(JET_SESID sesid, JET_INDEXLIST indexlist)
        {
            var numSegments = (int)Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidcColumn);
            Debug.Assert(numSegments > 0, "Index has zero index segments");

            // If we use the wide API (Vista+), then the temp table will be in UTF-16.
            Encoding encodingOfTextColumns = EsentVersion.SupportsVistaFeatures ? Encoding.Unicode : LibraryHelpers.EncodingASCII;

            var segments = new IndexSegment[numSegments];
            for (int i = 0; i < numSegments; ++i)
            {
                string columnName = Api.RetrieveColumnAsString(
                    sesid,
                    indexlist.tableid,
                    indexlist.columnidcolumnname,
                    encodingOfTextColumns,
                    RetrieveColumnGrbit.None);
                columnName = StringCache.TryToIntern(columnName);
                var coltyp = (JET_coltyp)Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidcoltyp);
                var grbit =
                    (IndexKeyGrbit)Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidgrbitColumn);
                bool isAscending = IndexKeyGrbit.Ascending == grbit;
                var cp = (JET_CP)Api.RetrieveColumnAsInt16(sesid, indexlist.tableid, indexlist.columnidCp);
                bool isASCII = JET_CP.ASCII == cp;

                segments[i] = new IndexSegment(columnName, coltyp, isAscending, isASCII);

                if (i < numSegments - 1)
                {
                    Api.JetMove(sesid, indexlist.tableid, JET_Move.Next, MoveGrbit.None);
                }
            }

            return segments;
        }

        /// <summary>
        /// Create an IndexInfo object from the data in the current JET_INDEXLIST entry.
        /// </summary>
        /// <param name="sesid">The session to use.</param>
        /// <param name="indexlist">The indexlist to take the data from.</param>
        /// <returns>An IndexInfo object containing the information from that record.</returns>
        private IndexInfo GetIndexInfoFromIndexlist(JET_SESID sesid, JET_INDEXLIST indexlist)
        {
            // If we use the wide API (Vista+), then the temp table will be in UTF-16.
            Encoding encodingOfTextColumns = EsentVersion.SupportsVistaFeatures ? Encoding.Unicode : LibraryHelpers.EncodingASCII;

            string name = Api.RetrieveColumnAsString(
                sesid, indexlist.tableid, indexlist.columnidindexname, encodingOfTextColumns, RetrieveColumnGrbit.None);
            name = StringCache.TryToIntern(name);

            CultureInfo cultureInfo = null;

            if (EsentVersion.SupportsWindows8Features)
            {
                string localeName;
                this.GetIndexInfo(sesid, name, out localeName, Windows8IdxInfo.LocaleName);
                cultureInfo = new CultureInfo(localeName);
            }
            else
            {
#if !MANAGEDESENT_ON_CORECLR
                // This probably won't work on platforms that don't support LCIDs. Newer environments have dropped
                // LCIDs in favour of locale names. But currently JET_INDEXLIST doesn't expose columnidLocale.
                int lcid = (int)Api.RetrieveColumnAsInt16(sesid, indexlist.tableid, indexlist.columnidLangid);
                cultureInfo = LibraryHelpers.CreateCultureInfoByLcid(lcid);
#endif // !MANAGEDESENT_ON_CORECLR
            }

            uint lcmapFlags = (uint)Api.RetrieveColumnAsUInt32(sesid, indexlist.tableid, indexlist.columnidLCMapFlags);
            CompareOptions compareOptions = Conversions.CompareOptionsFromLCMapFlags(lcmapFlags);
            uint grbit = (uint)Api.RetrieveColumnAsUInt32(sesid, indexlist.tableid, indexlist.columnidgrbitIndex);

            int keys = (int)Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidcKey);
            int entries = (int)Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidcEntry);
            int pages = (int)Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidcPage);

            IndexSegment[] segments = GetIndexSegmentsFromIndexlist(sesid, indexlist);

            return new IndexInfo(
                name,
                cultureInfo,
                compareOptions,
                segments,
                (CreateIndexGrbit)grbit,
                keys,
                entries,
                pages);
        }
    }
}