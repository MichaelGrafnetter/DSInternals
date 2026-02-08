using System.Text.Json.Serialization;

namespace DSInternals.Common.Data;

[JsonSerializable(typeof(KeyMaterialFido))]
internal partial class KeyCredentialSerializationContext : JsonSerializerContext { }
