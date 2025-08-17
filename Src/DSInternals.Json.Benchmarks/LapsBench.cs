using System.Text;
using BenchmarkDotNet.Attributes;
using DSInternals.Common.Serialization;
using DSInternals.Common.Data;

namespace DSInternals.Json.Benchmarks
{
    [MemoryDiagnoser]
    public class LapsBench
    {
        private const string LapsJson = "{\"n\":\"Administrator\",\"p\":\"a998aoEUUXxO32\",\"t\":\"2025-06-01T08:00:00Z\"}";
        private byte[] _utf8;
        private byte[] _utf16le;
        private string _str;

        [GlobalSetup]
        public void Setup()
        {
            _str = LapsJson;
            _utf8 = Encoding.UTF8.GetBytes(LapsJson);
            _utf16le = Encoding.Unicode.GetBytes(LapsJson);
        }

        [Benchmark] public object Laps_String() => DsiJson.DeserializeLenient<LapsClearTextPassword>(_str)!;
        [Benchmark] public object Laps_Utf8_Bytes() => DsiJson.DeserializeLenient<LapsClearTextPassword>(_utf8, utf16: false)!;
        [Benchmark] public object Laps_Utf16_Bytes() => DsiJson.DeserializeLenient<LapsClearTextPassword>(_utf16le, utf16: true)!;
    }
}
