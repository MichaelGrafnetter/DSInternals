namespace DSInternals.Common.Data.Fido
{
    using System;
    using PeterO.Cbor;
    using System.Security.Cryptography;
    public class CredentialPublicKey
    {
        public RSACng RSA
        {
            get
            {
                if (Type == COSE.KeyType.RSA)
                {
                    var rsa = new RSACng();
                    rsa.ImportParameters(
                        new RSAParameters()
                        {
                            Modulus = _cpk[CBORObject.FromObject(COSE.KeyTypeParameter.n)].GetByteString(),
                            Exponent = _cpk[CBORObject.FromObject(COSE.KeyTypeParameter.e)].GetByteString()
                        }
                    );
                    return rsa;
                }
                return null;
            }
        }
        public ECDsa ECDsa
        {
            get
            {
                if (Type == COSE.KeyType.EC2)
                {
                    var point = new ECPoint
                    {
                        X = _cpk[CBORObject.FromObject(COSE.KeyTypeParameter.x)].GetByteString(),
                        Y = _cpk[CBORObject.FromObject(COSE.KeyTypeParameter.y)].GetByteString(),
                    };
                    ECCurve curve;
                    var crv = (COSE.EllipticCurve)_cpk[CBORObject.FromObject(COSE.KeyTypeParameter.crv)].AsInt32();
                    switch (Alg)
                    {
                        case COSE.Algorithm.ES256:
                            switch (crv)
                            {
                                case COSE.EllipticCurve.P256:
                                case COSE.EllipticCurve.P256K:
                                    curve = ECCurve.NamedCurves.nistP256;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException(string.Format("Missing or unknown crv {0}", crv.ToString()));
                            }
                            break;
                        case COSE.Algorithm.ES384:
                            switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                            {
                                case COSE.EllipticCurve.P384:
                                    curve = ECCurve.NamedCurves.nistP384;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException(string.Format("Missing or unknown crv {0}", crv.ToString()));
                            }
                            break;
                        case COSE.Algorithm.ES512:
                            switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                            {
                                case COSE.EllipticCurve.P521:
                                    curve = ECCurve.NamedCurves.nistP521;
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException(string.Format("Missing or unknown crv {0}", crv.ToString()));
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(string.Format("Missing or unknown alg {0}", Alg.ToString()));
                    }
                    return ECDsa.Create(new ECParameters
                    {
                        Q = point,
                        Curve = curve
                    });
                }
                return null;
            }
        }

        public RSASignaturePadding Padding
        {
            get
            {
                if (Type == COSE.KeyType.RSA)
                {
                    switch (Alg) // https://www.iana.org/assignments/cose/cose.xhtml#algorithms
                    {
                        case COSE.Algorithm.PS256:
                        case COSE.Algorithm.PS384:
                        case COSE.Algorithm.PS512:
                            return RSASignaturePadding.Pss;

                        case COSE.Algorithm.RS1:
                        case COSE.Algorithm.RS256:
                        case COSE.Algorithm.RS384:
                        case COSE.Algorithm.RS512:
                            return RSASignaturePadding.Pkcs1;
                        default:
                            throw new ArgumentOutOfRangeException(string.Format("Missing or unknown alg {0}", Alg.ToString()));
                    }
                }
                return null;
            }
        }

        public byte[] EdDSAPublicKey
        {
            get
            {
                if (Type == COSE.KeyType.OKP)
                {
                    switch (Alg) // https://www.iana.org/assignments/cose/cose.xhtml#algorithms
                    {
                        case COSE.Algorithm.EdDSA:
                            var crv = (COSE.EllipticCurve)_cpk[CBORObject.FromObject(COSE.KeyTypeParameter.crv)].AsInt32();
                            switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                            {
                                case COSE.EllipticCurve.Ed25519:
                                    var publicKey = _cpk[CBORObject.FromObject(COSE.KeyTypeParameter.x)].GetByteString();
                                    return publicKey;
                                default:
                                    throw new ArgumentOutOfRangeException(string.Format("Missing or unknown crv {0}", crv.ToString()));
                            }
                        default:
                            throw new ArgumentOutOfRangeException(string.Format("Missing or unknown alg {0}", Alg.ToString()));
                    }
                }
                return null;
            }
        }
        public COSE.KeyType Type;
        public COSE.Algorithm Alg;
        internal CBORObject _cpk;

        public CredentialPublicKey(CBORObject cpk)
        {
            _cpk = cpk;
            Type = (COSE.KeyType) cpk[CBORObject.FromObject(COSE.KeyCommonParameter.kty)].AsInt32();
            Alg = (COSE.Algorithm) cpk[CBORObject.FromObject(COSE.KeyCommonParameter.alg)].AsInt32();
        }
        public override string ToString()
        {
            return _cpk.ToString();
        }
        public byte[] GetBytes()
        {
            return _cpk.EncodeToBytes();
        }
    }
}
