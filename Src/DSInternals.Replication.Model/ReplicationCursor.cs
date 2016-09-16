using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// The ReplicationCursor class represents a replication operation occurrence.
    /// </summary>
    public class ReplicationCursor
    {
        public ReplicationCursor(Guid invocationId, long highestUsn)
        {
            this.UpToDatenessUsn = highestUsn;
            this.SourceInvocationId = invocationId;
        }

        /// <summary>
        /// Gets or sets the invocation identifier of the replication source server.
        /// </summary>
        public Guid SourceInvocationId
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the maximum update sequence number (USN)
        /// for which the destination server has accepted changes from the source server.
        /// </summary>
        public long UpToDatenessUsn
        {
            get;
            protected set;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", this.SourceInvocationId, this.UpToDatenessUsn);
        }
    }
}
