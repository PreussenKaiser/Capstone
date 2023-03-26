namespace Scheduler.Domain.Models;

///	<summary>
///	Represents a data model.
///	</summary>
/// <remarks>Could add a type parameter for extra extendability, we only use GUID's right now though.</remarks>
public abstract record Entity
{
	/// <summary>
	/// Initializes the <see cref="Entity"/> record.
	/// </summary>
	public Entity()
	{
		this.Id = Guid.NewGuid();
	}

	/// <summary>
	/// The entity's unique identifier.
	/// </summary>
	public Guid Id { get; init; }
}
