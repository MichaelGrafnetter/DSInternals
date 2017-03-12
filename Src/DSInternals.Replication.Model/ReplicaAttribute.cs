
using System.Collections.Generic;
namespace DSInternals.Replication.Model
{
    public class ReplicaAttribute
    {
        public ReplicaAttribute(int id, byte[][] values)
        {
            this.Id = id;
            this.Values = values;
        }

        public ReplicaAttribute(int id, byte[] value)
        {
            this.Id = id;
            this.Values = new byte[1][];
            this.Values[0] = value;
        }

        public int Id
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
