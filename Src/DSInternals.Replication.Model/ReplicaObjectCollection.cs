using System.Collections.Generic;

namespace DSInternals.Replication.Model
{
    public class ReplicaObjectCollection : List<ReplicaObject>
    {
        // TODO: Move parent as member.
        public ReplicaObjectCollection() : base()
        {
        }
        public ReplicaObjectCollection(int numObjects)
            : base(numObjects)
        {
        }
    }
}
