using System;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// USN Vector
    /// </summary>
    [Serializable]
    public sealed class ReplicationCookie
    {
        public ReplicationCookie(string namingContext)
        {
            this.NamingContext = namingContext;
        }
        public ReplicationCookie(string namingContext, Guid invocationId, Int64 highObjectUpdate, Int64 highPropUpdate, Int64 reserved)
        {
            this.NamingContext = namingContext;
            this.InvocationId = invocationId;
            this.HighObjUpdate = highObjectUpdate;
            this.HighPropUpdate = highPropUpdate;
            this.Reserved = reserved;
        }

        private ReplicationCookie()
        {

        }

        /// <summary>
        /// Performs memberwise assignment.
        /// </summary>
        /// <param name="cookie">The cookie to assign.</param>
        public void Assign(ReplicationCookie cookie)
        {
            this.NamingContext = cookie.NamingContext;
            this.InvocationId = cookie.InvocationId;
            this.HighObjUpdate = cookie.HighObjUpdate;
            this.Reserved = cookie.Reserved;
            this.HighPropUpdate = cookie.HighPropUpdate;
        }

        public string NamingContext
        {
            get;
            set;
        }
        public Guid InvocationId
        {
            get;
            set;
        }
        public Int64 HighObjUpdate
        {
            get;
            set;
        }
        public Int64 Reserved
        {
            get;
            set;
        }
        public Int64 HighPropUpdate
        {
            get;
            set;
        }
    }
}
