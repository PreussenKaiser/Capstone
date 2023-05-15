using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Validation;

/// <summary>
/// Checks if a seperate property is false before determining if the field is required.
/// </summary>
public sealed class RequiredIfFalseAttribute : RequiredAttribute
{
	/// <summary>
	/// Initializes <see cref="RequiredIfFalseAttribute"/> with property to check.
	/// </summary>
	/// <param name="propertyName">The property to check.</param>
	public RequiredIfFalseAttribute(string propertyName)
	{
		this.PropertyName = propertyName;
	}

	/// <summary>
	/// The property to check.
	/// </summary>
	private string PropertyName { get; }

	/// <inheritdoc/>
	protected override ValidationResult? IsValid(object? value, ValidationContext context)
	{
		object instance = context.ObjectInstance;
		Type type = instance.GetType();

		bool propertyValue = type.GetProperty(this.PropertyName)?.GetValue(instance) as bool?
			?? throw new ArgumentException("Property must be a bool.");

		return !propertyValue && value is null
			? new ValidationResult(base.ErrorMessage)
			: ValidationResult.Success;
	}
}
