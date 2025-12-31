namespace DSInternals.Common.Data.Fido;

/// <summary>
/// CBOR Object Signing and Encryption RFC8152 https://tools.ietf.org/html/rfc8152
/// </summary>
public static class COSE
{
    /// <summary>
    /// COSE Algorithms https://www.iana.org/assignments/cose/cose.xhtml#algorithms
    /// </summary>
    public enum Algorithm
    {
        /// <summary> 
        /// RSASSA-PKCS1-v1_5 w/ SHA-1
        /// </summary>
        RS1 = -65535,
        /// <summary> 
        /// RSASSA-PKCS1-v1_5 w/ SHA-512
        /// </summary>
        RS512 = -259,
        /// <summary> 
        /// RSASSA-PKCS1-v1_5 w/ SHA-384
        /// </summary>
        RS384 = -258,
        /// <summary> 
        /// RSASSA-PKCS1-v1_5 w/ SHA-256
        /// </summary>
        RS256 = -257,
        /// <summary> 
        /// RSASSA-PSS w/ SHA-512
        /// </summary>
        PS512 = -39,
        /// <summary> 
        /// RSASSA-PSS w/ SHA-384
        /// </summary>
        PS384 = -38,
        /// <summary> 
        /// RSASSA-PSS w/ SHA-256
        /// </summary>
        PS256 = -37,
        /// <summary> 
        /// ECDSA w/ SHA-512
        /// </summary>
        ES512 = -36,
        /// <summary> 
        /// ECDSA w/ SHA-384
        /// </summary>
        ES384 = -35,
        /// <summary> 
        /// EdDSA
        /// </summary>
        EdDSA = -8,
        /// <summary> 
        /// ECDSA w/ SHA-256
        /// </summary>
        ES256 = -7
    }

    /// <summary>
    /// COSE Key Common Parameters https://www.iana.org/assignments/cose/cose.xhtml#key-common-parameters
    /// </summary>
    public enum KeyCommonParameter : uint
    {
        /// <summary> 
        /// This value is reserved
        /// </summary>
        Reserved = 0,
        /// <summary> 
        /// Identification of the key type	
        /// </summary>
        KeyType = 1,
        /// <summary> 
        /// Key identification value - match to kid in message	
        /// </summary>
        KeyId = 2,
        /// <summary> 
        /// Key usage restriction to this algorithm	
        /// </summary>
        Alg = 3,
        /// <summary> 
        /// Restrict set of permissible operations	
        /// </summary>
        KeyOps = 4,
        /// <summary> 
        /// Base IV to be XORed with Partial IVs	
        /// </summary>
        BaseIV = 5
    }

    /// <summary>
    /// COSE Key Type Parameters https://www.iana.org/assignments/cose/cose.xhtml#key-type-parameters
    /// </summary>
    public enum KeyTypeParameter : int
    {
        /// <summary> 
        /// EC identifier
        /// </summary>
        Crv = -1,
        /// <summary> 
        /// Key Value
        /// </summary>
        K = -1,
        /// <summary> 
        /// x-coordinate
        /// </summary>
        X = -2,
        /// <summary> 
        /// y-coordinate	
        /// </summary>
        Y = -3,
        /// <summary> 
        /// the RSA modulus n	
        /// </summary>
        N = -1,
        /// <summary> 
        /// the RSA public exponent e	
        /// </summary>
        E = -2
    }

    /// <summary>
    /// COSE Key Types https://www.iana.org/assignments/cose/cose.xhtml#key-type
    /// </summary>
    public enum KeyType : uint
    {
        /// <summary>
        /// This value is reserved
        /// </summary>
        Reserved = 0,
        /// <summary> 
        /// Octet Key Pair
        /// </summary>
        OKP = 1,
        /// <summary>
        /// Elliptic Curve Keys w/ x- and y-coordinate pair
        /// </summary>
        EC2 = 2,
        /// <summary> 
        /// RSA Key
        /// </summary>
        RSA = 3,
        /// <summary> 
        /// Symmetric Keys
        /// </summary>
        Symmetric = 4
    }

    /// <summary>
    /// COSE Elliptic Curves https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
    /// </summary>
    public enum EllipticCurve : uint
    {
        /// <summary> 
        /// This value is reserved
        /// </summary>
        Reserved = 0,
        /// <summary> 
        /// NIST P-256 also known as secp256r1
        /// </summary>
        P256 = 1,
        /// <summary> 
        /// NIST P-384 also known as secp384r1
        /// </summary>
        P384 = 2,
        /// <summary> 
        /// NIST P-521 also known as secp521r1
        /// </summary>
        P521 = 3,
        /// <summary> 
        /// X25519 for use w/ ECDH only
        /// </summary>
        X25519 = 4,
        /// <summary> 
        /// X448 for use w/ ECDH only
        /// </summary>
        X448 = 5,
        /// <summary> 
        /// Ed25519 for use w/ EdDSA only
        /// </summary>
        Ed25519 = 6,
        /// <summary> 
        /// Ed448 for use w/ EdDSA only
        /// </summary>
        Ed448 = 7,
        /// <summary> 
        /// secp256k1 (pending IANA - requested assignment 8)
        /// </summary>
        P256K = 8
    }
}
