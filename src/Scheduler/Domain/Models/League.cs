using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Models;

/// <summary>
/// Represents a PCYS league.
/// </summary>
public sealed class League : Entity
{
	/// <summary>
	/// Initializes the <see cref="League"/> record.
	/// </summary>
	public League(): base()
	{
	}

	/// <summary>
	/// The league's name.
	/// </summary>
	[Required]
	[MaxLength(32)]
	public required string Name { get; init; }
}