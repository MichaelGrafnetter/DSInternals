
namespace DSInternals.Common.Data
{
    /// <summary>
    /// Key types corresponding to various kerberos encryption types, as defined in ntsecapi.h.
    /// </summary>
    public enum KerberosKeyType : int
    {
        NULL =                             0,
        DES_CBC_CRC =                      1,
        DES_CBC_MD4 =                      2,
        DES_CBC_MD5 =                      3,
        DES3_CBC_MD5 =                     5,
        OLD_DES3_CBC_SHA1 =                7,
        SIGN_DSA_GENERATE =                8,
        ENCRYPT_RSA_PRIV =                 9,
        ENCRYPT_RSA_PUB =                 10,
        DES3_CBC_SHA1 =                   16,
        AES128_CTS_HMAC_SHA1_96 =         17,
        AES256_CTS_HMAC_SHA1_96 =         18,
        AES128_CTS_HMAC_SHA256_128 =      19,
        AES256_CTS_HMAC_SHA384_192 =      20,
        RC4_HMAC_NT =                     23,
        RC4_HMAC_NT_EXP =                 24,
        PK_CROSS =                        48,
        RC4_MD4 =                       -128,
        RC4_PLAIN2 =                    -129,
        RC4_LM =                        -130,
        RC4_SHA =                       -131,
        DES_PLAIN =                     -132,
        RC4_HMAC_OLD =                  -133,
        RC4_PLAIN_OLD =                 -134,
        RC4_HMAC_OLD_EXP =              -135,
        RC4_PLAIN_OLD_EXP =             -136,
        RC4_PLAIN =                     -140,
        RC4_PLAIN_EXP =                 -141,
        AES128_CTS_HMAC_SHA1_96_PLAIN = -148,
        AES256_CTS_HMAC_SHA1_96_PLAIN = -149
    }
}
