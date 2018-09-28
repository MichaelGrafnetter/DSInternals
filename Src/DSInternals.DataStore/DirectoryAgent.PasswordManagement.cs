namespace DSInternals.DataStore
{
    using DSInternals.Common;
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Data;
    using DSInternals.Common.Exceptions;
    using DSInternals.Common.Properties;
    using System;
    using System.Security;
    using System.Security.Principal;

    public partial class DirectoryAgent : IDisposable
    {
        #region SetAccountPassword
        public bool SetAccountPassword(DistinguishedName dn, SecureString newPassword, byte[] bootKey, bool skipMetaUpdate)
        {
            var obj = this.FindObject(dn);
            return this.SetAccountPassword(obj, dn, newPassword, bootKey, skipMetaUpdate);
        }

        public bool SetAccountPassword(SecurityIdentifier objectSid, SecureString newPassword, byte[] bootKey, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectSid);
            return this.SetAccountPassword(obj, objectSid, newPassword, bootKey, skipMetaUpdate);
        }

        public bool SetAccountPassword(string samAccountName, SecureString newPassword, byte[] bootKey, bool skipMetaUpdate)
        {
            var obj = this.FindObject(samAccountName);
            return this.SetAccountPassword(obj, samAccountName, newPassword, bootKey, skipMetaUpdate);
        }

        public bool SetAccountPassword(Guid objectGuid, SecureString newPassword, byte[] bootKey, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectGuid);
            return this.SetAccountPassword(obj, objectGuid, newPassword, bootKey, skipMetaUpdate);
        }

        protected bool SetAccountPassword(DatastoreObject targetObject, object targetObjectIdentifier, SecureString newPassword, byte[] bootKey, bool skipMetaUpdate)
        {
            // Validate input
            Validator.AssertNotNull(newPassword, "newPassword");

            // Calculate NT hash
            byte[] ntHash = NTHash.ComputeHash(newPassword);

            // We need to read sAMAccountName and userPrincipalName to be able to generate the supplementalCredentials.
            string samAccountName;
            targetObject.ReadAttribute(CommonDirectoryAttributes.SAMAccountName, out samAccountName);

            string userPrincipalName;
            targetObject.ReadAttribute(CommonDirectoryAttributes.UserPrincipalName, out userPrincipalName);
            

            var supplementalCredentials = new SupplementalCredentials(
                newPassword,
                samAccountName,
                userPrincipalName,
                this.context.DomainController.NetBIOSDomainName,
                this.context.DomainController.Domain);

            return this.SetAccountPasswordHash(
                targetObject,
                targetObjectIdentifier,
                ntHash,
                supplementalCredentials,
                bootKey,
                skipMetaUpdate);
        }
        #endregion SetAccountPassword

        #region SetAccountPasswordHash
        public bool SetAccountPasswordHash(DistinguishedName dn, byte[] newNtHash, SupplementalCredentials newSupplementalCredentials, byte[] bootKey, bool skipMetaUpdate)
        {
            var obj = this.FindObject(dn);
            return this.SetAccountPasswordHash(obj, dn, newNtHash, newSupplementalCredentials, bootKey, skipMetaUpdate);
        }

        public bool SetAccountPasswordHash(SecurityIdentifier objectSid, byte[] newNtHash, SupplementalCredentials newSupplementalCredentials, byte[] bootKey, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectSid);
            return this.SetAccountPasswordHash(obj, objectSid, newNtHash, newSupplementalCredentials, bootKey, skipMetaUpdate);
        }

        public bool SetAccountPasswordHash(string samAccountName, byte[] newNtHash, SupplementalCredentials newSupplementalCredentials, byte[] bootKey, bool skipMetaUpdate)
        {
            var obj = this.FindObject(samAccountName);
            return this.SetAccountPasswordHash(obj, samAccountName, newNtHash, newSupplementalCredentials, bootKey, skipMetaUpdate);
        }

        public bool SetAccountPasswordHash(Guid objectGuid, byte[] newNtHash, SupplementalCredentials newSupplementalCredentials, byte[] bootKey, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectGuid);
            return this.SetAccountPasswordHash(obj, objectGuid, newNtHash, newSupplementalCredentials, bootKey, skipMetaUpdate);
        }

        protected bool SetAccountPasswordHash(DatastoreObject targetObject, object targetObjectIdentifier, byte[] newNtHash, SupplementalCredentials newSupplementalCredentials, byte[] bootKey, bool skipMetaUpdate)
        {
            // Validate input
            Validator.AssertLength(newNtHash, NTHash.HashSize, "newNtHash");
            Validator.AssertNotNull(bootKey, "bootKey");

            if (!targetObject.IsAccount)
            {
                throw new DirectoryObjectOperationException(Resources.ObjectNotSecurityPrincipalMessage, targetObjectIdentifier);
            }

            if (newSupplementalCredentials == null)
            {
                // Create empty supplemental credentials structure, beca
                newSupplementalCredentials = new SupplementalCredentials();
            }

            // Load the password encryption key
            var pek = this.GetSecretDecryptor(bootKey);

            // Calculate LM hash
            // Note that AD uses a random value in LM hash history since 2003.
            byte[] lmHash = new byte[LMHash.HashSize];
            new Random().NextBytes(lmHash);

            // Write the data
            using (var transaction = this.context.BeginTransaction())
            {
                // Load account RID as it is used in the key derivation process
                SecurityIdentifier sid;
                targetObject.ReadAttribute(CommonDirectoryAttributes.ObjectSid, out sid);
                int rid = sid.GetRid();

                // Start a database transaction
                this.dataTableCursor.BeginEditForUpdate();

                // Encrypt and set NT hash
                byte[] encryptedNtHash = pek.EncryptHash(newNtHash, rid);
                targetObject.SetAttribute(CommonDirectoryAttributes.NTHash, encryptedNtHash);

                // Clear the LM hash (Default behavior since 2003)
                targetObject.SetAttribute(CommonDirectoryAttributes.LMHash, null);

                // Encrypt and set NT hash history
                byte[] encryptedNtHashHistory = pek.EncryptHashHistory(new byte[][] { newNtHash }, rid);
                targetObject.SetAttribute(CommonDirectoryAttributes.NTHashHistory, encryptedNtHashHistory);

                // Encrypt and set LM hash history.
                byte[] encryptedLmHashHistory = pek.EncryptHashHistory(new byte[][] { lmHash }, rid);
                targetObject.SetAttribute(CommonDirectoryAttributes.LMHashHistory, encryptedLmHashHistory);

                // Encrypt and set Supplemental Credentials
                byte[] encryptedSupplementalCredentials = pek.EncryptSecret(newSupplementalCredentials.ToByteArray());
                targetObject.SetAttribute(CommonDirectoryAttributes.SupplementalCredentials, encryptedSupplementalCredentials);

                // Set the pwdLastSet attribute
                if (!skipMetaUpdate)
                {
                    targetObject.SetAttribute(CommonDirectoryAttributes.PasswordLastSet, DateTime.Now);
                }

                // As supplementalCredentials contains salted values, we will always presume that the values of password attributes have changed.
                bool passwordHasChanged = true;
                string[] passwordAttributes = {
                    CommonDirectoryAttributes.NTHash,
                    CommonDirectoryAttributes.NTHashHistory,
                    CommonDirectoryAttributes.LMHash,
                    CommonDirectoryAttributes.LMHashHistory,
                    CommonDirectoryAttributes.SupplementalCredentials,
                    CommonDirectoryAttributes.PasswordLastSet
                };
                this.CommitAttributeUpdate(targetObject, passwordAttributes, transaction, passwordHasChanged, skipMetaUpdate);
                return passwordHasChanged;
            }
        }
        #endregion SetAccountPasswordHash

        #region BootKey
        /// <summary>
        /// Re-encrypts the PEK list using a new boot key.
        /// </summary>
        /// <param name="oldBootKey">Current boot key, using which the PEK list is encrypted.</param>
        /// <param name="newBootKey">New boot key using which to encrypt the PEK list.</param>
        public void ChangeBootKey(byte[] oldBootKey, byte[] newBootKey)
        {
            // Validate
            Validator.AssertLength(oldBootKey, BootKeyRetriever.BootKeyLength, "oldBootKey");
            if (!this.context.DomainController.DomainNamingContextDNT.HasValue)
            {
                // The domain object must exist
                throw new DirectoryObjectNotFoundException("domain");
            }

            // Execute
            using (var transaction = this.context.BeginTransaction())
            {
                // Retrieve and decrypt
                var domain = this.FindObject(this.context.DomainController.DomainNamingContextDNT.Value);
                byte[] encryptedPEK;
                domain.ReadAttribute(CommonDirectoryAttributes.PEKList, out encryptedPEK);
                var pekList = new DataStoreSecretDecryptor(encryptedPEK, oldBootKey);

                // Encrypt with the new boot key (if blank, plain encoding is done instead)
                byte[] binaryPekList = pekList.ToByteArray(newBootKey);

                // Save the new value
                this.dataTableCursor.BeginEditForUpdate();
                bool hasChanged = domain.SetAttribute(CommonDirectoryAttributes.PEKList, binaryPekList);
                this.CommitAttributeUpdate(domain, CommonDirectoryAttributes.PEKList, transaction, hasChanged, true);
            }
        }
        #endregion BootKey
    }
}