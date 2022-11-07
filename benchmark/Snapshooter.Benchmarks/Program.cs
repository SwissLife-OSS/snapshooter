using BenchmarkDotNet.Running;
using Snapshooter.Benchmarks;

Base64ParserBenchmarks base64ParserBenchmarks = new Base64ParserBenchmarks();

BenchmarkRunner.Run<Base64ParserBenchmarks>();
