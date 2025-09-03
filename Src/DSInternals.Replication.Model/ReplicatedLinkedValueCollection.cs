using System;
using System.Collections.Generic;
using System.Linq;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// Represents a ReplicatedLinkedValueCollection.
    /// </summary>
    public class ReplicatedLinkedValueCollection : Dictionary<Guid, List<ReplicaAttribute>>
    {
        // TODO: Move parent as member.
        /// <summary>
        /// base implementation.
        /// </summary>
        public ReplicatedLinkedValueCollection() : base()
        {
        }

        /// <summary>
        /// Add implementation.
        /// </summary>
        public void Add(Guid objectId, ReplicaAttribute attribute)
        {
            if (this.ContainsKey(objectId))
            {
                this[objectId].Add(attribute);
            }
            else
            {
                var attributeList = new List<ReplicaAttribute>();
                attributeList.Add(attribute);
                this.Add(objectId, attributeList);
            }
        }

        /// <summary>
        /// Get implementation.
        /// </summary>
        public IEnumerable<ReplicaAttribute> Get(Guid objectId)
        {
            List<ReplicaAttribute> attributes;
            bool objectFound = this.TryGetValue(objectId, out attributes);
            if(!objectFound)
            {
                // The linked value collection does not contain any attributes of object objectId.
                return null;
            }

            // Combine multiple Id => byte[] into single Id => byte[][] 
            var consolidatedAttributes = attributes.
                GroupBy(attribute => attribute.Id).
                Select(grouppedAttribute =>
                {
                    byte[][] consolidatedValue = grouppedAttribute.SelectMany(attribute => attribute.Values).ToArray();
                    return new ReplicaAttribute(grouppedAttribute.Key, consolidatedValue);
                });

            return consolidatedAttributes;
        }
    }
}
