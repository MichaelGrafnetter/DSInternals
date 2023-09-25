using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DSInternals.Common.Interop;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Root key for the Group Key Distribution Service.
    /// </summary>
    public class KdsRootKey
    {
        private const int L0KeyIteration = 1;
        private const int L1KeyIteration = 32;
        private const int L2KeyIteration = 32;

        private int? version;
        private DateTime ?creationTime;
        private DateTime ?effectiveTime;
        private byte[] privateKey;
        private string kdfAlgorithmName;
        private byte[] rawKdfParameters;
        private string secretAgreementAlgorithmName;
        private byte[] secretAgreementAlgorithmParam;
        private int? privateKeyLength;
        private int? publicKeyLength;

        public KdsRootKey(DirectoryObject dsObject)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, "dsObject");
            // TODO: Validate object type

            // Key format version
            // TODO: Check that format == 1
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsVersion, out this.version);

            // Domain controller DN
            DistinguishedName dcDN;
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsDomainController, out dcDN);
            this.DomainController = dcDN.ToString();
            
            // Private key
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsPrivateKey, out this.privateKey);

            // Creation time
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsCreationTime, out this.creationTime);

            // Effective time
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsEffectiveTime, out this.effectiveTime);

            // Guid
            string cn;
            dsObject.ReadAttribute(CommonDirectoryAttributes.CommonName, out cn);
            this.KeyId = Guid.Parse(cn);

            // KDF algorithm
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsKdfAlgorithm, out this.kdfAlgorithmName);

            // KDF algorithm parameters (only 1 in current implementation)
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsKdfParameters, out this.rawKdfParameters);

            // Secret agreement algorithm
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsSecretAgreementAlgorithm, out this.secretAgreementAlgorithmName);

            // Secret agreement algorithm parameters
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsSecretAgreementParameters, out this.secretAgreementAlgorithmParam);

            // Secret agreement private key length
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsSecretAgreementPrivateKeyLength, out this.privateKeyLength);

            // Secret agreement public  key length
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsSecretAgreementPublicKeyLength, out this.publicKeyLength);
        }

        /// <summary>
        /// Gets the unique identifier of this root key.
        /// </summary>
        public Guid KeyId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the version number of this root key.
        /// </summary>
        public int VersionNumber
        {
            get
            {
                return this.version.Value;
            }
        }

        /// <summary>
        /// Gets the root key.
        /// </summary>
        public byte[] KeyValue
        {
            get
            {
                return this.privateKey;
            }
        }

        /// <summary>
        /// Gets the time after which this root key may be used.
        /// </summary>
        public DateTime EffectiveTime
        {
            get
            {
                return this.effectiveTime.Value;
            }
        }

        /// <summary>
        /// Gets the time when this root key was created.
        /// </summary>
        public DateTime CreationTime
        {
            get
            {
                return this.creationTime.Value;
            }
        }

        /// <summary>
        /// Gets distinguished name of the Domain Controller which generated this root key.
        /// </summary>
        public string DomainController
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the algorithm name of the key derivation function used to compute keys.
        /// </summary>
        public string KdfAlgorithm
        {
            get
            {
                return this.kdfAlgorithmName;
            }
        }

        /// <summary>
        /// Parameters for the key derivation algorithm.
        /// </summary>
        public Dictionary<uint, string> KdfParameters
        {
            get
            {
                return ParseKdfParameters(this.rawKdfParameters);
            }
        }

        /// <summary>
        /// Gets the name of the secret agreement algorithm to be used with public keys.
        /// </summary>
        public string SecretAgreementAlgorithm
        {
            get
            {
                return this.secretAgreementAlgorithmName;
            }
        }

        /// <summary>
        /// Gets the parameters for the secret agreement algorithm.
        /// </summary>
        public byte[] SecretAgreementParameters
        {
            get
            {
                return this.secretAgreementAlgorithmParam;
            }
        }

        /// <summary>
        /// Gets the length of the secret agreement public key.
        /// </summary>
        public int SecretAgreementPublicKeyLength
        {
            get
            {
                return this.publicKeyLength.Value;
            }
        }

        /// <summary>
        /// Gets the length of the secret agreement private key.
        /// </summary>
        public int SecretAgreementPrivateKeyLength
        {
            get
            {
                return this.privateKeyLength.Value;
            }
        }

        public byte[] ComputeL0Key(int l0KeyId)
        {
            return ComputeL0Key(
                this.KeyId,
                this.KeyValue,
                this.KdfAlgorithm,
                this.rawKdfParameters,
                l0KeyId
            );
        }

        public static byte[] ComputeL0Key(
            Guid kdsRootKeyId,
            byte[] kdsRootKey,
            string kdfAlgorithm,
            byte[] kdfParameters,
            int l0KeyId)
        {
            var result = NativeMethods.GenerateKDFContext(
                kdsRootKeyId,
                l0KeyId,
                -1,
                -1,
                GroupKeyLevel.L0,
                out byte[] kdfContext,
                out int counterOffset
            );

            Validator.AssertSuccess(result);

            result = NativeMethods.GenerateDerivedKey(
                kdfAlgorithm,
                kdfParameters,
                kdsRootKey,
                kdfContext,
                null,
                null,
                L0KeyIteration,
                out byte[] l0Key,
                out string invalidAtribute
            );

            Validator.AssertSuccess(result);

            return l0Key;
        }

        public static Dictionary<uint,string> ParseKdfParameters(byte[] blob)
        {
            if(blob == null || blob.Length == 0)
            {
                return null;
            }
            // TODO: Validate length

            var result = new Dictionary<uint,string>();

            using (Stream stream = new MemoryStream(blob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    uint version = reader.ReadUInt32();
                    // TODO: Test that version == 0

                    uint parameterCount = reader.ReadUInt32();

                    for (uint i = 0; i < parameterCount; i++)
                    {
                        int valueLength = reader.ReadInt32();
                        uint parameterId = reader.ReadUInt32();
                        // TODO: Test that paramId == 0

                        byte[] binaryValue = reader.ReadBytes(valueLength);
                        
                        // Remove the trailing 0 at the end.
                        string value = UnicodeEncoding.Unicode.GetString(binaryValue, 0, valueLength - sizeof(char));
                        result.Add(parameterId, value);
                    }
                }
            }
            return result;
        }

        public static void ParseSecretAgreementParameters(byte[] blob)
        {
            if (blob == null || blob.Length == 0)
            {
                return;
            }
            // TODO: Validate minimum length

            using (Stream stream = new MemoryStream(blob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    var length = reader.ReadInt32();
                    // TODO: validate actual length

                    var binaryMagic = reader.ReadBytes(sizeof(int));
                    string magic = ASCIIEncoding.ASCII.GetString(binaryMagic);
                    // TODO: Test that magic is DHPM

                    // DH:
                    int keySize = reader.ReadInt32();
                    var p = reader.ReadBytes(keySize);
                    var g = reader.ReadBytes(keySize);
                }
            }
        }
    }
}
