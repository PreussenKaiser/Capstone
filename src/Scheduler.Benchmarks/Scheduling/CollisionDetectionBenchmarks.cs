using BenchmarkDotNet.Attributes;
using Scheduler.Domain.Models;
using Scheduler.Domain.Utility;
using Scheduler.Domain.Validation;

namespace Scheduler.Benchmarks.Scheduling;

/// <summary>
/// Contains benchmarks for conflict detection.
/// </summary>
public class CollisionDetectionBenchmarks
{
	/// <summary>
	/// The list of events to detect collisions against.
	/// </summary>
	private readonly Event[] events;

	/// <summary>
	/// The conflicting event.
	/// </summary>
	private readonly Event scheduledEvent;

	/// <summary>
	/// Initializes the <see cref="CollisionDetectionBenchmarks"/> class.
	/// </summary>
	public CollisionDetectionBenchmarks()
	{
		this.events = SeedData.Events
			.OrderBy(e => e.StartDate)
			.ToArray();

		this.scheduledEvent = this.events.Last();
	}

	/// <summary>
	/// Previous implementation of collision detection.
	/// </summary>
	[Benchmark]
	public void Old()
	{
		// Type or member is obsolete, used here for benchmarking.
#pragma warning disable CS0618
		this.scheduledEvent.FindConflictOld(this.events);
#pragma warning restore CS0618
	}

	/// <summary>
	/// New version of collision detection.
	/// </summary>
	[Benchmark]
	public void New()
	{
		this.scheduledEvent.FindConflict(this.events);
	}
}
