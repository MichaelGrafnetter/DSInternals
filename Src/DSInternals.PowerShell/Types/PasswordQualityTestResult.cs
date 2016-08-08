namespace DSInternals.PowerShell
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary>
    /// Contains results of Active Directory password quality analysis.
    /// </summary>
    public class PasswordQualityTestResult
    {
        /// <summary>
        /// List of accounts whose passwords are stored using reversible encryption.
        /// </summary>
        public StringDictionary ClearTextPassword = new StringDictionary();
        /// <summary>
        /// List of accounts whose LM hashes are stored in the database.
        /// </summary>
        public StringCollection LMHash = new StringCollection();
        /// <summary>
        /// List of accounts that have no password set.
        /// </summary>
        public StringCollection EmptyPassword = new StringCollection();
        /// <summary>
        /// List of accounts that have a weak password.
        /// </summary>
        public StringDictionary WeakPassword = new StringDictionary();
        /// <summary>
        /// List of accounts that had a weak password.
        /// </summary>
        public StringDictionary WeakHistoricalPassword = new StringDictionary();
        /// <summary>
        /// List of computer accounts with default passwords.
        /// </summary>
        public StringCollection DefaultComputerPassword = new StringCollection();
        /// <summary>
        /// List of accounts that do not require a password.
        /// </summary>
        public StringCollection PasswordNotRequired = new StringCollection();
        /// <summary>
        /// List of accounts whose passwords never expire.
        /// </summary>
        public StringCollection PasswordNeverExpires = new StringCollection();
        /// <summary>
        /// List of accounts that are missing AES keys.
        /// </summary>
        public StringCollection AESKeysMissing = new StringCollection();
        /// <summary>
        /// List of accounts on which preauthentication is not enforced.
        /// </summary>
        public StringCollection PreAuthNotRequired = new StringCollection();
        /// <summary>
        /// List of accounts that can only be authenticated using DES.
        /// </summary>
        public StringCollection DESEncryptionOnly = new StringCollection();
        /// <summary>
        /// List of administrative accounts that can be delegated.
        /// </summary>
        public StringCollection DelegatableAdmins = new StringCollection();
        /// <summary>
        /// List of collections of accounts with the same password hashes.
        /// </summary>
        public IList<StringCollection> DuplicatePasswordGroups;
    }
}
