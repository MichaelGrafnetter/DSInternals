using System.Management.Automation;
using System.Security;

namespace DSInternals.PowerShell;

// TODO: Modify comments for ValidateSecureStringLengthAttribute.
/// <summary>
/// The ValidatePasswordLength attribute specifies the minimum and maximum number of characters for a cmdlet parameter argument. This attribute can also be used by Windows PowerShell functions.
/// </summary>
[AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class ValidatePasswordLengthAttribute : ValidateEnumeratedArgumentsAttribute
{
    /// <summary>
    /// Initializes a new instance of the ValidatePasswordLengthAttribute class with the minimum and maximum lengths for the cmdlet parameter arguments.
    /// </summary>
    /// <param name="minLength">Specifies the minimum number of characters allowed.</param>
    /// <param name="maxLength">Specifies the maximum number of characters allowed.</param>
    public ValidatePasswordLengthAttribute(int minLength, int maxLength)
    {
        // Validate parameters:
        ArgumentOutOfRangeException.ThrowIfNegative(minLength);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxLength);

        if (maxLength < minLength)
        {
            throw new ValidationMetadataException("Maximum length must be greater than or equal to the minimum length.");
        }
        // Set properties:
        this.MinLength = minLength;
        this.MaxLength = maxLength;
    }

    /// <summary>
    /// Gets the maximum number of characters (length) that is allowed for the cmdlet parameter argument.
    /// </summary>
    public int MaxLength
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the minimum number of characters (length) that is allowed for the cmdlet parameter argument.
    /// </summary>
    public int MinLength
    {
        get;
        private set;
    }

    /// <summary>
    /// Validates a specified argument value of an enumerated argument.
    /// </summary>
    /// <param name="password">The argument to be validated.</param>
    protected override void ValidateElement(object password)
    {
        ArgumentNullException.ThrowIfNull(password);

        // This can throw for non-SecureString types
        SecureString pwd = (SecureString)password;

        int length = pwd.Length;
        if (length < this.MinLength || length > this.MaxLength)
        {
            string message = $"The password must be {this.MinLength}-{this.MaxLength} characters long.";
            throw new ValidationMetadataException(message);
        }
    }
}
