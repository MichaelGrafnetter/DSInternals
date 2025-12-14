using DSInternals.Common;
using System;
using System.Management.Automation;
using System.Security;

namespace DSInternals.PowerShell
{
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
            if (minLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minLength));
            }
            if (maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength));
            }
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
            SecureString pwd = password as SecureString;
            Validator.AssertNotNull(pwd, "password");
            int length = pwd.Length;
            if (length < this.MinLength || length > this.MaxLength)
            {
                string message = String.Format("The password must be {0}-{1} characters long.", this.MinLength, this.MaxLength);
                throw new ValidationMetadataException(message);
            }
        }
    }
}
