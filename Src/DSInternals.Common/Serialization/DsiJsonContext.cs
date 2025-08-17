using System.Text.Json.Serialization;

namespace DSInternals.Common.Serialization
{
    // These options mirror ReaderOptions: case-insensitive, string enums, etc.
    [JsonSourceGenerationOptions(
        PropertyNameCaseInsensitive = true,
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        UseStringEnumConverter = true
    )]
    [JsonSerializable(typeof(DSInternals.Common.Data.LapsClearTextPassword))]
    [JsonSerializable(typeof(DSInternals.Common.Data.KeyCredential))]
    [JsonSerializable(typeof(DSInternals.Common.Data.KeyMaterialFido))]
    internal partial class DsiJsonContext : JsonSerializerContext { }
}