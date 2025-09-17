using System;
using System.Management.Automation;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell
{
    /// <summary>
    /// Provides PowerShell type conversion for deserializing supplemental credentials from serialized objects.
    /// </summary>
    public class SupplementalCredentialsDeserializer : PSTypeConverter
    {
        private const string SerializationPropertyName = "Base64Blob";
        private static readonly string SerializationTypeName = "Deserialized." + typeof(SupplementalCredentials).FullName;

        /// <summary>
        /// Determines whether this converter can convert from the specified source type to the supplemental credentials type.
        /// </summary>
        /// <param name="sourceValue">The source value to convert from.</param>
        /// <param name="destinationType">The destination type to convert to.</param>
        /// <returns>True if the conversion is supported; otherwise, false.</returns>
        public override bool CanConvertFrom(object sourceValue, Type destinationType)
        {
            bool sourceTypeIsValid = sourceValue is PSObject &&
                ((PSObject)sourceValue).TypeNames.Contains(SerializationTypeName);
            bool destinationTypeIsValid = destinationType == typeof(SupplementalCredentials);
            return destinationTypeIsValid && sourceTypeIsValid;
        }

        /// <summary>
        /// Determines whether this converter can convert from the supplemental credentials type to the specified destination type.
        /// </summary>
        /// <param name="sourceValue">The source value to convert from.</param>
        /// <param name="destinationType">The destination type to convert to.</param>
        /// <returns>Always throws NotImplementedException as conversion to other types is not supported.</returns>
        public override bool CanConvertTo(object sourceValue, Type destinationType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts a PowerShell deserialized object back to SupplementalCredentials by deserializing the Base64-encoded blob.
        /// </summary>
        /// <param name="sourceValue">The PowerShell object containing the serialized credentials.</param>
        /// <param name="destinationType">The destination type (SupplementalCredentials).</param>
        /// <param name="formatProvider">The format provider (not used).</param>
        /// <param name="ignoreCase">Whether to ignore case (not used).</param>
        /// <returns>A SupplementalCredentials object deserialized from the Base64 blob.</returns>
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
