namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common;
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Data;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Management.Automation;
    using System.Text;

    [Cmdlet(VerbsDiagnostic.Test, "PasswordQuality")]
    [OutputType(new Type[] { typeof(PasswordQualityTestResult) })]
    public class TestPasswordQualityCommand : PSCmdletEx
    {
        #region Constants
        /// <summary>
        /// Expected number of users being processed
        /// </summary>
        private const int PasswordDictionaryInitialCapacity = 10000;

        /// <summary>
        /// Separator of hashes in the file from HaveIBeenPwned.
        /// </summary>
        private const char HashSeparator = ':';
        #endregion Constants

        #region Parameters
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true
        )]
        [Alias("ADAccount", "DSAccount")]
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
        public string[] WeakPasswords
        {
            get;
            set;
        }

        [Parameter]
        [ValidateNotNullOrEmpty]
        public string WeakPasswordsFile
        {
            get;
            set;
        }

        [Parameter]
        [ValidateNotNullOrEmpty]
        public string WeakPasswordHashesFile
        {
            get;
            set;
        }
        #endregion Parameters

        #region Fields

        /// <summary>
        /// Maps password hashes to lists of account names.
        /// </summary>
        private IDictionary<byte[], SortedSet<string>> hashToAccountMap;

        private PasswordQualityTestResult result;

        #endregion Fields

        #region Cmdlet Overrides

        protected override void BeginProcessing()
        {
            // Test the optional file path in advance to throw an early error.
            this.ResolveFilePath(this.WeakPasswordHashesFile);
            this.ResolveFilePath(this.WeakPasswordsFile);

            if (this.ShouldTestWeakPasswords || !this.SkipDuplicatePasswordTest.IsPresent)
            {
                // We need to cache NT hashes of all accounts for the Duplicate and Weak Password Tests.
                this.hashToAccountMap = new Dictionary<byte[], SortedSet<string>>(PasswordDictionaryInitialCapacity, HashEqualityComparer.GetInstance());
            }

            // Initialize the test results.
            this.result = new PasswordQualityTestResult();
        }

        protected override void ProcessRecord()
        {
            if (this.Account.Enabled == false && !this.IncludeDisabledAccounts.IsPresent)
            {
                // The account is disabled and should be skipped.
                string message = String.Format("Skipping account {0}, because it is disabled.", this.Account.SamAccountName);
                this.WriteVerbose(message);
                return;
            }

            // Verbose message
            string message2 = String.Format("Processing account {0}...", this.Account.SamAccountName);
            this.WriteVerbose(message2);

            if (this.Account.UserAccountControl.HasFlag(UserAccountControl.PasswordNeverExpires))
            {
                // The account has a non-expiring password.
                this.result.PasswordNeverExpires.Add(this.Account.SamAccountName);
            }

            if (this.Account.UserAccountControl.HasFlag(UserAccountControl.UseDesKeyOnly))
            {
                // Only DES kerberos encryption type is used with this account.
                this.result.DESEncryptionOnly.Add(this.Account.SamAccountName);
            }

            if (this.Account.AdminCount && !this.Account.UserAccountControl.HasFlag(UserAccountControl.NotDelegated))
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

            if (this.Account.SupplementalCredentials != null)
            {
                if (this.Account.SupplementalCredentials.ClearText != null)
                {
                    // Account has ClearText password (stored using reversible encryption)
                    this.result.ClearTextPassword.Add(this.Account.SamAccountName);
                }

                if (this.Account.SupplementalCredentials.KerberosNew == null && !this.Account.UserAccountControl.HasFlag(UserAccountControl.SmartCardRequired))
                {
                    // Account is missing the AES kerberos keys. This is only OK if smart card auth is enforced for this account.
                    this.result.AESKeysMissing.Add(this.Account.SamAccountName);
                }
            }

            if (this.Account.LMHash != null)
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

            if (this.hashToAccountMap != null)
            {
                // Add the current account's NT hash to the map for further processing.
                this.AddAccountToHashMap();
            }
        }

        protected override void EndProcessing()
        {
            // Process duplicate passwords
            if (!this.SkipDuplicatePasswordTest.IsPresent)
            {
                this.result.DuplicatePasswordGroups = this.hashToAccountMap.Values.Where(set => set.Count > 1).ToList();
            }

            // Process Weak Passwords
            this.TestWeakPasswords();

            // The processing has finished, so return the results.
            this.WriteObject(this.result);
        }

        #endregion Cmdlet Overrides

        #region Helper Methods
        private bool ShouldTestWeakPasswords
        {
            get
            {
                // Weak passwords must be provided in at least one way.
                return this.WeakPasswords != null || this.WeakPasswordsFile != null || WeakPasswordHashesFile != null;
            }
        }

        private void TestWeakPasswords()
        {
            // Process the list of weak passwords, if present
            if (this.WeakPasswords != null)
            {
                foreach (string weakPassword in this.WeakPasswords)
                {
                    this.TestWeakPassword(weakPassword);
                }
            }

            // Process the file containing weak passwords, if present
            this.TestWeakPasswordsFromFile();

            // Process the file containing weak password hashes, if present
            this.TestWeakPasswordHashesFromFile();
        }

        private void TestWeakPasswordsFromFile()
        {
            if (this.WeakPasswordsFile == null)
            {
                // No file containing passwords has been provided, so we skip this test.
                return;
            }

            string resolvedPath = this.ResolveFilePath(this.WeakPasswordsFile);

            // Start reporting the progress
            var progress = new ProgressRecord(2, "Weak Password Test", "Checking accounts against weak passwords.");

            // We expect the file to contain one UTF8 password per line
            using (var reader = new StreamReader(resolvedPath, Encoding.UTF8))
            {
                string weakPassword;
                while ((weakPassword = reader.ReadLine()) != null)
                {
                    this.TestWeakPassword(weakPassword);
                    this.ReportProgress(reader.BaseStream, progress);
                }
            }
        }

        private void TestWeakPasswordHashesFromFile()
        {
            if (this.WeakPasswordHashesFile == null)
            {
                // No file containing hashes has been provided, so we skip this test.
                return;
            }

            string resolvedPath = this.ResolveFilePath(this.WeakPasswordHashesFile);

            // Start reporting the progress
            var progress = new ProgressRecord(3, "Weak Password Test", "Checking accounts against weak password hashes.");

            // We expect the file to contain one HEX NT hash per line.
            // Note that the hash list from haveibeenpwned.com also contains cardinalities, e.g. "32ED87BDB5FDC5E9CBA88547376818D4:22390492", so we need to cut them off.
            using (var reader = new StreamReader(resolvedPath, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string weakHash = line.Split(HashSeparator)[0];
                    // TODO: Handle malformed lines
                    this.TestWeakPasswordHash(weakHash.HexToBinary());
                    this.ReportProgress(reader.BaseStream, progress);
                }
            }
        }

        private void ReportProgress(Stream stream, ProgressRecord progress)
        {
            if (stream.Position >= stream.Length)
            {
                // Report operation completion
                progress.RecordType = ProgressRecordType.Completed;
            }

            // Calculate the current progress
            int percentComplete = (int)(stream.Position * 100.0 / stream.Length);

            if (percentComplete != progress.PercentComplete)
            {
                // The percentage has changed so we need to update the progress bar.
                progress.PercentComplete = percentComplete;
                this.WriteProgress(progress);
            }
            else if (this.Stopping)
            {
                // React to a possible CTRL+C even before WriteProgress is called, which might take many iterations.
                throw new PipelineStoppedException();
            }
        }

        private void TestWeakPassword(string weakPassword)
        {
            // Windows has a hard limit on password length, so we ignore long ones
            if (weakPassword.Length <= NTHash.MaxInputLength)
            {
                byte[] weakHash = NTHash.ComputeHash(weakPassword);
                this.TestWeakPasswordHash(weakHash);
            }
        }

        private void TestWeakPasswordHash(byte[] weakHash)
        {
            SortedSet<string> matchingAccounts;
            bool foundAccounts = this.hashToAccountMap.TryGetValue(weakHash, out matchingAccounts);
            if (foundAccounts)
            {
                this.result.WeakPassword.UnionWith(matchingAccounts);
            }
        }

        private void AddAccountToHashMap()
        {
            byte[] currentHash = this.Account.NTHash;
            SortedSet<string> accountList;

            bool hashFoundInMap = this.hashToAccountMap.TryGetValue(currentHash, out accountList);

            if (!hashFoundInMap)
            {
                // Create a new account list for the hash, as it does not exist yet.
                accountList = new SortedSet<string>();
                this.hashToAccountMap.Add(currentHash, accountList);
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