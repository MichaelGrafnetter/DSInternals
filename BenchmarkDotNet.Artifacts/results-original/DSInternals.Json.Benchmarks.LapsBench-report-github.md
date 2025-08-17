```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4652/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 9.0.304
  [Host]     : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method           | Mean     | Error   | StdDev   | Gen0   | Allocated |
|----------------- |---------:|--------:|---------:|-------:|----------:|
| Laps_String      | 246.7 ns | 6.61 ns | 18.96 ns | 0.0124 |     208 B |
| Laps_Utf8_Bytes  | 259.3 ns | 6.84 ns | 20.16 ns | 0.0219 |     368 B |
| Laps_Utf16_Bytes | 281.1 ns | 8.17 ns | 24.08 ns | 0.0219 |     368 B |
