using System;

namespace DSInternals.Common.Data
{
    [Flags]
    /// <summary>
    /// Specifies which property sets to load when creating account objects from directory data.
    /// </summary>
    public enum AccountPropertySets : int
    {
        /// <summary>
        /// No properties are loaded.
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Load the distinguished name property.
        /// </summary>
        DistinguishedName = 1 << 0,
        
        /// <summary>
        /// Load generic account information including user principal name, SID history, last logon, and password last set.
        /// </summary>
        GenericAccountInfo = 1 << 1,
        
        /// <summary>
        /// Load generic user information including names, contact details, and organizational data.
        /// </summary>
        GenericUserInfo = 1 << 2,
        
        /// <summary>
        /// Load generic computer information including location and operating system details.
        /// </summary>
        GenericComputerInfo = 1 << 3,
        
        /// <summary>
        /// Load all generic information (account, user, and computer properties).
        /// </summary>
        GenericInfo = GenericAccountInfo | GenericUserInfo | GenericComputerInfo,
        
        /// <summary>
        /// Load the security descriptor for the account.
        /// </summary>
        SecurityDescriptor = 1 << 4,
        
        /// <summary>
        /// Load the NT hash (current password hash).
        /// </summary>
        NTHash = 1 << 5,
        
        /// <summary>
        /// Load the LM hash (legacy password hash).
        /// </summary>
        LMHash = 1 << 6,
        
        /// <summary>
        /// Load both NT and LM password hashes.
        /// </summary>
        PasswordHashes = NTHash | LMHash,
        
        /// <summary>
        /// Load the NT hash history (previous password hashes).
        /// </summary>
        NTHashHistory = 1 << 7,
        
        /// <summary>
        /// Load the LM hash history (previous legacy password hashes).
        /// </summary>
        LMHashHistory = 1 << 8,
        
        /// <summary>
        /// Load both NT and LM password hash histories.
        /// </summary>
        PasswordHashHistory = NTHashHistory | LMHashHistory,
        
        /// <summary>
        /// Load supplemental credentials including Kerberos keys and other authentication data.
        /// </summary>
        SupplementalCredentials = 1 << 9,
        
        /// <summary>
        /// Load all secret information (password hashes, hash history, and supplemental credentials).
        /// </summary>
        Secrets = PasswordHashes | PasswordHashHistory | SupplementalCredentials,
        
        /// <summary>
        /// Load key credentials for passwordless authentication (Windows Hello, FIDO keys).
        /// </summary>
        KeyCredentials = 1 << 10,
        
        /// <summary>
        /// Load roamed credentials including certificates and private keys.
        /// </summary>
        RoamedCredentials = 1 << 11,
        
        /// <summary>
        /// Load Windows LAPS (Local Administrator Password Solution) information.
        /// </summary>
        WindowsLAPS = 1 << 12,
        
        /// <summary>
        /// Load legacy LAPS information from older implementations.
        /// </summary>
        LegacyLAPS = 1 << 13,
        
        /// <summary>
        /// Load all LAPS information (Windows and legacy implementations).
        /// </summary>
        LAPS = WindowsLAPS | LegacyLAPS,
        
        /// <summary>
        /// Load the managed by property (for computer accounts).
        /// </summary>
        ManagedBy = 1 << 14,
        
        /// <summary>
        /// Load the manager property (for user accounts).
        /// </summary>
        Manager = 1 << 15,
        
        /// <summary>
        /// Load all available properties.
        /// </summary>
        All = DistinguishedName | GenericInfo | SecurityDescriptor | Secrets | KeyCredentials | RoamedCredentials | LAPS | ManagedBy | Manager
    }
}
