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
        public ReplicationResult(List<ReplicaObject> objects, bool hasMore, ReplicationCookie cookie, int totalObjectCount)
        {
            this.Objects = objects;
            this.HasMoreData = hasMore;
            this.Cookie = cookie;
            this.TotalObjectCount = totalObjectCount;
        }

        /// <summary>
        /// The replicated objects.
        /// </summary>
        public List<ReplicaObject> Objects
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates whether there are more objects to replicate.
        /// </summary>
        public bool HasMoreData
        {
            get;
            private set;
        }

        /// <summary>
        /// A cookie for continuation.
        /// </summary>
        public ReplicationCookie Cookie
        {
            get;
            private set;
        }

        /// <summary>
        /// The total number of objects available for replication.
        /// </summary>
        public int TotalObjectCount
        {
            get;
            private set;
        }
    }
}
