using BenchmarkDotNet.Running;
using Scheduler.Benchmarks.Scheduling;

var result = BenchmarkRunner.Run<CollisionDetectionBenchmarks>();

// Set breakpoint to see result.
_ = result;