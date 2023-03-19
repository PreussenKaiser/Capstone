using Scheduler.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents an event held at the facility.
/// </summary>
public record Event : IValidatableObject
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
	/// The event's unique identifier.
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// The user who scheduled the event.
	/// References <see cref="User.Id"/>.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The event's name.
	/// </summary>
	[Required(ErrorMessage = "Please enter the event's name.")]
	[MaxLength(32)]
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

	/// <summary>
	/// Performs additional validation for the <see cref="Event"/>.
	/// <para>
	/// <b>Validation performed:</b>
	/// <list type="bullet">
	/// <item>If an <see cref="Event"/> already falls between <see cref="StartDate"/> and <see cref="EndDate"/>.</item>
	/// </list>
	/// </para>
	/// </summary>
	/// <param name="validationContext">The current validation context.</param>
	/// <returns>Validation results.</returns>
	/// <exception cref="NullReferenceException"/>
	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		ICollection<ValidationResult> results = new List<ValidationResult>();

		if (this.EndDate <= (this.StartDate + TimeSpan.FromMinutes(29)))
			results.Add(new("End Time must be at least 30 minutes after Start Time."));

		return results;
	}
}
