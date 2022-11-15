using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Mathematics;

namespace Snapshooter.Benchmarks.DirectoryName;

[RPlotExporter, MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn(NumeralSystem.Arabic)]
public class DirectoryNameResolverBenchmarks
{
    private static string absoluteFileNameBackSlash = "C:\\temp\\user\\snapshot\\testing\\snapshotfile.snap";
    private static string absoluteFileNameSlash = "C:/temp/user/snapshot/testing/snapshotfile.snap";

    private static readonly DirectoryNameResolver _directoryNameResolver = new DirectoryNameResolver();

    [Benchmark(Baseline = true)]
    public void GetDirectoryName_PathString()
    {
        _directoryNameResolver.GetDirectoryName_PathString(absoluteFileNameBackSlash);
    }

    [Benchmark]
    public void GetDirectoryName_PathAsSpan()
    {
        _directoryNameResolver.GetDirectoryName_PathAsSpan(absoluteFileNameBackSlash);
    }

    [Benchmark]
    public void GetDirectoryName_Split()
    {
        _directoryNameResolver.GetDirectoryName_Split(absoluteFileNameBackSlash);
    }    

    [Benchmark]
    public void GetDirectoryName_IndexOfString()
    {
        _directoryNameResolver.GetDirectoryName_IndexOfString(absoluteFileNameBackSlash);
    }

    [Benchmark]
    public void GetDirectoryName_IndexOfSpan()
    {
        _directoryNameResolver.GetDirectoryName_IndexOfSpan(absoluteFileNameBackSlash);
    }

    [Benchmark]
    public void GetDirectoryName_IndexOfSpanToString()
    {
        _directoryNameResolver.GetDirectoryName_IndexOfSpanToString(absoluteFileNameBackSlash);
    }

    [Benchmark]
    public void GetDirectoryName_PathString_Slash()
    {
        _directoryNameResolver.GetDirectoryName_PathString(absoluteFileNameSlash);
    }

    [Benchmark]
    public void GetDirectoryName_IndexOfString_Slash()
    {
        _directoryNameResolver.GetDirectoryName_IndexOfString_Slash(absoluteFileNameSlash);
    }

    [Benchmark]
    public void GetDirectoryName_IndexOfString_BackSlash()
    {
        _directoryNameResolver.GetDirectoryName_IndexOfString_Slash(absoluteFileNameBackSlash);
    }
}

