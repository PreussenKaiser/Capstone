namespace Scheduler.Options;

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
	public required byte Time { get; init; }
}
