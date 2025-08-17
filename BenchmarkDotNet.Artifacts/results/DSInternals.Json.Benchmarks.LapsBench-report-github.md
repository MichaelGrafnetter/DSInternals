```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4652/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 9.0.304
  [Host]     : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method           | Mean     | Error    | StdDev   | Gen0   | Allocated |
|----------------- |---------:|---------:|---------:|-------:|----------:|
| Laps_String      | 237.1 ns |  6.93 ns | 20.10 ns | 0.0124 |     208 B |
| Laps_Utf8_Bytes  | 201.1 ns |  5.81 ns | 17.03 ns | 0.0124 |     208 B |
| Laps_Utf16_Bytes | 341.4 ns | 21.83 ns | 64.37 ns | 0.0319 |     536 B |
