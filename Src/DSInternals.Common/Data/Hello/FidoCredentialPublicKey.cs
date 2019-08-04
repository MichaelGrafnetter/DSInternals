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
                            Modulus = _cpk[CBORObject.FromObject(COSE.KeyTypeParameter.N)].GetByteString(),
                            Exponent = _cpk[CBORObject.FromObject(COSE.KeyTypeParameter.E)].GetByteString()
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
                        X = _cpk[CBORObject.FromObject(COSE.KeyTypeParameter.X)].GetByteString(),
                        Y = _cpk[CBORObject.FromObject(COSE.KeyTypeParameter.Y)].GetByteString(),
                    };
                    ECCurve curve;
                    var crv = (COSE.EllipticCurve)_cpk[CBORObject.FromObject(COSE.KeyTypeParameter.Crv)].AsInt32();
                    switch (Algorithm)
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
                            throw new ArgumentOutOfRangeException(string.Format("Missing or unknown alg {0}", Algorithm.ToString()));
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
                    switch (Algorithm) // https://www.iana.org/assignments/cose/cose.xhtml#algorithms
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
                            throw new ArgumentOutOfRangeException(string.Format("Missing or unknown alg {0}", Algorithm.ToString()));
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
                    switch (Algorithm) // https://www.iana.org/assignments/cose/cose.xhtml#algorithms
                    {
                        case COSE.Algorithm.EdDSA:
                            var crv = (COSE.EllipticCurve)_cpk[CBORObject.FromObject(COSE.KeyTypeParameter.Crv)].AsInt32();
                            switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                            {
                                case COSE.EllipticCurve.Ed25519:
                                    var publicKey = _cpk[CBORObject.FromObject(COSE.KeyTypeParameter.X)].GetByteString();
                                    return publicKey;
                                default:
                                    throw new ArgumentOutOfRangeException(string.Format("Missing or unknown crv {0}", crv.ToString()));
                            }
                        default:
                            throw new ArgumentOutOfRangeException(string.Format("Missing or unknown alg {0}", Algorithm.ToString()));
                    }
                }
                return null;
            }
        }
        public COSE.KeyType Type;
        public COSE.Algorithm Algorithm;
        internal CBORObject _cpk;

        public CredentialPublicKey(CBORObject cpk)
        {
            _cpk = cpk;
            this.Type = (COSE.KeyType) cpk[CBORObject.FromObject(COSE.KeyCommonParameter.KeyType)].AsInt32();
            this.Algorithm = (COSE.Algorithm) cpk[CBORObject.FromObject(COSE.KeyCommonParameter.Alg)].AsInt32();
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
