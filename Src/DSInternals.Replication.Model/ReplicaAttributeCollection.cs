using DSInternals.Common;
using DSInternals.Common.Schema;
using System.Collections.Generic;
using System.Linq;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// Represents a ReplicaAttributeCollection.
    /// </summary>
    public class ReplicaAttributeCollection : Dictionary<AttributeType, ReplicaAttribute>
    {
        // TODO: Move parent as member.
        /// <summary>
        /// base implementation.
        /// </summary>
        public ReplicaAttributeCollection() : base()
        {
        }
        public ReplicaAttributeCollection(int numAttributes)
            : base(numAttributes)
        {
        }

        /// <summary>
        /// Add implementation.
        /// </summary>
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
