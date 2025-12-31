namespace DSInternals.Common.Data;

/// <summary>
/// Specifies which properties to load for an Active Directory account.
/// </summary>
[Flags]
public enum AccountPropertySets : int
{
    /// <summary>
    /// Only basic attributes like objectGuid and name will be loaded.
    /// </summary>
    None = 0,

    /// <summary>
    /// The distinguished name of the account.
    /// </summary>
    DistinguishedName = 1 << 0,

    /// <summary>
    /// Generic account information such as UPN, SID history, last logon, and description.
    /// </summary>
    GenericAccountInfo = 1 << 1,

    /// <summary>
    /// User-specific information such as display name, given name, surname, and contact details.
    /// </summary>
    GenericUserInfo = 1 << 2,

    /// <summary>
    /// Computer-specific information such as DNS host name, location, and operating system details.
    /// </summary>
    GenericComputerInfo = 1 << 3,

    /// <summary>
    /// Combination of <see cref="GenericAccountInfo"/>, <see cref="GenericUserInfo"/>, and <see cref="GenericComputerInfo"/>.
    /// </summary>
    GenericInfo = GenericAccountInfo | GenericUserInfo | GenericComputerInfo,

    /// <summary>
    /// The security descriptor of the account.
    /// </summary>
    SecurityDescriptor = 1 << 4,

    /// <summary>
    /// The NT hash of the current password.
    /// </summary>
    NTHash = 1 << 5,

    /// <summary>
    /// The LM hash of the current password.
    /// </summary>
    LMHash = 1 << 6,

    /// <summary>
    /// Combination of <see cref="NTHash"/> and <see cref="LMHash"/>.
    /// </summary>
    PasswordHashes = NTHash | LMHash,

    /// <summary>
    /// The NT hash history of previous passwords.
    /// </summary>
    NTHashHistory = 1 << 7,

    /// <summary>
    /// The LM hash history of previous passwords.
    /// </summary>
    LMHashHistory = 1 << 8,

    /// <summary>
    /// Combination of <see cref="NTHashHistory"/> and <see cref="LMHashHistory"/>.
    /// </summary>
    PasswordHashHistory = NTHashHistory | LMHashHistory,

    /// <summary>
    /// Supplemental credentials including Kerberos keys and other authentication data.
    /// </summary>
    SupplementalCredentials = 1 << 9,

    /// <summary>
    /// Combination of <see cref="PasswordHashes"/>, <see cref="PasswordHashHistory"/>, and <see cref="SupplementalCredentials"/>.
    /// </summary>
    Secrets = PasswordHashes | PasswordHashHistory | SupplementalCredentials,

    /// <summary>
    /// Windows Hello for Business and FIDO key credentials.
    /// </summary>
    KeyCredentials = 1 << 10,

    /// <summary>
    /// Credential roaming data including certificates and DPAPI master keys.
    /// </summary>
    RoamedCredentials = 1 << 11,

    /// <summary>
    /// Windows LAPS (Local Administrator Password Solution) password information.
    /// </summary>
    WindowsLAPS = 1 << 12,

    /// <summary>
    /// Legacy LAPS password information.
    /// </summary>
    LegacyLAPS = 1 << 13,

    /// <summary>
    /// Combination of <see cref="WindowsLAPS"/> and <see cref="LegacyLAPS"/>.
    /// </summary>
    LAPS = WindowsLAPS | LegacyLAPS,

    /// <summary>
    /// The distinguished name of the user or group that manages the computer.
    /// </summary>
    ManagedBy = 1 << 14,

    /// <summary>
    /// The distinguished name of the user's manager.
    /// </summary>
    Manager = 1 << 15,

    /// <summary>
    /// All available property sets.
    /// </summary>
    All = DistinguishedName | GenericInfo | SecurityDescriptor | Secrets | KeyCredentials | RoamedCredentials | LAPS | ManagedBy | Manager
}
