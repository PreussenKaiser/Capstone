using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents an event held at the facility.
/// </summary>
public record Event : Entity
{
	/// <summary>
	/// Initializes the <see cref="Event"/> record.
	/// </summary>
	public Event()
	{
		this.Name = string.Empty;
		this.Fields = new List<Field>();
	}

	/// <summary>
	/// The user who scheduled the event.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The event's name.
	/// </summary>
	[Required]
	[MaxLength(32)]
	public string Name { get; set; }

	/// <summary>
	/// When the event starts.
	/// </summary>
	public DateTime StartDate { get; set; }

	/// <summary>
	/// When the event ends.
	/// </summary>
	public DateTime EndDate { get; set; }

	/// <summary>
	/// The event's reccurence pattern if it's recurring.
	/// </summary>
	public Recurrence? Recurrence { get; set; }

	/// <summary>
	/// Fields where the event is taking place.
	/// </summary>
	public List<Field> Fields { get; set; }

	public void ChangeName(string name)
	{
		this.Name = name;
	}

	public virtual void Relocate(params Field[] fields)
	{
		this.Fields.Clear();
		this.Fields.AddRange(fields);
	}
}
