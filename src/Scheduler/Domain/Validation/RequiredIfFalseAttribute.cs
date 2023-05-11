using System.ComponentModel.DataAnnotations;

namespace Scheduler.Domain.Validation;

/// <summary>
/// Checks if a seperate property is false before determining if the field is required.
/// </summary>
public sealed class RequiredIfFalseAttribute : RequiredAttribute
{
	private string PropertyName { get; set; }

	public RequiredIfFalseAttribute(string propertyName)
	{
		this.PropertyName = propertyName;
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext context)
	{
		object instance = context.ObjectInstance;
		Type type = instance.GetType();

		bool.TryParse(type.GetProperty(this.PropertyName)?.GetValue(instance)?.ToString(), out bool propertyValue);

		if (!propertyValue && string.IsNullOrWhiteSpace(value?.ToString()))
		{
			return new ValidationResult(base.ErrorMessage);
		}

		return ValidationResult.Success;
	}
}
