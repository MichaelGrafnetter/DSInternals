using System;
using System.Runtime.InteropServices;
using System.IO;
using DSInternals.Common.Cryptography;
using DSInternals.Common;
using System.Security.Cryptography;

namespace DSInternals.DataStore
{
    public class DataStoreSecretDecryptor : DirectorySecretDecryptor
    {
        private const int BootKeySaltHashRounds = 1000;
        private const int EncryptedPekListOffset = sizeof(PekListVersion) + sizeof(PekListFlags) + SaltSize;
        /// <summary>
        /// Signature/Authenticator that all decrypted PEK Lists must contain at the beginning to be considered valid.
        /// </summary>
        private static readonly Guid ExpectedSignature = new Guid(0x4881d956, 0x91ec, 0x11d1, 0x90, 0x5a, 0x00, 0xc0, 0x4f, 0xc2, 0xd4, 0xcf);

        public byte[][] Keys
        {
            get;
            private set;
        }

        public int CurrentKeyIndex
        {
            get;
            private set;
        }

        public override byte[] CurrentKey
        {
            get
            {
                return this.Keys[this.CurrentKeyIndex];
            }
        }

        public override SecretEncryptionType EncryptionType
        {
            get
            {
                return SecretEncryptionType.DatabaseSecretWithSalt;
            }
        }

        public DateTime LastGenerated
        {
            get;
            private set;
        }

        public DataStoreSecretDecryptor(byte[] encryptedPEKListBlob, byte[] bootKey)
        {
            // Decrypt
            byte[] decryptedPekList = DecryptPekList(encryptedPEKListBlob, bootKey);
            // Parse the inner structure
            this.Initialize(decryptedPekList);
        }

        public DataStoreSecretDecryptor(byte[] cleartextPEKListBlob)
        {
            this.Initialize(cleartextPEKListBlob);
        }

        public override byte[] DecryptSecret(byte[] blob)
        {
            // Blob structure: Algorithm ID (2B), Flags (2B), PEK ID (4B), Salt (16B), Encrypted secret (rest)
            const int EncryptedDataOffset = 2 * sizeof(short) + sizeof(uint) + SaltSize;
            Validator.AssertMinLength(blob, EncryptedDataOffset + 1, "blob");

            // Extract salt and the actual encrypted data from the blob
            byte[] salt;
            byte[] decryptionKey;
            using (Stream stream = new MemoryStream(blob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // TODO: Validate encryption type
                    SecretEncryptionType encryptionType = (SecretEncryptionType) reader.ReadUInt16();
                    if(encryptionType != SecretEncryptionType.DatabaseSecretWithSalt)
                    {
                        // TODO: Extract as resource
                        var ex = new FormatException("Unsupported encryption type.");
                        ex.Data.Add("SecretEncryptionType", encryptionType);
                        throw ex;
                    }
                    
                    // The flags field is actually not used by AD and is always 0.
                    uint flags = reader.ReadUInt16();
                    uint keyId = reader.ReadUInt32();
                    // TODO: Check if the key exists
                    decryptionKey = this.Keys[keyId];
                    salt = reader.ReadBytes(SaltSize);
                }
            }
            // Perform decryption           
            byte[] encryptedSecret = blob.Cut(EncryptedDataOffset);
            byte[] decryptedSecret = DecryptUsingRC4(encryptedSecret, salt, decryptionKey);
            return decryptedSecret;
        }

        private void Initialize(byte[] cleartextBlob)
        {
            // TODO: Check min blob length
            // Blob structure: Signature (16B), Last Generated (8B), Current Key (4B), Key Count (4B), { Key Id (4B), Key (16B) } * Key Count
            const int minBlobLength = 16 + sizeof(long) + 3 * sizeof(uint) + KeySize;
            Validator.AssertMinLength(cleartextBlob, minBlobLength, "cleartextBlob");

            using (Stream stream = new MemoryStream(cleartextBlob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    byte[] binarySignature = reader.ReadBytes(Marshal.SizeOf(typeof(Guid)));
                    Guid signature = new Guid(binarySignature);
                    if(signature != ExpectedSignature)
                    {
                        // TODO: Exception type
                        throw new Exception("Invalid PEK signature.");
                    }

                    long lastGeneratedFileTime = reader.ReadInt64();
                    this.LastGenerated = DateTime.FromFileTimeUtc(lastGeneratedFileTime);

                    this.CurrentKeyIndex = (int)reader.ReadUInt32();

                    uint numKeys = reader.ReadUInt32();
                    this.Keys = new byte[numKeys][];

                    // TODO: Check CurrentKeyIndex against numKeys

                    // In most cases, we only need to read one key.
                    for (int i = 0; i < numKeys; i++)
                    {
                        uint keyId = reader.ReadUInt32();
                        byte[] key = reader.ReadBytes(KeySize);
                        // Add the key to the list
                        // TODO: Check array bounds
                        this.Keys[keyId] = key;
                    }
                }
            }
        }

        private static byte[] EncryptPekList(byte[] cleartextBlob, byte[] bootKey = null)
        {
            // Do not encrypt by default.
            PekListFlags flags = PekListFlags.Clear;
            if (bootKey != null)
            {
                // Encrypt if the boot key is provided.
                Validator.AssertLength(bootKey, BootKeyRetriever.BootKeyLength, "bootKey");
                flags = PekListFlags.Encrypted;
            }

            int bufferSize = EncryptedPekListOffset + cleartextBlob.Length;
            byte[] buffer = new byte[bufferSize];

            // Generate random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // Header
                    writer.Write((uint)PekListVersion.Current);
                    writer.Write((uint)flags);
                    writer.Write(salt);
                    // Data
                    switch(flags)
                    {
                        case PekListFlags.Clear:
                            writer.Write(cleartextBlob);
                            break;
                        case PekListFlags.Encrypted:
                            byte[] encryptedBlob = EncryptUsingRC4(cleartextBlob, salt, bootKey, BootKeySaltHashRounds);
                            writer.Write(encryptedBlob);
                            break;
                    }
                }
            }
            return buffer;

        }

        private static byte[] DecryptPekList(byte[] encryptedBlob, byte[] bootKey)
        {
            // Blob structure: Version (4B), Flags (4B), Salt (16B), Encrypted PEK List
            Validator.AssertMinLength(encryptedBlob, EncryptedPekListOffset + 1, "blob");
            Validator.AssertLength(bootKey, BootKeyRetriever.BootKeyLength, "bootKey");

            // Parse blob
            byte[] salt;
            PekListFlags flags;
            using (Stream stream = new MemoryStream(encryptedBlob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    PekListVersion version = (PekListVersion) reader.ReadUInt32();
                    if(version != PekListVersion.Current)
                    {
                        // TODO: Extract as resource.
                        throw new FormatException("Unsupported PEK list version.");
                    }
                    flags = (PekListFlags) reader.ReadUInt32();
                    salt = reader.ReadBytes(SaltSize);
                }
            }

            byte[] encryptedPekList = encryptedBlob.Cut(EncryptedPekListOffset);
            byte[] decryptedPekList;
            switch(flags)
            {
                case PekListFlags.Encrypted:
                    // Decrypt
                    decryptedPekList = DecryptUsingRC4(encryptedPekList, salt, bootKey, BootKeySaltHashRounds);
                    break;
                case PekListFlags.Clear:
                    // No decryption is needed. This is a very rare case.
                    decryptedPekList = encryptedPekList;
                    break;
                default:
                    // TODO: Extract as a resource.
                    throw new FormatException("Unsupported PEK list flags");
            }

            return decryptedPekList;
        }

        public byte[] ToByteArray(byte[] bootKey = null)
        {
            if (this.Keys.Length == 0)
            {
                return null;
            }
            int bufferSize = Marshal.SizeOf(typeof(Guid)) + sizeof(long) + 2 * sizeof(int) + Keys.Length * (sizeof(int) + KeySize);
            byte[] buffer = new byte[bufferSize];

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // Header
                    writer.Write(ExpectedSignature.ToByteArray());
                    writer.Write(this.LastGenerated.ToFileTimeUtc());
                    writer.Write(this.CurrentKeyIndex);
                    writer.Write(this.Keys.Length);
                    
                    // Keys
                    for(int i = 0; i < this.Keys.Length; i++)
                    {
                        writer.Write(i);
                        writer.Write(this.Keys[i]);
                    }
                }
            }
            byte[] encryptedPekList = EncryptPekList(buffer, bootKey);
            return encryptedPekList;
        }
    }
}
