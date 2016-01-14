using System;
using System.Runtime.InteropServices;
using System.IO;
using DSInternals.Common.Cryptography;
using DSInternals.Common;
using System.Security.Cryptography;
using System.Text;

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
                switch(this.Version)
                {
                    case PekListVersion.W2016:
                        return SecretEncryptionType.DatabaseVNext;
                    case PekListVersion.W2k:
                    default:
                        return SecretEncryptionType.DatabaseRC4WithSalt;
                }
            }
        }

        public PekListVersion Version
        {
            get;
            private set;
        }

        public DateTime LastGenerated
        {
            get;
            private set;
        }

        public DataStoreSecretDecryptor(byte[] encryptedPEKListBlob, byte[] bootKey)
        {
            // Decrypt and set version
            byte[] decryptedPekList = this.DecryptPekList(encryptedPEKListBlob, bootKey);
            // Parse the inner structure
            this.ParsePekList(decryptedPekList);
        }

        public DataStoreSecretDecryptor(byte[] cleartextPEKListBlob, PekListVersion version)
        {
            this.Version = version;
            this.ParsePekList(cleartextPEKListBlob);
        }

        public override byte[] DecryptSecret(byte[] blob)
        {
            // Blob structure Win2k:   Algorithm ID (2B), Flags (2B), PEK ID (4B), Salt (16B), Encrypted secret (rest)
            const int EncryptedDataOffsetDES = 2 * sizeof(short) + sizeof(uint) + SaltSize;

            // Validate (DES has shorter blob)
            Validator.AssertMinLength(blob, EncryptedDataOffsetDES + 1, "blob");

            // Extract salt and metadata from the blob
            byte[] decryptionKey;
            byte[] encryptedSecret;
            byte[] salt;
            SecretEncryptionType encryptionType;
            using (var stream = new MemoryStream(blob, false))
            {
                using (var reader = new BinaryReader(stream))
                {
                    encryptionType = (SecretEncryptionType) reader.ReadUInt16();
                    // The flags field is actually not used by AD and is always 0.
                    uint flags = reader.ReadUInt16();
                    uint keyId = reader.ReadUInt32();
                    // TODO: Check if the key exists
                    decryptionKey = this.Keys[keyId];
                    salt = reader.ReadBytes(SaltSize);
                    // Read the underlaying stream to end
                    encryptedSecret = stream.ReadToEnd();    
                }
            }
            // Decrypt
            byte[] decryptedSecret;
            switch (encryptionType)
            {
                case SecretEncryptionType.DatabaseRC4WithSalt:
                    decryptedSecret = DecryptUsingRC4(encryptedSecret, salt, decryptionKey);
                    break;
                default:
                    // TODO: Extract as resource
                    var ex = new FormatException("Unsupported encryption type.");
                    ex.Data.Add("SecretEncryptionType", encryptionType);
                    throw ex;
            }

            return decryptedSecret;
        }

        private void ParsePekList(byte[] cleartextBlob)
        {
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
                    // TODO: Write version corresponding to the DB or original version!!!
                    writer.Write((uint)PekListVersion.W2k);
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

        private byte[] DecryptPekList(byte[] encryptedBlob, byte[] bootKey)
        {
            // Blob structure: Version (4B), Flags (4B), Salt (16B), Encrypted PEK List
            Validator.AssertMinLength(encryptedBlob, EncryptedPekListOffset + 1, "blob");
            Validator.AssertLength(bootKey, BootKeyRetriever.BootKeyLength, "bootKey");

            // Parse blob
            byte[] salt;
            PekListFlags flags;
            byte[] encryptedPekList;
            using (var stream = new MemoryStream(encryptedBlob))
            {
                using (var reader = new BinaryReader(stream))
                {
                    this.Version = (PekListVersion) reader.ReadUInt32();
                    flags = (PekListFlags) reader.ReadUInt32();
                    salt = reader.ReadBytes(SaltSize);
                    encryptedPekList = stream.ReadToEnd();
                }
            }

            // Decrypt 
            byte[] decryptedPekList;
            switch(flags)
            {
                case PekListFlags.Encrypted:
                    // Decrypt
                    switch(this.Version)
                    {
                        case PekListVersion.W2k:
                            decryptedPekList = DecryptUsingRC4(encryptedPekList, salt, bootKey, BootKeySaltHashRounds);
                            break;
                        default:
                            // TODO: Extract as resource.
                            throw new FormatException("Unsupported PEK list version.");
                    }                    
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
