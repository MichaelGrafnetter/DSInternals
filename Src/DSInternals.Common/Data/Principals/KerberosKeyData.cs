using DSInternals.Common.Cryptography;
using System.Security;

namespace DSInternals.Common.Data
{
    // https://msdn.microsoft.com/en-us/library/cc941809.aspx
    public class KerberosKeyData
    {
        // Size: Reserved1 (2 bytes) + Reserved2 (2 bytes) + Reserved3 (4 bytes) + KeyType (4 bytes) + KeyLength (4 bytes) + KeyOffset (4 bytes)
        internal const int StructureSize = sizeof(short) + sizeof(short) + sizeof(int) + sizeof(KerberosKeyType) + sizeof(int) + sizeof(int);

        public KerberosKeyData(KerberosKeyType keyType, byte[] key)
        {
            Validator.AssertNotNull(key, "key");
            this.KeyType = keyType;
            this.Key = key;
        }

        public KerberosKeyData(KerberosKeyType type, SecureString password, string salt)
        {
            Validator.AssertNotNull(password, "password");
            Validator.AssertNotNull(salt, "salt");

            this.KeyType = type;
            this.Key = KerberosKeyDerivation.DeriveKey(type, password, salt);
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
