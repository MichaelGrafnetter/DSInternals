using System.Collections.Generic;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// Represents a ReplicaObjectCollection.
    /// </summary>
    public class ReplicaObjectCollection : List<ReplicaObject>
    {
        // TODO: Move parent as member.
        /// <summary>
        /// base implementation.
        /// </summary>
        public ReplicaObjectCollection() : base()
        {
        }
        public ReplicaObjectCollection(int numObjects)
            : base(numObjects)
        {
        }
    }
}
