```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4652/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 9.0.304
  [Host]     : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method            | Mean       | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|------------------ |-----------:|----------:|----------:|-------:|-------:|----------:|
| STJ_String_Small  |   287.1 ns |   3.48 ns |   3.25 ns | 0.0448 |      - |     752 B |
| STJ_Bytes_Small   |   247.2 ns |   2.79 ns |   2.18 ns | 0.0448 |      - |     752 B |
| STJ_String_Medium |   499.8 ns |   8.72 ns |   6.81 ns | 0.2584 | 0.0038 |    4336 B |
| STJ_Bytes_Medium  |   512.3 ns |  13.51 ns |  38.77 ns | 0.2589 | 0.0038 |    4336 B |
| STJ_String_Large  | 4,610.5 ns | 114.34 ns | 337.12 ns | 3.9215 | 0.7782 |   65776 B |
| STJ_Bytes_Large   | 3,421.0 ns |  72.74 ns | 208.71 ns | 3.9215 | 0.7820 |   65776 B |
