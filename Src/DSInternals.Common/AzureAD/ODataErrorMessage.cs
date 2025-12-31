using System.Text.Json.Serialization;

namespace DSInternals.Common.AzureAD;

public class ODataErrorMessage
{
    [JsonPropertyName("lang")]
    public string Language
    {
        get;
        private set;
    }

    [JsonPropertyName("value")]
    [JsonRequired]
    public string Value
    {
        get;
        private set;
    }
}
