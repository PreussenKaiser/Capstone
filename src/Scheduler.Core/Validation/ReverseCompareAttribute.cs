using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Scheduler.Core.Validation;

/// <summary>
/// Similar to <see cref="CompareAttribute"/> but will fail if the applied property and <see cref="otherProperty"/> are equal.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ReverseCompareAttribute : ValidationAttribute
{
	/// <summary>
	/// The property to validate against.
	/// </summary>
	public required string OtherProperty { get; init; }

	/// <inheritdoc />
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		object? otherPropertyValue = validationContext.ObjectType
			.GetRuntimeProperty(this.OtherProperty)
			?.GetValue(validationContext.ObjectInstance, null);

		if (Equals(value, otherPropertyValue))
			return new(this.ErrorMessage);

		return null;
	}
}
