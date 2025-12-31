using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Common.Data;

/// <summary>
/// Converts the CustomKeyInformation class to and from a base 64 string value. 
/// </summary>
public class CustomKeyInformationConverter : JsonConverter<CustomKeyInformation>
{
    public override CustomKeyInformation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            try
            {
                byte[] blob = Convert.FromBase64String(reader.GetString());
                return new CustomKeyInformation(blob);
            }
            catch (Exception e)
            {
                throw new JsonException("Cannot convert invalid value to CustomKeyInformation.", e);
            }
        }

        throw new JsonException("Unexpected token parsing CustomKeyInformation.");
    }

    public override void Write(Utf8JsonWriter writer, CustomKeyInformation value, JsonSerializerOptions options)
    {
        if (value != null)
        {
            writer.WriteStringValue(Convert.ToBase64String(value.ToByteArray()));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
