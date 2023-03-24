using Scheduler.Core.Validation;
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
	public Guid UserId { get; init; }

	/// <summary>
	/// The event's name.
	/// </summary>
	[Required(ErrorMessage = "Please enter the event's name.")]
	[MaxLength(32, ErrorMessage = "Name should be less than 32 characters long.")]
	public string Name { get; set; }

	/// <summary>
	/// When the event starts.
	/// </summary>
	[Display(Name = "Start date")]
	[DisplayFormat(DataFormatString = "{0:M/dd/yyyy h:mm tt}")]
	[Required(ErrorMessage = "Please enter when the event begins.")]
	[RequireFuture(ErrorMessage = "Date must not be in the past.")]
	public DateTime StartDate { get; set; }

	/// <summary>
	/// When the event ends.
	/// </summary>
	[Display(Name = "End date")]
	[DisplayFormat(DataFormatString = "{0:M/dd/yyyy h:mm tt}")]
	[Required(ErrorMessage = "Please enter when the event ends.")]
	public DateTime EndDate { get; set; }

	/// <summary>
	/// The event's reccurence pattern if it's recurring.
	/// </summary>
	public Recurrence? Recurrence { get; set; }

	/// <summary>
	/// Fields where the event is taking place.
	/// </summary>
	public List<Field> Fields { get; init; }

	public void ChangeName(string name)
	{
		this.Name = name;
	}

	public void Reschedule(
		DateTime startDate,
		DateTime endDate,
		Recurrence? recurrence = null)
	{
		this.StartDate = startDate;
		this.EndDate = endDate;
		this.Recurrence = recurrence;
	}

	public virtual void Relocate(params Field[] fields)
	{
		this.Fields.Clear();
		this.Fields.AddRange(fields);
	}
}
