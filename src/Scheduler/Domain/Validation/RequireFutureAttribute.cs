using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Validation;

/// <summary>
/// Constrains a <see cref="DateTime"/> to only occur past <see cref="DateTime.Now"/>.
/// </summary>
[AttributeUsage(
	AttributeTargets.Property |
	AttributeTargets.Field |
	AttributeTargets.Parameter)]
public sealed class RequireFutureAttribute : ValidationAttribute
{
	/// <inheritdoc />
	protected override ValidationResult? IsValid(
		object? value, ValidationContext validationContext)
	{
		if (value is not DateTime time)
		{
			return new("Unsupported date format.");
		}

		return time > DateTime.Now
			? null
			: new(this.ErrorMessage);
	}
}
