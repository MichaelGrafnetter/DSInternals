using System.Text.Json;
using System.Text.Encodings.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSInternals.Common.Serialization;

namespace DSInternals.Common.Test
{
    internal static class JsonAssert
    {
        // Canonicalize both JSONs and compare
        public static void AreEqual(string expectedJson, string actualJson)
        {
            // Use DsiJson.DeserializeLenient to handle single-quoted JSON
            var expectedElement = DsiJson.DeserializeLenient<JsonElement>(expectedJson);
            var actualElement = DsiJson.DeserializeLenient<JsonElement>(actualJson);

            // Re-serialize with the same (relaxed) encoder so '+' is emitted on both sides
            var opts = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            var canonE = JsonSerializer.Serialize(expectedElement, opts);
            var canonA = JsonSerializer.Serialize(actualElement, opts);

            Assert.AreEqual(canonE, canonA, "JSON differs after canonicalization.");
        }
    }
}