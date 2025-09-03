using System;
using System.Collections.Generic;
using System.Linq;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// A collection that stores replicated linked attributes grouped by object GUID for Active Directory replication.
    /// </summary>
    public class ReplicatedLinkedValueCollection : Dictionary<Guid, List<ReplicaAttribute>>
    {
        // TODO: Move parent as member.
        /// <summary>
        /// Initializes a new instance of the ReplicatedLinkedValueCollection class.
        /// </summary>
        public ReplicatedLinkedValueCollection() : base()
        {
        }

        /// <summary>
        /// Adds a replicated attribute to the collection for the specified object.
        /// </summary>
        /// <param name="objectId">The GUID of the object to add the attribute to.</param>
        /// <param name="attribute">The replicated attribute to add.</param>
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
        /// Retrieves and consolidates all replicated attributes for the specified object.
        /// </summary>
        /// <param name="objectId">The GUID of the object to retrieve attributes for.</param>
        /// <returns>A collection of consolidated replica attributes, or null if the object is not found.</returns>
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
