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
	/// The other property's name.
	/// </summary>
	private readonly string otherProperty;

	/// <summary>
	/// Initializes the <see cref="ReverseCompareAttribute"/> class.
	/// </summary>
	/// <param name="otherProperty">The other property's name.</param>
	public ReverseCompareAttribute(string otherProperty)
	{
		this.otherProperty = otherProperty;
	}

	/// <summary>
	/// Determines if the applied property and <see cref="otherProperty"/> are the same.
	/// </summary>
	/// <param name="value">The applied property's valie.</param>
	/// <param name="validationContext">Current validation context.</param>
	/// <returns>
	/// <see cref="ValidationResult"/> with supplied error message if invalid.
	/// <see langword="null"/> if valid.
	/// </returns>
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		object? otherPropertyValue = validationContext.ObjectType
			.GetRuntimeProperty(this.otherProperty)
			?.GetValue(validationContext.ObjectInstance, null);

		if (Equals(value, otherPropertyValue))
			return new(this.ErrorMessage);

		return null;
	}
}
