/// <remarks>
/// Adds methods required by DSInternals.
/// </remarks>
namespace Microsoft.Database.Isam
{
    using System.Collections.Generic;
    using Microsoft.Isam.Esent.Interop;

    public partial class TableDefinition
    {
        /// <summary>
        /// Gets a collection containing the tables indices.
        /// </summary>
        public IEnumerable<IndexInfo> Indices2
        {
            // HACK: We added support to retrieve the low-level IndexInfo instead of high-level IndexCollection.
            /* There is a bug in Isam IndexCollection enumerator, which causes it to loop indefinitely
             * through the first few indices under some very rare circumstances. */
            get
            {
                // TODO: Possibly add a lock for thread safety.
                return Api.GetTableIndexes(this.IsamSession.Sesid, this.Database.Dbid, this.Name);
            }
        }
    }
}
