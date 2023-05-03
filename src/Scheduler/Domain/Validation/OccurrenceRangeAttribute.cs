using Scheduler.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Scheduler.Domain.Validation;

/// <summary>
/// Validates for if an <see cref="int"/> is falls within the recurrence pattern of a year.
/// </summary>
[AttributeUsage(
	AttributeTargets.Property |
	AttributeTargets.Field |
	AttributeTargets.Parameter)]
public sealed class OccurrenceRangeAttribute : ValidationAttribute
{
	/// <summary>
	/// The maximum amount of days in a year, accounting for leap years.
	/// </summary>
	private const ushort MAX_DAYS = 366;

	/// <summary>
	/// The maximum amount of weeks in a year.
	/// </summary>
	private const ushort MAX_WEEKS = 52;

	/// <summary>
	/// The maximum amount of months in a year.
	/// </summary>
	private const ushort MAX_MONTHS = 12;

	/// <summary>
	/// Initializes the <see cref="OccurrenceRangeAttribute"/> class.
	/// </summary>
	/// <param name="propertyName">The name of the properety which holds <see cref="RecurrenceType"/>.</param>
	/// <param name="type">Optinoal manual <see cref="RecurrenceType"/>.</param>
	public OccurrenceRangeAttribute(
		string propertyName,
		RecurrenceType type = default)
	{
		this.PropertyName = propertyName;
		this.Type = type;

		base.ErrorMessage = "Scheduled events can only occur up to a year.";
	}

	/// <summary>
	/// Gets the name of the property to check recurrence for.
	/// </summary>
	public string PropertyName { get; }

	/// <summary>
	/// Gets the recurrence type to constrain with.
	/// </summary>
	public RecurrenceType? Type { get; private set; }

	/// <inheritdoc/>
	protected override ValidationResult? IsValid(
		object? value, ValidationContext validationContext)
	{
		var type = validationContext?.ObjectType
			.GetRuntimeProperty(this.PropertyName)
			?.GetValue(validationContext.ObjectInstance) as RecurrenceType?;

		if (type is not null)
		{
			this.Type = type;
		}	

		int occurrences = value is int
			? (int)value
			: throw new ArgumentException("Value must be of type ushort.");

		bool inRange = occurrences >= 2 && this.Type switch
		{
			RecurrenceType.Daily => occurrences <= MAX_DAYS,
			RecurrenceType.Weekly => occurrences <= MAX_WEEKS,
			RecurrenceType.Monthly => occurrences <= MAX_MONTHS,
			_ => occurrences <= MAX_DAYS // Should never happen.
		};

		return inRange
			? ValidationResult.Success
			: new ValidationResult(base.ErrorMessage);
	}
}
