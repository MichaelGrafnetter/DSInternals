using System;
using System.Management.Automation;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell
{
    /// <summary>
    /// Represents a SupplementalCredentialsDeserializer.
    /// </summary>
    public class SupplementalCredentialsDeserializer : PSTypeConverter
    {
        private const string SerializationPropertyName = "Base64Blob";
        private static readonly string SerializationTypeName = "Deserialized." + typeof(SupplementalCredentials).FullName;

        /// <summary>
        /// CanConvertFrom implementation.
        /// </summary>
        public override bool CanConvertFrom(object sourceValue, Type destinationType)
        {
            bool sourceTypeIsValid = sourceValue is PSObject &&
                ((PSObject)sourceValue).TypeNames.Contains(SerializationTypeName);
            bool destinationTypeIsValid = destinationType == typeof(SupplementalCredentials);
            return destinationTypeIsValid && sourceTypeIsValid;
        }

        /// <summary>
        /// CanConvertTo implementation.
        /// </summary>
        public override bool CanConvertTo(object sourceValue, Type destinationType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ConvertFrom implementation.
        /// </summary>
        public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            // We expect that CanConvertFrom has already been called and returned true.
            var deserializedObject = (PSObject)sourceValue;
            var base64Value = deserializedObject.Properties[SerializationPropertyName].Value;
            byte[] binaryCredentials = Convert.FromBase64String((string)base64Value);
            return new SupplementalCredentials(binaryCredentials);
        }

        /// <summary>
        /// ConvertTo implementation.
        /// </summary>
        public override object ConvertTo(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            throw new NotImplementedException();
        }
    }
}
