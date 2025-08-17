```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4652/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 9.0.304
  [Host]     : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method            | Mean       | Error    | StdDev   | Gen0   | Gen1   | Allocated |
|------------------ |-----------:|---------:|---------:|-------:|-------:|----------:|
| STJ_String_Small  |   283.3 ns |  3.32 ns |  3.10 ns | 0.0448 |      - |     752 B |
| STJ_Bytes_Small   |   243.5 ns |  2.57 ns |  2.15 ns | 0.0448 |      - |     752 B |
| STJ_String_Medium |   503.9 ns |  4.85 ns |  4.05 ns | 0.2584 | 0.0038 |    4336 B |
| STJ_Bytes_Medium  |   410.1 ns |  7.92 ns |  8.13 ns | 0.2589 | 0.0038 |    4336 B |
| STJ_String_Large  | 3,994.9 ns | 38.03 ns | 35.57 ns | 3.9215 | 0.7782 |   65776 B |
| STJ_Bytes_Large   | 2,881.5 ns |  9.88 ns |  7.71 ns | 3.9215 | 0.7820 |   65776 B |
