using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// The view model for profile management.
/// </summary>
public sealed class ProfileViewModel
{
	/// <summary>
	/// The user being profiled.
	/// </summary>
	public required Guid UserId { get; init; }

	/// <summary>
	/// The user's first name.
	/// </summary>
	[Display(Name = "First name")]
	[Required(ErrorMessage = "Please enter your first name.")]
	[MaxLength(32, ErrorMessage = "First name must be below 32 characters in length.")]
	public required string FirstName { get; init; }

	/// <summary>
	/// The user's last name.
	/// </summary>
	[Display(Name = "Last name")]
	[Required(ErrorMessage = "Please enter your first name.")]
	[MaxLength(32, ErrorMessage = "Last name must be below 32 characters in length.")]
	public required string LastName { get; init; }

	/// <summary>
	/// The user's email.
	/// </summary>
	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	public required string Email { get; set; }

	/// <summary>
	/// Whether the user is an admin or not.
	/// </summary>
	public bool IsAdmin { get; init; }

}
