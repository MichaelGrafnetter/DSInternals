using DSInternals.Common.Cryptography;
using System;
using System.IO;
using System.Security;
using System.Text;

namespace DSInternals.Common.Data
{
    public class KerberosCredentialNew
    {
        private const short RequiredRevision = 4;
        private const short RequiredCredentialCount = -1;
        private const short MaxOldCredentialCount = -1;

        public KerberosCredentialNew(byte[] blob)
        {
            this.ReadCredentials(blob);
        }

        public KerberosCredentialNew(SecureString password, string principal, string realm)
        {
            // Generate salt
            this.DefaultSalt = KerberosKeyDerivation.DeriveSalt(principal, realm);

            // Generate AES keys
            this.DefaultIterationCount = KerberosKeyDerivation.DefaultIterationCount;

            byte[] aes128Key = KerberosKeyDerivation.DeriveKey(KerberosKeyType.AES128_CTS_HMAC_SHA1_96, password, this.DefaultSalt);
            var aes128KeyData = new KerberosKeyDataNew(KerberosKeyType.AES128_CTS_HMAC_SHA1_96, aes128Key, KerberosKeyDerivation.DefaultIterationCount);

            byte[] aes256Key = KerberosKeyDerivation.DeriveKey(KerberosKeyType.AES256_CTS_HMAC_SHA1_96, password, this.DefaultSalt);
            var aes256KeyData = new KerberosKeyDataNew(KerberosKeyType.AES256_CTS_HMAC_SHA1_96_PLAIN, aes256Key, KerberosKeyDerivation.DefaultIterationCount);

            this.Credentials = new KerberosKeyDataNew[] { aes128KeyData, aes256KeyData };
        }

        public short Flags
        {
            get;
            private set;
        }
        public KerberosKeyDataNew[] Credentials
        {
            get;
            private set;
        }
        public KerberosKeyDataNew[] ServiceCredentials
        {
            get;
            private set;
        }
        public KerberosKeyDataNew[] OldCredentials
        {
            get;
            private set;
        }
        public KerberosKeyDataNew[] OlderCredentials
        {
            get;
            private set;
        }
        public string DefaultSalt
        {
            get;
            private set;
        }
        public int DefaultIterationCount
        {
            get;
            private set;
        }
        private void ReadCredentials(byte[] blob)
        {
            using (Stream stream = new MemoryStream(blob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // This value MUST be set to 4.
                    short revision = reader.ReadInt16();
                    // This value MUST be zero and ignored on read.
                    this.Flags = reader.ReadInt16();
                    // This is the count of elements in the Credentials array. This value MUST be set to 2.
                    short credentialCount = reader.ReadInt16();
                    // This is the count of elements in the ServiceCredentials field. It MUST be zero.
                    short serviceCredentialCount = reader.ReadInt16();
                    // This is the count of elements in the OldCredentials array that contain the keys for the previous password. This value MUST be set to 0 or 2.
                    short oldCredentialCount = reader.ReadInt16();
                    // This is the count of elements in the OlderCredentials field that contain the keys for the previous password.
                    short olderCredentialCount = reader.ReadInt16();
                    // The length, in bytes, of a salt value.
                    short defaultSaltLength = reader.ReadInt16();
                    // The length, in bytes, of the buffer containing the salt value.
                    short defaultSaltMaximumLength = reader.ReadInt16();
                    // An offset, in little-endian byte order, from the beginning of the attribute value (that is, from the beginning of the Revision field of KERB_STORED_CREDENTIAL) to where the salt value starts.
                    int defaultSaltOffset = reader.ReadInt32();
                    // The default iteration count used to calculate the password hashes.
                    this.DefaultIterationCount = reader.ReadInt32();
                    // Credentials (variable): An array of CredentialCountKERB_KEY_DATA (section 2.2.10.5) elements.
                    if(credentialCount > 0)
                    {
                        this.Credentials = new KerberosKeyDataNew[credentialCount];
                        for (int i = 0; i < credentialCount; i++)
                        {
                            this.Credentials[i] = ReadCredential(reader, blob);
                        }
                    }
                    // ServiceCredentials (variable): (This field is optional.) An array of ServiceCredentialCount KERB_KEY_DATA_NEW elements.
                    if(serviceCredentialCount > 0)
                    {
                        this.ServiceCredentials = new KerberosKeyDataNew[serviceCredentialCount];
                        for (int i = 0; i < serviceCredentialCount; i++)
                        {
                            this.ServiceCredentials[i] = ReadCredential(reader, blob);
                        }
                    }
                    // OldCredentials (variable): An array of OldCredentialCount KERB_KEY_DATA elements.
                    if (oldCredentialCount > 0)
                    {
                        this.OldCredentials = new KerberosKeyDataNew[oldCredentialCount];
                        for (int i = 0; i < oldCredentialCount; i++)
                        {
                            this.OldCredentials[i] = ReadCredential(reader, blob);
                        }
                    }
                    // OlderCredentials (variable): (This field is optional.) An array of OlderCredentialCount KERB_KEY_DATA_NEW elements.
                    if (olderCredentialCount > 0)
                    {
                        this.OlderCredentials = new KerberosKeyDataNew[olderCredentialCount];
                        for (int i = 0; i < olderCredentialCount; i++)
                        {
                            this.OlderCredentials[i] = ReadCredential(reader, blob);
                        }
                    }

                    // DefaultSalt (variable): The default salt value.
                    this.DefaultSalt = Encoding.Unicode.GetString(blob, defaultSaltOffset, defaultSaltLength);
                    // KeyValues (variable): An array of CredentialCount + OldCredentialCount key values. Each key value MUST be located at the offset specified by the corresponding KeyOffset values specified in Credentials and OldCredentials.
                }
            }
        }
        private static KerberosKeyDataNew ReadCredential(BinaryReader reader, byte[] blob)
        {
            // Reserved1 (2 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            short reserved1 = reader.ReadInt16();
            // Reserved2 (2 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            short reserved2 = reader.ReadInt16();
            // Reserved3 (4 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            int reserved3 = reader.ReadInt32();
            // Indicates the iteration count used to calculate the password hashes.
            int iterationCount = reader.ReadInt32();
            // KeyType (4 bytes): Indicates the type of key, stored as a 32-bit unsigned integer in little-endian byte order. This MUST be set to one of the following values, which are defined in section 2.2.10.8.
            KerberosKeyType keyType = (KerberosKeyType)reader.ReadInt32();
            // KeyLength (4 bytes): The length, in bytes, of the value beginning at KeyOffset. The value of this field is stored in little-endian byte order.
            int keyLength = reader.ReadInt32();
            // KeyOffset (4 bytes): An offset, in little-endian byte order, from the beginning of the property value (that is, from the beginning of the Revision field of KERB_STORED_CREDENTIAL) to where the key value starts. The key value is the hash value specified according to the KeyType.
            int keyOffset = reader.ReadInt32();
            // Load key:
            byte[] key = new byte[keyLength];
            Buffer.BlockCopy((Array)blob, keyOffset, key, 0, keyLength);
            // Finally, create key information:
            return new KerberosKeyDataNew(keyType, key, iterationCount);
        }
    }
}
