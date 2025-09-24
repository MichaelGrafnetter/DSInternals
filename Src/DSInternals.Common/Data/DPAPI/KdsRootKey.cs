using System.Collections.Concurrent;
using System.Text;
using DSInternals.Common.Interop;
using DSInternals.Common.Schema;
using Windows.Win32;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Root key for the Group Key Distribution Service.
    /// </summary>
    public class KdsRootKey
    {
        internal const string DefaultKdfAlgorithm = "SP800_108_CTR_HMAC";
        internal static readonly byte[] DefaultKdfParameters = "00000000010000000e000000000000005300480041003500310032000000".HexToBinary();
        internal const string DefaultSecretAgreementAlgorithm = "DH";
        internal static readonly byte[] DefaultSecretAgreementParameters = "0c0200004448504d0001000087a8e61db4b6663cffbbd19c651959998ceef608660dd0f25d2ceed4435e3b00e00df8f1d61957d4faf7df4561b2aa3016c3d91134096faa3bf4296d830e9a7c209e0c6497517abd5a8a9d306bcf67ed91f9e6725b4758c022e0b1ef4275bf7b6c5bfc11d45f9088b941f54eb1e59bb8bc39a0bf12307f5c4fdb70c581b23f76b63acae1caa6b7902d52526735488a0ef13c6d9a51bfa4ab3ad8347796524d8ef6a167b5a41825d967e144e5140564251ccacb83e6b486f6b3ca3f7971506026c0b857f689962856ded4010abd0be621c3a3960a54e710c375f26375d7014103a4b54330c198af126116d2276e11715f693877fad7ef09cadb094ae91e1a15973fb32c9b73134d0b2e77506660edbd484ca7b18f21ef205407f4793a1a0ba12510dbc15077be463fff4fed4aac0bb555be3a6c1b0c6b47b1bc3773bf7e8c6f62901228f8c28cbb18a55ae31341000a650196f931c77a57f2ddf463e5e9ec144b777de62aaab8a8628ac376d282d6ed3864e67982428ebc831d14348f6f2f9193b5045af2767164e1dfc967c1fb3f2e55a4bd1bffe83b9c80d052b985d182ea0adb2a3b7313d3fe14c8484b1e052588b9b7d2bbd2df016199ecd06e1557cd0915b3353bbb64e0ec377fd028370df92b52c7891428cdc67eb6184b523d1db246c32f63078490f00ef8d647d148d47954515e2327cfef98c582664b4c0f6cc41659".HexToBinary();
        internal const int L0KeyModulus = 1;
        internal const int L1KeyModulus = 32;
        internal const int L2KeyModulus = 32;
        internal const long KdsKeyCycleDuration = 360000000000; // 10 hrs in FileTime
        private const string RootKeyDistinguishedNameFormat = "CN={0},CN=Master Root Keys,CN=Group Key Distribution Service,CN=Services,{1}";
        private const string GkdsKdfLabel = "KDS service";
        private const string GkdsPublicKeyLabel = "KDS public key";
        private const int ExpectedRootKeyVersion = 1;
        private const int ExpectedKdfParametersVersion = 0;
        private const int SecretAgreementParametersMinSize = 2 * sizeof(int) + 4; // Lengths + magic
        private const int DefaultKdsKeySize = 64;
        internal const int DefaultPublicKeyLength = 2048;
        internal const int DefaultPrivateKeyLength = 512;

        // The cache is intentionally thread-safe
        private IDictionary<int, byte[]> L0KeyCache = new ConcurrentDictionary<int, byte[]>();

        /// <summary>
        /// Gets the unique identifier of this root key.
        /// </summary>
        public Guid KeyId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the KDS root key value.
        /// </summary>
        public byte[] KeyValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the time after which this root key may be used.
        /// </summary>
        public DateTime? EffectiveTime
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the time when this root key was created.
        /// </summary>
        public DateTime? CreationTime
        {
            get;
            private set;
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
            get;
            private set;
        }

        /// <summary>
        /// Parameters for the key derivation algorithm.
        /// </summary>
        public Dictionary<int, string> KdfParameters
        {
            get
            {
                return ParseKdfParameters(this.RawKdfParameters);
            }
        }

        public byte[] RawKdfParameters
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the secret agreement algorithm to be used with public keys.
        /// </summary>
        public string SecretAgreementAlgorithm
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the parameters for the secret agreement algorithm.
        /// </summary>
        public byte[] SecretAgreementParameters
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the length of the secret agreement public key.
        /// </summary>
        public int SecretAgreementPublicKeyLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the length of the secret agreement private key.
        /// </summary>
        public int SecretAgreementPrivateKeyLength
        {
            get;
            private set;
        }

        public KdsRootKey(DirectoryObject dsObject)
        {
            if (dsObject == null)
            {
                throw new ArgumentNullException(nameof(dsObject));
            }    
            // TODO: Validate object type

            // Key format version
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsVersion, out int? version);

            if (version.HasValue)
            {
                Validator.AssertEquals(ExpectedRootKeyVersion, version.Value, nameof(version));
            }

            // Domain controller DN
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsDomainController, out DistinguishedName dcDN);
            this.DomainController = dcDN?.ToString();
            
            // Private key
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsRootKeyData, out byte[] key);
            this.KeyValue = key;

            // Creation time
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsCreationTime, out DateTime? creationTime, false);
            this.CreationTime = creationTime;

            // Effective time
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsEffectiveTime, out DateTime? effectiveTime, false);
            this.EffectiveTime = effectiveTime;

            // Guid
            dsObject.ReadAttribute(CommonDirectoryAttributes.RDN, out string cn);

            if (cn == null)
            {
                throw new ArgumentException("Could not read the root key common name.", nameof(dsObject));    
            }

            this.KeyId = Guid.Parse(cn);

            // KDF algorithm
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsKdfAlgorithmId, out string kdfAlgorithmName);
            this.KdfAlgorithm = kdfAlgorithmName ?? DefaultKdfAlgorithm;

            // KDF algorithm parameters (only 1 in current implementation)
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsKdfParameters, out byte[] kdfParameters);
            this.RawKdfParameters = kdfParameters ?? DefaultKdfParameters;

            // Secret agreement algorithm
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsSecretAgreementAlgorithmId, out string secretAgreementAlgorithmName);
            this.SecretAgreementAlgorithm = secretAgreementAlgorithmName ?? DefaultSecretAgreementAlgorithm;

            // Secret agreement algorithm parameters
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsSecretAgreementParameters, out byte[] secretAgreementAlgorithmParam);
            this.SecretAgreementParameters = secretAgreementAlgorithmParam ?? DefaultSecretAgreementParameters;

            // Secret agreement private key length
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsSecretAgreementPrivateKeyLength, out int? privateKeyLength);
            this.SecretAgreementPrivateKeyLength = privateKeyLength ?? DefaultPrivateKeyLength;

            // Secret agreement public key length
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsSecretAgreementPublicKeyLength, out int? publicKeyLength);
            this.SecretAgreementPublicKeyLength = publicKeyLength ?? DefaultPublicKeyLength;
        }

        public KdsRootKey(Guid keyId, byte[] key)
        {
            this.KeyId = keyId;
            this.KeyValue = key ?? throw new ArgumentNullException(nameof(key));
            this.KdfAlgorithm = DefaultKdfAlgorithm;
            this.RawKdfParameters = DefaultKdfParameters;
            this.SecretAgreementAlgorithm = DefaultSecretAgreementAlgorithm;
            this.SecretAgreementParameters = DefaultSecretAgreementParameters;
            this.SecretAgreementPublicKeyLength = DefaultPublicKeyLength;
            this.SecretAgreementPrivateKeyLength = DefaultPrivateKeyLength;
        }

        public (byte[] l1Key, byte[] l2Key) ComputeSidPrivateKey(
            byte[] securityDescriptor,
            int l0KeyId,
            int l1KeyId,
            int l2KeyId,
            bool isPublicKey = false
            )
        {
            // Calculate and cache the L0 key first
            byte[] l0Key = GetL0Key(l0KeyId);

            // Calculate L1 key
            (byte[] l1KeyCurrent, byte[] l1KeyPrevious) =
                GenerateL1Key(this.KeyId, this.KdfAlgorithm, this.RawKdfParameters, l0Key, l0KeyId, l1KeyId, securityDescriptor);

            if (l2KeyId == L2KeyModulus - 1 && isPublicKey == false)
            {
                return (l1KeyCurrent, null);
            }
            else
            {
                // Calculate L2 key
                byte[] l2Key = DeriveKey(this.KeyId, this.KdfAlgorithm, this.RawKdfParameters, secret: l1KeyCurrent, label: null, GroupKeyLevel.L2, l0KeyId, l1KeyId, l2KeyId: L2KeyModulus - 1, iteration: L2KeyModulus - l2KeyId, DefaultKdsKeySize);
                byte[] l1Key = l1KeyId != 0 ? l1KeyPrevious : null;
                return (l1Key, l2Key);
            }
        }

        public byte[] GetL0Key(int l0KeyId)
        {
            if (l0KeyId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(l0KeyId));
            }

            if (this.L0KeyCache.TryGetValue(l0KeyId, out byte[] l0Key))
            {
                // L0 key cache hit
                return l0Key;
            }

            // Cache miss, compute the L0 key
            l0Key = DeriveKey(this.KeyId, this.KdfAlgorithm, this.RawKdfParameters, secret: this.KeyValue, label: null, GroupKeyLevel.L0, l0KeyId, l1KeyId: -1, l2KeyId: -1, iteration: L0KeyModulus, DefaultKdsKeySize);
            this.L0KeyCache[l0KeyId] = l0Key;
            return l0Key;
        }

        public static (byte[] l1KeyCurrent, byte[] l1KeyPrevious) GenerateL1Key(
            Guid kdsRootKeyId,
            string kdfAlgorithm,
            byte[] kdfParameters,
            byte[] l0Key,
            int l0KeyId,
            int l1KeyId,
            byte[] securityDescriptor)
        {
            Validator.AssertNotNull(securityDescriptor, nameof(securityDescriptor));

            var result = NativeMethods.GenerateKDFContext(
                kdsRootKeyId,
                l0KeyId,
                l1KeyId: L1KeyModulus - 1,
                l2KeyId: -1,
                GroupKeyLevel.L1,
                out byte[] context,
                out int counterOffset);
            Validator.AssertSuccess(result);

            // Append the security descriptor to the context for the last key
            byte[] lastKeyContext = new byte[context.Length + securityDescriptor.Length];
            context.CopyTo(lastKeyContext, 0);
            securityDescriptor.CopyTo(lastKeyContext, context.Length);

            result = NativeMethods.GenerateDerivedKey(
                kdfAlgorithm,
                kdfParameters,
                l0Key,
                lastKeyContext,
                counterOffset,
                label: null,
                iteration: 1,
                DefaultKdsKeySize,
                out byte[] l1KeyLast,
                out string invalidAttribute);

            Validator.AssertSuccess(result);

            byte[] l1KeyCurrent = l1KeyLast;

            int iteration = L1KeyModulus - l1KeyId - 1;

            if (iteration > 0) // l1KeyId < 31
            {
                // Decrease the counter
                context[counterOffset]--;

                result = NativeMethods.GenerateDerivedKey(
                    kdfAlgorithm,
                    kdfParameters,
                    l1KeyLast,
                    context,
                    counterOffset,
                    label: null,
                    iteration,
                    DefaultKdsKeySize,
                    out l1KeyCurrent,
                    out string invalidAttribute2);

                Validator.AssertSuccess(result);
            }

            byte[] l1KeyPrevious = null;

            if (l1KeyId > 0)
            {
                // Set L1 key ID
                context[counterOffset] = (byte)(l1KeyId - 1);

                result = NativeMethods.GenerateDerivedKey(
                    kdfAlgorithm,
                    kdfParameters,
                    secret: l1KeyCurrent,
                    context,
                    counterOffset,
                    label: null,
                    iteration: 1,
                    DefaultKdsKeySize,
                    out l1KeyPrevious,
                    out string invalidAttribute3);

                Validator.AssertSuccess(result);
            }

            return (l1KeyCurrent, l1KeyPrevious);
        }

        public static (byte[] nextL1Key, byte[] nextL2Key) ClientComputeL2Key(
            Guid kdsRootKeyId,
            string kdfAlgorithm,
            byte[] kdfParameters,
            byte[] l1Key,
            byte[] l2Key,
            int l0KeyId,
            int l1KeyId,
            int l1KeyIteration,
            int l2KeyIteration,
            int nextL1KeyId,
            int nextL2KeyId)
        {
            byte[] nextL1Key = l1Key;
            if (l1KeyIteration > 0)
            {
                // TODO: Add test cases for this branch
                // Recalculate L1 key
                nextL1Key = DeriveKey(kdsRootKeyId, kdfAlgorithm, kdfParameters, l1Key, label: null, GroupKeyLevel.L1, l0KeyId, nextL1KeyId, -1, l1KeyIteration, DefaultKdsKeySize);
            }

            byte[] nextL2Key = l2Key;

            if (l2KeyIteration > 0)
            {
                // TODO: Add test cases for this branch
                // Recalculate L2 key. If the original key is empty, derive it from the L1 key
                byte[] l2KeyBase = (l2Key == null || l2Key.Length == 0) ? nextL1Key : l2Key;
                nextL2Key = DeriveKey(kdsRootKeyId, kdfAlgorithm, kdfParameters, l2KeyBase, label: null, GroupKeyLevel.L2, l0KeyId, l1KeyId, nextL2KeyId, l2KeyIteration, DefaultKdsKeySize);
            }

            return (nextL1Key, nextL2Key);
        }

        public static DateTime GetKeyStartTime(int l0KeyId, int l1KeyId, int l2KeyId)
        {
            if(l0KeyId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(l0KeyId));
            }

            if(l1KeyId < 0 || l1KeyId >= L1KeyModulus)
            {
                throw new ArgumentOutOfRangeException(nameof(l1KeyId));
            }

            if (l2KeyId < 0 || l2KeyId >= L2KeyModulus)
            {
                throw new ArgumentOutOfRangeException(nameof(l2KeyId));
            }

            long effectiveTimestamp = l2KeyId + l1KeyId * L1KeyModulus + l0KeyId * L1KeyModulus * L2KeyModulus;

            return DateTime.FromFileTime(effectiveTimestamp * KdsKeyCycleDuration);
        }

        public static (int l0KeyId, int l1KeyId, int l2KeyId) GetKeyId(DateTime effectiveTime)
        {
            long effectiveTimeNumeric = effectiveTime.ToFileTimeUtc();
            int effectiveKdsCycleId = (int)(effectiveTimeNumeric / KdsRootKey.KdsKeyCycleDuration);

            int l0KeyId = effectiveKdsCycleId / (KdsRootKey.L1KeyModulus * KdsRootKey.L2KeyModulus);
            int l1KeyId = (effectiveKdsCycleId / KdsRootKey.L2KeyModulus) % KdsRootKey.L1KeyModulus;
            int l2KeyId = effectiveKdsCycleId % KdsRootKey.L2KeyModulus;

            return (l0KeyId, l1KeyId, l2KeyId);
        }

        public static Dictionary<int,string> ParseKdfParameters(byte[] blob)
        {
            if(blob == null || blob.Length == 0)
            {
                return null;
            }

            int aggregatedMinLength = 2 * sizeof(int); // version + param count
            Validator.AssertMinLength(blob, aggregatedMinLength, nameof(blob));

            var result = new Dictionary<int,string>();

            using (Stream stream = new MemoryStream(blob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    int version = reader.ReadInt32();
                    Validator.AssertEquals(ExpectedKdfParametersVersion, version, nameof(version));

                    int parameterCount = reader.ReadInt32();

                    aggregatedMinLength += parameterCount * 2 * sizeof(int); // + valueLength + parameterId
                    Validator.AssertMinLength(blob, aggregatedMinLength, nameof(blob));

                    for (uint i = 0; i < parameterCount; i++)
                    {
                        int valueLength = reader.ReadInt32();
                        int parameterId = reader.ReadInt32();

                        aggregatedMinLength += valueLength;
                        Validator.AssertMinLength(blob, aggregatedMinLength, nameof(blob));

                        byte[] binaryValue = reader.ReadBytes(valueLength);

                        // Remove the trailing 0 at the end.
                        string value = UnicodeEncoding.Unicode.GetString(binaryValue, 0, valueLength - sizeof(char));
                        result.Add(parameterId, value);
                    }

                    // Check that there are no unread bytes left.
                    Validator.AssertLength(blob, aggregatedMinLength, nameof(blob));
                }
            }

            return result;
        }

        public static (byte[] p, byte[] g) ParseSecretAgreementParameters(byte[] blob)
        {
            if (blob == null || blob.Length == 0)
            {
                return (null, null);
            }

            Validator.AssertMinLength(blob, SecretAgreementParametersMinSize, nameof(blob));

            using (Stream stream = new MemoryStream(blob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    int length = reader.ReadInt32();
                    Validator.AssertLength(blob, length, nameof(blob));

                    uint magic = reader.ReadUInt32();

                    if (magic != PInvoke.BCRYPT_DH_PARAMETERS_MAGIC)
                    {
                        throw new ArgumentException("Invalid magic value.", nameof(blob));
                    }

                    // DH public parameters:
                    int keySize = reader.ReadInt32();
                    Validator.AssertEquals(length, SecretAgreementParametersMinSize + 2 * keySize, nameof(length));

                    byte[] p = reader.ReadBytes(keySize);
                    byte[] g = reader.ReadBytes(keySize);

                    return (p, g);
                }
            }
        }

        public static string GetDistinguishedName(Guid rootKeyId, string configurationNamingContext)
        {
            if (configurationNamingContext == null) throw new ArgumentNullException(nameof(configurationNamingContext));

            return string.Format(RootKeyDistinguishedNameFormat, rootKeyId, configurationNamingContext);
        }

        private static byte[] DeriveKey(Guid kdsRootKeyId, string kdfAlgorithm, byte[] kdfParameters, byte[] secret, string label, GroupKeyLevel level, int l0KeyId, int l1KeyId, int l2KeyId, int iteration, int desiredKeyLength)
        {
            var result = NativeMethods.GenerateKDFContext(
                            kdsRootKeyId,
                            l0KeyId,
                            l1KeyId,
                            l2KeyId,
                            level,
                            out byte[] context,
                            out int counterOffset);

            Validator.AssertSuccess(result);

            result = NativeMethods.GenerateDerivedKey(
                    kdfAlgorithm,
                    kdfParameters,
                    secret,
                    context,
                    counterOffset,
                    label,
                    iteration,
                    desiredKeyLength,
                    out byte[] derivedKey,
                    out string invalidAttribute);

            Validator.AssertSuccess(result);

            return derivedKey;
        }
    }
}
