using System.Management.Automation;
using System.Text;

namespace DSInternals.PowerShell;

/// <summary>
/// Converts well-known encoding names (such as <c>UTF8</c> or <c>ASCII</c>) into the corresponding
/// <see cref="Encoding" /> static instance, so a cmdlet parameter of type <see cref="Encoding" />
/// can be supplied either as a string or as an <see cref="Encoding" /> object.
/// The supported names mirror the static properties of <see cref="Encoding" />.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public sealed class EncodingTransformationAttribute : ArgumentTransformationAttribute
{
    private static readonly Dictionary<string, Encoding> NameToEncoding =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { EncodingArgumentCompleter.Ascii, Encoding.ASCII },
            { EncodingArgumentCompleter.BigEndianUnicode, Encoding.BigEndianUnicode },
            { EncodingArgumentCompleter.Unicode, Encoding.Unicode },
            { EncodingArgumentCompleter.Utf32, Encoding.UTF32 },
#pragma warning disable SYSLIB0001 // UTF-7 is kept for parity with the PowerShell encoding set.
            { EncodingArgumentCompleter.Utf7, Encoding.UTF7 },
#pragma warning restore SYSLIB0001
            { EncodingArgumentCompleter.Utf8, Encoding.UTF8 },
        };

    public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
    {
        string encodingName;

        // Unwrap PSObject and short-circuit when the caller already supplied an Encoding instance.
        if (inputData is PSObject psObject)
        {
            if (psObject.BaseObject is Encoding)
            {
                return psObject.BaseObject;
            }

            encodingName = psObject.BaseObject as string;
        }
        else if (inputData is Encoding)
        {
            return inputData;
        }
        else
        {
            encodingName = inputData as string;
        }

        if (encodingName == null)
        {
            // Let the parameter binder report a conversion error for unsupported types.
            return inputData;
        }

        if (NameToEncoding.TryGetValue(encodingName, out Encoding match))
        {
            return match;
        }

        throw new ArgumentTransformationMetadataException(
            $"Cannot convert value \"{encodingName}\" to type \"System.Text.Encoding\". " +
            $"Supported values are: {string.Join(", ", EncodingArgumentCompleter.EncodingNames)}.");
    }
}
