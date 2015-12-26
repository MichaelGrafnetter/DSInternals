namespace DSInternals.Common.Cryptography
{
    public enum SecretEncryptionType : ushort
    {
        // TODO: Add support for SAM encryption types

        /// <summary>
        /// Database secret encryption using PEK without salt.
        /// </summary>
        /// <remarks>Used until Windows Server 2000 Beta 2</remarks>
        DatabaseSecret = 0x10,

        /// <summary>
        /// Database secret encryption using PEK with salt.
        /// </summary>
        DatabaseSecretWithSalt = 0x11,

        /// <summary>
        /// Replicated secret encryption using Session Key with salt.
        /// </summary>
        ReplicationSecretWithSalt = 0x12
    }
}
