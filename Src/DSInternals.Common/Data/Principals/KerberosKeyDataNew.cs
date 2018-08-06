namespace DSInternals.Common.Data
{
    using DSInternals.Common;
    using DSInternals.Common.Cryptography;
    using System.Security;

    // https://msdn.microsoft.com/en-us/library/cc941809.aspx
    public class KerberosKeyDataNew : KerberosKeyData
    {
        // Size: Reserved1 (2 bytes) + Reserved2 (2 bytes) + Reserved3 (4 bytes) + IterationCount (4 bytes) + KeyType (4 bytes) + KeyLength (4 bytes) + KeyOffset (4 bytes)
        new internal const int StructureSize = sizeof(short) + sizeof(short) + sizeof(int) + sizeof(int) + sizeof(KerberosKeyType) + sizeof(int) + sizeof(int);

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
