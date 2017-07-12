using DSInternals.Common;
using DSInternals.Common.Cryptography;
using System;

namespace DSInternals.Replication
{
    public class ReplicationSecretDecryptor : DirectorySecretDecryptor
    {
        private const int SaltOffset = 0;
        private const int EncryptedBlobMinSize = SaltSize + 1;

        private byte[] key;

        public override byte[] CurrentKey
        {
            get
            {
                return this.key;
            }
        }

        public override SecretEncryptionType EncryptionType
        {
            get
            {
                return SecretEncryptionType.ReplicationRC4WithSalt;
            }
        }

        public ReplicationSecretDecryptor(byte[] key)
        {
            Validator.AssertNotNull(key, "key");
            // Session key size: NTLM - 16B, Kerberos - 32B
            Validator.AssertMinLength(key, KeySize, "key");
            this.key = key;
        }

        public override byte[] DecryptSecret(byte[] blob)
        {
            // Blob structure: Salt (16B), Encrypted secret (rest)
            Validator.AssertMinLength(blob, EncryptedBlobMinSize, "blob");

            // Extract salt and the actual encrypted data from the blob
            byte[] salt = blob.Cut(SaltOffset, SaltSize);
            byte[] encryptedSecret = blob.Cut(SaltOffset + SaltSize);

            // Perform decryption
            byte[] decryptedBlob = DecryptUsingRC4(encryptedSecret, salt, this.CurrentKey);

            // The blob is prepended with CRC
            byte[] decryptedSecret;
            uint expectedCrc = BitConverter.ToUInt32(decryptedBlob, 0);
            decryptedSecret = decryptedBlob.Cut(sizeof(uint));
            Validator.AssertCrcMatches(decryptedSecret, expectedCrc);

            return decryptedSecret;
        }

        public override byte[] EncryptSecret(byte[] secret)
        {
            throw new NotImplementedException("We will never act as a replication source so secret encryption is out of scope.");
        }
    }
}