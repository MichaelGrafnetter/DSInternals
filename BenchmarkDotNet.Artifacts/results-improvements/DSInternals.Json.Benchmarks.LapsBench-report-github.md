```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4652/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 9.0.304
  [Host]     : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method           | Mean     | Error   | StdDev   | Gen0   | Allocated |
|----------------- |---------:|--------:|---------:|-------:|----------:|
| Laps_String      | 222.4 ns | 5.39 ns | 15.63 ns | 0.0124 |     208 B |
| Laps_Utf8_Bytes  | 195.4 ns | 5.11 ns | 15.05 ns | 0.0124 |     208 B |
| Laps_Utf16_Bytes | 318.1 ns | 7.64 ns | 22.16 ns | 0.0381 |     640 B |
