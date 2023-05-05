using Scheduler.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Models;

/// <summary>
/// Represents a pattern of recurrence.
/// </summary>
public sealed class Recurrence : Entity
{
	/// <summary>
	/// Events with he recurrence pattern.
	/// </summary>
	private readonly List<Event> events;

	/// <summary>
	/// Initializes the <see cref="Recurrence"/> record.
	/// </summary>
	public Recurrence() : base()
	{
		this.events = new List<Event>();
	}

	/// <summary>
	/// How many times the pattern occurs.
	/// </summary>
	[OccurrenceRange(nameof(Type))]
	public int Occurrences { get; init; }

	/// <summary>
	/// The pattern's type.
	/// </summary>
	[Display(Name = "Repeats")]
	public RecurrenceType Type { get; init; }

	/// <summary>
	/// The events with recurrence.
	/// </summary>
	public IReadOnlyCollection<Event> Events
		=> this.events.AsReadOnly();
}