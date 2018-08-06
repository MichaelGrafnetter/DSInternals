namespace DSInternals.Common.Data
{
    using DSInternals.Common.Cryptography;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Text;

    public class KerberosCredentialNew
    {
        // Header: Revision (2 bytes) + Flags (2 bytes) + CredentialCount (2 bytes) + ServiceCredentialCount (2 bytes) + OldCredentialCount (2 bytes) + OlderCredentialCount (2 bytes) + DefaultSaltLength (2 bytes) + DefaultSaltMaximumLength (2 bytes) + DefaultSaltOffset (4 bytes) + DefaultIterationCount (4 bytes):
        private const short HeaderSize = 8 * sizeof(short) + 2 * sizeof(int);
        private const short CurrentRevision = 4;
        private const short DefaultFlags = 0;
        private const short RequiredCredentialCount = 4;

        public KerberosCredentialNew(byte[] blob)
        {
            Validator.AssertNotNull(blob, "blob");
            this.ReadCredentials(blob);
        }

        public KerberosCredentialNew(SecureString password, string principal, string realm) :
            this(password, KerberosKeyDerivation.DeriveSalt(principal, realm))
        {
        }

        public KerberosCredentialNew(SecureString password, string salt)
        {
            Validator.AssertNotNull(password, "password");
            Validator.AssertNotNull(salt, "salt");

            this.DefaultSalt = salt;
            this.DefaultIterationCount = KerberosKeyDerivation.DefaultIterationCount;

            // Generate DES key
            byte[] desKey = KerberosKeyDerivation.DeriveKey(KerberosKeyType.DES_CBC_MD5, password, this.DefaultSalt);
            var desKeyData = new KerberosKeyDataNew(KerberosKeyType.DES_CBC_MD5, desKey, this.DefaultIterationCount);

            // Generate AES keys
            byte[] aes128Key = KerberosKeyDerivation.DeriveKey(KerberosKeyType.AES128_CTS_HMAC_SHA1_96, password, this.DefaultSalt);
            var aes128KeyData = new KerberosKeyDataNew(KerberosKeyType.AES128_CTS_HMAC_SHA1_96, aes128Key, this.DefaultIterationCount);

            byte[] aes256Key = KerberosKeyDerivation.DeriveKey(KerberosKeyType.AES256_CTS_HMAC_SHA1_96, password, this.DefaultSalt);
            var aes256KeyData = new KerberosKeyDataNew(KerberosKeyType.AES256_CTS_HMAC_SHA1_96, aes256Key, this.DefaultIterationCount);

            this.Credentials = new KerberosKeyDataNew[] { aes256KeyData, aes128KeyData, desKeyData };
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

        public byte[] ToByteArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // Revision (2 bytes): This value MUST be set to 4.
                    writer.Write(CurrentRevision);

                    // Flags (2 bytes): This value MUST be zero and ignored on read.
                    writer.Write(DefaultFlags);

                    var allCredentials = new List<KerberosKeyDataNew>();

                    // CredentialCount (2 bytes): This is the count of elements in the Credentials field.
                    short credentialCount = 0;
                    if (this.Credentials != null)
                    {
                        credentialCount = (short)this.Credentials.Length;
                        allCredentials.AddRange(this.Credentials);
                    }
                    writer.Write(credentialCount);

                    // ServiceCredentialCount(2 bytes):  This is the count of elements in the ServiceCredentials field.It MUST be zero.
                    short serviceCredentialCount = 0;
                    if (this.ServiceCredentials != null)
                    {
                        serviceCredentialCount = (short)this.ServiceCredentials.Length;
                        allCredentials.AddRange(this.ServiceCredentials);
                    }
                    writer.Write(serviceCredentialCount);

                    // OldCredentialCount (2 bytes): This is the count of elements in the OldCredentials field that contain the keys for the previous password.
                    short oldCredentialCount = 0;
                    if (this.OldCredentials != null)
                    {
                        oldCredentialCount = (short)this.OldCredentials.Length;
                        allCredentials.AddRange(this.OldCredentials);
                    }
                    writer.Write(oldCredentialCount);

                    // OlderCredentialCount(2 bytes):  This is the count of elements in the OlderCredentials field that contain the keys for the previous password.
                    short olderCredentialCount = 0;
                    if (this.OlderCredentials != null)
                    {
                        olderCredentialCount = (short)this.OlderCredentials.Length;
                        allCredentials.AddRange(this.OlderCredentials);
                    }
                    writer.Write(olderCredentialCount);

                    // DefaultSaltLength (2 bytes): The length, in bytes, of a salt value.
                    short defaultSaltLength = (short)(this.DefaultSalt != null ? Encoding.Unicode.GetByteCount(this.DefaultSalt) : 0);
                    writer.Write(defaultSaltLength);

                    // DefaultSaltMaximumLength(2 bytes): The length, in bytes, of the buffer containing the salt value.
                    writer.Write(defaultSaltLength);

                    // DefaultSaltOffset (4 bytes): An offset, in little-endian byte order, from the beginning of the attribute value (that is, from the beginning of the Revision field of KERB_STORED_CREDENTIAL) to where DefaultSalt starts.
                    int defaultSaltOffset = HeaderSize + (RequiredCredentialCount + serviceCredentialCount + oldCredentialCount + olderCredentialCount) * KerberosKeyDataNew.StructureSize;
                    writer.Write(defaultSaltOffset);

                    // DefaultIterationCount(4 bytes): The default iteration count used to calculate the password hashes.
                    writer.Write(this.DefaultIterationCount);

                    int currentKeyValueOffset = defaultSaltOffset + defaultSaltLength;

                    // Credentials (variable): An array of CredentialCount KERB_KEY_DATA_NEW (section 2.2.10.7) elements.
                    foreach (var credential in allCredentials)
                    {
                        WriteCredential(writer, credential, currentKeyValueOffset);
                        currentKeyValueOffset += credential.Key.Length;
                    }

                    // Add optional padding
                    for (short i = credentialCount; i < RequiredCredentialCount; i++)
                    {
                        writer.Write(Enumerable.Repeat(Byte.MinValue, KerberosKeyDataNew.StructureSize).ToArray());
                    }

                    // DefaultSalt (variable): The default salt value.
                    if (defaultSaltLength > 0)
                    {
                        writer.Write(Encoding.Unicode.GetBytes(this.DefaultSalt));
                    }

                    // KeyValues (variable): An array of CredentialCount + ServiceCredentialCount + OldCredentialCount + OlderCredentialCount key values. Each key value MUST be located at the offset specified by the corresponding KeyOffset values specified in Credentials, ServiceCredentials, OldCredentials, and OlderCredentials.
                    allCredentials.ForEach(credential => writer.Write(credential.Key));
                }

                return stream.ToArray();
            }
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

        private static void WriteCredential(BinaryWriter writer, KerberosKeyDataNew credential, int keyValueOffset)
        {
            // Reserved1 (2 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            writer.Write((short)0);

            // Reserved2 (2 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            writer.Write((short)0);

            // Reserved3 (4 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            writer.Write((int)0);

            // IterationCount (4 bytes): Indicates the iteration count used to calculate the password hashes.
            writer.Write(credential.IterationCount);

            // KeyType (4 bytes): Indicates the type of key, stored as a 32-bit unsigned integer in little-endian byte order. This MUST be set to one of the following values, which are defined in section 2.2.10.8.
            writer.Write((int)credential.KeyType);

            // KeyLength (4 bytes): The length, in bytes, of the value beginning at KeyOffset. The value of this field is stored in little-endian byte order.
            writer.Write(credential.Key.Length);

            // KeyOffset (4 bytes): An offset, in little-endian byte order, from the beginning of the property value (that is, from the beginning of the Revision field of KERB_STORED_CREDENTIAL) to where the key value starts. The key value is the hash value specified according to the KeyType.
            writer.Write(keyValueOffset);
        }
    }
}
