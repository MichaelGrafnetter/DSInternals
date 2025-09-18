namespace DSInternals.Common.Data.Fido;

using System;
using System.Security.Cryptography;

public class CredentialPublicKey
{
    public COSE.KeyType Type { get; private set; }
    public COSE.Algorithm Algorithm { get; private set; }
    public AsymmetricAlgorithm PublicKey { get; private set; }

    public CredentialPublicKey(COSE.KeyType type, COSE.Algorithm algorithm, AsymmetricAlgorithm publicKey)
    {
        if (publicKey == null)
        {
            throw new ArgumentNullException(nameof(publicKey));
        }

        Type = type;
        Algorithm = algorithm;
        PublicKey = publicKey;
    }

    public static (CredentialPublicKey publicKey, int bytesRead) Parse(ReadOnlyMemory<byte> cpkData)
    {
        (CborMap map, int bytesRead) = CborMap.Parse(cpkData);

        COSE.KeyType? kty = ReadKeyType(map);
        COSE.Algorithm? alg = ReadAlgorithm(map);

        if (alg == null)
        {
            throw new CryptographicException("Credential public key algorithm is not specified.");
        }

        AsymmetricAlgorithm publicKey;

        switch (kty)
        {
            case COSE.KeyType.EC2:
                COSE.EllipticCurve? crv = ReadCurve(map);
                // TODO: Test that the algorithm matches the curve
                ECCurve curve = crv switch
                {
                    COSE.EllipticCurve.P256 or COSE.EllipticCurve.P256K => ECCurve.NamedCurves.nistP256,
                    COSE.EllipticCurve.P384 => ECCurve.NamedCurves.nistP384,
                    COSE.EllipticCurve.P521 => ECCurve.NamedCurves.nistP521,
                    _ => throw new CryptographicException($"Unsupported curve {crv}.")
                };
                var point = new ECPoint
                {
                    X = ReadKeyParameter(map, COSE.KeyTypeParameter.X),
                    Y = ReadKeyParameter(map, COSE.KeyTypeParameter.Y),
                };
                var eccPublicKey = new ECParameters
                {
                    Q = point,
                    Curve = curve
                };
                publicKey = ECDsa.Create(eccPublicKey);
                break;
            case COSE.KeyType.RSA:
                var rsaPublicKey = new RSAParameters()
                {
                    Modulus = ReadKeyParameter(map, COSE.KeyTypeParameter.N),
                    Exponent = ReadKeyParameter(map, COSE.KeyTypeParameter.E)
                };
                publicKey = RSA.Create(rsaPublicKey);
                break;
            default:
                throw new CryptographicException($"Unsupported key type {kty}.");
        }

        var cpk = new CredentialPublicKey(kty.Value, alg.Value, publicKey);
        return (cpk, bytesRead);
    }

    private static COSE.KeyType? ReadKeyType(CborMap map) => (COSE.KeyType?)(map[COSE.KeyCommonParameter.KeyType] as uint?);
    private static COSE.Algorithm? ReadAlgorithm(CborMap map) => (COSE.Algorithm?)(map[COSE.KeyCommonParameter.Alg] as int?);
    private static COSE.EllipticCurve? ReadCurve(CborMap map) => (COSE.EllipticCurve?)(map[COSE.KeyTypeParameter.Crv] as uint?);
    private static byte[]? ReadKeyParameter(CborMap map, COSE.KeyTypeParameter parameter) => map[parameter] as byte[];
}
