namespace DSInternals.Common.Cryptography
{
    public enum SecretEncryptionType : ushort
    {
        // TODO: Add support for SAM encryption types

        /// <summary>
        /// Database secret encryption using PEK without salt.
        /// </summary>
        /// <remarks>Used until Windows Server 2000 Beta 2</remarks>
        DatabaseRC4 = 0x10,

        /// <summary>
        /// Database secret encryption using PEK with salt.
        /// </summary>
        /// <remarks>Used in Windows Server 2000 - Windows Server 2012 R2.</remarks>
        DatabaseRC4WithSalt = 0x11,

        /// <summary>
        /// Replicated secret encryption using Session Key with salt.
        /// </summary>
        ReplicationRC4WithSalt = 0x12,

        /// <summary>
        /// Database secret encryption using PEK and AES.
        /// </summary>
        /// <remarks>Used since Windows Server 2016 TP4.</remarks>
        DatabaseAES = 0x13
    }
}
