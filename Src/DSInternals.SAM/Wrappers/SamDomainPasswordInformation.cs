using System.Runtime.InteropServices;

namespace DSInternals.SAM;

/// <summary>
/// The DOMAIN_PASSWORD_INFORMATION structure contains information
/// about a domain's password policy, such as the minimum length
/// for passwords and how unique passwords must be.
/// </summary>
/// <see>https://msdn.microsoft.com/en-us/library/windows/desktop/aa375371%28v=vs.85%29.aspx</see>
[StructLayout(LayoutKind.Sequential)]
public struct SamDomainPasswordInformation
{
    /// <summary>
    /// Specifies the minimum length, in characters, of a valid password.
    /// </summary>
    public ushort MinPasswordLength;

    /// <summary>
    /// Indicates the number of previous passwords saved in the history list. A user cannot reuse a password in the history list.
    /// </summary>
    public ushort PasswordHistoryCount;

    /// <summary>
    /// Flags that describe the password properties
    /// </summary>
    public SamDomainPasswordProperties PasswordProperties;

    private long maxPasswordAge;
    private long minPasswordAge;

    /// <summary>
    /// Specifies the maximum length of time that a password can remain the same. Passwords older than this must be changed. Because SAM stores relative times as negative values and absolute times as positive numbers, the time is stored as a FILETIME structure with negative values.
    /// </summary>
    public TimeSpan MaxPasswordAge
    {
        get
        {
            return -new TimeSpan(maxPasswordAge);
        }
        set
        {
            maxPasswordAge = value.Ticks;
        }
    }

    /// <summary>
    /// Specifies the minimum length of time before a password can be changed. Because SAM stores relative times as negative values and absolute times as positive numbers, the time is stored as a FILETIME structure with negative values.
    /// </summary>
    public TimeSpan MinPasswordAge
    {
        get
        {
            return -new TimeSpan(this.minPasswordAge);
        }
        set
        {
            this.minPasswordAge = value.Ticks;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether passwords must meet complexity requirements.
    /// </summary>
    public bool ComplexityEnabled
    {
        get
        {
            return this.PasswordProperties.HasFlag(SamDomainPasswordProperties.PasswordComplexity);
        }
        set
        {
            if (value == true)
            {
                // Add flag
                this.PasswordProperties |= SamDomainPasswordProperties.PasswordComplexity;
            }
            else
            {
                // Remove flag
                this.PasswordProperties &= ~SamDomainPasswordProperties.PasswordComplexity;
            }

        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the "Store passwords using reversible encryption" option is enabled.
    /// </summary>
    public bool ReversibleEncryptionEnabled
    {
        get
        {
            return this.PasswordProperties.HasFlag(SamDomainPasswordProperties.ClearTextPassword);
        }
        set
        {
            if (value == true)
            {
                // Add flag
                this.PasswordProperties |= SamDomainPasswordProperties.ClearTextPassword;
            }
            else
            {
                // Remove flag
                this.PasswordProperties &= ~SamDomainPasswordProperties.ClearTextPassword;
            }
        }
    }
}
