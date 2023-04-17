using BenchmarkDotNet.Attributes;
using Scheduler.Domain.Models;
using Scheduler.Domain.Utility;
using Scheduler.Domain.Validation;

namespace Scheduler.Benchmarks.Scheduling;

/// <summary>
/// Contains benchmarks for conflict detection.
/// </summary>
public sealed class CollisionDetectionBenchmarks
{
	/// <summary>
	/// The list of events to detect collisions against.
	/// </summary>
	private readonly IEnumerable<Event> events;

	/// <summary>
	/// The conflicting event.
	/// </summary>
	private readonly Event scheduledEvent;

	/// <summary>
	/// Initializes the <see cref="CollisionDetectionBenchmarks"/> class.
	/// </summary>
	public CollisionDetectionBenchmarks()
	{
		this.events = SeedData.Events;
		this.scheduledEvent = new()
		{
			StartDate = new DateTime(2023, 3, 15, 17, 0, 0),
			EndDate = new DateTime(2023, 3, 15, 20, 0, 0)
		};
	}

	/// <summary>
	/// Previous implementation of collision detection.
	/// </summary>
	[Benchmark]
	public Event? Old()
	{
		return this.scheduledEvent.FindConflict(this.events);
	}

	/// <summary>
	/// New version of collision detection.
	/// </summary>
	[Benchmark]
	public void New()
	{
	}
}
