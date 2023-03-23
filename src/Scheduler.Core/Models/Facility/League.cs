using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a PCYS league.
/// </summary>
public sealed record League : Entity
{
	/// <summary>
	/// The league's name.
	/// </summary>
	[Required]
	[MaxLength(32)]
	public required string Name { get; init; }
}