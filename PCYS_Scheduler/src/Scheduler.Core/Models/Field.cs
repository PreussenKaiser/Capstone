namespace Scheduler.Core.Models;

/// <summary>
/// Represents a field in the facility.
/// </summary>
public sealed class Field
{
	/// <summary>
	/// Initializes the <see cref="Field"/> class.
	/// </summary>
	public Field()
	{
		this.Name = string.Empty;
	}

	/// <summary>
	/// The field's unique identifier.
	/// </summary>
	public Guid Id { get; init; }

	/// <summary>
	/// The field's name.
	/// </summary>
	public string Name { get; set; }
}
