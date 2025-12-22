using BenchmarkDotNet.Running;
using Snapshooter.Benchmarks.Base64;
using Snapshooter.Benchmarks.DirectoryName;

BenchmarkRunner.Run<Base64ParserBenchmarks>();
BenchmarkRunner.Run<DirectoryNameResolverBenchmarks>();
