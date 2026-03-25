using System.Collections;
using System.Text.Json.Serialization;

namespace DSInternals.Common.Data;

[JsonSerializable(typeof(KeyMaterialFido))]
[JsonSerializable(typeof(Hashtable))]
internal partial class KeyCredentialSerializationContext : JsonSerializerContext { }
