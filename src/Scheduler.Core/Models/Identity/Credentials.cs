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
	[Display(Name = "Username")]	
	[Required(ErrorMessage = "Please enter your user.")]
	[MaxLength(256)]
	public string UserName { get; init; } = string.Empty;

	/// <summary>
	/// The user's password.
	/// </summary>
	[Required(ErrorMessage = "Please enter a password.")]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[DataType(DataType.Password)]
	public string Password { get; init; } = string.Empty;
}
