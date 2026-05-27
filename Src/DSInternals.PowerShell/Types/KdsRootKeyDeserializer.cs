using System.Management.Automation;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell;

/// <summary>
/// Converts a CLIXML-deserialized <see cref="KdsRootKey"/> (i.e. a <see cref="PSObject"/> whose
/// type chain starts with <c>Deserialized.DSInternals.Common.Data.KdsRootKey</c>) back into a live
/// <see cref="KdsRootKey"/> instance so it can be passed to cmdlets that consume it.
/// </summary>
/// <remarks>
/// Registered in <c>DSInternals.types.ps1xml</c> together with a <c>TargetTypeForDeserialization</c>
/// hint on <c>Deserialized.DSInternals.Common.Data.KdsRootKey</c>, which causes PowerShell's
/// parameter binder to invoke this converter when an imported <c>KdsRootKey</c> is bound to a
/// strongly-typed parameter (for example after <c>Import-Clixml</c>).
/// </remarks>
public class KdsRootKeyDeserializer : PSTypeConverter
{
    /// <summary>
    /// Type name that PowerShell prefixes onto a deserialized <see cref="KdsRootKey"/> instance.
    /// </summary>
    private static readonly string SerializationTypeName = "Deserialized." + typeof(KdsRootKey).FullName;

    /// <summary>
    /// Indicates whether this converter can convert <paramref name="sourceValue"/> to
    /// <paramref name="destinationType"/>.
    /// </summary>
    /// <param name="sourceValue">The value to convert.</param>
    /// <param name="destinationType">The target type.</param>
    /// <returns>
    /// <see langword="true"/> when <paramref name="sourceValue"/> is a deserialized
    /// <see cref="KdsRootKey"/> <see cref="PSObject"/> and <paramref name="destinationType"/> is
    /// <see cref="KdsRootKey"/>; otherwise <see langword="false"/>.
    /// </returns>
    public override bool CanConvertFrom(object sourceValue, Type destinationType)
    {
        return destinationType == typeof(KdsRootKey)
            && sourceValue is PSObject psObject
            && psObject.TypeNames.Contains(SerializationTypeName);
    }

    /// <summary>
    /// Not supported. This converter only deserializes from <see cref="PSObject"/> to
    /// <see cref="KdsRootKey"/>, never the reverse direction.
    /// </summary>
    /// <param name="sourceValue">Ignored.</param>
    /// <param name="destinationType">Ignored.</param>
    /// <returns>This method never returns.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    public override bool CanConvertTo(object sourceValue, Type destinationType)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Rebuilds a live <see cref="KdsRootKey"/> from a deserialized <see cref="PSObject"/>
    /// by reading the note properties produced by CLIXML serialization and forwarding them
    /// to the full-state <see cref="KdsRootKey"/> constructor.
    /// </summary>
    /// <param name="sourceValue">The deserialized <see cref="PSObject"/>. Must satisfy <see cref="CanConvertFrom"/>.</param>
    /// <param name="destinationType">Must be <see cref="KdsRootKey"/>.</param>
    /// <param name="formatProvider">Format provider used by fallback conversions. May be <see langword="null"/>.</param>
    /// <param name="ignoreCase">Ignored; property lookups are already case-insensitive.</param>
    /// <returns>A live <see cref="KdsRootKey"/> with all properties restored.</returns>
    public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
    {
        // We expect that CanConvertFrom has already been called and returned true.
        var deserialized = (PSObject)sourceValue;

        Guid keyId = GetValue<Guid>(deserialized, nameof(KdsRootKey.KeyId));
        byte[] keyValue = GetValue<byte[]>(deserialized, nameof(KdsRootKey.KeyValue));
        string domainController = GetValue<string>(deserialized, nameof(KdsRootKey.DomainController));
        DateTime? creationTime = GetNullableValue<DateTime>(deserialized, nameof(KdsRootKey.CreationTime));
        DateTime? effectiveTime = GetNullableValue<DateTime>(deserialized, nameof(KdsRootKey.EffectiveTime));
        string kdfAlgorithm = GetValue<string>(deserialized, nameof(KdsRootKey.KdfAlgorithm));
        byte[] rawKdfParameters = GetValue<byte[]>(deserialized, nameof(KdsRootKey.RawKdfParameters));
        string secretAgreementAlgorithm = GetValue<string>(deserialized, nameof(KdsRootKey.SecretAgreementAlgorithm));
        byte[] secretAgreementParameters = GetValue<byte[]>(deserialized, nameof(KdsRootKey.SecretAgreementParameters));
        int publicKeyLength = GetValue<int>(deserialized, nameof(KdsRootKey.SecretAgreementPublicKeyLength));
        int privateKeyLength = GetValue<int>(deserialized, nameof(KdsRootKey.SecretAgreementPrivateKeyLength));

        return new KdsRootKey(
            keyId,
            keyValue,
            domainController,
            creationTime,
            effectiveTime,
            kdfAlgorithm,
            rawKdfParameters,
            secretAgreementAlgorithm,
            secretAgreementParameters,
            publicKeyLength,
            privateKeyLength);
    }

    /// <summary>
    /// Not supported. This converter only deserializes from <see cref="PSObject"/> to
    /// <see cref="KdsRootKey"/>, never the reverse direction.
    /// </summary>
    /// <param name="sourceValue">Ignored.</param>
    /// <param name="destinationType">Ignored.</param>
    /// <param name="formatProvider">Ignored.</param>
    /// <param name="ignoreCase">Ignored.</param>
    /// <returns>This method never returns.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    public override object ConvertTo(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Reads a value of a reference or value type from a deserialized <see cref="PSObject"/>'s
    /// note properties, unwrapping any nested <see cref="PSObject"/> and falling back to
    /// <see cref="LanguagePrimitives.ConvertTo(object, Type)"/> when a direct cast is not possible.
    /// </summary>
    /// <typeparam name="T">The target type of the property value.</typeparam>
    /// <param name="source">The deserialized <see cref="PSObject"/>.</param>
    /// <param name="propertyName">Name of the note property to read.</param>
    /// <returns>
    /// The property value converted to <typeparamref name="T"/>, or <c>default(T)</c> when the
    /// property is missing or null.
    /// </returns>
    private static T GetValue<T>(PSObject source, string propertyName)
    {
        var property = source.Properties[propertyName];

        if (property == null || property.Value == null)
        {
            return default;
        }

        if (property.Value is T typedValue)
        {
            return typedValue;
        }

        if (property.Value is PSObject pso && pso.BaseObject is T baseTypedValue)
        {
            return baseTypedValue;
        }

        return (T)LanguagePrimitives.ConvertTo(property.Value, typeof(T));
    }

    /// <summary>
    /// Reads a nullable value-type property from a deserialized <see cref="PSObject"/>,
    /// returning <see langword="null"/> when the property is missing or null on the source.
    /// </summary>
    /// <typeparam name="T">The underlying (non-nullable) value type.</typeparam>
    /// <param name="source">The deserialized <see cref="PSObject"/>.</param>
    /// <param name="propertyName">Name of the note property to read.</param>
    /// <returns>
    /// The property value converted to <c>T?</c>, or <see langword="null"/> when the property is
    /// missing or null.
    /// </returns>
    private static T? GetNullableValue<T>(PSObject source, string propertyName) where T : struct
    {
        var property = source.Properties[propertyName];

        if (property == null || property.Value == null)
        {
            return null;
        }

        if (property.Value is T typedValue)
        {
            return typedValue;
        }

        if (property.Value is PSObject pso && pso.BaseObject is T baseTypedValue)
        {
            return baseTypedValue;
        }

        return (T)LanguagePrimitives.ConvertTo(property.Value, typeof(T));
    }
}
