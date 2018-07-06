namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Linq;
    using System.Management.Automation;
    using DSInternals.Common.Data;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using DSInternals.Common.Cryptography;

    [Cmdlet(VerbsDiagnostic.Test, "PasswordQuality")]
    [OutputType(new Type[] { typeof(PasswordQualityTestResult) })]
    public class TestPasswordQualityCommand : PSCmdlet
    {
        #region Constants
        /// <summary>
        /// Expected number of users being processed
        /// </summary>
        private const int PasswordDictionaryInitialCapacity = 10000;

        #endregion Constants

        #region Parameters

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true
        )]
        [Alias("ADAccount","DSAccount")]
        public DSAccount Account
        {
            get;
            set;
        }

        [Parameter]
        public SwitchParameter SkipDuplicatePasswordTest
        {
            get;
            set;
        }

        [Parameter]
        public SwitchParameter IncludeDisabledAccounts
        {
            get;
            set;
        }

        [Parameter]
        [ValidateNotNull]
        public IDictionary<byte[], string> WeakPasswordHashes
        {
            get;
            set;
        }

        [Parameter]
        public SwitchParameter ShowPlainTextPasswords
        {
            get;
            set;
        }

        #endregion Parameters

        #region Fields
        
        // Maps password hash to list of user names.
        private IDictionary<byte[], StringCollection> duplicatePasswordDictionary;
        private PasswordQualityTestResult result;

        #endregion Fields

        #region Cmdlet Overrides

        protected override void BeginProcessing()
        {
            // Perform some initialization.
            if(! this.SkipDuplicatePasswordTest.IsPresent)
            {
                this.duplicatePasswordDictionary = new Dictionary<byte[], StringCollection>(PasswordDictionaryInitialCapacity, HashEqualityComparer.GetInstance());
            }

            this.result = new PasswordQualityTestResult();
        }

        protected override void ProcessRecord()
        {
            if(this.Account.Enabled == false && !this.IncludeDisabledAccounts.IsPresent)
            {
                // The account is disabled and should be skipped.
                // TODO: Move to resources.
                string message = String.Format("Skipping account {0}, because it is disabled.", this.Account.SamAccountName);
                this.WriteVerbose(message);
                return;
            }
            
            // Verbose message
            // TODO: Move to resources.
            string message2 = String.Format("Processing account {0}...", this.Account.SamAccountName);
            this.WriteVerbose(message2);

            if (this.Account.UserAccountControl.HasFlag(UserAccountControl.PasswordNeverExpires))
            {
                // The account has a non-expiring password.
                this.result.PasswordNeverExpires.Add(this.Account.SamAccountName);
            }

            if(this.Account.UserAccountControl.HasFlag(UserAccountControl.UseDesKeyOnly))
            {
                // Only DES kerbero encryption type is used with this account.
                this.result.DESEncryptionOnly.Add(this.Account.SamAccountName);
            }

            if(this.Account.AdminCount && ! this.Account.UserAccountControl.HasFlag(UserAccountControl.NotDelegated))
            {
                // This administrative account can be delegated.
                this.result.DelegatableAdmins.Add(this.Account.SamAccountName);
            }

            if (this.Account.UserAccountControl.HasFlag(UserAccountControl.PasswordNotRequired))
            {
                // The account's password is not required.
                this.result.PasswordNotRequired.Add(this.Account.SamAccountName);
            }

            if (this.Account.UserAccountControl.HasFlag(UserAccountControl.PreAuthNotRequired))
            {
                // Pre-authentication is not required for this account account.
                this.result.PreAuthNotRequired.Add(this.Account.SamAccountName);
            }

            if(this.Account.SupplementalCredentials != null)
            {
                if(this.Account.SupplementalCredentials.ClearText != null)
                {
                    // Account has ClearText password (stored using reversible encryption)
                    // Only reveal the password if explicitly asked to do so.
                    string outputPassword = this.ShowPlainTextPasswords.IsPresent ? this.Account.SupplementalCredentials.ClearText : String.Empty;
                    this.result.ClearTextPassword.Add(this.Account.SamAccountName, outputPassword);
                }

                if(this.Account.SupplementalCredentials.KerberosNew == null && ! this.Account.UserAccountControl.HasFlag(UserAccountControl.SmartCardRequired))
                {
                    // Account is missing the AES kerberos keys. This is only OK if smart card auth is enforced for this account.
                    this.result.AESKeysMissing.Add(this.Account.SamAccountName);
                }
            }

            if(this.Account.LMHash != null)
            {
                // Account has the LM hash present.
                this.result.LMHash.Add(this.Account.SamAccountName);
            }

            if (this.Account.NTHash == null)
            {
                // The account has no password.
                this.result.EmptyPassword.Add(this.Account.SamAccountName);
                
                // All the remaining tests are based on NT hash, so we can skip them.
                return;
            }

            if (HashEqualityComparer.GetInstance().Equals(this.Account.NTHash, NTHash.Empty))
            {
                // The account has an empty password.
                this.result.EmptyPassword.Add(this.Account.SamAccountName);

                // Skip the remaining tests, because they only make sense for non-empty passwords.
                return;
            }

            if (this.Account.SamAccountType == SamAccountType.Computer)
            {
                // Check if the computer has a default password.
                this.TestComputerDefaultPassword();
            }

            if (this.WeakPasswordHashes != null)
            {
                // Check if the account has a weak password.
                this.TestWeakHashes();
            }

            if (!this.SkipDuplicatePasswordTest.IsPresent)
            {
                // Find password duplicates
                this.TestDuplicateHash();
            }
        }

        protected override void EndProcessing()
        {
            // Process duplicate passwords
            if(!this.SkipDuplicatePasswordTest.IsPresent)
            {
                this.result.DuplicatePasswordGroups = this.duplicatePasswordDictionary.Values.Where(list => list.Count > 1).ToList();
            }

            // The processing has finished, so return the results.
            this.WriteObject(this.result);
        }

        #endregion Cmdlet Overrides

        #region Helper Methods
        private void TestWeakHashes()
        {
            // Check the current hash
            string foundPassword;
            bool isInDictionary = this.WeakPasswordHashes.TryGetValue(this.Account.NTHash, out foundPassword);
            if(isInDictionary)
            {
                // The current password hash is on the list
                // Only reveal the password if explicitly asked to do so.
                string outputPassword = this.ShowPlainTextPasswords.IsPresent ? foundPassword : String.Empty;
                this.result.WeakPassword.Add(this.Account.SamAccountName, outputPassword);
            }

            if (this.Account.NTHashHistory == null || this.Account.NTHashHistory.Length <= 1)
            {
                // The account does not contain any historical password hashes, so we skip the remaining tests.
                return;
            }
                // The first hash in history is the current one, so we skip it.
            var historicalHashes = this.Account.NTHashHistory.Skip(1);
            foreach(byte[] hash in historicalHashes)
            {
                isInDictionary = this.WeakPasswordHashes.TryGetValue(hash, out foundPassword);
                if(isInDictionary)
                {
                    // A historical password is on the list
                    // Only reveal the password if explicitly asked to do so.
                    string outputPassword = this.ShowPlainTextPasswords.IsPresent ? foundPassword : String.Empty;
                    this.result.WeakHistoricalPassword.Add(this.Account.SamAccountName, outputPassword);

                    // We already found one matching hash, so we skip the rest of the hashes.
                    break;
                }
            }
        }

        private void TestDuplicateHash()
        {
            byte[] currentHash = this.Account.NTHash;
            StringCollection accountList;
            bool hashFoundInDictionary = this.duplicatePasswordDictionary.TryGetValue(currentHash, out accountList);
            if(!hashFoundInDictionary)
            {
                // Create a new account list for the hash, as it does not exist yet.
                accountList = new StringCollection();
                this.duplicatePasswordDictionary.Add(currentHash, accountList);
            }
            accountList.Add(this.Account.SamAccountName);
        }

        private void TestComputerDefaultPassword()
        {
            string defaultPassword = this.Account.SamAccountName.TrimEnd('$').ToLower();
            byte[] defaultHash = NTHash.ComputeHash(defaultPassword);
            if (HashEqualityComparer.GetInstance().Equals(this.Account.NTHash, defaultHash))
            {
                // The computer has the default password.
                this.result.DefaultComputerPassword.Add(this.Account.SamAccountName);
            }
        }

        #endregion Helper Methods
    }
}