using DSInternals.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSInternals.Common.Data
{
    // https://msdn.microsoft.com/en-us/library/cc941809.aspx
    public class KerberosKeyDataNew : KerberosKeyData
    {
        public KerberosKeyDataNew(KerberosKeyType keyType, byte[] key, int iterationCount)
            : base(keyType, key)
        {
            this.IterationCount = iterationCount;
        }
        public int IterationCount
        {
            get;
            private set;
        }
        public override string ToString()
        {
            return string.Format("Type: {0}, Iterations: {1}, Key: {2}", base.KeyType, this.IterationCount, base.Key.ToHex());
        }
    }
}
