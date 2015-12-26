namespace DSInternals.Common.Data
{
    using System;

    /// <summary>
    /// Flags that control the behavior of the user account.
    /// </summary>
    [Flags]
    public enum UserAccountControl : int
    {
        /// <summary>
        /// The logon script is executed.
        /// </summary>
        Script = 0x00000001,

        /// <summary>
        /// The user account is disabled.
        /// </summary>
        Disabled = 0x00000002,

        /// <summary>
        /// The home directory is required.
        /// </summary>
        HomeDirRequired = 0x00000008,

        /// <summary>
        /// The account is currently locked out.
        /// </summary>
        LockedOut = 0x00000010,

        /// <summary>
        /// No password is required.
        /// </summary>
        PasswordNotRequired = 0x00000020,

        /// <summary>
        /// The user cannot change the password.
        /// </summary>
        PasswordCantChange = 0x00000040,

        /// <summary>
        /// The user can send an encrypted password.
        /// </summary>
        PlaintextPasswordAllowed = 0x00000080,

        /// <summary>
        /// This is an account for users whose primary account is in another domain. This account provides user access to this domain, but not to any domain that trusts this domain. Also known as a local user account.
        /// </summary>
        TempDuplicateAccount = 0x00000100,

        /// <summary>
        /// This is a default account type that represents a typical user.
        /// </summary>
        NormalAccount = 0x00000200,

        /// <summary>
        /// This is a permit to trust account for a system domain that trusts other domains.
        /// </summary>
        TrustAccount = 0x00000800,

        /// <summary>
        /// This is a computer account for a computer that is a member of this domain.
        /// </summary>
        WorkstationAccount = 0x00001000,

        /// <summary>
        /// This is a computer account for a system backup domain controller that is a member of this domain.
        /// </summary>
        ServerAccount = 0x00002000,

        /// <summary>
        /// The password for this account will never expire.
        /// </summary>
        PasswordNeverExpires = 0x00010000,

        /// <summary>
        /// This is an Majority Node Set (MNS) logon account. With MNS, you can configure a multi-node Windows cluster without using a common shared disk.
        /// </summary>
        MNSAccount = 0x00020000,

        /// <summary>
        /// The user must log on using a smart card.
        /// </summary>
        SmartCardRequired = 0x00040000,

        /// <summary>
        /// The service account (user or computer account), under which a service runs, is trusted for Kerberos delegation. Any such service can impersonate a client requesting the service.
        /// </summary>
        TrustedForDelegation = 0x00080000,

        /// <summary>
        /// The security context of the user will not be delegated to a service even if the service account is set as trusted for Kerberos delegation.
        /// </summary>
        NotDelegated = 0x00100000,

        /// <summary>
        /// Restrict this principal to use only Data Encryption Standard (DES) encryption types for keys.
        /// </summary>
        UseDesKeyOnly = 0x00200000,

        /// <summary>
        /// This account does not require Kerberos pre-authentication for logon.
        /// </summary>
        PreAuthNotRequired = 0x00400000,

        /// <summary>
        /// The user password has expired. This flag is created by the system using data from the Pwd-Last-Set attribute and the domain policy.
        /// </summary>
        PasswordExpired = 0x00800000,

        /// <summary>
        /// The account is enabled for delegation. This is a security-sensitive setting; accounts with this option enabled should be strictly controlled. This setting enables a service running under the account to assume a client identity and authenticate as that user to other remote servers on the network.
        /// </summary>
        TrustedToAuthenticateForDelegation = 0x01000000
    }
}