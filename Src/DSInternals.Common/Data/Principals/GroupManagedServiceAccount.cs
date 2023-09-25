using DSInternals.Common.Cryptography;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Group Managed Service Account.
    /// </summary>
    /// <see>https://learn.microsoft.com/en-us/windows/win32/adschema/c-msds-groupmanagedserviceaccount</see>
    public class GroupManagedServiceAccount : DSAccount
    {
        private const int DefaultPasswordValidityInterval = 30;

        public ProtectionKeyIdentifier ManagedPasswordId
        {
            get;
            private set;
        }

        public ProtectionKeyIdentifier ManagedPasswordPreviousId
        {
            get;
            private set;
        }

        public int ManagedPasswordInterval
        {
            get;
            private set;
        }

        public GroupManagedServiceAccount(DirectoryObject dsObject, string netBIOSDomainName, DirectorySecretDecryptor pek) : base(dsObject, netBIOSDomainName, pek)
        {
            // TODO: Check that this object is a gMSA

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
            this.ManagedPasswordInterval = managedPasswordInterval ?? DefaultPasswordValidityInterval;
        }
    }
}
