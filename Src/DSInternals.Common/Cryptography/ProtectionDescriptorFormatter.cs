using System.Linq;
using System.Security.Principal;
using System.Text;
using DSInternals.Common;

namespace DSInternals.Common.Cryptography;

/// <summary>
/// Formats CNG DPAPI-NG protection descriptor rule strings.
/// </summary>
/// <remarks>
/// Protection descriptor rule strings are documented by Microsoft as provider assignments joined by
/// capitalized <c>AND</c> and <c>OR</c> operators. See
/// <see href="https://learn.microsoft.com/en-us/windows/win32/seccng/protection-descriptors">Protection Descriptors</see>.
/// </remarks>
internal static class ProtectionDescriptorFormatter
{
    /// <summary>
    /// Represents the documented disjunction operator between protection alternatives.
    /// </summary>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301 OR LOCAL=machine</code>
    /// </example>
    private const string OrOperator = " OR ";

    /// <summary>
    /// Represents the documented conjunction operator between protectors that must all match.
    /// </summary>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301 AND SID=S-1-5-21-3101812</code>
    /// </example>
    private const string AndOperator = " AND ";

    /// <summary>
    /// Formats protection descriptor alternatives joined by the capitalized <c>OR</c> operator.
    /// </summary>
    /// <param name="values">The formatted descriptor fragments.</param>
    /// <returns>A formatted disjunction, or <see langword="null" /> when no child descriptor is available.</returns>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301 OR LOCAL=machine</code>
    /// </example>
    internal static string? FormatOr(IEnumerable<string?>? values) => Join(OrOperator, values);

    /// <summary>
    /// Formats protection descriptor requirements joined by the capitalized <c>AND</c> operator.
    /// </summary>
    /// <param name="values">The formatted descriptor fragments.</param>
    /// <returns>A formatted conjunction, or <see langword="null" /> when no child descriptor is available.</returns>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301 AND SID=S-1-5-21-3101812</code>
    /// </example>
    internal static string? FormatAnd(IEnumerable<string?>? values) => Join(AndOperator, values);

    /// <summary>
    /// Joins formatted descriptor fragments with a protection descriptor operator.
    /// </summary>
    /// <param name="separator">The protection descriptor operator separator.</param>
    /// <param name="values">The formatted descriptor fragments.</param>
    /// <returns>A formatted descriptor string, or <see langword="null" /> when no child descriptor is available.</returns>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301 AND SID=S-1-5-21-3101812</code>
    /// </example>
    private static string? Join(string separator, IEnumerable<string?>? values)
    {
        if (values == null)
        {
            return null;
        }

        var descriptor = new StringBuilder();
        IEnumerable<string?> descriptors = values.Where(static value => !string.IsNullOrEmpty(value));
        descriptor.AppendJoin<string?>(separator, descriptors);

        return descriptor.Length == 0 ? null : descriptor.ToString();
    }

    /// <summary>
    /// Formats a single provider assignment in a protection descriptor rule string.
    /// </summary>
    /// <param name="providerName">The protection provider name.</param>
    /// <param name="providerAttribute">The provider attribute value.</param>
    /// <returns>The formatted provider assignment.</returns>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301</code>
    /// <code language="text">WEBCREDENTIALS=MyPasswordName,myweb.com</code>
    /// </example>
    internal static string FormatProvider(string providerName, string? providerAttribute)
    {
        return $"{providerName}={EscapeValue(providerAttribute)}";
    }

    /// <summary>
    /// Formats a SID-based protection descriptor.
    /// </summary>
    /// <param name="sid">The target security identifier.</param>
    /// <returns>The formatted SID protection descriptor.</returns>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301</code>
    /// </example>
    internal static string FormatSid(SecurityIdentifier sid)
    {
        return FormatProvider(DpapiNgConstants.SidName, sid?.Value);
    }

    /// <summary>
    /// Formats an SDDL-based protection descriptor.
    /// </summary>
    /// <param name="sddl">The target SDDL string.</param>
    /// <returns>The formatted SDDL protection descriptor.</returns>
    /// <example>
    /// <code language="text">SDDL=O:S-1-5-5-0-290724G:SYD:(A;;CCDC;;;S-1-5-5-0-290724)(A;;DC;;;WD)</code>
    /// </example>
    internal static string FormatSddl(string sddl)
    {
        return FormatProvider(DpapiNgConstants.SddlName, sddl);
    }

    /// <summary>
    /// Formats a certificate hash protection descriptor.
    /// </summary>
    /// <param name="certificateHash">The certificate hash value.</param>
    /// <returns>The formatted certificate protection descriptor.</returns>
    /// <example>
    /// <code language="text">CERTIFICATE=HashID:5BD6F6AF8477755554026221BC81D1F3DAF5C4CD</code>
    /// </example>
    internal static string FormatCertificateHash(ReadOnlySpan<byte> certificateHash)
    {
        return FormatProvider(DpapiNgConstants.CertificateName, $"HashID:{certificateHash.ToHex(caps: true)}");
    }

    /// <summary>
    /// Rewrites the provider name of a formatted descriptor while preserving its provider attribute value.
    /// </summary>
    /// <param name="descriptor">The source protection descriptor.</param>
    /// <param name="sourceProviderName">The expected source provider name.</param>
    /// <param name="targetProviderName">The target provider name.</param>
    /// <returns>The rebound descriptor, or <see langword="null" /> when the source descriptor has a different provider.</returns>
    /// <example>
    /// <code language="text">DRACERTIFICATE=HashID:5BD6F6AF8477755554026221BC81D1F3DAF5C4CD</code>
    /// </example>
    internal static string? RebindProvider(string? descriptor, string sourceProviderName, string targetProviderName)
    {
        string sourcePrefix = $"{sourceProviderName}=";
        string targetPrefix = $"{targetProviderName}=";

        if (string.IsNullOrEmpty(descriptor) ||
            !descriptor.StartsWith(sourcePrefix, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return targetPrefix + descriptor.Substring(sourcePrefix.Length);
    }

    /// <summary>
    /// Escapes a provider attribute value for a protection descriptor rule string.
    /// </summary>
    /// <param name="value">The provider attribute value.</param>
    /// <returns>The escaped provider attribute value.</returns>
    /// <remarks>
    /// The documented grammar requires escaping values that would otherwise be ambiguous in a rule string,
    /// including a backslash, a leading number sign or space, a trailing space, and a null character.
    /// Provider-specific values such as SDDL are preserved in the same readable form shown in the Microsoft examples.
    /// </remarks>
    /// <example>
    /// <code language="text">LOCAL=machine</code>
    /// </example>
    internal static string EscapeValue(string? value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        var escaped = new StringBuilder(value.Length);

        for (int index = 0; index < value.Length; index++)
        {
            char character = value[index];
            bool isLeading = index == 0;
            bool isTrailing = index == value.Length - 1;

            // The Microsoft grammar uses backslash escaping for ambiguous descriptor-layer characters.
            if (character == '\0')
            {
                escaped.Append(@"\00");
            }
            else if (character == '\\' ||
                (character == '#' && isLeading) ||
                (character == ' ' && (isLeading || isTrailing)))
            {
                escaped.Append('\\');
                escaped.Append(character);
            }
            else
            {
                escaped.Append(character);
            }
        }

        return escaped.ToString();
    }
}
