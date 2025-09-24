namespace DSInternals.SAM;

/// <summary>
/// Flags that describe the password properties.
/// </summary>
[Flags]
public enum SamDomainPasswordProperties : uint
{
    /// <summary>
    /// No flags are set.
    /// </summary>
    None = 0,

    /// <summary>
    /// The password must have a mix of at least two of the following types of characters: Uppercase characters, Lowercase characters, Numerals
    /// </summary>
    PasswordComplexity = 0x00000001,

    /// <summary>
    /// The password cannot be changed without logging on. Otherwise, if your password has expired, you can change your password and then log on.
    /// </summary>
    RequireLogonToChangePassword = 0x00000002,

    /// <summary>
    /// Forces the client to use a protocol that does not allow the domain controller to get the plaintext password.
    /// </summary>
    NoClearChange = 0x00000004,

    /// <summary>
    /// Allows the built-in administrator account to be locked out from network logons.
    /// </summary>
    LockoutAdmins = 0x00000008,

    /// <summary>
    /// The directory service is storing a plaintext password for all users instead of a hash function of the password.
    /// </summary>
    ClearTextPassword = 0x00000010,

    /// <summary>
    /// Removes the requirement that the machine account password be automatically changed every week.
    /// This value should not be used as it can weaken security.
    /// </summary>
    RefuseMachinePasswordChange = 0x00000020
}
