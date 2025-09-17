
namespace DSInternals.Common.Data
{
    /// <summary>
    /// Key types corresponding to various kerberos encryption types, as defined in ntsecapi.h.
    /// </summary>
    public enum KerberosKeyType : int
    {
        /// <summary>
        /// No encryption.
        /// </summary>
        NULL =                             0,
        
        /// <summary>
        /// DES encryption with CBC mode and CRC integrity check.
        /// </summary>
        DES_CBC_CRC =                      1,
        
        /// <summary>
        /// DES encryption with CBC mode and MD4 integrity check.
        /// </summary>
        DES_CBC_MD4 =                      2,
        
        /// <summary>
        /// DES encryption with CBC mode and MD5 integrity check.
        /// </summary>
        DES_CBC_MD5 =                      3,
        
        /// <summary>
        /// Triple DES encryption with CBC mode and MD5 integrity check.
        /// </summary>
        DES3_CBC_MD5 =                     5,
        
        /// <summary>
        /// Old Triple DES encryption with CBC mode and SHA1 integrity check.
        /// </summary>
        OLD_DES3_CBC_SHA1 =                7,
        
        /// <summary>
        /// DSA signature generation algorithm.
        /// </summary>
        SIGN_DSA_GENERATE =                8,
        
        /// <summary>
        /// RSA private key encryption.
        /// </summary>
        ENCRYPT_RSA_PRIV =                 9,
        
        /// <summary>
        /// RSA public key encryption.
        /// </summary>
        ENCRYPT_RSA_PUB =                 10,
        
        /// <summary>
        /// Triple DES encryption with CBC mode and SHA1 integrity check.
        /// </summary>
        DES3_CBC_SHA1 =                   16,
        
        /// <summary>
        /// AES 128-bit encryption with CTS mode and HMAC-SHA1-96 integrity check.
        /// </summary>
        AES128_CTS_HMAC_SHA1_96 =         17,
        
        /// <summary>
        /// AES 256-bit encryption with CTS mode and HMAC-SHA1-96 integrity check.
        /// </summary>
        AES256_CTS_HMAC_SHA1_96 =         18,
        
        /// <summary>
        /// AES 128-bit encryption with CTS mode and HMAC-SHA256-128 integrity check.
        /// </summary>
        AES128_CTS_HMAC_SHA256_128 =      19,
        
        /// <summary>
        /// AES 256-bit encryption with CTS mode and HMAC-SHA384-192 integrity check.
        /// </summary>
        AES256_CTS_HMAC_SHA384_192 =      20,
        
        /// <summary>
        /// RC4 encryption with HMAC using NT hash.
        /// </summary>
        RC4_HMAC_NT =                     23,
        
        /// <summary>
        /// RC4 encryption with HMAC using NT hash (export strength).
        /// </summary>
        RC4_HMAC_NT_EXP =                 24,
        
        /// <summary>
        /// Public key cross-realm authentication.
        /// </summary>
        PK_CROSS =                        48,
        
        /// <summary>
        /// RC4 encryption with MD4 hash (Microsoft-specific).
        /// </summary>
        RC4_MD4 =                       -128,
        
        /// <summary>
        /// RC4 plain encryption version 2 (Microsoft-specific).
        /// </summary>
        RC4_PLAIN2 =                    -129,
        
        /// <summary>
        /// RC4 encryption with LM hash (Microsoft-specific).
        /// </summary>
        RC4_LM =                        -130,
        
        /// <summary>
        /// RC4 encryption with SHA hash (Microsoft-specific).
        /// </summary>
        RC4_SHA =                       -131,
        
        /// <summary>
        /// DES plain encryption (Microsoft-specific).
        /// </summary>
        DES_PLAIN =                     -132,
        
        /// <summary>
        /// RC4 HMAC old format (Microsoft-specific).
        /// </summary>
        RC4_HMAC_OLD =                  -133,
        
        /// <summary>
        /// RC4 plain old format (Microsoft-specific).
        /// </summary>
        RC4_PLAIN_OLD =                 -134,
        
        /// <summary>
        /// RC4 HMAC old format with export strength (Microsoft-specific).
        /// </summary>
        RC4_HMAC_OLD_EXP =              -135,
        
        /// <summary>
        /// RC4 plain old format with export strength (Microsoft-specific).
        /// </summary>
        RC4_PLAIN_OLD_EXP =             -136,
        
        /// <summary>
        /// RC4 plain encryption (Microsoft-specific).
        /// </summary>
        RC4_PLAIN =                     -140,
        
        /// <summary>
        /// RC4 plain encryption with export strength (Microsoft-specific).
        /// </summary>
        RC4_PLAIN_EXP =                 -141,
        
        /// <summary>
        /// AES 128-bit CTS with HMAC-SHA1-96 in plain format (Microsoft-specific).
        /// </summary>
        AES128_CTS_HMAC_SHA1_96_PLAIN = -148,
        
        /// <summary>
        /// AES 256-bit CTS with HMAC-SHA1-96 in plain format (Microsoft-specific).
        /// </summary>
        AES256_CTS_HMAC_SHA1_96_PLAIN = -149
    }
}
