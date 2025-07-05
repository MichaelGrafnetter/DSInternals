using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using DSInternals.Common.Interop;
using Windows.Win32;
using Windows.Win32.Security;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Group Key Envelope
    /// </summary>
    /// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-gkdi/192c061c-e740-4aa0-ab1d-6954fb3e58f7</remarks>
    public class GroupKeyEnvelope
    {
        private const string KdsKeyMagic = "KDSK";
        private const int ExpectedVersion = 1;
        private const int StructureHeaderLength = 16 * sizeof(int) + 16;

        /// <summary>
        /// The L0 index of the key being enveloped.
        /// </summary>
        public int L0KeyId
        {
            get;
            private set;
        }

        /// <summary>
        /// The L1 index of the key being enveloped.
        /// </summary>
        /// <remarks>
        /// Must be a number between 0 and 31, inclusive.
        /// </remarks>
        public int L1KeyId
        {
            get;
            private set;
        }

        /// <summary>
        /// The L2 index of the key being enveloped.
        /// </summary>
        /// <remarks>
        /// Must be a number between 0 and 31, inclusive.
        /// </remarks>
        public int L2KeyId
        {
            get;
            private set;
        }

        /// <summary>
        /// The root key identifier of the key being enveloped.
        /// </summary>
        public Guid RootKeyId
        {
            get;
            private set;
        }

        /// <summary>
        /// The domain name of the server in Domain Name System (DNS) format.
        /// </summary>
        public string DomainName
        {
            get;
            private set;
        }

        /// <summary>
        /// The forest name of the server in Domain Name System (DNS) format.
        /// </summary>
        public string ForestName
        {
            get;
            private set;
        }

        /// <summary>
        /// KDF algorithm name associated with the root key.
        /// </summary>
        public string KeyDerivationAlgorithm
        {
            get;
            private set;
        } = Data.KdsRootKey.DefaultKdfAlgorithm;

        /// <summary>
        /// KDF algorithm parameters associated with the root key.
        /// </summary>
        public byte[] KeyDerivationParameters
        {
            get;
            private set;
        } = Data.KdsRootKey.DefaultKdfParameters;

        /// <summary>
        /// Secret agreement algorithm name associated with the root key.
        /// </summary>
        public string SecretAgreementAlgorithm
        {
            get;
            private set;
        } = Data.KdsRootKey.DefaultSecretAgreementAlgorithm;

        /// <summary>
        /// Secret agreement algorithm parameters associated with the root key.
        /// </summary>
        public byte[] SecretAgreementParameters
        {
            get;
            private set;
        } = Data.KdsRootKey.DefaultSecretAgreementParameters;

        /// <summary>
        /// The L1 seed key.
        /// </summary>
        public byte[] L1Key
        {
            get;
            private set;
        }

        /// <summary>
        /// The L2 seed key or the group public key.
        /// </summary>
        public byte[] L2Key
        {
            get;
            private set;
        }

        public GroupKeyEnvelopeFlags Flags
        {
            get;
            private set;
        }

        /// <summary>
        /// The public key length associated with the root key.
        /// </summary>
        public int PublicKeyLength
        {
            get;
            private set;
        } = Data.KdsRootKey.DefaultPublicKeyLength;

        /// <summary>
        /// The private key length associated with the root key.
        /// </summary>
        public int PrivateKeyLength
        {
            get;
            private set;
        } = Data.KdsRootKey.DefaultPrivateKeyLength;

        public byte[] TargetSecurityDescriptor
        {
            get;
            private set;
        }

        private GroupKeyEnvelope()
        {
            // Private constructor to prevent instantiation without parameters.
        }

        public GroupKeyEnvelope(byte[] blob)
        {
            Validator.AssertMinLength(blob, StructureHeaderLength, nameof(blob));

            using (Stream stream = new MemoryStream(blob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // Version must be 0x00000001
                    int version = reader.ReadInt32();
                    Validator.AssertEquals(ExpectedVersion, version, nameof(version));

                    // Magic must be 0x4B53444B
                    byte[] binaryMagic = reader.ReadBytes(sizeof(int));
                    string magic = ASCIIEncoding.ASCII.GetString(binaryMagic);
                    Validator.AssertEquals(KdsKeyMagic, magic, nameof(magic));

                    this.Flags = (GroupKeyEnvelopeFlags) reader.ReadInt32();

                    // An L0 index
                    this.L0KeyId = reader.ReadInt32();

                    // An L1 index
                    this.L1KeyId = reader.ReadInt32();

                    // An L2 index
                    this.L2KeyId = reader.ReadInt32();

                    // A root key identifier
                    byte[] binaryRootKeyId = reader.ReadBytes(Marshal.SizeOf(typeof(Guid)));
                    this.RootKeyId = new Guid(binaryRootKeyId);

                    // Variable data lengths
                    int kdfAlgorithmLength = reader.ReadInt32();
                    int kdfParametersLength = reader.ReadInt32();
                    int secretAgreementAlgorithmLength = reader.ReadInt32();
                    int secretAgreementParametersLength = reader.ReadInt32();
                    this.PrivateKeyLength = reader.ReadInt32();
                    this.PublicKeyLength = reader.ReadInt32();
                    int l1KeyLength = reader.ReadInt32();
                    int l2KeyLength = reader.ReadInt32();
                    int domainNameLength = reader.ReadInt32();
                    int forestNameLength = reader.ReadInt32();

                    // Validate variable data length
                    int expectedLength = StructureHeaderLength + kdfAlgorithmLength + kdfParametersLength + secretAgreementAlgorithmLength + secretAgreementParametersLength + domainNameLength + forestNameLength + l1KeyLength + l2KeyLength;
                    Validator.AssertMinLength(blob, expectedLength, nameof(blob));

                    if (kdfAlgorithmLength > 0)
                    {
                        byte[] binaryKdf = reader.ReadBytes(kdfAlgorithmLength);
                        // Trim \0
                        this.KeyDerivationAlgorithm = Encoding.Unicode.GetString(binaryKdf, 0, binaryKdf.Length - sizeof(char));
                    }

                    if (kdfParametersLength > 0)
                    {
                        // Additional info used in key derivation
                        this.KeyDerivationParameters = reader.ReadBytes(kdfParametersLength);
                    }

                    if (secretAgreementAlgorithmLength > 0)
                    {
                        byte[] binaryAlgorithm = reader.ReadBytes(secretAgreementAlgorithmLength);
                        // Trim \0
                        this.SecretAgreementAlgorithm = Encoding.Unicode.GetString(binaryAlgorithm, 0, binaryAlgorithm.Length - sizeof(char));

                    }

                    if (secretAgreementParametersLength > 0)
                    {
                        this.SecretAgreementParameters = reader.ReadBytes(secretAgreementParametersLength);
                    }

                    if (domainNameLength > 0)
                    {
                        // DNS-style name of the Active Directory domain in which this identifier was created.
                        byte[] binaryDomainName = reader.ReadBytes(domainNameLength);
                        // Trim \0
                        this.DomainName = Encoding.Unicode.GetString(binaryDomainName, 0, binaryDomainName.Length - sizeof(char));
                    }

                    if (forestNameLength > 0)
                    {
                        // DNS-style name of the Active Directory forest in which this identifier was created.
                        byte[] binaryForestName = reader.ReadBytes(forestNameLength);
                        // Trim \0
                        this.ForestName = Encoding.Unicode.GetString(binaryForestName, 0, binaryForestName.Length - sizeof(char));
                    }

                    if (l1KeyLength > 0)
                    {
                        this.L1Key = reader.ReadBytes(l1KeyLength);
                    }

                    if (l2KeyLength > 0)
                    {
                        this.L2Key = reader.ReadBytes(l2KeyLength);
                    }
                }
            }
        }

        public static GroupKeyEnvelope Create(KdsRootKey rootKey, ProtectionKeyIdentifier keyIdentifier, SecurityIdentifier targetSID)
        {
            if (rootKey == null)
            {
                throw new ArgumentNullException(nameof(rootKey));
            }

            if (rootKey.KeyId != keyIdentifier.RootKeyId)
            {
                throw new ArgumentException("The provided KDS root key does not match the key identifier.", nameof(rootKey));
            }

            return Create(
                rootKey,
                keyIdentifier.L0KeyId,
                keyIdentifier.L1KeyId,
                keyIdentifier.L2KeyId,
                targetSID,
                keyIdentifier.DomainName,
                keyIdentifier.ForestName
            );
        }

        public static GroupKeyEnvelope Create(KdsRootKey rootKey, int l0KeyId, int l1KeyId, int l2KeyId, SecurityIdentifier targetSID, string domain, string forest)
        {
            if (targetSID == null)
            {
                throw new ArgumentNullException(nameof(targetSID));
            }

            byte[] targetSecurityDescriptor = ConvertSidToSecurityDescriptor(targetSID);
            return Create(rootKey, l0KeyId, l1KeyId, l2KeyId, targetSecurityDescriptor, domain, forest);
        }

        public static GroupKeyEnvelope Create(KdsRootKey rootKey, int l0KeyId, int l1KeyId, int l2KeyId, byte[] targetSecurityDescriptor, string domainName, string forestName)
        {
            if (rootKey == null)
            {
                throw new ArgumentNullException(nameof(rootKey));
            }

            // Populate new envelope properties from the provided parameters
            var envelope = new GroupKeyEnvelope();
            envelope.L0KeyId = l0KeyId;
            envelope.L1KeyId = l1KeyId;
            envelope.L2KeyId = l2KeyId;
            envelope.TargetSecurityDescriptor = targetSecurityDescriptor;
            envelope.Flags = GroupKeyEnvelopeFlags.PrivateAsymmetricKey;
            envelope.DomainName = domainName;
            envelope.ForestName = forestName;

            // Populate envelope properties from the KDS root key object
            envelope.RootKeyId = rootKey.KeyId;
            envelope.KeyDerivationAlgorithm = rootKey.KdfAlgorithm;
            envelope.KeyDerivationParameters = rootKey.RawKdfParameters;
            envelope.SecretAgreementAlgorithm = rootKey.SecretAgreementAlgorithm;
            envelope.SecretAgreementParameters = rootKey.SecretAgreementParameters;
            envelope.PublicKeyLength = rootKey.SecretAgreementPublicKeyLength;
            envelope.PrivateKeyLength = rootKey.SecretAgreementPrivateKeyLength;

            // Calculate the L1 and L2 keys based on the L0 key and the target security descriptor
            (envelope.L1Key, envelope.L2Key) = rootKey.ComputeSidPrivateKey(targetSecurityDescriptor, l0KeyId, l1KeyId, l2KeyId);

            return envelope;
        }

        public void WriteToCache()
        {
            if (this.TargetSecurityDescriptor == null)
            {
                throw new InvalidOperationException("The target SD must be populated.");
            }

            Win32ErrorCode result = NativeMethods.GetSIDKeyCacheFolder(this.TargetSecurityDescriptor, out string userStorageArea, out string sidKeyCacheFolder);
            Validator.AssertSuccess(result);

            byte[] binarySidKey = this.ToByteArray();

            result = NativeMethods.WriteSIDKeyInCache(binarySidKey, this.TargetSecurityDescriptor, sidKeyCacheFolder, userStorageArea);
            Validator.AssertSuccess(result);
        }

        public byte[] ToByteArray()
        {
            int structSize = StructureHeaderLength +
                GetStringLength(KeyDerivationAlgorithm) +
                (this.KeyDerivationParameters?.Length ?? 0) +
                GetStringLength(this.SecretAgreementAlgorithm) +
                (this.SecretAgreementParameters?.Length ?? 0) +
                GetStringLength(this.DomainName) +
                GetStringLength(this.ForestName) +
                (this.L1Key?.Length ?? 0) +
                (this.L2Key?.Length ?? 0);

            byte[] buffer = new byte[structSize];
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // Serialize struct header
                    writer.Write(ExpectedVersion);

                    byte[] magic = Encoding.ASCII.GetBytes(KdsKeyMagic);
                    writer.Write(magic);

                    writer.Write((int)this.Flags);
                    writer.Write(this.L0KeyId);
                    writer.Write(this.L1KeyId);
                    writer.Write(this.L2KeyId);
                    writer.Write(this.RootKeyId.ToByteArray());

                    writer.Write(GetStringLength(this.KeyDerivationAlgorithm));
                    writer.Write(KeyDerivationParameters?.Length ?? 0);
                    writer.Write(GetStringLength(this.SecretAgreementAlgorithm));
                    writer.Write(SecretAgreementParameters?.Length ?? 0);
                    writer.Write(this.PrivateKeyLength);
                    writer.Write(this.PublicKeyLength);
                    writer.Write(this.L1Key?.Length ?? 0);
                    writer.Write(this.L2Key?.Length ?? 0);
                    writer.Write(GetStringLength(this.DomainName));
                    writer.Write(GetStringLength(this.ForestName));

                    // Serialize variable length data
                    byte[] binaryKDF = Encoding.Unicode.GetBytes(this.KeyDerivationAlgorithm ?? string.Empty);
                    writer.Write(binaryKDF);
                    writer.Write(ushort.MinValue); // Null terminator

                    if (this.KeyDerivationParameters != null)
                    {
                        writer.Write(this.KeyDerivationParameters);
                    }

                    byte[] binarySecretAgreementAlgorithm = Encoding.Unicode.GetBytes(this.SecretAgreementAlgorithm ?? string.Empty);
                    writer.Write(binarySecretAgreementAlgorithm);
                    writer.Write(ushort.MinValue); // Null terminator

                    if (this.SecretAgreementParameters != null)
                    {
                        writer.Write(this.SecretAgreementParameters);
                    }

                    byte[] binaryDomainName = Encoding.Unicode.GetBytes(this.DomainName ?? string.Empty);
                    writer.Write(binaryDomainName);
                    writer.Write(ushort.MinValue); // Null terminator

                    byte[] binaryForestName = Encoding.Unicode.GetBytes(this.ForestName ?? string.Empty);
                    writer.Write(binaryForestName);
                    writer.Write(ushort.MinValue); // Null terminator

                    if (this.L1Key != null)
                    {
                        writer.Write(this.L1Key);
                    }

                    if (this.L2Key != null)
                    {
                        writer.Write(this.L2Key);
                    }
                }
            }
            return buffer;
        }

        public static void DeleteAllCachedKeys()
        {
            Win32ErrorCode result = NativeMethods.DeleteAllCachedKeys();
            Validator.AssertSuccess(result);
        }

        private static int GetStringLength(string str)
        {
            return ((str?.Length ?? 0) + 1) * sizeof(char); // +1 for the null terminator
        }

        /// <remarks>This method returns slightly different results than the managed ACLs.</remarks>
        public static unsafe byte[] ConvertSidToSecurityDescriptor(SecurityIdentifier targetSID)
        {
            if (targetSID == null)
            {
                throw new ArgumentNullException(nameof(targetSID));
            }

            byte[] targetSidBinary = new byte[targetSID.BinaryLength];
            targetSID.GetBinaryForm(targetSidBinary, 0);

            var everyoneSid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            byte[] everyoneSidBinary = new byte[everyoneSid.BinaryLength];
            everyoneSid.GetBinaryForm(everyoneSidBinary, 0);

            var systemSid = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
            byte[] systemSidBinary = new byte[systemSid.BinaryLength];
            systemSid.GetBinaryForm(systemSidBinary, 0);

            // Initialize the DACL
            int daclSize = Marshal.SizeOf<ACL>() + 2 * (Marshal.SizeOf<ACCESS_ALLOWED_ACE>() - sizeof(uint)) + targetSidBinary.Length + everyoneSidBinary.Length;
            byte[] daclBuffer = new byte[daclSize];

            SECURITY_DESCRIPTOR absoluteSD = default;

            fixed (byte* daclPtr = daclBuffer)
            fixed (byte* targetSidPtr = targetSidBinary)
            fixed (byte* everyoneSidPtr = everyoneSidBinary)
            fixed (byte* systemSidPtr = systemSidBinary)
            {
                // Populate the DACL header
                bool result = PInvoke.InitializeAcl((ACL*)daclPtr, (uint)daclSize, ACE_REVISION.ACL_REVISION);
                if (result)
                {
                    Validator.AssertSuccess((Win32ErrorCode)Marshal.GetLastWin32Error());
                }

                // Add R/W permissions for the target SID
                result = PInvoke.AddAccessAllowedAce((ACL*)daclPtr, ACE_REVISION.ACL_REVISION, (uint)(FileSystemRights.ReadData | FileSystemRights.WriteData), new PSID(targetSidPtr));
                if (result)
                {
                    Validator.AssertSuccess((Win32ErrorCode)Marshal.GetLastWin32Error());
                }

                // Add Write permissions for Everyone
                result = PInvoke.AddAccessAllowedAce((ACL*)daclPtr, ACE_REVISION.ACL_REVISION, (uint)(FileSystemRights.WriteData), new PSID(everyoneSidPtr));
                if (result)
                {
                    Validator.AssertSuccess((Win32ErrorCode)Marshal.GetLastWin32Error());
                }

                result = PInvoke.InitializeSecurityDescriptor(new PSECURITY_DESCRIPTOR(&absoluteSD), PInvoke.SECURITY_DESCRIPTOR_REVISION);
                if (result)
                {
                    Validator.AssertSuccess((Win32ErrorCode)Marshal.GetLastWin32Error());
                }

                // Set SYSTEM as the owner
                result = PInvoke.SetSecurityDescriptorOwner(new PSECURITY_DESCRIPTOR(&absoluteSD), new PSID(systemSidPtr), false);
                if (result)
                {
                    Validator.AssertSuccess((Win32ErrorCode)Marshal.GetLastWin32Error());
                }

                // Set SYSTEM as the owner group
                result = PInvoke.SetSecurityDescriptorGroup(new PSECURITY_DESCRIPTOR(&absoluteSD), new PSID(systemSidPtr), false);
                if (result)
                {
                    Validator.AssertSuccess((Win32ErrorCode)Marshal.GetLastWin32Error());
                }

                result = PInvoke.SetSecurityDescriptorDacl(new PSECURITY_DESCRIPTOR(&absoluteSD), true, (ACL*)daclPtr, false);
                if (result)
                {
                    Validator.AssertSuccess((Win32ErrorCode)Marshal.GetLastWin32Error());
                }

                // Check the security descriptor size
                uint sdBufferLength = 0;
                result = PInvoke.MakeSelfRelativeSD(new PSECURITY_DESCRIPTOR(&absoluteSD), PSECURITY_DESCRIPTOR.Null, ref sdBufferLength);
                if (result)
                {
                    var error = (Win32ErrorCode)Marshal.GetLastWin32Error();

                    if (error != Win32ErrorCode.INSUFFICIENT_BUFFER)
                    {
                        Validator.AssertSuccess(error);
                    }
                }

                // Serialize the security descriptor
                byte[] sdBuffer = new byte[sdBufferLength];
                fixed (byte* sdBufferPtr = sdBuffer)
                {
                    result = PInvoke.MakeSelfRelativeSD(new PSECURITY_DESCRIPTOR(&absoluteSD), new PSECURITY_DESCRIPTOR(sdBufferPtr), ref sdBufferLength);
                    if (result)
                    {
                        var error = (Win32ErrorCode)Marshal.GetLastWin32Error();
                    }
                }

                return sdBuffer;
            }
        }
    }
}
