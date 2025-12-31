using System.Security.Cryptography;

namespace DSInternals.Common.Data.Fido;
/// <summary>
/// Represents a FIDO2/WebAuthn credential public key encoded in COSE format.
/// </summary>
/// <seealso href="https://www.w3.org/TR/webauthn/#credential-public-key"/>
public sealed class CredentialPublicKey : IDisposable
{
    /// <summary>
    /// Gets the COSE key type.
    /// </summary>
    public COSE.KeyType Type { get; private set; }

    /// <summary>
    /// Gets the COSE algorithm identifier.
    /// </summary>
    public COSE.Algorithm Algorithm { get; private set; }

    /// <summary>
    /// Gets the asymmetric public key.
    /// </summary>
    public AsymmetricAlgorithm PublicKey { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CredentialPublicKey"/> class.
    /// </summary>
    /// <param name="type">The COSE key type.</param>
    /// <param name="algorithm">The COSE algorithm identifier.</param>
    /// <param name="publicKey">The asymmetric public key.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="publicKey"/> parameter is <see langword="null"/>.</exception>
    public CredentialPublicKey(COSE.KeyType type, COSE.Algorithm algorithm, AsymmetricAlgorithm publicKey)
    {
        Type = type;
        Algorithm = algorithm;
        PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
    }

    /// <summary>
    /// Parses a credential public key from CBOR-encoded data.
    /// </summary>
    /// <param name="cpkData">The CBOR-encoded credential public key data.</param>
    /// <returns>A tuple containing the parsed <see cref="CredentialPublicKey"/> and the number of bytes read.</returns>
    /// <exception cref="CryptographicException">The credential public key algorithm is not specified or the key type is not supported.</exception>
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
                    COSE.EllipticCurve.P256 => ECCurve.NamedCurves.nistP256,
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

    public void Dispose()
    {
        PublicKey?.Dispose();
        PublicKey = null;
    }
}
