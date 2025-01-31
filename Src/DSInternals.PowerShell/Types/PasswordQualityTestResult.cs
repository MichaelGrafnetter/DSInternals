namespace DSInternals.PowerShell
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains results of Active Directory password quality analysis.
    /// </summary>
    public class PasswordQualityTestResult
    {
        /// <summary>
        /// List of accounts whose passwords are stored using reversible encryption.
        /// </summary>
        public ISet<string> ClearTextPassword = new SortedSet<string>();

        /// <summary>
        /// List of accounts whose LM hashes are stored in the database.
        /// </summary>
        public ISet<string> LMHash = new SortedSet<string>();

        /// <summary>
        /// List of accounts that have no password set.
        /// </summary>
        public ISet<string> EmptyPassword = new SortedSet<string>();

        /// <summary>
        /// List of accounts that have a weak password.
        /// </summary>
        public ISet<string> WeakPassword = new SortedSet<string>();

        /// <summary>
        /// List of user accounts with SamAccountName as passwords.
        /// </summary>
        public ISet<string> SamAccountNameAsPassword = new SortedSet<string>();

        /// <summary>
        /// List of computer accounts with default passwords.
        /// </summary>
        public ISet<string> DefaultComputerPassword = new SortedSet<string>();

        /// <summary>
        /// List of accounts that do not require a password.
        /// </summary>
        public ISet<string> PasswordNotRequired = new SortedSet<string>();

        /// <summary>
        /// List of accounts whose passwords never expire.
        /// </summary>
        public ISet<string> PasswordNeverExpires = new SortedSet<string>();

        /// <summary>
        /// List of accounts that are missing AES keys.
        /// </summary>
        public ISet<string> AESKeysMissing = new SortedSet<string>();

        /// <summary>
        /// List of accounts on which preauthentication is not enforced.
        /// </summary>
        public ISet<string> PreAuthNotRequired = new SortedSet<string>();

        /// <summary>
        /// List of accounts that can only be authenticated using DES.
        /// </summary>
        public ISet<string> DESEncryptionOnly = new SortedSet<string>();

        /// <summary>
        /// List of accounts that are susceptible to the Kerberoasting attack.
        /// </summary>
        public ISet<string> Kerberoastable = new SortedSet<string>();

        /// <summary>
        /// List of administrative accounts that can be delegated.
        /// </summary>
        public ISet<string> DelegatableAdmins = new SortedSet<string>();

        /// <summary>
        /// List of smart-card enabled accounts that have a password set.
        /// </summary>
        public ISet<string> SmartCardUsersWithPassword = new SortedSet<string>();

        /// <summary>
        /// List of collections of accounts with the same password hashes.
        /// </summary>
        public IEnumerable<ISet<string>> DuplicatePasswordGroups = new List<SortedSet<string>>();
    }
}
