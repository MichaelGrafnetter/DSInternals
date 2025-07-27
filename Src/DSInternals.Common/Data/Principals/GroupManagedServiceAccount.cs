using System;
using System.Security;
using System.Security.Principal;
using DSInternals.Common.Interop;
using DSInternals.Common.Schema;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Group Managed Service Account.
    /// </summary>
    /// <see>https://learn.microsoft.com/en-us/windows/win32/adschema/c-msds-groupmanagedserviceaccount</see>
    public class GroupManagedServiceAccount
    {
        /// <summary>
        /// Default security descriptor for gMSA passwords.
        /// </summary>
        /// <remarks>
        /// SDDL: O:SYD:(A;;FRFW;;;ED) - Enterprise Domain Controllers
        /// https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/9cd2fc5e-7305-4fb8-b233-2a60bc3eec68
        /// </remarks>
        private static readonly byte[] DefaultGMSASecurityDescriptor = "010004803000000000000000000000001400000002001c0001000000000014009f011200010100000000000509000000010100000000000512000000".HexToBinary();
        private const string GmsaKdfLabel = "GMSA PASSWORD";
        private const int GmsaPasswordLength = 256;
        private const int DefaultManagedPasswordInterval = 30; // in days
        private static readonly TimeSpan MaxClockSkew = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Key identifier for the current managed password data.
        /// </summary>
        public ProtectionKeyIdentifier? ManagedPasswordId
        {
            get;
            private set;
        }

        /// <summary>
        /// Key identifier for the previous managed password data.
        /// </summary>
        public ProtectionKeyIdentifier? ManagedPasswordPreviousId
        {
            get;
            private set;
        }

        /// <summary>
        /// Number of days before the managed password is automatically changed.
        /// </summary>
        public int? ManagedPasswordInterval
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the distinguished name (DN) for this <see cref="DSAccount"/>.
        /// </summary>
        public string? DistinguishedName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Security ID (SID) of the <see cref="DSAccount"/>.
        /// </summary>
        public SecurityIdentifier? Sid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the GUID associated with this <see cref="DSAccount"/>.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public Guid? Guid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the description of the <see cref="DSAccount"/>.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string? Description
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a boolean value indicating whether this <see cref="DSAccount"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get
            {
                return !this.UserAccountControl.HasFlag(UserAccountControl.Disabled);
            }
        }

        /// <summary>
        /// Gets the flags that control the behavior of the user account.
        /// </summary>
        /// <value>
        /// The value can be zero or a combination of one or more flags.
        /// </value>
        public UserAccountControl UserAccountControl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the encryption types supported by this trust relationship.
        /// </summary>
        /// <remarks>Implemented on Windows Server 2008 operating system and later.</remarks>
        public SupportedEncryptionTypes? SupportedEncryptionTypes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a boolean value indicating whether this <see cref="DSAccount"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the SAM account name for this <see cref="DSAccount"/>.
        /// </summary>
        public string? SamAccountName
        {
            get;
            private set;
        }

        /// <summary>
        /// List of principal names used for mutual authentication with an instance of a service.
        /// </summary>
        public string[]? ServicePrincipalName
        {
            get;
            private set;
        }

        public DateTime? PasswordLastSet 
        {
            get;
            private set;
        }

        public DateTime WhenCreated
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current encrypted password.
        /// </summary>
        public SecureString? SecureManagedPassword
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current cleartext password.
        /// </summary>
        public string? ManagedPassword
        {
            get
            {
                return this.SecureManagedPassword?.ToUnicodeString();
            }
        }

        /// <summary>
        /// Gets the account's password in Windows NT operating system one-way format (OWF).
        /// </summary>
        public byte[]? NTHash
        {
            get;
            private set;
        }

        public KerberosKeyDataNew[]? KerberosKeys
        {
            get;
            private set;
        }

        public ProtectionKeyIdentifier? EffectivePasswordId
        {
            get;
            private set;
        }

        public GroupManagedServiceAccount(DirectoryObject dsObject)
        {
            // TODO: Check that this object is a gMSA or dMSA

            /* Load gMSA-specific attributes first */

            // Read and parse msDS-ManagedPasswordId
            dsObject.ReadAttribute(CommonDirectoryAttributes.ManagedPasswordId, out byte[] rawManagedPasswordId);
            if (rawManagedPasswordId != null )
            {
                this.ManagedPasswordId = new ProtectionKeyIdentifier(rawManagedPasswordId);
            }

            // Read and parse msDS-ManagedPasswordPreviousId
            dsObject.ReadAttribute(CommonDirectoryAttributes.ManagedPasswordPreviousId, out byte[] rawManagedPasswordPreviousId);
            if (rawManagedPasswordPreviousId != null)
            {
                this.ManagedPasswordPreviousId = new ProtectionKeyIdentifier(rawManagedPasswordPreviousId);
            }

            // Read msDS-ManagedPasswordInterval
            dsObject.ReadAttribute(CommonDirectoryAttributes.ManagedPasswordInterval, out int? managedPasswordInterval);
            this.ManagedPasswordInterval = managedPasswordInterval;

            /* Load other account attributes */

            // Guid:
            this.Guid = dsObject.Guid;

            // DN:
            this.DistinguishedName = dsObject.DistinguishedName;

            // Sid:
            this.Sid = dsObject.Sid;

            // Description
            dsObject.ReadAttribute(CommonDirectoryAttributes.Description, out string description);
            this.Description = description;

            // Service Principal Name(s)
            dsObject.ReadAttribute(CommonDirectoryAttributes.ServicePrincipalName, out string[] spn);
            this.ServicePrincipalName = spn;

            // UAC (should never be null)
            dsObject.ReadAttribute(CommonDirectoryAttributes.UserAccountControl, out int? numericUac);
            this.UserAccountControl = (UserAccountControl)(numericUac ?? 0);

            // Deleted:
            dsObject.ReadAttribute(CommonDirectoryAttributes.IsDeleted, out bool isDeleted);
            this.Deleted = isDeleted;

            // SamAccountName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SamAccountName, out string samAccountName);
            this.SamAccountName = samAccountName;

            // SuportedEncryptionTypes
            dsObject.ReadAttribute(CommonDirectoryAttributes.SupportedEncryptionTypes, out int? numericSupportedEncryptionTypes);
            // Note: The value is store as int in the DB, but the documentation says that it is an unsigned int
            this.SupportedEncryptionTypes = (SupportedEncryptionTypes?)numericSupportedEncryptionTypes;

            // pwdLastSet
            dsObject.ReadAttribute(CommonDirectoryAttributes.PasswordLastSet, out DateTime? passwordLastSet, false);
            this.PasswordLastSet = passwordLastSet;

            // whenCreated (should never be null)
            dsObject.ReadAttribute(CommonDirectoryAttributes.WhenCreated, out DateTime? whenCreated, true);
            this.WhenCreated = whenCreated ?? DateTime.MinValue;
        }

        public void CalculatePassword(KdsRootKey kdsRootKey, DateTime effectiveTime)
        {
            if (kdsRootKey == null)
            {
                throw new ArgumentOutOfRangeException(nameof(kdsRootKey));
            }

            // Calculate and cache the effective managed password cycle
            DateTime previousPasswordChange = this.PasswordLastSet ?? this.WhenCreated;
            (int l0KeyId, int l1KeyId, int l2KeyId) = GetIntervalId(previousPasswordChange, effectiveTime, this.ManagedPasswordInterval ?? DefaultManagedPasswordInterval);
            this.EffectivePasswordId = new ProtectionKeyIdentifier(kdsRootKey.KeyId, l0KeyId, l1KeyId, l2KeyId);

            // Calculate and cache the effective managed password
            byte[] managedPassword = CalculateManagedPassword(this.Sid, this.EffectivePasswordId.Value, kdsRootKey);
            this.SecureManagedPassword = managedPassword.ReadSecureWString(0);
            managedPassword.ZeroFill(); // Remove the cleartext password from memory

            // Derive password hashes, as the password itself is not that important
            this.NTHash = Cryptography.NTHash.ComputeHash(this.SecureManagedPassword);

            if (this.ManagedPasswordId.HasValue && this.ManagedPasswordId.Value.DomainName != null)
            {
                // We only need the DNS domain name from the managed password id. This attribute should always be populated.
                // TODO: We are not generating DES keys, as this feature does not yield expected results yet.
                this.KerberosKeys = new KerberosCredentialNew(this.SecureManagedPassword, this.SamAccountName, this.ManagedPasswordId.Value.DomainName, false).Credentials;
            }
        }

        public static byte[] CalculateManagedPassword(SecurityIdentifier gMsaSid, ProtectionKeyIdentifier keyIdentifier, KdsRootKey kdsRootKey)
        {
            if (gMsaSid == null)
            {
                throw new ArgumentNullException(nameof(gMsaSid));
            }

            if (kdsRootKey == null)
            {
                throw new ArgumentOutOfRangeException(nameof(kdsRootKey));
            }

            if (keyIdentifier.RootKeyId != kdsRootKey.KeyId)
            {
                throw new InvalidOperationException("The provided KDS root key does not match the gMSA password identifier.");
            }

            (byte[] l1Key, byte[] l2Key) = kdsRootKey.ComputeSidPrivateKey(DefaultGMSASecurityDescriptor, keyIdentifier.L0KeyId, keyIdentifier.L1KeyId, keyIdentifier.L2KeyId);

            if (l2Key == null || l2Key.Length == 0)
            {
                // TODO: Add test cases for this branch
                // Recalculate the L2 key with new parameters
                int nextl2KeyId = KdsRootKey.L2KeyModulus - 1;
                (_, l2Key) = KdsRootKey.ClientComputeL2Key(
                    kdsRootKey.KeyId,
                    kdsRootKey.KdfAlgorithm,
                    kdsRootKey.RawKdfParameters,
                    l1Key,
                    l2Key: null,
                    keyIdentifier.L0KeyId,
                    keyIdentifier.L1KeyId,
                    l1KeyIteration: 0,
                    l2KeyIteration: 1,
                    nextL1KeyId: 0,
                    nextl2KeyId);
            }

            Win32ErrorCode result = NativeMethods.GenerateDerivedKey(
                    kdsRootKey.KdfAlgorithm,
                    kdsRootKey.RawKdfParameters,
                    secret: l2Key,
                    context: gMsaSid.GetBinaryForm(),
                    counterOffset: null,
                    GmsaKdfLabel,
                    iteration: 1,
                    GmsaPasswordLength,
                    out byte[] generatedPassword,
                    out string invalidAttribute
                );

            Validator.AssertSuccess(result);

            return generatedPassword;
        }

        public static (int l0KeyId, int l1KeyId, int l2KeyId) GetIntervalId(
            DateTime previousPasswordChange,
            DateTime effectiveTime,
            int managedPasswordInterval = DefaultManagedPasswordInterval,
            bool isClockSkewConsidered = false
            )
        {
            if (managedPasswordInterval <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(managedPasswordInterval));
            }

            if (effectiveTime < previousPasswordChange)
            {
                throw new ArgumentOutOfRangeException(nameof(effectiveTime));
            }

            int daysDifference = (int)(effectiveTime - previousPasswordChange).TotalDays;
            int totalPasswordCycles = daysDifference / managedPasswordInterval;
            DateTime effectiveIntervalStartTime = previousPasswordChange.AddDays(managedPasswordInterval * totalPasswordCycles);

            if (isClockSkewConsidered)
            {
                effectiveIntervalStartTime += MaxClockSkew;
            }

            return KdsRootKey.GetKeyId(effectiveIntervalStartTime);
        }
    }
}
