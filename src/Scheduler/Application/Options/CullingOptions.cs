using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Options;

/// <summary>
/// Contains options for culling events.
/// </summary>
public sealed class CullingOptions
{
	/// <summary>
	/// The name of the options hierarchy.
	/// </summary>
	public const string Culling = "Culling";

	/// <summary>
	/// Gets the time of culling, represented as a digit in the 24-hour format.
	/// </summary>
	[Range(1, 24)]
	public required byte Time { get; init; }
}
