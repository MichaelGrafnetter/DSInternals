using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Database.Isam;
using Microsoft.Isam.Esent.Interop;

namespace DSInternals.DataStore
{
    public static class TableDefinitionExtentions
    {
        /// <summary>
        /// Gets a collection containing the tables indices.
        /// </summary>
        public static IEnumerable<IndexInfo> GetIndices2(this TableDefinition tableDefinition, IsamDatabase database)
        {
            if (tableDefinition == null)
            {
                throw new ArgumentNullException(nameof(tableDefinition));
            }

            // HACK: We added support to retrieve the low-level IndexInfo instead of high-level IndexCollection.
            /* There is a bug in Isam IndexCollection enumerator, which causes it to loop indefinitely
             * through the first few indices under some very rare circumstances. */

            // HACK: Use reflection to access internal fields
            JET_DBID dbid = (JET_DBID)typeof(IsamDatabase).GetField("dbid", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(database);
            JET_SESID sesid = (JET_SESID)typeof(IsamSession).GetField("sesid", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(database.IsamSession);

            // TODO: Possibly add a lock for thread safety.
            return Api.GetTableIndexes(sesid, dbid, tableDefinition.Name);
        }
    }
}
