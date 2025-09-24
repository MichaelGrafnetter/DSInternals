using DSInternals.Common;
using DSInternals.Common.Cryptography;

namespace DSInternals.Replication
{
    /// <summary>
    /// Provides functionality to decrypt secrets (passwords) obtained via replication.
    /// </summary>
    public class ReplicationSecretDecryptor : DirectorySecretDecryptor
    {
        /// <summary>
        /// The offset of the salt in the encrypted blob.
        /// </summary>
        private const int SaltOffset = 0;

        /// <summary>
        /// The minimum size of the encrypted blob.
        /// </summary>
        private const int EncryptedBlobMinSize = SaltSize + 1;

        /// <summary>
        /// Decryption key.
        /// </summary>
        private byte[] key;

        /// <summary>
        /// Gets the current decryption key.
        /// </summary>
        public override byte[] CurrentKey => this.key;

        /// <summary>
        /// Gets the encryption algorithm used to protect the secrets.
        /// </summary>
        public override SecretEncryptionType EncryptionType => SecretEncryptionType.ReplicationRC4WithSalt;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplicationSecretDecryptor"/> class.
        /// </summary>
        /// <param name="key">The decryption key.</param>
        public ReplicationSecretDecryptor(byte[] key)
        {
            Validator.AssertNotNull(key, "key");
            // Session key size: NTLM - 16B, Kerberos - 32B
            Validator.AssertMinLength(key, KeySize, "key");
            this.key = key;
        }

        /// <summary>
        /// Decrypts the provided secret blob.
        /// </summary>
        /// <param name="blob">The secret blob to decrypt.</param>
        /// <returns>The decrypted secret.</returns>
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

        /// <summary>
        /// Encrypts the provided secret.
        /// </summary>
        /// <exception cref="NotImplementedException">This method is not implemented.</exception>
        public override byte[] EncryptSecret(byte[] secret)
        {
            throw new NotImplementedException("We will never act as a replication source so secret encryption is out of scope.");
        }
    }
}
