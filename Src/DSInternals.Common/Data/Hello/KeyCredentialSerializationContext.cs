using System.Collections;
using System.Text.Json.Serialization;

namespace DSInternals.Common.Data;

[JsonSerializable(typeof(KeyMaterialFido))]
[JsonSerializable(typeof(Hashtable))]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(byte[]))]
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(uint))]
[JsonSerializable(typeof(string))]
internal partial class KeyCredentialSerializationContext : JsonSerializerContext { }
