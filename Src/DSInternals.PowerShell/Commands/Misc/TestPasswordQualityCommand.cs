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

    [Cmdlet(VerbsDiagnostic.Test, "PasswordQuality", DefaultParameterSetName = ParamSetSingleSortedFile)]
    [OutputType(new Type[] { typeof(PasswordQualityTestResult) })]
    public class TestPasswordQualityCommand : PSCmdletEx, IDisposable
    {
        #region Constants
        protected const string ParamSetSingleSortedFile = "SingleFile";

        protected const string ParamSetMultipuleSortedFile = "MultiFile";

        /// <summary>
        /// Expected number of users being processed.
        /// </summary>
        private const int PasswordDictionaryInitialCapacity = 10000;

        /// <summary>
        /// Size of buffer to use when reading input files. We use 64K.
        /// </summary>
        private const int SequentialReadBufferSize = 65536;

        /// <summary>
        /// Report progress every 1000 lines.
        /// </summary>
        private const long FileReadProgressFrequency = 1000;

        /// <summary>
        /// Separator of hashes in the file from HaveIBeenPwned.
        /// </summary>
        private const char HashSeparator = ':';

        /// <summary>
        /// Length of the hash prefix (K-anonymity) in the files from HaveIBeenPwned.
        /// </summary>
        private const int HashPrefixLength = 5;
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

        [Parameter(ParameterSetName = ParamSetSingleSortedFile)]
        [Alias("HIBPFile", "HaveIBeenPwnedFile")]
        [ValidateNotNullOrEmpty]
        public string WeakPasswordHashesSortedFile
        {
            get;
            set;
        }

        [Parameter(ParameterSetName = ParamSetMultipuleSortedFile)]
        [Alias("WeakPasswordHashesSortedDirectory", "HIBPDirectory", "HaveIBeenPwnedDirectory")]
        [ValidateNotNullOrEmpty]
        public string WeakPasswordHashesSortedFilePath
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

        private SortedFileSearcher sortedHashFileSearcher;
        #endregion Fields

        #region Cmdlet Overrides

        protected override void BeginProcessing()
        {
            // Test the optional file path in advance to throw an early error.
            this.ResolveFilePath(this.WeakPasswordHashesFile);
            this.ResolveFilePath(this.WeakPasswordsFile);
            this.ResolveDirectoryPath(this.WeakPasswordHashesSortedFilePath);

            // Open the sorted weak password hashes file, as we will be searching it on-the-fly.
            string sortedHashesFile = this.ResolveFilePath(this.WeakPasswordHashesSortedFile);
            if (sortedHashesFile != null)
            {
                this.sortedHashFileSearcher = new SortedFileSearcher(sortedHashesFile);
            }

            if (this.ShouldTestWeakPasswordsInMemory || !this.SkipDuplicatePasswordTest.IsPresent)
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
                string message = String.Format("Skipping account {0}, because it is disabled.", this.Account.LogonName);
                this.WriteVerbose(message);
                return;
            }

            // Verbose message
            string message2 = String.Format("Processing account {0}...", this.Account.LogonName);
            this.WriteVerbose(message2);

            if (this.Account.UserAccountControl.HasFlag(UserAccountControl.PasswordNeverExpires))
            {
                // The account has a non-expiring password.
                this.result.PasswordNeverExpires.Add(this.Account.LogonName);
            }

            if (this.Account.UserAccountControl.HasFlag(UserAccountControl.UseDesKeyOnly))
            {
                // Only DES kerberos encryption type is used with this account.
                this.result.DESEncryptionOnly.Add(this.Account.LogonName);
            }

            if (this.Account.AdminCount && !this.Account.UserAccountControl.HasFlag(UserAccountControl.NotDelegated))
            {
                // This administrative account can be delegated.
                this.result.DelegatableAdmins.Add(this.Account.LogonName);
            }

            if (this.Account.UserAccountControl.HasFlag(UserAccountControl.PasswordNotRequired))
            {
                // The account's password is not required.
                this.result.PasswordNotRequired.Add(this.Account.LogonName);
            }

            if (this.Account.UserAccountControl.HasFlag(UserAccountControl.PreAuthNotRequired))
            {
                // Pre-authentication is not required for this account account.
                this.result.PreAuthNotRequired.Add(this.Account.LogonName);
            }

            if(this.Account.SamAccountType == SamAccountType.User && this.Account.ServicePrincipalName?.Length > 0 && !this.Account.SupportsKerberosAESEncryption)
            {
                // This is a kerberoastable user/service account, because it has a SPN configured, but has Kerberos AES encryption support disabled.
                this.result.Kerberoastable.Add(this.Account.LogonName);
            }

            if (this.Account.SupplementalCredentials != null)
            {
                if (this.Account.SupplementalCredentials.ClearText != null)
                {
                    // Account has ClearText password (stored using reversible encryption)
                    this.result.ClearTextPassword.Add(this.Account.LogonName);
                }

                if(this.Account.UserAccountControl.HasFlag(UserAccountControl.SmartCardRequired))
                {
                    // Smart card user
                    if (this.Account.SupplementalCredentials.Kerberos != null)
                    {
                        // Accounts that require smart card authentication should have an empty supplemental credentials data structure.
                        this.result.SmartCardUsersWithPassword.Add(this.Account.LogonName);
                    }
                }
                else
                {
                    // Not a smart card user
                    if (this.Account.SupplementalCredentials.KerberosNew == null)
                    {
                        // Account is missing the AES kerberos keys. This is only OK if smart card auth is enforced for this account.
                        this.result.AESKeysMissing.Add(this.Account.LogonName);
                    }
                }
            }

            if (this.Account.LMHash != null)
            {
                // Account has the LM hash present.
                this.result.LMHash.Add(this.Account.LogonName);
            }

            if (this.Account.NTHash == null)
            {
                // The account has no password.
                this.result.EmptyPassword.Add(this.Account.LogonName);

                // All the remaining tests are based on NT hash, so we can skip them.
                return;
            }

            if (HashEqualityComparer.GetInstance().Equals(this.Account.NTHash, NTHash.Empty))
            {
                // The account has an empty password.
                this.result.EmptyPassword.Add(this.Account.LogonName);

                // Skip the remaining tests, because they only make sense for non-empty passwords.
                return;
            }
            if (this.Account.SamAccountType == SamAccountType.User)
            {
                // Check if the user has the SamAccountName as password.
                this.TestSamAccountNameAsPassword();
            }
            if (this.Account.SamAccountType == SamAccountType.Computer)
            {
                // Check if the computer has a default password.
                this.TestComputerDefaultPassword();
            }
            else
            {
                // Computer accounts typically have random passwords, so we only perform the following tests for other account types like User or Trust.
                this.LookupAccountNTHashInSortedFile();

                if (this.hashToAccountMap != null)
                {
                    // Add the current account's NT hash to the map for further processing.
                    this.AddAccountToHashMap();
                }
            }
        }

        protected override void EndProcessing()
        {
            // Close any open files (we can do it sooner than during dispose)
            if (this.sortedHashFileSearcher != null)
            {
                this.sortedHashFileSearcher.Dispose();
                this.sortedHashFileSearcher = null;
            }

            // Process duplicate passwords
            if (!this.SkipDuplicatePasswordTest.IsPresent)
            {
                this.result.DuplicatePasswordGroups = this.hashToAccountMap.Values.Where(set => set.Count > 1).ToList();
            }

            // Process Weak Passwords
            this.TestWeakPasswordsInMemory();

            // The processing has finished, so return the results.
            this.WriteObject(this.result);
        }

        #endregion Cmdlet Overrides

        #region Helper Methods
        private bool ShouldTestWeakPasswordsInMemory
        {
            get
            {
                // Weak passwords must be provided in at least one way.
                // Note: The Sorted Hash File is handled independently
                return this.WeakPasswords != null || this.WeakPasswordsFile != null || WeakPasswordHashesFile != null;
            }
        }

        private void LookupAccountNTHashInSortedFile()
        {
            string hash = this.Account.NTHash.ToHex(true);
            // If there is a file path present, the hashes are in seperate sorted files
            if (this.WeakPasswordHashesSortedFilePath != null)
            {
                // The files in the path should be named with the first 5 chararacters of the hash and the extension txt, like ABDD0.txt
                string sortedHashesFile = this.ResolveFilePath(this.WeakPasswordHashesSortedFilePath + hash.Substring(0, HashPrefixLength) + ".txt");
                if (sortedHashesFile != null)
                {
                    // Assuming all went well, we should be able to set up to search this much smaller file for the hashes
                    this.sortedHashFileSearcher = new SortedFileSearcher(sortedHashesFile);

                    // In the split database the hashes are stored in the sorted files starting with the 6th character (since the filename is the first 5
                    hash = hash.Substring(HashPrefixLength);
                }
            }

            if (this.sortedHashFileSearcher != null)
            {
                // Check the password on the fly in the sorted file using binary search
                bool found = this.sortedHashFileSearcher.FindString(hash);
                if (found)
                {
                    this.result.WeakPassword.UnionWith(new string[] { this.Account.LogonName });
                }
            }
        }

        private void TestWeakPasswordsInMemory()
        {
            // Process the list of weak passwords, if present
            this.TestWeakPasswordsFromList();

            // Process the file containing weak passwords, if present
            this.TestWeakPasswordsFromUnsortedFile();

            // Process the file containing weak password hashes, if present
            this.TestWeakNTHashesFromUnsortedFile();
        }

        private void TestWeakPasswordsFromList()
        {
            if (this.WeakPasswords != null)
            {
                foreach (string weakPassword in this.WeakPasswords)
                {
                    this.TestWeakPassword(weakPassword);
                }
            }
        }

        private void TestWeakPasswordsFromUnsortedFile()
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
            using (var reader = new StreamReader(resolvedPath, Encoding.UTF8, true, SequentialReadBufferSize))
            {
                long fileLength = reader.BaseStream.Length;
                long linesRead = 0;
                string weakPassword;

                while ((weakPassword = reader.ReadLine()) != null)
                {
                    this.TestWeakPassword(weakPassword);

                    // For performance reasons, we do not want to report progress too often.
                    if (linesRead++ % FileReadProgressFrequency == 0)
                    {
                        this.ReportProgress(fileLength, reader.BaseStream.Position, progress);
                    }
                }

                // Report progress completion just to be sure
                this.ReportProgress(fileLength, fileLength, progress);
            }
        }

        private void TestWeakNTHashesFromUnsortedFile()
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
            using (var reader = new StreamReader(resolvedPath, Encoding.ASCII, true, SequentialReadBufferSize))
            {
                long fileLength = reader.BaseStream.Length;
                long linesRead = 0;
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    // Files from HIBP have this format: <NT Hash>:<Cardinality>
                    int hashLength = line.IndexOf(HashSeparator);
                    if (hashLength == -1)
                    {
                        hashLength = line.Length;
                    }

                    // TODO: Handle malformed lines
                    this.TestWeakNTHash(line.HexToBinary(0, hashLength));

                    // For performance reasons, we do not want to report progress too often.
                    if (linesRead++ % FileReadProgressFrequency == 0)
                    {
                        this.ReportProgress(fileLength, reader.BaseStream.Position, progress);
                    }
                }

                // Report progress completion just to be sure
                this.ReportProgress(fileLength, fileLength, progress);
            }
        }

        private void ReportProgress(long fileLength, long position, ProgressRecord progress)
        {
            if (position >= fileLength)
            {
                // Report operation completion
                progress.RecordType = ProgressRecordType.Completed;
            }

            // Calculate the current progress
            int percentComplete = (int)(position * 100.0 / fileLength);

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
                this.TestWeakNTHash(weakHash);
            }
        }

        private void TestWeakNTHash(byte[] weakHash)
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

            accountList.Add(this.Account.LogonName);
        }

        private void TestSamAccountNameAsPassword()
        {
            string userLowerPassword = this.Account.SamAccountName.ToLower();
            byte[] userLowerHash = NTHash.ComputeHash(userLowerPassword);

            if (HashEqualityComparer.GetInstance().Equals(this.Account.NTHash, userLowerHash))
            {
                // Username Password is lowercase SamAccountName
                this.result.SamAccountNameAsPassword.Add(this.Account.LogonName);
            }
            else
            {
                string userExactPassword = this.Account.SamAccountName;
                byte[] userExactHash = NTHash.ComputeHash(userExactPassword);
                if (HashEqualityComparer.GetInstance().Equals(this.Account.NTHash, userExactHash))
                {
                    // Username Password is exact SamAccountName
                    this.result.SamAccountNameAsPassword.Add(this.Account.LogonName);
                }
            }
        }

        private void TestComputerDefaultPassword()
        {
            string defaultPassword = this.Account.SamAccountName.TrimEnd('$').ToLower();
            byte[] defaultHash = NTHash.ComputeHash(defaultPassword);
            if (HashEqualityComparer.GetInstance().Equals(this.Account.NTHash, defaultHash))
            {
                // The computer has the default password.
                this.result.DefaultComputerPassword.Add(this.Account.LogonName);
            }
        }
        #endregion Helper Methods

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.sortedHashFileSearcher != null)
            {
                this.sortedHashFileSearcher.Dispose();
                this.sortedHashFileSearcher = null;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion IDisposable Support
    }
}
