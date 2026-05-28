using DSInternals.Common;
using DSInternals.Common.Cryptography;

namespace DSInternals.Replication;

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
    /// Decryption keys, ordered most-recently-negotiated first.
    /// </summary>
    /// <remarks>
    /// The RPC session key can be renegotiated mid-replication, so secrets within a single
    /// replication stream may be encrypted under different keys. We therefore keep every key we
    /// have seen and try them newest-first when decrypting.
    /// </remarks>
    private readonly List<byte[]> sessionKeys;

    /// <summary>
    /// Gets the most recently negotiated decryption key.
    /// </summary>
    public override byte[] CurrentKey => this.sessionKeys[0];

    /// <summary>
    /// Gets the encryption algorithm used to protect the secrets.
    /// </summary>
    public override SecretEncryptionType EncryptionType => SecretEncryptionType.ReplicationRC4WithSalt;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReplicationSecretDecryptor"/> class.
    /// </summary>
    /// <param name="initialSessionKey">The session key in effect when the decryptor is created.</param>
    public ReplicationSecretDecryptor(byte[] initialSessionKey)
    {
        ArgumentNullException.ThrowIfNull(initialSessionKey);
        // Session key size: NTLM - 16B, Kerberos - 32B
        Validator.AssertMinLength(initialSessionKey, KeySize);
        this.sessionKeys = new List<byte[]>() { initialSessionKey };
    }

    /// <summary>
    /// Registers a newly negotiated session key as the preferred decryption key.
    /// </summary>
    /// <param name="newKey">The new session key.</param>
    /// <remarks>
    /// Previously seen keys are retained so that secrets encrypted before the renegotiation can
    /// still be decrypted. This method does not throw, as it is invoked from an event raised inside
    /// the native RPC security callback.
    /// </remarks>
    public void ChangeSessionKey(byte[] newKey)
    {
        if (newKey != null)
        {
            this.sessionKeys.Insert(0, newKey);
        }
    }

    /// <summary>
    /// Decrypts the provided secret blob.
    /// </summary>
    /// <param name="blob">The secret blob to decrypt.</param>
    /// <returns>The decrypted secret.</returns>
    public override byte[] DecryptSecret(byte[] blob)
    {
        // Blob structure: Salt (16B), Encrypted secret (rest)
        Validator.AssertMinLength(blob, EncryptedBlobMinSize);

        // Extract salt and the actual encrypted data from the blob
        byte[] salt = blob.Cut(SaltOffset, SaltSize);
        byte[] encryptedSecret = blob.Cut(SaltOffset + SaltSize);

        // The session key may have been renegotiated mid-replication, so try the keys we have seen
        // newest-first and accept the first one that yields a valid CRC.
        foreach (byte[] sessionKey in this.sessionKeys)
        {
            byte[] decryptedBlob = DecryptUsingRC4(encryptedSecret, salt, sessionKey);

            // The blob is prepended with CRC
            uint expectedCrc = BitConverter.ToUInt32(decryptedBlob, 0);
            byte[] decryptedSecret = decryptedBlob.Cut(sizeof(uint));
            if (Crc32.Calculate(decryptedSecret) == expectedCrc)
            {
                return decryptedSecret;
            }
        }

        // None of the known session keys produced a valid CRC.
        throw new FormatException("Secret decryption failed: CRC check did not match for any known session key.");
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
