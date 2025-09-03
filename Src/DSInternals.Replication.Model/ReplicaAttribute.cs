using DSInternals.Common.Schema;
namespace DSInternals.Replication.Model
{
    /// <summary>
    /// Represents a replicated Active Directory attribute with its type and values.
    /// </summary>
    public class ReplicaAttribute
    {
        public ReplicaAttribute(AttributeType id, byte[][] values)
        {
            this.Id = id;
            this.Values = values;
        }

        public ReplicaAttribute(AttributeType id, byte[] value)
        {
            this.Id = id;
            this.Values = new byte[1][];
            this.Values[0] = value;
        }

        public AttributeType Id
        {
            get;
            private set;
        }

        public byte[][] Values
        {
            get;
            private set;
        }
    }
}
