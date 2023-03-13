namespace Scheduler.Core.Models;

public abstract class ModelBase
{
	/// <summary>
	/// The model's unique identifier.
	/// </summary>
	public Guid Id { get; init; }
}
