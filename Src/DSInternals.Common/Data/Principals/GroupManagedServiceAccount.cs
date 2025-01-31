using System;
using System.Security;
using System.Security.Principal;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Group Managed Service Account.
    /// </summary>
    /// <see>https://learn.microsoft.com/en-us/windows/win32/adschema/c-msds-groupmanagedserviceaccount</see>
    public class GroupManagedServiceAccount
    {
        /// <summary>
        /// Key identifier for the current managed password data.
        /// </summary>
        public ProtectionKeyIdentifier ManagedPasswordId
        {
            get;
            private set;
        }

        /// <summary>
        /// Key identifier for the previous managed password data.
        /// </summary>
        public ProtectionKeyIdentifier ManagedPasswordPreviousId
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
        public string SamAccountName
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
        /// Gets the current password.
        /// </summary>
        public SecureString SecureManagedPassword
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current password.
        /// </summary>
        public string ManagedPassword
        {
            get
            {
                return this.SecureManagedPassword.ToUnicodeString();
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

        public KerberosKeyDataNew[] KerberosKeys
        {
            get;
            private set;
        }

        public GroupManagedServiceAccount(DirectoryObject dsObject)
        {
            // TODO: Check that this object is a gMSA

            /* Load gMSA-specific attributes first */

            // Read and parse msDS-ManagedPasswordId
            dsObject.ReadAttribute(CommonDirectoryAttributes.ManagedPasswordId, out byte[] rawManagedPasswordId);
            if(rawManagedPasswordId != null )
            {
                this.ManagedPasswordId = new ProtectionKeyIdentifier(rawManagedPasswordId);
            }

            // Read and parse msDS-ManagedPasswordPreviousId
            dsObject.ReadAttribute(CommonDirectoryAttributes.ManagedPasswordPreviousId, out byte[] rawManagedPasswordPreviousId);
            if(rawManagedPasswordPreviousId != null)
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

            // UAC:
            dsObject.ReadAttribute(CommonDirectoryAttributes.UserAccountControl, out int? numericUac);
            this.UserAccountControl = (UserAccountControl)numericUac.Value;

            // Deleted:
            dsObject.ReadAttribute(CommonDirectoryAttributes.IsDeleted, out bool isDeleted);
            this.Deleted = isDeleted;

            // SamAccountName:
            dsObject.ReadAttribute(CommonDirectoryAttributes.SAMAccountName, out string samAccountName);
            this.SamAccountName = samAccountName;

            // SuportedEncryptionTypes
            dsObject.ReadAttribute(CommonDirectoryAttributes.SupportedEncryptionTypes, out int? numericSupportedEncryptionTypes);
            // Note: The value is store as int in the DB, but the documentation says that it is an unsigned int
            this.SupportedEncryptionTypes = (SupportedEncryptionTypes?)numericSupportedEncryptionTypes;

            // pwdLastSet
            dsObject.ReadAttribute(CommonDirectoryAttributes.PasswordLastSet, out DateTime? passwordLastSet);
            this.PasswordLastSet = passwordLastSet;

            // whenCreated (should never be null)
            dsObject.ReadAttribute(CommonDirectoryAttributes.WhenCreated, out long? whenCreated);
            this.WhenCreated = whenCreated.Value.FromGeneralizedTime();
        }

        public void CalculatePassword(KdsRootKey kdsRootKey, DateTime effectiveTime)
        {
            Validator.AssertNotNull(kdsRootKey, nameof(kdsRootKey));

            // Calculate the managed password
            DateTime previousPasswordChange = this.PasswordLastSet ?? this.WhenCreated;
            byte[] managedPassword = kdsRootKey.GetManagedPassword(this.Sid, previousPasswordChange, effectiveTime, this.ManagedPasswordInterval);
            this.SecureManagedPassword = managedPassword.ReadSecureWString(0);
            managedPassword.ZeroFill(); // Remove the cleartext password from memory

            // Derive password hashes, as the password itself is not that important
            this.NTHash = Cryptography.NTHash.ComputeHash(this.SecureManagedPassword);

            if (this.ManagedPasswordId != null)
            {
                // We only need the DNS domain name from the managed password id. This attribute should always be populated.
                // TODO: We are not generating DES keys, as this feature does not yield expected results yet.
                this.KerberosKeys = new KerberosCredentialNew(this.SecureManagedPassword, this.SamAccountName, this.ManagedPasswordId.DomainName, false).Credentials;
            }
        }
    }
}
