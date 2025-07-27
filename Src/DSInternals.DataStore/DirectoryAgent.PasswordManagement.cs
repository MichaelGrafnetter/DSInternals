﻿namespace DSInternals.DataStore
{
    using System;
    using System.Security;
    using System.Security.Cryptography;
    using System.Security.Principal;
    using DSInternals.Common;
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Data;
    using DSInternals.Common.Exceptions;
    using DSInternals.Common.Properties;
    using DSInternals.Common.Schema;
    using Microsoft.Database.Isam;

    public partial class DirectoryAgent : IDisposable
    {
        #region SetAccountPassword
        public bool SetAccountPassword(DistinguishedName dn, SecureString newPassword, byte[] bootKey, bool skipMetaUpdate)
        {
            lock (this.dataTableCursor)
            {
                var obj = this.FindObject(dn);
                return this.SetAccountPassword(obj, dn, newPassword, bootKey, skipMetaUpdate);
            }
        }

        public bool SetAccountPassword(SecurityIdentifier objectSid, SecureString newPassword, byte[] bootKey, bool skipMetaUpdate)
        {
            lock (this.dataTableCursor)
            {
                var obj = this.FindObject(objectSid);
                return this.SetAccountPassword(obj, objectSid, newPassword, bootKey, skipMetaUpdate);
            }
        }

        public bool SetAccountPassword(string samAccountName, SecureString newPassword, byte[] bootKey, bool skipMetaUpdate)
        {
            lock (this.dataTableCursor)
            {
                var obj = this.FindObject(samAccountName);
                return this.SetAccountPassword(obj, samAccountName, newPassword, bootKey, skipMetaUpdate);
            }
        }

        public bool SetAccountPassword(Guid objectGuid, SecureString newPassword, byte[] bootKey, bool skipMetaUpdate)
        {
            lock (this.dataTableCursor)
            {
                var obj = this.FindObject(objectGuid);
                return this.SetAccountPassword(obj, objectGuid, newPassword, bootKey, skipMetaUpdate);
            }
        }

        protected bool SetAccountPassword(DatastoreObject targetObject, object targetObjectIdentifier, SecureString newPassword, byte[] bootKey, bool skipMetaUpdate)
        {
            // Validate input
            Validator.AssertNotNull(newPassword, "newPassword");

            // Calculate NT hash
            byte[] ntHash = NTHash.ComputeHash(newPassword);

            // We need to read sAMAccountName and userPrincipalName to be able to generate the supplementalCredentials.
            string samAccountName;
            targetObject.ReadAttribute(CommonDirectoryAttributes.SamAccountName, out samAccountName);

            string userPrincipalName;
            targetObject.ReadAttribute(CommonDirectoryAttributes.UserPrincipalName, out userPrincipalName);
            

            var supplementalCredentials = new SupplementalCredentials(
                newPassword,
                samAccountName,
                userPrincipalName,
                this.context.DomainController.NetBIOSDomainName,
                this.context.DomainController.DomainName);

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
            Validator.AssertLength(newNtHash, NTHash.HashSize, nameof(newNtHash));
            Validator.AssertNotNull(bootKey, nameof(bootKey));

            // Check that the object is an account
            targetObject.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out SamAccountType? accountType);

            switch (accountType)
            {
                case SamAccountType.User:
                case SamAccountType.Computer:
                case SamAccountType.Trust:
                    break;
                default:
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
            if (this.context.DomainController.IsADAM)
            {
                // Only AD DBs are supported for this operation
                throw new InvalidOperationException("AD LDS databases are not encrypted using external keys.");
            }

            // Validate
            Validator.AssertLength(oldBootKey, BootKeyRetriever.BootKeyLength, "oldBootKey");
            
            if(newBootKey != null)
            {
                // This value is optional. No encryption is used when blank key is provided.
                Validator.AssertLength(newBootKey, BootKeyRetriever.BootKeyLength, "newBootKey");
            }

            if(HashEqualityComparer.GetInstance().Equals(oldBootKey, newBootKey))
            {
                // Both keys are the same so no change is required.
                return;
            }

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

                try
                {
                    // This can throw a cryptographic exception if oldBootKey is invalid.
                    var pekList = new DataStoreSecretDecryptor(encryptedPEK, oldBootKey);

                    // Encrypt with the new boot key (if blank, plain encoding is done instead)
                    byte[] binaryPekList = pekList.ToByteArray(newBootKey);

                    // Save the new PEK value
                    this.dataTableCursor.BeginEditForUpdate();
                    bool hasChanged = domain.SetAttribute(CommonDirectoryAttributes.PEKList, binaryPekList);
                    this.CommitAttributeUpdate(domain, CommonDirectoryAttributes.PEKList, transaction, hasChanged, true);
                }
                catch (CryptographicException cryptoException)
                {
                    // Cancel any changes
                    transaction.Rollback();

                    try
                    {
                        // For the call to be re-entrant, check if the new boot key is the right one.
                        var pekList = new DataStoreSecretDecryptor(encryptedPEK, newBootKey);
                        // OK, nothing to change
                    }
                    catch (CryptographicException)
                    {
                        // Neither oldBootKey nor newBootKey are correct, so throw the original exception.
                        throw cryptoException;
                    }
                }
            }
        }

        /// <summary>
        /// Checks the validity of a given boot key.
        /// </summary>
        /// <param name="bootKey">The boot key to be checked.</param>
        /// <returns>Returns True if and only if the boot key can be used to decrypt the PEK list.</returns>
        public bool CheckBootKey(byte[] bootKey)
        {
            try
            {
                var decryptor = this.GetSecretDecryptor(bootKey);
                return decryptor != null;
            }
            catch
            {
                return false;
            }
        }

        public DirectorySecretDecryptor GetSecretDecryptor(byte[] bootKey = null)
        {
            if (bootKey == null && !this.context.DomainController.IsADAM)
            {
                // This is an AD DS DB, so the BootKey is stored in the registry. Stop processing if it is not provided.
                return null;

            }

            if (this.context.DomainController.State == DatabaseState.Boot)
            {
                // The initial DB definitely does not contain any secrets.
                return null;
            }

            // Do not use the shared cursor, which might already be pointing to a different location
            using (var cursor = this.context.OpenDataTable())
            {
                // We will be searching AD objects by the Distinguished Name Tag (DNT)
                cursor.CurrentIndex = DirectorySchema.DistinguishedNameTagIndex;

                // Locate the object holding the password encryption key
                DNTag pekListDNT;

                if (this.context.DomainController.IsADAM)
                {
                    // This is a AD LDS DB, so the BootKey is stored directly in the DB.
                    // Retrieve the pekList attribute of the root object:
                    bool rootObjectFound = cursor.GotoKey(Key.Compose(DNTag.RootObject));

                    if (!rootObjectFound)
                    {
                        throw new DirectoryObjectNotFoundException(DNTag.RootObject);
                    }

                    var rootObject = new DatastoreObject(cursor, this.context);
                    rootObject.ReadAttribute(CommonDirectoryAttributes.PEKList, out byte[] rootPekList);

                    // Retrieve the pekList attribute of the schema object:
                    bool schemaNamingContextFound = cursor.GotoKey(Key.Compose(this.context.DomainController.SchemaNamingContextDNT));

                    if (!schemaNamingContextFound)
                    {
                        throw new DirectoryObjectNotFoundException(this.context.DomainController.SchemaNamingContextDNT);
                    }

                    var schemaObject = new DatastoreObject(cursor, this.context);
                    schemaObject.ReadAttribute(CommonDirectoryAttributes.PEKList, out byte[] schemaPekList);

                    // Combine these things together into the BootKey/SysKey
                    bootKey = BootKeyRetriever.GetBootKey(rootPekList, schemaPekList);

                    // The actual PEK list is located on the Configuration NC object.
                    pekListDNT = this.context.DomainController.ConfigurationNamingContextDNT;
                }
                else
                {
                    // This is an AD DS DB, so the PEK list is located on the Domain NC object.
                    pekListDNT = this.context.DomainController.DomainNamingContextDNT.Value;
                }

                // Load the PEK List attribute from the holding object and decrypt it using Boot Key.
                bool pekListHolderFound = cursor.GotoKey(Key.Compose(pekListDNT));

                if (!pekListHolderFound)
                {
                    throw new DirectoryObjectNotFoundException(pekListDNT);
                }

                var pekListHolder = new DatastoreObject(cursor, this.context);
                pekListHolder.ReadAttribute(CommonDirectoryAttributes.PEKList, out byte[] encryptedPEK);
                return new DataStoreSecretDecryptor(encryptedPEK, bootKey);
            }
        }
        #endregion BootKey
    }
}
