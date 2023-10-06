namespace DSInternals.Common.Data
{
    using DSInternals.Common.Cryptography;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Text;

    public class KerberosCredential
    {
        private const short CurrentRevision = 3;
        private const short DefaultFlags = 0;
        private const short RequiredCredentialCount = 2;
        private const short MaxOldCredentialCount = 2;

        // Header: Revision (2 bytes) + Flags (2 bytes) + CredentialCount (2 bytes) + OldCredentialCount (2 bytes) + DefaultSaltLength (2 bytes) + DefaultSaltMaximumLength (2 bytes) + DefaultSaltOffset (4 bytes):
        private const short HeaderSize = 6 * sizeof(short) + sizeof(int);

        public KerberosCredential(byte[] blob)
        {
            this.ReadCredentials(blob);
        }

        public KerberosCredential(SecureString password, string principal, string realm)
        {
            // Generate salt
            this.DefaultSalt = KerberosKeyDerivation.DeriveSalt(principal, realm);

            // Generate DES keys
            byte[] desKey = KerberosKeyDerivation.DeriveKey(KerberosKeyType.DES_CBC_MD5, password, this.DefaultSalt);
            var desKeyData = new KerberosKeyData(KerberosKeyType.DES_CBC_MD5, desKey);

            // TODO: Generate RC4 key
            // byte[] rc4Key = KerberosKeyDerivation.DeriveKey(KerberosKeyType.RC4_HMAC_NT, password, this.DefaultSalt);
            // var rc4KeyData = new KerberosKeyData(KerberosKeyType.RC4_HMAC_NT, rc4Key);

            this.Credentials = new KerberosKeyData[] { desKeyData /*, rc4KeyData */ };
        }

        public short Flags
        {
            get;
            private set;
        }

        public KerberosKeyData[] Credentials
        {
            get;
            private set;
        }

        public KerberosKeyData[] OldCredentials
        {
            get;
            private set;
        }

        public string DefaultSalt
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
                    // This value MUST be set to 3.
                    writer.Write(CurrentRevision);

                    // This value MUST be zero and ignored on read.
                    writer.Write(DefaultFlags);

                    var allCredentials = new List<KerberosKeyData>();

                    // This is the count of elements in the Credentials array.
                    short credentialCount = 0;
                    if (this.Credentials != null)
                    {
                        credentialCount = (short)this.Credentials.Length;
                        allCredentials.AddRange(this.Credentials);
                    }
                    writer.Write(credentialCount);

                    // This is the count of elements in the OldCredentials array that contain the keys for the previous password.
                    short oldCredentialCount = 0;
                    if (this.OldCredentials != null)
                    {
                        oldCredentialCount = (short)this.OldCredentials.Length;
                        allCredentials.AddRange(this.OldCredentials);
                    }
                    writer.Write(oldCredentialCount);

                    // The length, in bytes, of a salt value.
                    short defaultSaltLength = (short)(this.DefaultSalt != null ? Encoding.Unicode.GetByteCount(this.DefaultSalt) : 0);
                    writer.Write(defaultSaltLength);

                    // The length, in bytes, of the buffer containing the salt value.
                    writer.Write(defaultSaltLength);

                    // An offset, in little-endian byte order, from the beginning of the attribute value (that is, from the beginning of the Revision field of KERB_STORED_CREDENTIAL) to where the salt value starts.
                    int defaultSaltOffset = HeaderSize + (RequiredCredentialCount + oldCredentialCount) * KerberosKeyData.StructureSize;
                    writer.Write(defaultSaltOffset);

                    int currentKeyValueOffset = defaultSaltOffset + defaultSaltLength;

                    // Credentials (variable): An array of CredentialCount KERB_KEY_DATA (section 2.2.10.5) elements.
                    foreach(var credential in allCredentials)
                    {
                        WriteCredential(writer, credential, currentKeyValueOffset);
                        currentKeyValueOffset += credential.Key.Length;
                    }

                    // Add padding (just to get the same result as Windows Server 2008)
                    writer.Write(Enumerable.Repeat(Byte.MinValue, KerberosKeyData.StructureSize).ToArray());

                    // DefaultSalt (variable): The default salt value.
                    if (defaultSaltLength > 0)
                    {
                        writer.Write(Encoding.Unicode.GetBytes(this.DefaultSalt));
                    }

                    // KeyValues (variable): An array of CredentialCount + OldCredentialCount key values. Each key value MUST be located at the offset specified by the corresponding KeyOffset values specified in Credentials and OldCredentials.
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
                    // This value MUST be set to 3.
                    short revision = reader.ReadInt16();

                    // This value MUST be zero and ignored on read.
                    this.Flags = reader.ReadInt16();

                    // This is the count of elements in the Credentials array. This value MUST be set to 2.
                    short credentialCount = reader.ReadInt16();

                    // This is the count of elements in the OldCredentials array that contain the keys for the previous password. This value MUST be set to 0 or 2.
                    short oldCredentialCount = reader.ReadInt16();

                    // The length, in bytes, of a salt value.
                    short defaultSaltLength = reader.ReadInt16();

                    // The length, in bytes, of the buffer containing the salt value.
                    short defaultSaltMaximumLength = reader.ReadInt16();

                    // An offset, in little-endian byte order, from the beginning of the attribute value (that is, from the beginning of the Revision field of KERB_STORED_CREDENTIAL) to where the salt value starts.
                    int defaultSaltOffset = reader.ReadInt32();

                    // Credentials (variable): An array of CredentialCountKERB_KEY_DATA (section 2.2.10.5) elements.
                    if(credentialCount > 0)
                    {
                        this.Credentials = new KerberosKeyData[credentialCount];
                        for (int i = 0; i < credentialCount; i++)
                        {
                            this.Credentials[i] = ReadCredential(reader, blob);
                        }
                    }

                    // OldCredentials (variable): An array of OldCredentialCount KERB_KEY_DATA elements.
                    if(oldCredentialCount > 0)
                    {
                        this.OldCredentials = new KerberosKeyData[oldCredentialCount];
                        for (int i = 0; i < oldCredentialCount; i++)
                        {
                            this.OldCredentials[i] = ReadCredential(reader, blob);
                        }
                    }

                    // DefaultSalt (variable): The default salt value.
                    this.DefaultSalt = Encoding.Unicode.GetString(blob, defaultSaltOffset, defaultSaltLength);

                    // KeyValues (variable): An array of CredentialCount + OldCredentialCount key values. Each key value MUST be located at the offset specified by the corresponding KeyOffset values specified in Credentials and OldCredentials.
                }
            }
        }

        private static KerberosKeyData ReadCredential(BinaryReader reader, byte[] blob)
        {
            // Reserved1 (2 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            short reserved1 = reader.ReadInt16();

            // Reserved2 (2 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            short reserved2 = reader.ReadInt16();

            // Reserved3 (4 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            int reserved3 = reader.ReadInt32();

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
            return new KerberosKeyData(keyType, key);
        }

        private static void WriteCredential(BinaryWriter writer, KerberosKeyData credential, int keyValueOffset)
        {
            // Reserved1 (2 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            writer.Write((short)0);

            // Reserved2 (2 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            writer.Write((short)0);

            // Reserved3 (4 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
            writer.Write((int)0);

            // KeyType (4 bytes): Indicates the type of key, stored as a 32-bit unsigned integer in little-endian byte order. This MUST be set to one of the following values, which are defined in section 2.2.10.8.
            writer.Write((int)credential.KeyType);

            // KeyLength (4 bytes): The length, in bytes, of the value beginning at KeyOffset. The value of this field is stored in little-endian byte order.
            writer.Write(credential.Key.Length);

            // KeyOffset (4 bytes): An offset, in little-endian byte order, from the beginning of the property value (that is, from the beginning of the Revision field of KERB_STORED_CREDENTIAL) to where the key value starts. The key value is the hash value specified according to the KeyType.
            writer.Write(keyValueOffset);
        }
    }
}
