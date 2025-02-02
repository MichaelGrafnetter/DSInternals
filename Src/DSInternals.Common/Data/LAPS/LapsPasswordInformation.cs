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
        /// Constructor for a Windows LAPS password.
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

        public string ComputerName { get; private set; }
        public string Account { get; private set; }
        public string Password { get; private set; }
        public DateTime? PasswordUpdateTime { get; private set; }
        public DateTime? ExpirationTimestamp { get; private set; }
        public LapsPasswordSource Source { get; private set; }
        public LapsDecryptionStatus DecryptionStatus { get; private set; }
    }
}
