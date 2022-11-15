using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Mathematics;

namespace Snapshooter.Benchmarks.Base64;

[RPlotExporter, MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn(NumeralSystem.Arabic)]
public class Base64ParserBenchmarks
{
    private static string SmallBase64String = "VGhpcyBpcyBhIHNtYWxsIGJhc2UgNjQgc3RyaW5nIGZvciBiZW5jaG1hcmsgdGVzdGluZw==";
    private static string SmallNoneBase64String = "Thisisasmallbase64stringforbenchmarktesting";

    private static readonly Base64Parser _base64Parser = new Base64Parser();

    [Benchmark(Baseline = true)]
    public void ClassicSmallNoneBase64Parse()
    {
        _base64Parser.IsBase64FromBase64String(SmallNoneBase64String);
    }

    [Benchmark]
    public void ClassicSmallBase64Parse()
    {
        _base64Parser.IsBase64FromBase64String(SmallBase64String);
    }

    [Benchmark]
    public void NewSmallNoneBase64Parse()
    {
        _base64Parser.IsBase64TryFromBase64String(SmallNoneBase64String);
    }

    [Benchmark]
    public void NewSmallBase64Parse()
    {
        _base64Parser.IsBase64TryFromBase64String(SmallBase64String);
    }
}

