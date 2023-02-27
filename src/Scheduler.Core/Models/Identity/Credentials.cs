using System.ComponentModel.DataAnnotations;

namespace Scheduler.Core.Models.Identity;

/// <summary>
/// Property data and validation for <see cref="User"/> credentials.
/// </summary>
public sealed record Credentials
{
	/// <summary>
	/// The user's email.
	/// </summary>
	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	public string Email { get; init; } = string.Empty;

	/// <summary>
	/// The user's password.
	/// </summary>
	[Required(ErrorMessage = "Please enter a password.")]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[DataType(DataType.Password)]
	public string Password { get; init; } = string.Empty;

	[Required(ErrorMessage = "Please enter a first name.")]
	[MaxLength(32)]
	public string FirstName { get; init; } = string.Empty;

	[Required(ErrorMessage = "Please enter a last name.")]
	[MaxLength(32)]
	public string LastName { get; init; } = string.Empty;
}
