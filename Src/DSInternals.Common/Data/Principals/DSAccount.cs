namespace DSInternals.Common.Data
{
    using System;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Properties;

    public class DSAccount
    {
        public DSAccount(DirectoryObject dsObject, string netBIOSDomainName, DirectorySecretDecryptor pek, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, nameof(dsObject));
            Validator.AssertNotNull(netBIOSDomainName, nameof(netBIOSDomainName));

            // Load and validate SamAccountType
            dsObject.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out SamAccountType? accountType);

            if(propertySets.HasFlag(AccountPropertySets.KeyCredentials) && ! propertySets.HasFlag(AccountPropertySets.DistinguishedName))
            {
                // Object DN is needed for key credential construction
                propertySets |= AccountPropertySets.DistinguishedName;
            }

            switch (accountType)
            {
                case SamAccountType.User:
                case SamAccountType.Computer:
                case SamAccountType.Trust:
                    this.SamAccountType = accountType.Value;
                    break;
                default:
                    throw new ArgumentException(Resources.ObjectNotAccountMessage);
            }

            // Common properties
            this.LoadAccountInfo(dsObject, netBIOSDomainName, propertySets);

            // Hashes and Supplemental Credentials
            if (pek != null)
            {
                // Only continue if we have a decryption key
                this.LoadSecrets(dsObject, pek, propertySets);
            }

            if (propertySets.HasFlag(AccountPropertySets.KeyCredentials))
            {
                // Windows Hello for Business
                this.LoadKeyCredentials(dsObject);
            }
        }

        /// <summary>
        /// Gets the distinguished name (DN) for this <see cref="DSAccount"/>.
        /// </summary>
        public string DistinguishedName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Security ID (SID) of the <see cref="DSAccount"/>.
        /// </summary>
        public SecurityIdentifier Sid
        {
            get;
            private set;
        }

        public SecurityIdentifier[] SidHistory
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the security descriptor of the object.
        /// </summary>
        public RawSecurityDescriptor SecurityDescriptor
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
        public Guid Guid
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
        public string Description
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

        public bool SupportsKerberosAESEncryption
        {
            get
            {
                if (!this.SupportedEncryptionTypes.HasValue)
                {
                    return false;
                }

                return this.SupportedEncryptionTypes.Value.HasFlag(Data.SupportedEncryptionTypes.AES128_CTS_HMAC_SHA1_96) ||
                       this.SupportedEncryptionTypes.Value.HasFlag(Data.SupportedEncryptionTypes.AES256_CTS_HMAC_SHA1_96);
            }
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
        /// Gets the Nullable DateTime that specifies the date and time of the last logon for this <see cref="DSAccount"/>.
        /// </summary>
        /// <remarks>
        /// Local, nonreplicated value.
        /// </remarks>
        public DateTime? LastLogon
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Nullable DateTime that specifies the date and time of the last logon for this <see cref="DSAccount"/>.
        /// </summary>
        /// <remarks>
        /// Replicated value
        /// </remarks>
        public DateTime? LastLogonTimestamp
        {
            get;
            private set;
        }

        public DateTime? LastLogonDate
        {
            get
            {
                // lastLogon is not replicated, lastLogonTimestamp is but it's not as accurate, so if we can't find lastLogon, try using lastLogonTimestamp instead
                return this.LastLogon ?? this.LastLogonTimestamp;
            }
        }

        /// <summary>
        /// Gets the date and time when the password was set for this account.
        /// </summary>
        public DateTime? PasswordLastSet
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the user principal name (UPN) associated with this <see cref="DSAccount"/>.
        /// </summary>
        public string UserPrincipalName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the SAM account name for this <see cref="DSAccount"/>.
        /// </summary>
        public string SamAccountName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the pre-Windows 2000 logon name of this this <see cref="DSAccount"/>.
        /// </summary>
        public string LogonName
        {
            get;
            private set;
        }

        public int PrimaryGroupId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of the account object.
        /// </summary>
        public SamAccountType SamAccountType
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates that a given object has had its ACLs changed to a more secure value
        /// by the system because it was a member of one of the administrative groups
        /// (directly or transitively).
        /// </summary>
        public bool AdminCount
        {
            get;
            private set;
        }

        /// <summary>
        /// List of principal names used for mutual authentication with an instance of a service.
        /// </summary>
        public string[] ServicePrincipalName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the account's password in Windows NT operating system one-way format (OWF).
        /// </summary>
        public byte[] NTHash
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the account's password in LAN Manager (LM) one-way format (OWF).
        /// </summary>
        /// <remarks>The LM OWF is used for compatibility with LAN Manager 2.x clients, Windows 95, and Windows 98.</remarks>
        public byte[] LMHash
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets password history of the user in Windows NT operating system one-way format (OWF). 
        /// </summary>
        public byte[][] NTHashHistory
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the password history of the user in LAN Manager (LM) one-way format (OWF).
        /// </summary>
        /// <value>
        /// The lm hash history.
        /// </value>
        public byte[][] LMHashHistory
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the stored credentials for use in authenticating.
        /// </summary>
        public SupplementalCredentials SupplementalCredentials
        {
            get;
            private set;
        }


        /// <summary>
        /// Device Registration / Windows Hello for Business Keys
        /// </summary>
        public KeyCredential[] KeyCredentials
        {
            get;
            private set;
        }

        protected void LoadAccountInfo(DirectoryObject dsObject, string netBIOSDomainName, AccountPropertySets propertySets)
        {
            // SamAccountName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SAMAccountName, out string samAccountName);
            this.SamAccountName = samAccountName;

            // LogonName (DOMAIN\SamAccountName):
            if (!string.IsNullOrEmpty(samAccountName))
            {
                this.LogonName = new NTAccount(netBIOSDomainName, samAccountName).ToString();
            }

            // Service Principal Name(s):
            dsObject.ReadAttribute(CommonDirectoryAttributes.ServicePrincipalName, out string[] spn);
            this.ServicePrincipalName = spn;

            // ObjectGuid:
            this.Guid = dsObject.Guid;

            // ObjectSid:
            this.Sid = dsObject.Sid;

            // UAC:
            dsObject.ReadAttribute(CommonDirectoryAttributes.UserAccountControl, out int? numericUac);
            this.UserAccountControl = (UserAccountControl)numericUac.Value;

            // Deleted:
            dsObject.ReadAttribute(CommonDirectoryAttributes.IsDeleted, out bool isDeleted);
            this.Deleted = isDeleted;

            // AdminCount (Although the schema defines it as Int32, it can only have values 0 and 1, so we directly convert it to bool)
            dsObject.ReadAttribute(CommonDirectoryAttributes.AdminCount, out bool adminCount);
            this.AdminCount = adminCount;

            // SuportedEncryptionTypes:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SupportedEncryptionTypes, out int? numericSupportedEncryptionTypes);
            // Note: The value is stored as int in the DB, but the documentation says that it is an unsigned int
            this.SupportedEncryptionTypes = (SupportedEncryptionTypes?)numericSupportedEncryptionTypes;

            // PrimaryGroupId:
            dsObject.ReadAttribute(CommonDirectoryAttributes.PrimaryGroupId, out int? groupId);
            this.PrimaryGroupId = groupId.Value;

            if (propertySets.HasFlag(AccountPropertySets.DistinguishedName))
            {
                // Note: DN loading from the DB involves one or more seeks.
                this.DistinguishedName = dsObject.DistinguishedName;
            }

            if(propertySets.HasFlag(AccountPropertySets.SecurityDescriptor))
            {
                // Note: Security descriptor loading from the DB involves a seek and binary data parsing.
                dsObject.ReadAttribute(CommonDirectoryAttributes.SecurityDescriptor, out RawSecurityDescriptor securityDescriptor);
                this.SecurityDescriptor = securityDescriptor;
            }

            if(propertySets.HasFlag(AccountPropertySets.GenericAccountInfo))
            {
                // SidHistory:
                dsObject.ReadAttribute(CommonDirectoryAttributes.SIDHistory, out SecurityIdentifier[] sidHistory);
                this.SidHistory = sidHistory;

                // UPN:
                dsObject.ReadAttribute(CommonDirectoryAttributes.UserPrincipalName, out string upn);
                this.UserPrincipalName = upn;

                // LastLogon:
                dsObject.ReadAttribute(CommonDirectoryAttributes.LastLogon, out DateTime? lastLogon, false);
                this.LastLogon = lastLogon;

                // LastLogonTimestamp:
                dsObject.ReadAttribute(CommonDirectoryAttributes.LastLogonTimestamp, out DateTime? lastLogonTimestamp, false);
                this.LastLogonTimestamp = lastLogonTimestamp;

                // PwdLastSet
                dsObject.ReadAttribute(CommonDirectoryAttributes.PasswordLastSet, out DateTime? pwdLastSet, false);
                this.PasswordLastSet = pwdLastSet;

                // Description
                dsObject.ReadAttribute(CommonDirectoryAttributes.Description, out string description);
                this.Description = description;
            }
        }

        protected void LoadSecrets(DirectoryObject dsObject, DirectorySecretDecryptor pek, AccountPropertySets propertySets)
        {
            if (propertySets.HasFlag(AccountPropertySets.LMHash))
            {
                // LM Hash:
                byte[] encryptedLmHash;
                dsObject.ReadAttribute(CommonDirectoryAttributes.LMHash, out encryptedLmHash);
                if (encryptedLmHash != null)
                {
                    this.LMHash = pek.DecryptHash(encryptedLmHash, this.Sid.GetRid());
                }
            }

            if (propertySets.HasFlag(AccountPropertySets.LMHashHistory))
            {
                // LM Hash History:
                byte[] encryptedLmHashHistory;
                dsObject.ReadAttribute(CommonDirectoryAttributes.LMHashHistory, out encryptedLmHashHistory);
                if (encryptedLmHashHistory != null)
                {
                    this.LMHashHistory = pek.DecryptHashHistory(encryptedLmHashHistory, this.Sid.GetRid());
                }
            }

            if (propertySets.HasFlag(AccountPropertySets.NTHash))
            {
                // NT Hash:
                byte[] encryptedNtHash;
                dsObject.ReadAttribute(CommonDirectoryAttributes.NTHash, out encryptedNtHash);
                if (encryptedNtHash != null)
                {
                    this.NTHash = pek.DecryptHash(encryptedNtHash, this.Sid.GetRid());
                }
            }

            if (propertySets.HasFlag(AccountPropertySets.NTHashHistory))
            {
                // NT Hash History:
                byte[] encryptedNtHashHistory;
                dsObject.ReadAttribute(CommonDirectoryAttributes.NTHashHistory, out encryptedNtHashHistory);
                if (encryptedNtHashHistory != null)
                {
                    this.NTHashHistory = pek.DecryptHashHistory(encryptedNtHashHistory, this.Sid.GetRid());
                }
            }

            if (propertySets.HasFlag(AccountPropertySets.SupplementalCredentials))
            {
                // Supplemental Credentials:
                byte[] encryptedSupplementalCredentials;
                dsObject.ReadAttribute(CommonDirectoryAttributes.SupplementalCredentials, out encryptedSupplementalCredentials);
                if (encryptedSupplementalCredentials != null)
                {
                    byte[] binarySupplementalCredentials = pek.DecryptSecret(encryptedSupplementalCredentials);
                    this.SupplementalCredentials = new SupplementalCredentials(binarySupplementalCredentials);
                }
            }
        }

        /// <summary>
        /// Loads key credentials.
        /// </summary>
        protected void LoadKeyCredentials(DirectoryObject dsObject)
        {
            // This attribute has been added in Windows Server 2016, so it might not be present on older DCs.
            byte[][] keyCredentialBlobs;
            dsObject.ReadLinkedValues(CommonDirectoryAttributes.KeyCredentialLink, out keyCredentialBlobs);

            if (keyCredentialBlobs != null)
            {
                // Parse the blobs and combine them into one array.
                this.KeyCredentials = new KeyCredential[keyCredentialBlobs.Length];

                for(int i = 0; i < keyCredentialBlobs.Length; i++)
                {
                    this.KeyCredentials[i] = new KeyCredential(keyCredentialBlobs[i], this.DistinguishedName);
                }
            }
        }
    }
}
