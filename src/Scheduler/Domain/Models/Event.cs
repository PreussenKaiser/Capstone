using Scheduler.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Models;

/// <summary>
/// Represents an event held at the facility.
/// </summary>
public record Event : Entity
{
	/// <summary>
	/// Initializes the <see cref="Event"/> record.
	/// </summary>
	public Event() : base()
	{
		this.Name = string.Empty;
		this.Fields = new List<Field>();
	}

	/// <summary>
	/// References the user who created the event.
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
	/// Whether the event takes up the entire facility or not.
	/// </summary>
	public bool IsBlackout { get; init; }

	/// <summary>
	/// Recurrence pattern for the event.
	/// </summary>
	public Recurrence? Recurrence { get; set; }

	/// <summary>
	/// Fields where the event happens.
	/// </summary>
	public List<Field> Fields { get; init; }

	/// <summary>
	/// Reschedules the event.
	/// </summary>
	/// <param name="startDate">New start date.</param>
	/// <param name="endDate">New end date.</param>
	/// <param name="recurrence">New recurrence pattern.</param>
	public void Reschedule(
		DateTime startDate,
		DateTime endDate,
		Recurrence? recurrence = null)
	{
		this.StartDate = startDate;
		this.EndDate = endDate;
		this.Recurrence = recurrence;
	}

	/// <summary>
	/// Relocates the event to different fields.
	/// </summary>
	/// <param name="fields">New fields.</param>
	public virtual void Relocate(params Field[] fields)
	{
		this.Fields.Clear();
		this.Fields.AddRange(fields);
	}
}
