using System.Text;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using DSInternals.Common.Serialization;

namespace DSInternals.Json.Benchmarks
{
    [MemoryDiagnoser]
    public class JsonDecodeBench
    {
        private byte[] _utf8Small, _utf8Medium, _utf8Large;
        private string _utf8SmallString, _utf8MediumString, _utf8LargeString;

        [GlobalSetup]
        public void Setup()
        {
            _utf8SmallString = MakeJson(512);     // ~0.5 KB
            _utf8MediumString = MakeJson(4096);   // ~4 KB
            _utf8LargeString = MakeJson(65536);   // ~64 KB

            _utf8Small  = Encoding.UTF8.GetBytes(_utf8SmallString);
            _utf8Medium = Encoding.UTF8.GetBytes(_utf8MediumString);
            _utf8Large  = Encoding.UTF8.GetBytes(_utf8LargeString);
        }

        private static string MakeJson(int approxSize)
        {
            var sb = new StringBuilder(approxSize + 64);
            sb.Append('{');
            sb.Append("\"n\":\"Administrator\",\"t\":\"2024-05-07T12:34:56Z\",\"p\":\"");
            sb.Append(new string('A', approxSize - 64));
            sb.Append("\"}");
            return sb.ToString();
        }

        [Benchmark] public object STJ_String_Small()  => JsonSerializer.Deserialize<JsonElement>(_utf8SmallString, DsiJson.Options);
        [Benchmark] public object STJ_Bytes_Small()   => DsiJson.DeserializeLenient<JsonElement>(_utf8Small, utf16: false)!;

        [Benchmark] public object STJ_String_Medium() => JsonSerializer.Deserialize<JsonElement>(_utf8MediumString, DsiJson.Options);
        [Benchmark] public object STJ_Bytes_Medium()  => DsiJson.DeserializeLenient<JsonElement>(_utf8Medium, utf16: false)!;

        [Benchmark] public object STJ_String_Large()  => JsonSerializer.Deserialize<JsonElement>(_utf8LargeString, DsiJson.Options);
        [Benchmark] public object STJ_Bytes_Large()   => DsiJson.DeserializeLenient<JsonElement>(_utf8Large, utf16: false)!;
    }
}
