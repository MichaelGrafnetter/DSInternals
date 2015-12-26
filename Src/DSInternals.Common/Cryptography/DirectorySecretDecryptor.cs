using DSInternals.Common.Interop;
using System;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography
{
    /// <summary>
    /// Base class for SessionKey (used in DRS) and PEK List (used in ntds.dit).
    /// </summary>
    public abstract class DirectorySecretDecryptor
    {
        private const int HashSize = NativeMethods.NTHashNumBytes;
        
        protected const int KeySize = 16;
        protected const int SaltSize = 16;
        protected const int DefaultSaltHashRounds = 1;
        public abstract byte[] CurrentKey
        {
            get;
        }

        /// <summary>
        /// Decrypts a generic secret retrieved either from the DB or through replication.
        /// </summary>
        /// <param name="blob">Encrypted blob.</param>
        /// <returns>Decrypted data.</returns>
        public abstract byte[] DecryptSecret(byte[] blob);

        public abstract SecretEncryptionType EncryptionType
        {
            get;
        }

        public byte[] DecryptHash(byte[] blob, int rid)
        {
            // Decrypt layer 1:
            byte[] partiallyDecryptedHash = this.DecryptSecret(blob);
            // TODO: Validate decrypted hash size

            // Hashes are encrypted using RID in addition to the standard secret encryption:
            return DecryptUsingDES(partiallyDecryptedHash, rid);
        }

        public byte[][] DecryptHashHistory(byte[] blob, int rid)
        {
            // Decrypt layer 1:
            byte[] partiallyDecryptedHashes = this.DecryptSecret(blob);

            // TODO: Validate partiallyDecryptedHashes.Length % HashSize

            // Split the partially decrypted blob to single hashes and decrypt them one by one using DES+RID:
            int hashCount = partiallyDecryptedHashes.Length / HashSize;
            byte[][] result = new byte[hashCount][];
            for (int i = 0; i < hashCount; i++)
            {
                byte[] encryptedHash = partiallyDecryptedHashes.Cut(i * HashSize, HashSize);
                byte[] decryptedHash = DecryptUsingDES(encryptedHash, rid);
                result[i] = decryptedHash;
            }
            return result;
        }

        protected static byte[] DecryptUsingRC4(byte[] data, byte[] salt, byte[] decryptionKey, int saltHashRounds = DefaultSaltHashRounds)
        {
            byte[] rc4Key = ComputeMD5(decryptionKey, salt, saltHashRounds);
            byte[] decryptedData;
            NativeMethods.RtlDecryptData2(data, rc4Key, out decryptedData);
            return decryptedData;
        }

        protected static byte[] EncryptUsingRC4(byte[] data, byte[] salt, byte[] encryptionKey, int saltHashRounds = DefaultSaltHashRounds)
        {
            // Encryption = Decryption with RC4
            return DecryptUsingRC4(data, salt, encryptionKey, saltHashRounds);
        }

        protected static byte[] ComputeMD5(byte[] key, byte[] salt, int saltHashRounds = DefaultSaltHashRounds)
        {
            // TODO: Perform in a MD5 cycle
            int bufferSize = key.Length + salt.Length * saltHashRounds;
            byte[] buffer = new byte[bufferSize];
            // Copy key to buffer
            Buffer.BlockCopy((Array)key, 0, (Array)buffer, 0, key.Length);
            // Copy salt to buffer saltHashRounds-times
            for (int i = 0; i < saltHashRounds; i++)
            {
                int bufferOffset = key.Length + i * salt.Length;
                Buffer.BlockCopy((Array)salt, 0, (Array)buffer, bufferOffset, salt.Length);
            }
            return ComputeMD5(buffer);
        }

        private static byte[] ComputeMD5(byte[] data)
        {
            using (MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                cryptoServiceProvider.ComputeHash(data);
                return cryptoServiceProvider.Hash;
            }
        }

        private static byte[] DecryptUsingDES(byte[] encryptedHash, int rid)
        {
            byte[] decryptedHash;
            NativeMethods.RtlDecryptNtOwfPwdWithIndex(encryptedHash, rid, out decryptedHash);
            return decryptedHash;
        }
    }
}
