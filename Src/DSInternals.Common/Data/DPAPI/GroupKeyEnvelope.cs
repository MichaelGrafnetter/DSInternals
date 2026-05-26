using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using DSInternals.Common.Interop;
using Windows.Win32;
using Windows.Win32.Security;

namespace DSInternals.Common.Data;

/// <summary>
/// Represents a Group Key Envelope (<c>KDSK</c>), a structure used by Windows DPAPI-NG to
/// transport the L1 and L2 seed keys (or the group public key) derived from a KDS root key.
/// </summary>
/// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-gkdi/192c061c-e740-4aa0-ab1d-6954fb3e58f7</remarks>
public class GroupKeyEnvelope
{
    /// <summary>
    /// ASCII magic value (<c>KDSK</c>) that identifies a Group Key Envelope structure.
    /// </summary>
    private const string KdsKeyMagic = "KDSK";

    /// <summary>
    /// Expected version of the Group Key Envelope structure.
    /// </summary>
    private const int ExpectedVersion = 1;

    /// <summary>
    /// Size of the fixed-length header of the Group Key Envelope structure, in bytes.
    /// </summary>
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

    /// <summary>
    /// Flags indicating whether the envelope carries a private key, a public key, or a symmetric key.
    /// </summary>
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

    /// <summary>
    /// The self-relative security descriptor of the principal that is allowed to derive the key.
    /// </summary>
    public byte[] TargetSecurityDescriptor
    {
        get;
        private set;
    }

    /// <summary>
    /// Initializes a new, empty <see cref="GroupKeyEnvelope"/> instance.
    /// </summary>
    /// <remarks>
    /// Private to force callers through the parsing constructor or the <see cref="Create(KdsRootKey, ProtectionKeyIdentifier, SecurityIdentifier)"/> factory methods.
    /// </remarks>
    private GroupKeyEnvelope()
    {
        // Private constructor to prevent instantiation without parameters.
    }

    /// <summary>
    /// Initializes a new <see cref="GroupKeyEnvelope"/> instance by parsing a binary <c>KDSK</c> blob.
    /// </summary>
    /// <param name="blob">The raw bytes of the Group Key Envelope structure.</param>
    /// <exception cref="ArgumentOutOfRangeException">The blob is too short or has an invalid version or magic value.</exception>
    public GroupKeyEnvelope(byte[] blob)
    {
        Validator.AssertMinLength(blob, StructureHeaderLength, nameof(blob));

        using (Stream stream = new MemoryStream(blob))
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // Version must be 0x00000001
                int version = reader.ReadInt32();

                ArgumentOutOfRangeException.ThrowIfNotEqual(version, ExpectedVersion);

                // Magic must be 0x4B53444B
                byte[] binaryMagic = reader.ReadBytes(sizeof(int));
                string magic = ASCIIEncoding.ASCII.GetString(binaryMagic);

                ArgumentOutOfRangeException.ThrowIfNotEqual(magic, KdsKeyMagic);

                this.Flags = (GroupKeyEnvelopeFlags)reader.ReadInt32();

                // An L0 index
                this.L0KeyId = reader.ReadInt32();

                // An L1 index
                this.L1KeyId = reader.ReadInt32();

                // An L2 index
                this.L2KeyId = reader.ReadInt32();

                // A root key identifier
                byte[] binaryRootKeyId = reader.ReadBytes(Marshal.SizeOf<Guid>());
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

    /// <summary>
    /// Creates a new <see cref="GroupKeyEnvelope"/> derived from a KDS root key and identified
    /// by a <see cref="ProtectionKeyIdentifier"/>, scoped to the supplied SID.
    /// </summary>
    /// <param name="rootKey">The KDS root key that the envelope's key is derived from.</param>
    /// <param name="keyIdentifier">The protection key identifier whose L0/L1/L2 cycle is being enveloped.</param>
    /// <param name="targetSID">The SID of the principal that is allowed to derive the key.</param>
    /// <returns>The constructed Group Key Envelope.</returns>
    /// <exception cref="ArgumentException">The supplied root key does not match the key identifier.</exception>
    public static GroupKeyEnvelope Create(KdsRootKey rootKey, ProtectionKeyIdentifier keyIdentifier, SecurityIdentifier targetSID)
    {
        ArgumentNullException.ThrowIfNull(rootKey);

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

    /// <summary>
    /// Creates a new <see cref="GroupKeyEnvelope"/> derived from a KDS root key for the given L0/L1/L2 cycle and target SID.
    /// </summary>
    /// <param name="rootKey">The KDS root key that the envelope's key is derived from.</param>
    /// <param name="l0KeyId">The L0 index of the key being enveloped.</param>
    /// <param name="l1KeyId">The L1 index of the key being enveloped.</param>
    /// <param name="l2KeyId">The L2 index of the key being enveloped.</param>
    /// <param name="targetSID">The SID of the principal that is allowed to derive the key.</param>
    /// <param name="domain">DNS name of the Active Directory domain that issued the key.</param>
    /// <param name="forest">DNS name of the Active Directory forest that issued the key.</param>
    /// <returns>The constructed Group Key Envelope.</returns>
    public static GroupKeyEnvelope Create(KdsRootKey rootKey, int l0KeyId, int l1KeyId, int l2KeyId, SecurityIdentifier targetSID, string domain, string forest)
    {
        ArgumentNullException.ThrowIfNull(targetSID);

        byte[] targetSecurityDescriptor = ConvertSidToSecurityDescriptor(targetSID);
        return Create(rootKey, l0KeyId, l1KeyId, l2KeyId, targetSecurityDescriptor, domain, forest);
    }

    /// <summary>
    /// Creates a new <see cref="GroupKeyEnvelope"/> derived from a KDS root key for the given L0/L1/L2 cycle and target security descriptor.
    /// </summary>
    /// <param name="rootKey">The KDS root key that the envelope's key is derived from.</param>
    /// <param name="l0KeyId">The L0 index of the key being enveloped.</param>
    /// <param name="l1KeyId">The L1 index of the key being enveloped.</param>
    /// <param name="l2KeyId">The L2 index of the key being enveloped.</param>
    /// <param name="targetSecurityDescriptor">The self-relative security descriptor of the principal that is allowed to derive the key.</param>
    /// <param name="domainName">DNS name of the Active Directory domain that issued the key.</param>
    /// <param name="forestName">DNS name of the Active Directory forest that issued the key.</param>
    /// <returns>The constructed Group Key Envelope.</returns>
    public static GroupKeyEnvelope Create(KdsRootKey rootKey, int l0KeyId, int l1KeyId, int l2KeyId, byte[] targetSecurityDescriptor, string domainName, string forestName)
    {
        ArgumentNullException.ThrowIfNull(rootKey);

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

    /// <summary>
    /// Writes this envelope into the local SID key cache that the native DPAPI-NG implementation consults during decryption.
    /// </summary>
    /// <returns>The full path to the cache file that holds the envelope.</returns>
    /// <exception cref="InvalidOperationException">The <see cref="TargetSecurityDescriptor"/> has not been populated.</exception>
    public string WriteToCache()
    {
        if (this.TargetSecurityDescriptor == null)
        {
            throw new InvalidOperationException("The target SD must be populated.");
        }

        Win32ErrorCode result = NativeMethods.GetSIDKeyCacheFolder(this.TargetSecurityDescriptor, out string userStorageArea, out string sidKeyCacheFolder);
        Validator.AssertSuccess(result);

        bool publicKey = this.Flags == GroupKeyEnvelopeFlags.PublicAsymmetricKey;
        result = NativeMethods.GetSIDKeyFileName(this.RootKeyId, this.L0KeyId, publicKey, sidKeyCacheFolder, out string? sidKeyFileName);
        Validator.AssertSuccess(result);

        if (sidKeyFileName != null && File.Exists(sidKeyFileName))
        {
            // The key is already cached.
            return sidKeyFileName;
        }

        byte[] binarySidKey = this.ToByteArray();

        result = NativeMethods.WriteSIDKeyInCache(binarySidKey, this.TargetSecurityDescriptor, sidKeyCacheFolder, userStorageArea);
        Validator.AssertSuccess(result);

        return sidKeyFileName;
    }

    /// <summary>
    /// Serializes this envelope into its on-the-wire <c>KDSK</c> binary representation.
    /// </summary>
    /// <returns>The serialized Group Key Envelope.</returns>
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

    /// <summary>
    /// Deletes all SID-protected group keys cached on the local machine by the current user.
    /// </summary>
    public static void DeleteAllCachedKeys()
    {
        Win32ErrorCode result = NativeMethods.DeleteAllCachedKeys();

        // FILE_NOT_FOUND is returned when there is nothing cached to delete, which we treat as success.
        if (result == Win32ErrorCode.FILE_NOT_FOUND)
        {
            return;
        }

        Validator.AssertSuccess(result);
    }

    /// <summary>
    /// Returns the on-the-wire length, in bytes, of a UTF-16 string with a trailing null terminator.
    /// </summary>
    /// <param name="str">The string whose serialized length is being computed. <see langword="null"/> is treated as an empty string.</param>
    /// <returns>The number of bytes the string occupies when serialized, including the null terminator.</returns>
    private static int GetStringLength(string str)
    {
        return ((str?.Length ?? 0) + 1) * sizeof(char); // +1 for the null terminator
    }

    /// <summary>
    /// Builds a self-relative security descriptor that the SID key cache uses to authorize derivation of a SID-scoped group key.
    /// </summary>
    /// <param name="targetSID">The SID of the principal that is allowed to derive the key.</param>
    /// <returns>The self-relative security descriptor as a byte array.</returns>
    /// <remarks>This method returns slightly different results than the managed ACLs.</remarks>
    public static unsafe byte[] ConvertSidToSecurityDescriptor(SecurityIdentifier targetSID)
    {
        ArgumentNullException.ThrowIfNull(targetSID);

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
