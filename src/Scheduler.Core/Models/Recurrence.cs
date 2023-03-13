using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a pattern of recurrence.
/// </summary>
public sealed class Recurrence
{
	/// <summary>
	/// The pattern's unique identifier.
	/// References <see cref="Event.Id"/>.
	/// </summary>
	public Guid Id { get; init; }

	/// <summary>
	/// How many times the pattern occurs.
	/// </summary>
	[Range(1, byte.MaxValue, ErrorMessage = "There must be at least one occurrence.")]
	public byte Occurrences { get; init; }

	/// <summary>
	/// The pattern's type.
	/// </summary>
	[Display(Name = "Repeats")]
	public RecurrenceType Type { get; init; }

	/// <summary>
	/// The <see cref="Event"/> with recurrence.
	/// </summary>
	public Event? Event { get; set; }
}

/// <summary>
/// Types of recurrence.
/// </summary>
public enum RecurrenceType : byte
{
	Daily,
	Weekly,
	Monthly,
	Yearly
}