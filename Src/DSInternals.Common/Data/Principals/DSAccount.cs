namespace DSInternals.Common.Data
{
    using DSInternals.Common.Cryptography;
using System;
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
        private bool isDeleted;
        private bool adminCount;
        private SecurityIdentifier[] sidHistory;
        private RawSecurityDescriptor securityDescriptor;
        private DateTime? lastLogon;

        public DSAccount(DirectoryObject dsObject, DirectorySecretDecryptor pek)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, "dsObject");
            if(!dsObject.IsAccount)
            {
                // TODO: Exteption type
                throw new Exception("Not an account.");
            }

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

            // Enabled:
            // TODO: Move to DirectoryObject?
            int? numericUac;
            dsObject.ReadAttribute(CommonDirectoryAttributes.UserAccountControl, out numericUac);
            UserAccountControl uac = (UserAccountControl)numericUac.Value;
            this.Enabled = !uac.HasFlag(UserAccountControl.Disabled);

            // Deleted:
            dsObject.ReadAttribute(CommonDirectoryAttributes.IsDeleted, out this.isDeleted);
            
            // LastLogon:
            dsObject.ReadAttribute(CommonDirectoryAttributes.LastLogon, out this.lastLogon);

            // UPN:
            dsObject.ReadAttribute(CommonDirectoryAttributes.UserPrincipalName, out this.upn);

            // SamAccountName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SAMAccountName, out this.samAccountName);

            // SamAccountType:
            // TODO: Move to DirectoryObject?
            int? numericAccountType;
            dsObject.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out numericAccountType);
            this.SamAccountType = (SamAccountType)numericAccountType.Value;

            // PrimaryGroupId
            int? groupId;
            dsObject.ReadAttribute(CommonDirectoryAttributes.PrimaryGroupId, out groupId);
            this.PrimaryGroupId = groupId.Value;

            if(pek == null)
            {
                // Do not continue if we do not have a decryption key
                return;
            }
            // NTHash:
            byte[] encryptedNtHash;
            dsObject.ReadAttribute(CommonDirectoryAttributes.NTHash, out encryptedNtHash);
            if(encryptedNtHash != null)
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
    }
}
