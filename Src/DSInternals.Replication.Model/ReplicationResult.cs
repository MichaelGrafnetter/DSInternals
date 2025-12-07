using DSInternals.Common;
using DSInternals.Common.Schema;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// Represents the result of a replication operation, including the replicated objects, a cookie for continuation, and metadata about the operation.
    /// </summary>
    public sealed class ReplicationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplicationResult"/> class.
        /// </summary>
        /// <param name="objects">The replicated objects.</param>
        /// <param name="hasMore">Indicates whether there are more objects to replicate.</param>
        /// <param name="cookie">A cookie for continuation.</param>
        /// <param name="totalObjectCount">The total number of objects available for replication.</param>
        public ReplicationResult(List<ReplicaObject> objects, bool hasMore, ReplicationCookie cookie, PrefixTable prefixTable, int totalObjectCount)
        {
            Validator.AssertNotNull(objects, nameof(objects));
            Validator.AssertNotNull(cookie, nameof(cookie));

            this.Objects = objects;
            this.HasMoreData = hasMore;
            this.Cookie = cookie;
            this.PrefixTable = prefixTable;
            this.TotalObjectCount = totalObjectCount;
        }

        /// <summary>
        /// The replicated objects.
        /// </summary>
        public readonly List<ReplicaObject> Objects;

        /// <summary>
        /// Indicates whether there are more objects to replicate.
        /// </summary>
        public readonly bool HasMoreData;

        /// <summary>
        /// A cookie for continuation.
        /// </summary>
        public readonly ReplicationCookie Cookie;

        /// <summary>
        /// Table for translating ATTRTYP values in the response to OIDs.
        /// </summary>
        public readonly PrefixTable? PrefixTable;

        /// <summary>
        /// The total number of objects available for replication.
        /// </summary>
        public readonly int TotalObjectCount;
    }
}
