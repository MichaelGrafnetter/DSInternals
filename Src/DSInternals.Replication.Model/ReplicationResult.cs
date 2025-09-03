using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// Contains the results of an Active Directory replication operation, including replicated objects and metadata.
    /// </summary>
    public class ReplicationResult
    {
        // TODO: AsReadOnly
        public ReplicationResult(ReplicaObjectCollection objects, bool hasMore, ReplicationCookie cookie, int totalObjectCount)
        {
            this.Objects = objects;
            this.HasMoreData = hasMore;
            this.Cookie = cookie;
            this.TotalObjectCount = totalObjectCount;
        }
        public ReplicaObjectCollection Objects
        {
            get;
            private set;
        }
        public bool HasMoreData
        {
            get;
            private set;
        }
        public ReplicationCookie Cookie
        {
            get;
            private set;
        }
        public int TotalObjectCount
        {
            get;
            private set;
        }
    }
}
