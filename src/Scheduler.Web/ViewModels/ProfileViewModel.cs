using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// The view model for profile management.
/// </summary>
public sealed class ProfileViewModel
{
	/// <summary>
	/// The user's username.
	/// </summary>
	[Display(Name = "Username")]
	[Required(ErrorMessage = "Please enter a username.")]
	[MaxLength(256)]
	public required string UserName { get; set; }

	/// <summary>
	/// The user's email.
	/// </summary>
	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	public required string Email { get; set; }
}
