using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a PCYS league
/// </summary>
public sealed class League
{
	/// <summary>
	/// The league's unique id.
	/// </summary>
	public required Guid Id { get; init; }

	/// <summary>
	/// The league's name.
	/// </summary>
	[Required]
	[MaxLength(32)]
	public required string Name { get; set; }

	/// <summary>
	/// A value that indicates whether or not the league is currently active.
	/// </summary>
	public required bool IsArchived { get; set; }
}