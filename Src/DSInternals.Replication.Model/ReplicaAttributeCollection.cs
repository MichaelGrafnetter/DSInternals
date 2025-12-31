using DSInternals.Common.Schema;

namespace DSInternals.Replication.Model;

public class ReplicaAttributeCollection : Dictionary<AttributeType, ReplicaAttribute>
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
        ArgumentNullException.ThrowIfNull(attribute);

        ReplicaAttribute preexistingAttribute;
        bool attributeAlreadyPresent = this.TryGetValue(attribute.Id, out preexistingAttribute);

        if (attributeAlreadyPresent)
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
