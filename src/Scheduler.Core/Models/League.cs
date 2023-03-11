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
	public Guid Id { get; init; }

	/// <summary>
	/// The league's name.
	/// </summary>
	[Required]
	[MaxLength(32)]
	public required string Name { get; init; }
}