using DSInternals.Common;
using System.Collections.Generic;
using System.Linq;

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
            Validator.AssertNotNull(attribute, "attribute");

            ReplicaAttribute preexistingAttribute;
            bool attributeAlreadyPresent = this.TryGetValue(attribute.Id, out preexistingAttribute);
            
            if(attributeAlreadyPresent)
            {
                // TODO: Under what circumstances does this sometimes occur with linked attributes?
                // Combine the values into one attribute
                byte[][] combinedValues = attribute.Values.Concat(preexistingAttribute.Values).ToArray();
                this[attribute.Id] = new ReplicaAttribute(attribute.Id, combinedValues);
            }
            else
            {
                this.Add(attribute.Id, attribute);
            }
        }
    }
}
