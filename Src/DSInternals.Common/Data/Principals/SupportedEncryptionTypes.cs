using System;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Supported Encryption Types Bit Flags
    /// </summary>
    /// <see>https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-kile/6cfc7b50-11ed-4b4d-846d-6f08f0812919</see>
    [Flags]
    public enum SupportedEncryptionTypes : uint
    {
        Default = 0,

        /// <summary>
        /// Data Encryption Standard with Cipher Block Chaining using the Cyclic Redundancy Check function
        /// </summary>
        DES_CBC_CRC = 1,

        /// <summary>
        /// Data Encryption Standard with Cipher Block Chaining using the Message-Digest algorithm 5 checksum function
        /// </summary>
        DES_CBC_MD5 = 2,

        /// <summary>
        /// Rivest Cipher 4 with Hashed Message Authentication Code using the Message-Digest algorithm 5 checksum function
        /// </summary>
        RC4_HMAC = 4,

        /// <summary>
        /// Advanced Encryption Standard in 128-bit cipher block with Hashed Message Authentication Code using the Secure Hash Algorithm (1)
        /// </summary>
        AES128_CTS_HMAC_SHA1_96 = 8,

        /// <summary>
        /// Advanced Encryption Standard in 256-bit cipher block with Hashed Message Authentication Code using the Secure Hash Algorithm (1)
        /// </summary>
        AES256_CTS_HMAC_SHA1_96 = 16,

        /// <summary>
        /// Advanced Encryption Standard in 128-bit cipher block with Hashed Message Authentication Code using the Secure Hash Algorithm (2)
        /// </summary>
        AES128_CTS_HMAC_SHA256_128 = 32,

        /// <summary>
        /// Advanced Encryption Standard in 256-bit cipher block with Hashed Message Authentication Code using the Secure Hash Algorithm (2)
        /// </summary>
        AES256_CTS_HMAC_SHA384_192 = 64,

        /// <summary>
        /// Flexible Authentication Secure Tunneling (FAST) supported
        /// </summary>
        FAST = 0x10000,

        /// <summary>
        /// Compound identity supported
        /// </summary>
        CompoundIdentity = 0x20000,

        /// <summary>
        /// Claims supported
        /// </summary>
        Claims = 0x40000,

        /// <summary>
        /// Resource SID compression disabled
        /// </summary>
        ResourceSidCompressionDisabled = 0x80000
    }
}
