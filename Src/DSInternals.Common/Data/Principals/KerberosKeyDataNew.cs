namespace DSInternals.Common.Data
{
    using DSInternals.Common;
    using DSInternals.Common.Cryptography;
    using System.Security;

    // https://msdn.microsoft.com/en-us/library/cc941809.aspx
    public class KerberosKeyDataNew : KerberosKeyData
    {
        public KerberosKeyDataNew(KerberosKeyType keyType, byte[] key, int iterationCount)
            : base(keyType, key)
        {
            this.IterationCount = iterationCount;
        }

        public KerberosKeyDataNew(KerberosKeyType keyType, SecureString password, string principal, string realm, int iterationCount = KerberosKeyDerivation.DefaultIterationCount) :
            this(keyType, password, KerberosKeyDerivation.DeriveSalt(principal, realm), iterationCount)
        {
        }

        public KerberosKeyDataNew(KerberosKeyType keyType, SecureString password, string salt, int iterationCount = KerberosKeyDerivation.DefaultIterationCount) :
            base(keyType, KerberosKeyDerivation.DeriveKey(keyType, password, salt, iterationCount))
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
