﻿using Scheduler.Domain.Validation;
using Scheduler.Infrastructure.Extensions;
using Scheduler.Infrastructure.Persistence;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduler.Domain.Models;

/// <summary>
/// Represents an event held at the facility.
/// </summary>
public record Event : Entity, IValidatableObject
{
	/// <summary>
	/// Initializes the <see cref="Event"/> record.
	/// </summary>
	public Event() : base()
	{
		this.Name = string.Empty;
		this.FieldIds = Array.Empty<Guid>();
		this.Fields = new List<Field>();
	}

	/// <summary>
	/// Fields referenced by the <see cref="Event"/>.
	/// </summary>
	[NotMapped]
	[Display(Name = "Fields")]
	[RequiredIfFalse("IsBlackout", ErrorMessage = "Please select at least one field.")]
	public Guid[] FieldIds { get; init; }

	/// <summary>
	/// References the user who created the <see cref="Event"/>.
	/// </summary>
	public Guid UserId { get; init; }

	/// <summary>
	/// The event's name.
	/// </summary>
	[Required(ErrorMessage = "Please enter the event's name.")]
	[MaxLength(32, ErrorMessage = "Name should be less than 32 characters long.")]
	public string Name { get; set; }

	/// <summary>
	/// When the <see cref="Event"/> starts.
	/// </summary>
	[Display(Name = "Start date")]
	[DisplayFormat(DataFormatString = "{0:M/dd/yyyy h:mm tt}")]
	[Required(ErrorMessage = "Please enter when the event begins.")]
	[RequireFuture(ErrorMessage = "Date must not be in the past.")]
	public DateTime StartDate { get; set; }

	/// <summary>
	/// When the <see cref="Event"/> ends.
	/// </summary>
	[Display(Name = "End date")]
	[DisplayFormat(DataFormatString = "{0:M/dd/yyyy h:mm tt}")]
	[Required(ErrorMessage = "Please enter when the event ends.")]
	public DateTime EndDate { get; set; }

	/// <summary>
	/// Whether the <see cref="Event"/> takes up the entire facility or not.
	/// </summary>
	[Display(Name = "Blackout facility?")]
	public bool IsBlackout { get; set; }

	/// <summary>
	/// Recurrence pattern for the <see cref="Event"/>.
	/// </summary>
	public Recurrence? Recurrence { get; set; }

	/// <summary>
	/// Fields where the <see cref="Event"/> happens.
	/// </summary>
	public List<Field> Fields { get; init; }

	/// <summary>
	/// Reschedules the <see cref="Event"/>.
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

	/// <summary>
	/// 
	/// </summary>
	/// <param name="validationContext"></param>
	/// <returns></returns>
	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		var context = validationContext.GetRequiredService<SchedulerContext>();
		ICollection<ValidationResult> results = new List<ValidationResult>();

		if (this.EndDate <= (this.StartDate + TimeSpan.FromMinutes(29)))
		{
			results.Add(new("End time must be at least 30 minutes after start time."));
		}

		this.Relocate(context.Fields
			.Where(f => this.FieldIds.Contains(f.Id))
			.ToArray());

		Event? conflict = this
			?.FindConflict(context.Events
			.WithScheduling()
			.ToList());

		if (conflict is not null)
		{
			results.Add(new("An event is already scheduled for that date"));
		}

		return results;
	}
}