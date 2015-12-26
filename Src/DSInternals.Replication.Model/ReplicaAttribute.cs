
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
