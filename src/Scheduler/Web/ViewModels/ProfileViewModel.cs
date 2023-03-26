using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// The view model for the '/Identity/Profile' view.
/// </summary>
public sealed record ProfileViewModel
{
	/// <summary>
	/// The identifier of the user.
	/// </summary>
	public Guid UserId { get; init; }

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
	/// The user's email address.
	/// </summary>
	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	public required string Email { get; init; }

	/// <summary>
	/// Whether the user is an admin or not.
	/// </summary>
	[Display(Name = "Admin?")]
	public bool IsAdmin { get; init; }
}
