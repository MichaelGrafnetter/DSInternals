```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4652/24H2/2024Update/HudsonValley)
Unknown processor
.NET SDK 9.0.304
  [Host]     : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method            | Mean        | Error     | StdDev      | Median      | Gen0    | Gen1    | Gen2    | Allocated |
|------------------ |------------:|----------:|------------:|------------:|--------:|--------:|--------:|----------:|
| STJ_String_Small  |    279.8 ns |   4.15 ns |     3.88 ns |    279.0 ns |  0.0448 |       - |       - |     752 B |
| STJ_Bytes_Small   |    342.9 ns |   6.37 ns |     5.96 ns |    342.5 ns |  0.1063 |  0.0005 |       - |    1784 B |
| STJ_String_Medium |    631.9 ns |  28.32 ns |    83.50 ns |    649.3 ns |  0.2584 |  0.0038 |       - |    4336 B |
| STJ_Bytes_Medium  |    777.3 ns |  16.94 ns |    48.33 ns |    763.0 ns |  0.7496 |  0.0334 |       - |   12536 B |
| STJ_String_Large  |  4,043.0 ns |  66.74 ns |    52.11 ns |  4,041.0 ns |  3.9215 |  0.7782 |       - |   65776 B |
| STJ_Bytes_Large   | 36,013.7 ns | 743.78 ns | 2,193.05 ns | 35,435.2 ns | 41.6565 | 41.6565 | 41.6565 |  196884 B |
