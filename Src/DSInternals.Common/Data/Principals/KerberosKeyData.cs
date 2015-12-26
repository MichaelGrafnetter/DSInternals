using DSInternals.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSInternals.Common.Data
{
    // https://msdn.microsoft.com/en-us/library/cc941809.aspx
    public class KerberosKeyData
    {
        public KerberosKeyData(KerberosKeyType keyType, byte[] key)
        {
            this.KeyType = keyType;
            this.Key = key;
        }
        public KerberosKeyType KeyType
        {
            get;
            private set;
        }
        public byte[] Key
        {
            get;
            private set;
        }
        public override string ToString()
        {
            return string.Format("Type: {0}, Key: {1}", this.KeyType, this.Key.ToHex());
        }

    }
}
