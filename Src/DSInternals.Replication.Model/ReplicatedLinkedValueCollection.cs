namespace DSInternals.Replication.Model
{
    /// <summary>
    /// A collection of linked attribute values, organized by the object they belong to.
    /// </summary>
    public class ReplicatedLinkedValueCollection : Dictionary<Guid, List<ReplicaAttribute>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplicatedLinkedValueCollection"/> class.
        /// </summary>
        public ReplicatedLinkedValueCollection() : base()
        {
        }

        /// <summary>
        /// Adds a linked attribute value for the specified object.
        /// </summary>
        /// <param name="objectId">The identifier of the object.</param>
        /// <param name="attribute">The linked attribute to add.</param>
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
        /// Gets all linked attributes for the specified object, consolidating multiple values for the same attribute into a single attribute with multiple values.
        /// </summary>
        /// <param name="objectId">The identifier of the object.</param>
        /// <returns>A collection of linked attributes for the specified object.</returns>
        public IEnumerable<ReplicaAttribute> Get(Guid objectId)
        {
            List<ReplicaAttribute> attributes;
            bool objectFound = this.TryGetValue(objectId, out attributes);
            if (!objectFound)
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
