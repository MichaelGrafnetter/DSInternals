namespace DSInternals.PowerShell
{
    using DSInternals.Common;
    using System;
    using System.Management.Automation;
    using System.Text.RegularExpressions;

    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class ValidateHexStringAttribute : ValidateEnumeratedArgumentsAttribute
    {
        private const string HexPattern = @"^[0-9a-fA-F]+$";
        private const int CharsPerByte = 2;

        public ValidateHexStringAttribute()
        {
        }

        public ValidateHexStringAttribute(uint requiredByteCount)
        {
            // Set properties:
            this.RequiredByteCount = requiredByteCount;
        }

        public uint? RequiredByteCount
        {
            get;
            private set;
        }

        protected override void ValidateElement(object input)
        {
            string hexString = input as string;
            Validator.AssertNotNull(hexString, "input");

            int length = hexString.Length;
            bool isDivisible = length % CharsPerByte == 0;
            bool isHex = Regex.IsMatch(hexString, HexPattern);
            if(!isHex || !isDivisible)
            {
                // TODO: Extract as a resource
                throw new ValidationMetadataException("The string must be in hexadecimal format.");
            }
            
            int byteCount = length / CharsPerByte;
            if (this.RequiredByteCount.HasValue && byteCount != this.RequiredByteCount)
            {
                // TODO: Extract as resource
                string message = String.Format("The size of the binary value must be {0} bytes.", this.RequiredByteCount);
                throw new ValidationMetadataException(message);
            }
        }
    }
}
