using System;

namespace DSInternals.Common.Data
{
    public class LapsPasswordInformation
    {
        /// <summary>
        /// Constructor for a legacy LAPS password.
        /// </summary>
        public LapsPasswordInformation(string computerName, string password, DateTime? expiration) {
            this.Source = LapsPasswordSource.LegacyLapsCleartextPassword;
            this.DecryptionStatus = LapsDecryptionStatus.NotApplicable;
            this.ComputerName = computerName;
            this.ExpirationTimestamp = expiration;
            this.Password = password;
        }

        /// <summary>
        /// Constructor for a cleartext Windows LAPS password.
        /// </summary>
        public LapsPasswordInformation(string computerName, LapsClearTextPassword password, DateTime? expiration)
        {
            Validator.AssertNotNull(password, nameof(password));

            this.ComputerName = computerName;
            this.Account = password.AccountName;
            this.Password = password.Password;
            this.PasswordUpdateTime = password.UpdateTimestamp;
            this.ExpirationTimestamp = expiration;
            this.Source = LapsPasswordSource.CleartextPassword;
            this.DecryptionStatus = LapsDecryptionStatus.NotApplicable;
        }

        public LapsPasswordInformation(string computerName, LapsEncryptedPassword encryptedPassword, LapsPasswordSource source, DateTime? expiration, IKdsRootKeyResolver? rootKeyResolver = null)
        {
            Validator.AssertNotNull(encryptedPassword, nameof(encryptedPassword));

            // Validate the source type
            this.Source = source switch
            {
                LapsPasswordSource.EncryptedPassword or Data.LapsPasswordSource.EncryptedPasswordHistory or LapsPasswordSource.EncryptedDSRMPassword or LapsPasswordSource.EncryptedDSRMPasswordHistory => source,
                _ => throw new ArgumentOutOfRangeException(nameof(source))
            };

            this.ComputerName = computerName;
            this.PasswordUpdateTime = encryptedPassword.UpdateTimeStamp;
            this.ExpirationTimestamp = expiration;

            // Try to locate the root key and cache the derived group key.
            bool rootKeyFound = false;

            if (rootKeyResolver != null)
            {
                Guid rootKeyId = encryptedPassword.EncryptedBlob.ProtectionKeyIdentifier.RootKeyId;
                KdsRootKey? rootKey = rootKeyResolver.GetKdsRootKey(rootKeyId);

                if (rootKey != null)
                {
                    rootKeyFound = true;
                    var gke = GroupKeyEnvelope.Create(rootKey, encryptedPassword.EncryptedBlob.ProtectionKeyIdentifier, encryptedPassword.EncryptedBlob.TargetSid);
                    gke.WriteToCache();
                }
            }

            // Decrypt the data using the native Win32 API, which uses the pre-cached group keys if available.
            bool isSuccess = encryptedPassword.TryDecrypt(out LapsClearTextPassword decryptedPassword);

            if (isSuccess)
            {
                this.DecryptionStatus = LapsDecryptionStatus.Success;
                this.Account = decryptedPassword.AccountName;
                this.Password = decryptedPassword.Password;
            }
            else
            {
                // Check if offline or online decryption attempt failed.
                this.DecryptionStatus = rootKeyFound ? LapsDecryptionStatus.Error : LapsDecryptionStatus.Unauthorized;
            }
        }

        public string ComputerName { get; private set; }
        public string Account { get; private set; }
        public string Password { get; private set; }
        public DateTime? PasswordUpdateTime { get; private set; }
        public DateTime? ExpirationTimestamp { get; private set; }
        public LapsPasswordSource Source { get; private set; }
        public LapsDecryptionStatus DecryptionStatus { get; private set; }
    }
}
