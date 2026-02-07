using System.Text.Json.Serialization;

namespace DSInternals.Common.Data;

[JsonSerializable(typeof(LapsClearTextPassword))]
internal partial class LapsSerializationContext : JsonSerializerContext { }
