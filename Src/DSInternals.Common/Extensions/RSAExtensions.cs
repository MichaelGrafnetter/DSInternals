using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace DSInternals.Common
{
    public static class RSAExtensions
    {
        private const int BCryptKeyBlobHeaderSize = 6 * sizeof(uint);
        private const uint BCryptRSAPublicKeyMagic = 0x31415352; // "RSA1" in ASCII
        private const int TPM20KeyBlobHeaderSize = 4 * sizeof(int) + 9 * sizeof(uint);
        private const uint TPM20PublicKeyMagic = 0x4d504350; // "MPCP" in ASCII
        private const byte DERSequenceTag = 0x30;
        private const int DERPublicKeyMinSize = 260; // At least 2K RSA modulus + 3B public exponent + 1B sequence tag

        /// <summary>
        /// OID 1.2.840.113549.1.1.1 - Identifier for RSA encryption for use with Public Key Cryptosystem One defined by RSA Inc. 
        /// </summary>
        private static readonly Oid RsaOid = Oid.FromFriendlyName("RSA", OidGroup.PublicKeyAlgorithm);

        /// <summary>
        /// ASN.1 Tag NULL
        /// </summary>
        private static readonly AsnEncodedData Asn1Null = new AsnEncodedData(new byte[] { 5, 0 });

        /// <summary>
        /// BCRYPT_PUBLIC_KEY_BLOB Format
        /// </summary>
        private static readonly CngKeyBlobFormat BCryptRSAPublicKeyFormat = new CngKeyBlobFormat("RSAPUBLICBLOB");

        private static readonly BigInteger[] WeakKeyMarkers = {
            new BigInteger(6),
            new BigInteger(30),
            new BigInteger(126),
            new BigInteger(1026),
            new BigInteger(5658),
            new BigInteger(107286),
            new BigInteger(199410),
            new BigInteger(8388606),
            new BigInteger(536870910),
            new BigInteger(2147483646),
            new BigInteger(67109890),
            new BigInteger(2199023255550),
            new BigInteger(8796093022206),
            new BigInteger(140737488355326),
            new BigInteger(5310023542746834),
            new BigInteger(576460752303423486),
            new BigInteger(1455791217086302986),
            BigInteger.Parse("147573952589676412926"),
            BigInteger.Parse("20052041432995567486"),
            BigInteger.Parse("6041388139249378920330"),
            BigInteger.Parse("207530445072488465666"),
            BigInteger.Parse("9671406556917033397649406"),
            BigInteger.Parse("618970019642690137449562110"),
            BigInteger.Parse("79228162521181866724264247298"),
            BigInteger.Parse("2535301200456458802993406410750"),
            BigInteger.Parse("1760368345969468176824550810518"),
            BigInteger.Parse("50079290986288516948354744811034"),
            BigInteger.Parse("473022961816146413042658758988474"),
            BigInteger.Parse("10384593717069655257060992658440190"),
            BigInteger.Parse("144390480366845522447407333004847678774"),
            BigInteger.Parse("2722258935367507707706996859454145691646"),
            BigInteger.Parse("174224571863520493293247799005065324265470"),
            BigInteger.Parse("696898287454081973172991196020261297061886"),
            BigInteger.Parse("713623846352979940529142984724747568191373310"),
            BigInteger.Parse("1800793591454480341970779146165214289059119882"),
            BigInteger.Parse("126304807362733370595828809000324029340048915994"),
            BigInteger.Parse("11692013098647223345629478661730264157247460343806"),
            BigInteger.Parse("187072209578355573530071658587684226515959365500926")
        };

        private static readonly BigInteger[] Primes = {
            new BigInteger(3),
            new BigInteger(5),
            new BigInteger(7),
            new BigInteger(11),
            new BigInteger(13),
            new BigInteger(17),
            new BigInteger(19),
            new BigInteger(23),
            new BigInteger(29),
            new BigInteger(31),
            new BigInteger(37),
            new BigInteger(41),
            new BigInteger(43),
            new BigInteger(47),
            new BigInteger(53),
            new BigInteger(59),
            new BigInteger(61),
            new BigInteger(67),
            new BigInteger(71),
            new BigInteger(73),
            new BigInteger(79),
            new BigInteger(83),
            new BigInteger(89),
            new BigInteger(97),
            new BigInteger(101),
            new BigInteger(103),
            new BigInteger(107),
            new BigInteger(109),
            new BigInteger(113),
            new BigInteger(127),
            new BigInteger(131),
            new BigInteger(137),
            new BigInteger(139),
            new BigInteger(149),
            new BigInteger(151),
            new BigInteger(157),
            new BigInteger(163),
            new BigInteger(167)
        };

        /// <summary>
        /// Converts a RSA public key to BCRYPT_RSAKEY_BLOB.
        /// </summary>
        public static byte[] ExportRSAPublicKeyBCrypt(this X509Certificate2 certificate)
        {
            Validator.AssertNotNull(certificate, nameof(certificate));

            using (var rsa = (RSACng)certificate.GetRSAPublicKey())
            {
                using(var key = rsa.Key)
                {
                    return key.Export(BCryptRSAPublicKeyFormat);
                }
            }
        }

        /// <summary>
        /// Decodes a public key from a BCRYPT_RSAKEY_BLOB structure.
        /// </summary>
        public static RSAParameters ImportRSAPublicKeyBCrypt(this byte[] blob)
        {
            Validator.AssertNotNull(blob, nameof(blob));

            using (var key = CngKey.Import(blob, BCryptRSAPublicKeyFormat))
            {
                using (var rsa = new RSACng(key))
                {
                    return rsa.ExportParameters(false);
                }
            }
        }

        /// <summary>
        /// Exports a RSA public key to the DER format.
        /// </summary>
        public static byte[] ExportRSAPublicKeyDER(this X509Certificate2 certificate)
        {
            Validator.AssertNotNull(certificate, nameof(certificate));

            return certificate.PublicKey.EncodedKeyValue.RawData;
        }

        /// <summary>
        /// Decodes a DER RSA public key.
        /// </summary>
        public static RSAParameters ImportRSAPublicKeyDER(this byte[] blob)
        {
            Validator.AssertNotNull(blob, nameof(blob));

            var asn1Key = new AsnEncodedData(blob);
            var publicKey = new PublicKey(RsaOid, Asn1Null, asn1Key);
            using (var rsaKey = (RSACryptoServiceProvider)publicKey.Key)
            {
                return rsaKey.ExportParameters(false);
            }
        }

        /// <summary>
        /// Checks whether the input blob is in the BCRYPT_RSAKEY_BLOB format.
        /// </summary>
        public static bool IsBCryptRSAPublicKeyBlob(this byte[] blob)
        {
            if (blob == null || blob.Length < BCryptKeyBlobHeaderSize)
            {
                return false;
            }

            // Check if the byte sequence starts with the magic
            return BitConverter.ToUInt32(blob, 0) == BCryptRSAPublicKeyMagic;
        }

        /// <summary>
        /// Checks whether the input blob is in the PCP_KEY_BLOB_WIN8 format.
        /// </summary>
        public static bool IsTPM20PublicKeyBlob(this byte[] blob)
        {
            if (blob == null || blob.Length < TPM20KeyBlobHeaderSize)
            {
                return false;
            }

            // Check if the byte sequence starts with the magic
            return BitConverter.ToUInt32(blob, 0) == TPM20PublicKeyMagic;
        }

        /// <summary>
        /// Checks whether the input blob is a DER-encoded public key.
        /// </summary>
        public static bool IsDERPublicKeyBlob(this byte[] blob)
        {
            if (blob == null || blob.Length < DERPublicKeyMinSize)
            {
                return false;
            }

            // Check if the byte sequence starts with a DER sequence tag. This is a very vague test.
            return blob[0] == DERSequenceTag;
        }

        public static bool IsWeakKey(this RSAParameters publicKey)
        {
            // Convert the byte array modulus to unsigned BigInteger by changing it to little endian and appending 0 as sign bit:
            BigInteger modulus = new BigInteger(publicKey.Modulus.Reverse().Concat(new byte[] { Byte.MinValue }).ToArray());

            for (int i = 0; i < Primes.Length; i++)
            {
                if (((BigInteger.One << (int)(modulus % Primes[i])) & WeakKeyMarkers[i]) == BigInteger.Zero)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
