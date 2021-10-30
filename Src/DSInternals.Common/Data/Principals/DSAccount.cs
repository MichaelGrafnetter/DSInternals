namespace DSInternals.Common.Data
{
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Properties;
    using System;
    using System.Collections.Generic;
    using System.Security.AccessControl;
    using System.Security.Principal;

    public class DSAccount
    {
        public DSAccount(DirectoryObject dsObject, string netBIOSDomainName, DirectorySecretDecryptor pek)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, nameof(dsObject));
            Validator.AssertNotNull(netBIOSDomainName, nameof(netBIOSDomainName));

            if (!dsObject.IsAccount)
            {
                throw new ArgumentException(Resources.ObjectNotAccountMessage);
            }

            // Common properties
            this.LoadAccountInfo(dsObject, netBIOSDomainName);

            // Credential Roaming
            this.LoadRoamedCredentials(dsObject);

            // Windows Hello for Business
            this.LoadKeyCredentials(dsObject);

            // Hashes and Supplemental Credentials
            this.LoadHashes(dsObject, pek);
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
        /// Gets the display name for this <see cref="DSAccount"/>.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName
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
        /// Gets the given name for the <see cref="DSAccount"/>.
        /// </summary>
        public string GivenName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the surname for the user <see cref="DSAccount"/>. 
        /// </summary>
        public string Surname
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

        public DateTime? RoamedCredentialsCreated
        {
            get;
            private set;
        }

        public DateTime? RoamedCredentialsModified
        {
            get;
            private set;
        }

        public RoamedCredential[] RoamedCredentials
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

        protected void LoadAccountInfo(DirectoryObject dsObject, string netBIOSDomainName)
        {
            // Guid:
            this.Guid = dsObject.Guid;

            // DN:
            this.DistinguishedName = dsObject.DistinguishedName;

            // Sid:
            this.Sid = dsObject.Sid;

            // SidHistory:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SIDHistory, out SecurityIdentifier[] sidHistory);
            this.SidHistory = sidHistory;

            // DisplayName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.DisplayName, out string displayName);
            this.DisplayName = displayName;

            // Description
            dsObject.ReadAttribute(CommonDirectoryAttributes.Description, out string description);
            this.Description = description;

            // GivenName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.GivenName, out string givenName);
            this.GivenName = givenName;

            // Surname:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Surname, out string surname);
            this.Surname = surname;

            // Security Descriptor:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SecurityDescriptor, out RawSecurityDescriptor securityDescriptor);
            this.SecurityDescriptor = securityDescriptor;

            // AdminCount (Although the schema defines it as Int32, it can only have values 0 and 1, so we directly convert it to bool)
            dsObject.ReadAttribute(CommonDirectoryAttributes.AdminCount, out bool adminCount);
            this.AdminCount = adminCount;

            // Service Principal Name(s)
            dsObject.ReadAttribute(CommonDirectoryAttributes.ServicePrincipalName, out string[] spn);
            this.ServicePrincipalName = spn;

            // UAC:
            dsObject.ReadAttribute(CommonDirectoryAttributes.UserAccountControl, out int? numericUac);
            this.UserAccountControl = (UserAccountControl)numericUac.Value;

            // Deleted:
            dsObject.ReadAttribute(CommonDirectoryAttributes.IsDeleted, out bool isDeleted);
            this.Deleted = isDeleted;

            // LastLogon:
            dsObject.ReadAttribute(CommonDirectoryAttributes.LastLogon, out DateTime? lastLogon);
            this.LastLogon = lastLogon;

            dsObject.ReadAttribute(CommonDirectoryAttributes.LastLogonTimestamp, out DateTime? lastLogonTimestamp);
            this.LastLogonTimestamp = lastLogonTimestamp;

            // UPN:
            dsObject.ReadAttribute(CommonDirectoryAttributes.UserPrincipalName, out string upn);
            this.UserPrincipalName = upn;

            // SamAccountName + LogonName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SAMAccountName, out string samAccountName);
            this.SamAccountName = samAccountName;
            this.LogonName = new NTAccount(netBIOSDomainName, samAccountName).Value;

            // SamAccountType:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out int? numericAccountType);
            this.SamAccountType = (SamAccountType)numericAccountType.Value;

            // PrimaryGroupId
            dsObject.ReadAttribute(CommonDirectoryAttributes.PrimaryGroupId, out int? groupId);
            this.PrimaryGroupId = groupId.Value;

            // SuportedEncryptionTypes
            dsObject.ReadAttribute(CommonDirectoryAttributes.SupportedEncryptionTypes, out int? numericSupportedEncryptionTypes);
            // Note: The value is store as int in the DB, but the documentation says that it is an unsigned int
            this.SupportedEncryptionTypes = (SupportedEncryptionTypes?) numericSupportedEncryptionTypes;
        }

        protected void LoadHashes(DirectoryObject dsObject, DirectorySecretDecryptor pek)
        {
            if (pek == null)
            {
                // Do not continue if we do not have a decryption key
                return;
            }
            // NTHash:
            byte[] encryptedNtHash;
            dsObject.ReadAttribute(CommonDirectoryAttributes.NTHash, out encryptedNtHash);
            if (encryptedNtHash != null)
            {
                this.NTHash = pek.DecryptHash(encryptedNtHash, this.Sid.GetRid());
            }

            // LMHash
            byte[] encryptedLmHash;
            dsObject.ReadAttribute(CommonDirectoryAttributes.LMHash, out encryptedLmHash);
            if (encryptedLmHash != null)
            {
                this.LMHash = pek.DecryptHash(encryptedLmHash, this.Sid.GetRid());
            }

            // NTHashHistory:
            byte[] encryptedNtHashHistory;
            dsObject.ReadAttribute(CommonDirectoryAttributes.NTHashHistory, out encryptedNtHashHistory);
            if (encryptedNtHashHistory != null)
            {
                this.NTHashHistory = pek.DecryptHashHistory(encryptedNtHashHistory, this.Sid.GetRid());
            }

            // LMHashHistory:
            byte[] encryptedLmHashHistory;
            dsObject.ReadAttribute(CommonDirectoryAttributes.LMHashHistory, out encryptedLmHashHistory);
            if (encryptedLmHashHistory != null)
            {
                this.LMHashHistory = pek.DecryptHashHistory(encryptedLmHashHistory, this.Sid.GetRid());
            }

            // SupplementalCredentials:
            byte[] encryptedSupplementalCredentials;
            dsObject.ReadAttribute(CommonDirectoryAttributes.SupplementalCredentials, out encryptedSupplementalCredentials);
            if (encryptedSupplementalCredentials != null)
            {
                byte[] binarySupplementalCredentials = pek.DecryptSecret(encryptedSupplementalCredentials);
                this.SupplementalCredentials = new SupplementalCredentials(binarySupplementalCredentials);
            }
        }

        /// <summary>
        /// Loads credential roaming objects and timestamps.
        /// </summary>
        protected void LoadRoamedCredentials(DirectoryObject dsObject)
        {
            // These attributes have been added in Windows Server 2008, so they might not be present on older DCs.
            byte[] roamingTimeStamp;
            dsObject.ReadAttribute(CommonDirectoryAttributes.PKIRoamingTimeStamp, out roamingTimeStamp);

            if (roamingTimeStamp == null)
            {
                // This account does not have roamed credentials, so we skip their processing
                return;
            }

            // The 16B of the value consist of two 8B actual time stamps.
            long createdTimeStamp = BitConverter.ToInt64(roamingTimeStamp, 0);
            long modifiedTimeStamp = BitConverter.ToInt64(roamingTimeStamp, sizeof(long));

            this.RoamedCredentialsCreated = DateTime.FromFileTime(createdTimeStamp);
            this.RoamedCredentialsModified = DateTime.FromFileTime(modifiedTimeStamp);

            byte[][] masterKeyBlobs;
            dsObject.ReadLinkedValues(CommonDirectoryAttributes.PKIDPAPIMasterKeys, out masterKeyBlobs);

            byte[][] credentialBlobs;
            dsObject.ReadLinkedValues(CommonDirectoryAttributes.PKIAccountCredentials, out credentialBlobs);

            // Parse the blobs and combine them into one array.
            var credentials = new List<RoamedCredential>();

            if (masterKeyBlobs != null)
            {
                foreach (var blob in masterKeyBlobs)
                {
                    credentials.Add(new RoamedCredential(blob, this.SamAccountName, this.Sid));
                }
            }

            if(credentialBlobs != null)
            {
                foreach (var blob in credentialBlobs)
                {
                    credentials.Add(new RoamedCredential(blob, this.SamAccountName, this.Sid));
                }
            }

            this.RoamedCredentials = credentials.ToArray();
        }

        /// <summary>
        /// Loads key credentials.
        /// </summary>
        protected void LoadKeyCredentials(DirectoryObject dsObject)
        {
            // This attribute has been added in Windows Server 2016, so it might not be present on older DCs.
            byte[][] keyCredentialBlobs;
            dsObject.ReadLinkedValues(CommonDirectoryAttributes.KeyCredentialLink, out keyCredentialBlobs);

            // Parse the blobs and combine them into one array.
            var credentials = new List<KeyCredential>();

            if (keyCredentialBlobs != null)
            {
                foreach (var blob in keyCredentialBlobs)
                {
                    credentials.Add(new KeyCredential(blob, this.DistinguishedName));
                }
            }

            this.KeyCredentials = credentials.ToArray();
        }
    }
}
