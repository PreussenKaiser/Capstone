namespace Scheduler.Domain.Models;

/// <remarks>Could add a type parameter for extra extendability, we only use GUID's right now though.</remarks>
public abstract record Entity
{
	public Entity()
	{
		this.Id = Guid.NewGuid();
	}

	public Guid Id { get; init; }
}
