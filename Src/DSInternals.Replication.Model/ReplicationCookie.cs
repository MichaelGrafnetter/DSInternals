using DSInternals.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// USN Vector
    /// </summary>
    public sealed class ReplicationCookie
    {
        public ReplicationCookie(string namingContext)
        {
            //TODO: Validate namingContext not null or empty.
            this.NamingContext = namingContext;
        }
        public ReplicationCookie(string namingContext, Guid invocationId, Int64 highObjectUpdate, Int64 highPropUpdate, Int64 reserved)
        {
            //TODO: Validate namingContext not null or empty.
            this.NamingContext = namingContext;
            this.InvocationId = invocationId;
            this.HighObjUpdate = highObjectUpdate;
            this.HighPropUpdate = highPropUpdate;
            this.Reserved = reserved;
        }
        public string NamingContext
        {
            get;
            private set;
        }
        public Guid InvocationId
        {
            get;
            private set;
        }
        public Int64 HighObjUpdate
        {
            get;
            private set;
        }
        public Int64 Reserved
        {
            get;
            private set;
        }
        public Int64 HighPropUpdate
        {
            get;
            private set;
        }
    }
}
