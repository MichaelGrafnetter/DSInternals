using System.Collections.Generic;
namespace DSInternals.Replication.Model
{
    public class ReplicaAttributeCollection : Dictionary<int, ReplicaAttribute>
    {
        // TODO: Move parent as member.
        public ReplicaAttributeCollection() : base()
        {
        }
        public ReplicaAttributeCollection(int numAttributes)
            : base(numAttributes)
        {
        }

        public void Add(ReplicaAttribute attribute)
        {
            // TODO: Validate not null
            // TODO: Validate if not in collection
            this.Add(attribute.Id, attribute);
        }
    }
}
