﻿namespace DSInternals.Common.Data
{
    using DSInternals.Common.Cryptography;
    using System;
    using System.Collections.Generic;
    using System.Security.AccessControl;
    using System.Security.Principal;

    public class DSAccount
    {
        private string displayName;
        private string description;
        private string surname;
        private string givenName;
        private string samAccountName;
        private string upn;
        private string[] spn;
        private bool isDeleted;
        private bool adminCount;
        private SecurityIdentifier[] sidHistory;
        private RawSecurityDescriptor securityDescriptor;
        private DateTime? lastLogon;

        public DSAccount(DirectoryObject dsObject, DirectorySecretDecryptor pek)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, "dsObject");
            if (!dsObject.IsAccount)
            {
                // TODO: Exteption type
                throw new Exception("Not an account.");
            }

            // Common properties
            this.LoadAccountInfo(dsObject);

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
            get
            {
                return this.sidHistory;
            }
        }

        /// <summary>
        /// Gets the security descriptor of the object.
        /// </summary>
        public RawSecurityDescriptor SecurityDescriptor
        {
            get
            {
                return this.securityDescriptor;
            }
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
            get
            {
                return this.displayName;
            }
        }

        /// <summary>
        /// Gets the description of the <see cref="DSAccount"/>.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Gets the given name for the <see cref="DSAccount"/>.
        /// </summary>
        public string GivenName
        {
            get
            {
                return this.givenName;
            }
        }

        /// <summary>
        /// Gets the surname for the user <see cref="DSAccount"/>. 
        /// </summary>
        public string Surname
        {
            get
            {
                return this.surname;
            }
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
        /// Gets a boolean value indicating whether this <see cref="DSAccount"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted
        {
            get
            {
                return this.isDeleted;
            }
        }

        /// <summary>
        /// Gets the Nullable DateTime that specifies the date and time of the last logon for this <see cref="DSAccount"/>.
        /// </summary>
        public DateTime? LastLogon
        {
            get
            {
                return this.lastLogon;
            }
        }

        /// <summary>
        /// Gets or sets the user principal name (UPN) associated with this <see cref="DSAccount"/>.
        /// </summary>
        public string UserPrincipalName
        {
            get
            {
                return this.upn;
            }
        }

        /// <summary>
        /// Gets or sets the SAM account name for this <see cref="DSAccount"/>.
        /// </summary>
        public string SamAccountName
        {
            get
            {
                return this.samAccountName;
            }
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
            get
            {
                return this.adminCount;
            }
        }

        public string[] ServicePrincipalName
        {
            get
            {
                return this.spn;
            }
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

        protected void LoadAccountInfo(DirectoryObject dsObject)
        {

            // Guid:
            this.Guid = dsObject.Guid;

            // DN:
            this.DistinguishedName = dsObject.DistinguishedName;

            // Sid:
            this.Sid = dsObject.Sid;

            // SidHistory:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SIDHistory, out this.sidHistory);

            // DisplayName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.DisplayName, out this.displayName);

            // Description
            dsObject.ReadAttribute(CommonDirectoryAttributes.Description, out this.description);

            // GivenName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.GivenName, out this.givenName);

            // Surname:
            dsObject.ReadAttribute(CommonDirectoryAttributes.Surname, out this.surname);

            // Security Descriptor:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SecurityDescriptor, out this.securityDescriptor);

            // AdminCount (Although the schema defines it as Int32, it can only have values 0 and 1, so we directly convert it to bool)
            dsObject.ReadAttribute(CommonDirectoryAttributes.AdminCount, out this.adminCount);

            // Service Principal Name(s)
            dsObject.ReadAttribute(CommonDirectoryAttributes.ServicePrincipalName, out this.spn);

            // UAC:
            int? numericUac;
            dsObject.ReadAttribute(CommonDirectoryAttributes.UserAccountControl, out numericUac);
            this.UserAccountControl = (UserAccountControl)numericUac.Value;

            // Deleted:
            dsObject.ReadAttribute(CommonDirectoryAttributes.IsDeleted, out this.isDeleted);

            // LastLogon:
            dsObject.ReadAttribute(CommonDirectoryAttributes.LastLogon, out this.lastLogon);

            // UPN:
            dsObject.ReadAttribute(CommonDirectoryAttributes.UserPrincipalName, out this.upn);

            // SamAccountName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SAMAccountName, out this.samAccountName);

            // SamAccountType:
            int? numericAccountType;
            dsObject.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out numericAccountType);
            this.SamAccountType = (SamAccountType)numericAccountType.Value;

            // PrimaryGroupId
            int? groupId;
            dsObject.ReadAttribute(CommonDirectoryAttributes.PrimaryGroupId, out groupId);
            this.PrimaryGroupId = groupId.Value;
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
