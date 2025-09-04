using DSInternals.Common;
using DSInternals.Common.Schema;
using System.Collections.Generic;
using System.Linq;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// A collection of replica attributes indexed by attribute type for Active Directory objects.
    /// </summary>
    public class ReplicaAttributeCollection : Dictionary<AttributeType, ReplicaAttribute>
    {
        // TODO: Move parent as member.
        /// <summary>
        /// Initializes a new instance of the ReplicaAttributeCollection class.
        /// </summary>
        public ReplicaAttributeCollection() : base()
        {
        }
        /// <summary>
        /// Initializes a new instance of the ReplicaAttributeCollection class with the specified initial capacity.
        /// </summary>
        /// <param name="numAttributes">The initial number of attributes the collection can store.</param>
        public ReplicaAttributeCollection(int numAttributes)
            : base(numAttributes)
        {
        }

        /// <summary>
        /// Adds a replica attribute to the collection, merging with existing attributes of the same type.
        /// </summary>
        /// <param name="attribute">The replica attribute to add to the collection.</param>
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
