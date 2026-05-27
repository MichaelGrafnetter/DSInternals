using System.Collections;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace DSInternals.PowerShell;

/// <summary>
/// Provides tab completion for cmdlet parameters of type <see cref="System.Text.Encoding" />.
/// The suggested values mirror the static properties of <see cref="System.Text.Encoding" />
/// that the matching <see cref="EncodingTransformationAttribute" /> knows how to resolve.
/// </summary>
public sealed class EncodingArgumentCompleter : IArgumentCompleter
{
    internal const string Ascii = "ASCII";
    internal const string BigEndianUnicode = "BigEndianUnicode";
    internal const string Unicode = "Unicode";
    internal const string Utf32 = "UTF32";
    internal const string Utf7 = "UTF7";
    internal const string Utf8 = "UTF8";

    /// <summary>
    /// The set of encoding names suggested by tab completion. Must stay in sync with
    /// <see cref="EncodingTransformationAttribute" />.
    /// </summary>
    internal static readonly string[] EncodingNames =
    {
        Ascii,
        BigEndianUnicode,
        Unicode,
        Utf32,
        Utf7,
        Utf8,
    };

    public IEnumerable<CompletionResult> CompleteArgument(
        string commandName,
        string parameterName,
        string wordToComplete,
        CommandAst commandAst,
        IDictionary fakeBoundParameters)
    {
        var pattern = WildcardPattern.Get((wordToComplete ?? string.Empty) + "*", WildcardOptions.IgnoreCase);

        foreach (string name in EncodingNames)
        {
            if (pattern.IsMatch(name))
            {
                yield return new CompletionResult(name, name, CompletionResultType.ParameterValue, name);
            }
        }
    }
}
