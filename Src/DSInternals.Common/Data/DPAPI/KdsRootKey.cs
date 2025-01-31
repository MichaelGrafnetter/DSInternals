using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text;
using DSInternals.Common.Interop;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Root key for the Group Key Distribution Service.
    /// </summary>
    public class KdsRootKey
    {
        private const int L0KeyModulus = 1;
        private const int L1KeyModulus = 32;
        private const int L2KeyModulus = 32;
        private const long KdsKeyCycleDuration = 360000000000; // 10 hrs in FileTime
        private const long MaxClockSkew = 3000000000; // 5 min in FileTime
        private const string GmsaKdfLabel = "GMSA PASSWORD";
        private const string SecretAgreementParametersHeaderMagic = "DHPM";
        private const int ExpectedKdfParametersVersion = 0;
        private const int SecretAgreementParametersMinSize = 2 * sizeof(int) + 4; // Lengths + magic
        private const int DefaultKdsKeySize = 64;
        private const int GmsaPasswordLength = 256;
        private const int DefaultManagedPasswordInterval = 30; // 30 days

        // TODO: Move to GMSA
        private static readonly byte[] DefaultGMSASecurityDescriptor = {
            0x1, 0x0, 0x4, 0x80, 0x30, 0x0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x14, 0x00, 0x00, 0x00, 0x02, 0x0, 0x1C, 0x0, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0,
            0x14, 0x0, 0x9F, 0x1, 0x12, 0x0, 0x1, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x5, 0x9,
            0x0, 0x0, 0x0, 0x1, 0x1, 0x0, 0x0, 0x0, 0x0, 0x0, 0x5, 0x12, 0x0, 0x0, 0x0 }; // Enterprise Domain Controllers

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
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsVersion, out this.version);

            if(this.version.HasValue)
            {
                Validator.AssertEquals(1, this.version.Value, nameof(version));
            }

            // Domain controller DN
            DistinguishedName dcDN;
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsDomainController, out dcDN);
            this.DomainController = dcDN.ToString();
            
            // Private key
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsPrivateKey, out this.privateKey);

            // Creation time
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsCreationTime, out this.creationTime, false);

            // Effective time
            dsObject.ReadAttribute(CommonDirectoryAttributes.KdsEffectiveTime, out this.effectiveTime, false);

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
        public Dictionary<int, string> KdfParameters
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

        public byte[] GetManagedPassword(
            SecurityIdentifier gMsaSid,
            DateTime previousPasswordChange,
            DateTime effectiveTime,
            int? managedPasswordInterval = null
            )
        {
            return GetPassword(gMsaSid, this.KeyId, this.KeyValue, this.KdfAlgorithm, this.rawKdfParameters, previousPasswordChange, effectiveTime, managedPasswordInterval ?? DefaultManagedPasswordInterval);
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
                L0KeyModulus,
                DefaultKdsKeySize,
                out byte[] l0Key,
                out string invalidAtribute
            );

            Validator.AssertSuccess(result);

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
                L1KeyModulus - 1,
                -1,
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
                null,
                1,
                DefaultKdsKeySize,
                out byte[] l1KeyLast,
                out string invalidAttribute);

            Validator.AssertSuccess(result);

            byte[] l1KeyCurrent;

            int iteration = L1KeyModulus - l1KeyId - 1;

            if(iteration > 0)
            {
                // Decrease the counter
                context[counterOffset]--;

                result = NativeMethods.GenerateDerivedKey(
                    kdfAlgorithm,
                    kdfParameters,
                    l1KeyLast,
                    context,
                    counterOffset,
                    null,
                    iteration,
                    DefaultKdsKeySize,
                    out l1KeyCurrent,
                    out string invalidAttribute2);

                Validator.AssertSuccess(result);
            }
            else
            {
                l1KeyCurrent = l1KeyLast;
            }

            byte[] l1KeyPrevious;

            if(l1KeyId > 0)
            {
                // Set L1 key ID
                context[counterOffset] = (byte)(l1KeyId - 1);

                result = NativeMethods.GenerateDerivedKey(
                    kdfAlgorithm,
                    kdfParameters,
                    l1KeyCurrent,
                    context,
                    counterOffset,
                    null,
                    1,
                    DefaultKdsKeySize,
                    out l1KeyPrevious,
                    out string invalidAttribute3);

                Validator.AssertSuccess(result);
            }
            else
            {
                l1KeyPrevious = null;
            }

            return (l1KeyCurrent, l1KeyPrevious);
        }

        public static byte[] GenerateL2Key(
            Guid kdsRootKeyId,
            string kdfAlgorithm,
            byte[] kdfParameters,
            byte[] l1Key,
            int l0KeyId,
            int l1KeyId,
            int l2KeyId)
        {
            var result = NativeMethods.GenerateKDFContext(
                kdsRootKeyId,
                l0KeyId,
                l1KeyId,
                L2KeyModulus - 1,
                GroupKeyLevel.L2,
                out byte[] context,
                out int counterOffset);

            Validator.AssertSuccess(result);

            result = NativeMethods.GenerateDerivedKey(
                    kdfAlgorithm,
                    kdfParameters,
                    l1Key,
                    context,
                    counterOffset,
                    null,
                    L2KeyModulus - l2KeyId,
                    DefaultKdsKeySize,
                    out byte[] l2Key,
                    out string invalidAttribute);

            Validator.AssertSuccess(result);

            return l2Key;
        }

        public static byte[] ClientComputeL2Key(
            ProtectionKeyIdentifier managedPasswordId,
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
                // Recalculate L1 key
                var result = NativeMethods.GenerateKDFContext(
                    kdsRootKeyId,
                    l0KeyId,
                    nextL1KeyId,
                    -1,
                    GroupKeyLevel.L1,
                    out byte[] l1Context,
                    out int l1CounterOffset
                    );
                Validator.AssertSuccess(result);

                result = NativeMethods.GenerateDerivedKey(
                    kdfAlgorithm,
                    kdfParameters,
                    l1Key,
                    l1Context,
                    l1CounterOffset,
                    null,
                    l1KeyIteration,
                    DefaultKdsKeySize,
                    out nextL1Key,
                    out string invalidAttributeName);
                Validator.AssertSuccess(result);
            }

            byte[] l2KeyBase = l2Key;
            if(l2KeyBase == null || (managedPasswordId != null && l1KeyId > managedPasswordId.L1KeyId))
            {
                // There is either no L2 key available or an older L1 key is needed
                l2KeyBase = nextL1Key;
            }

            byte[] nextL2Key = l2Key;

            if (l2KeyIteration > 0)
            {
                // Recalculate L2 key
                var result = NativeMethods.GenerateKDFContext(
                    kdsRootKeyId,
                    l0KeyId,
                    managedPasswordId?.L1KeyId ?? l1KeyId,
                    nextL2KeyId,
                    GroupKeyLevel.L2,
                    out byte[] l2Context,
                    out int l2CounterOffset
                    );
                Validator.AssertSuccess(result);

                result = NativeMethods.GenerateDerivedKey(
                    kdfAlgorithm,
                    kdfParameters,
                    l2KeyBase,
                    l2Context,
                    l2CounterOffset,
                    null,
                    l2KeyIteration,
                    DefaultKdsKeySize,
                    out nextL2Key,
                    out string invalidAttributeName);
                Validator.AssertSuccess(result);
            }

            return nextL2Key;
        }

        public static (byte[] l1Key, byte[] l2Key) ComputeSidPrivateKey(
            Guid kdsRootKeyId,
            string kdfAlgorithm,
            byte[] kdfParameters,
            byte[] l0Key,
            byte[] securityDescriptor,
            int l0KeyId,
            int l1KeyId,
            int l2KeyId,
            bool isPublicKey
            )
        {
            // TODO: Consider caching the L0 key.
            (byte[] l1KeyCurrent, byte[] l1KeyPrevious) =
                GenerateL1Key(kdsRootKeyId, kdfAlgorithm, kdfParameters, l0Key, l0KeyId, l1KeyId, securityDescriptor);

            if(l2KeyId == L2KeyModulus - 1 && isPublicKey == false)
            {
                return (l1KeyCurrent, null);
            }
            else
            {
                byte[] l1Key = l1KeyId != 0 ? l1KeyPrevious : null;
                byte[] l2Key = GenerateL2Key(kdsRootKeyId, kdfAlgorithm, kdfParameters, l1KeyCurrent, l0KeyId, l1KeyId, l2KeyId);
                return (l1Key, l2Key);
            }
        }

        public static (byte[] l1Key, byte[] l2Key) GetSidKeyLocal(
            Guid kdsRootKeyId,
            byte[] kdsRootKey,
            string kdfAlgorithm,
            byte[] kdfParameters,
            byte[] securityDescriptor,
            int l0KeyId,
            int l1KeyId,
            int l2KeyId
            )
        {
            byte[] l0Key = ComputeL0Key(kdsRootKeyId, kdsRootKey, kdfAlgorithm, kdfParameters, l0KeyId);

            return ComputeSidPrivateKey(kdsRootKeyId, kdfAlgorithm, kdfParameters, l0Key, securityDescriptor, l0KeyId, l1KeyId, l2KeyId, false);
        }

        public static byte[] GetPassword(
            SecurityIdentifier gMsaSid,
            Guid kdsRootKeyId,
            byte[] kdsRootKey,
            string kdfAlgorithm,
            byte[] kdfParameters,
            DateTime lastPasswordChange
            )
        {
            return GetPassword(gMsaSid, kdsRootKeyId, kdsRootKey, kdfAlgorithm, kdfParameters, lastPasswordChange, lastPasswordChange);
        }

        public static byte[] GetPassword(
            SecurityIdentifier gMsaSid,
            Guid kdsRootKeyId,
            byte[] kdsRootKey,
            string kdfAlgorithm,
            byte[] kdfParameters,
            DateTime previousPasswordChange,
            DateTime effectiveTime,
            int managedPasswordInterval = DefaultManagedPasswordInterval
            )
        {
            (int l0KeyId, int l1KeyId, int l2KeyId) = GetIntervalId(previousPasswordChange, effectiveTime, managedPasswordInterval);

            return GetPassword(gMsaSid, kdsRootKeyId, kdsRootKey, kdfAlgorithm, kdfParameters, l0KeyId, l1KeyId, l2KeyId);
        }

        public static byte[] GetPassword(
            SecurityIdentifier gMsaSid,
            Guid kdsRootKeyId,
            byte[] kdsRootKey,
            string kdfAlgorithm,
            byte[] kdfParameters,
            int l0KeyId,
            int l1KeyId,
            int l2KeyId
            )
        {
            Validator.AssertNotNull(kdsRootKey, nameof(kdsRootKey));
            Validator.AssertNotNullOrWhiteSpace(kdfAlgorithm, nameof(kdfAlgorithm));
            Validator.AssertNotNull(gMsaSid, nameof(gMsaSid));

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

            (byte[] l1Key, byte[] l2Key) = GetSidKeyLocal(kdsRootKeyId, kdsRootKey, kdfAlgorithm, kdfParameters, DefaultGMSASecurityDescriptor, l0KeyId, l1KeyId, l2KeyId);

            if (l2Key == null || l2Key.Length == 0)
            {
                // Recalculate the L2 key with new parameters
                int nextl2KeyId = L2KeyModulus - 1;
                l2Key = ClientComputeL2Key(
                    null,
                    kdsRootKeyId,
                    kdfAlgorithm,
                    kdfParameters,
                    l1Key,
                    null,
                    l0KeyId,
                    l1KeyId,
                    0,
                    1,
                    0,
                    nextl2KeyId);
            }

            NativeMethods.GenerateDerivedKey(
                    kdfAlgorithm,
                    kdfParameters,
                    l2Key,
                    gMsaSid.GetBinaryForm(),
                    null,
                    GmsaKdfLabel,
                    1,
                    GmsaPasswordLength,
                    out byte[] generatedPassword,
                    out string invalidAttribute
                );

            return generatedPassword;
        }

        public static void ParseSIDKeyResult(
            ProtectionKeyIdentifier managedPasswordId,
            int l1KeyId,
            int l2KeyId,
            bool isL2KeyEmpty,
            out int l1KeyIteration,
            out int nextL1KeyId,
            out int l2KeyIteration,
            out int nextL2KeyId)
        {
            l1KeyIteration = 0;
            l2KeyIteration = 0;
            nextL1KeyId = 0;
            nextL2KeyId = 0;

            if (managedPasswordId != null)
            {
                if (isL2KeyEmpty)
                {
                    l1KeyIteration = l1KeyId - managedPasswordId.L1KeyId;
                    if (l1KeyIteration > 0)
                    {
                        nextL1KeyId = l1KeyId - 1;
                    }
                }
                else
                {
                    l1KeyIteration = l1KeyId - managedPasswordId.L1KeyId - 1;
                    if (l1KeyIteration > 0)
                    {
                        nextL1KeyId = l1KeyId - 2;
                    }
                }

                if (isL2KeyEmpty || l1KeyId > managedPasswordId.L1KeyId)
                {
                    l2KeyIteration = L2KeyModulus - managedPasswordId.L2KeyId;
                    nextL2KeyId = L2KeyModulus - 1;
                }
                else
                {
                    l2KeyIteration = l2KeyId - managedPasswordId.L2KeyId;
                    if (l2KeyIteration > 0)
                    {
                        nextL2KeyId = l2KeyId - 1;
                    }
                }
            }
            else
            {
                if (isL2KeyEmpty)
                {
                    l2KeyIteration = 1;
                    nextL2KeyId = L2KeyModulus - 1;
                }
            }
        }

        public static (int l0KeyId, int l1KeyId, int l2KeyId) GetIntervalId(
            DateTime previousPasswordChange,
            DateTime effectiveTime,
            int managedPasswordInterval = DefaultManagedPasswordInterval,
            bool isClockSkewConsidered = false
            )
        {
            if(managedPasswordInterval <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(managedPasswordInterval));
            }

            if(effectiveTime < previousPasswordChange)
            {
                throw new ArgumentOutOfRangeException(nameof(effectiveTime));
            }

            int daysDifference = (int)(effectiveTime - previousPasswordChange).TotalDays;
            int totalPasswordCycles = daysDifference / managedPasswordInterval;
            DateTime effectiveIntervalStartTime = previousPasswordChange.AddDays(managedPasswordInterval * totalPasswordCycles);

            long effectiveIntervalId = effectiveIntervalStartTime.ToFileTimeUtc();

            if(isClockSkewConsidered)
            {
                effectiveIntervalId += MaxClockSkew;
            }

            int effectiveKdsCycleId = (int)(effectiveIntervalId / KdsKeyCycleDuration);

            int l0KeyId = effectiveKdsCycleId / (L1KeyModulus * L2KeyModulus);
            int l1KeyId = (effectiveKdsCycleId / L2KeyModulus) % L1KeyModulus;
            int l2KeyId = effectiveKdsCycleId % L2KeyModulus;

            return (l0KeyId, l1KeyId, l2KeyId);
        }

        public static DateTime GetRootIntervalStart(int l0KeyId, int l1KeyId, int l2KeyId)
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

                    byte[] binaryMagic = reader.ReadBytes(sizeof(int));
                    string magic = ASCIIEncoding.ASCII.GetString(binaryMagic);
                    Validator.AssertEquals(SecretAgreementParametersHeaderMagic, magic, nameof(blob));

                    // DH public parameters:
                    int keySize = reader.ReadInt32();
                    Validator.AssertEquals(length, SecretAgreementParametersMinSize + 2 * keySize, nameof(length));

                    byte[] p = reader.ReadBytes(keySize);
                    byte[] g = reader.ReadBytes(keySize);

                    return (p, g);
                }
            }
        }
    }
}
