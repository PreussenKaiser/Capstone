using System.ComponentModel.DataAnnotations;
using Scheduler.Web.Controllers;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// Form data for <see cref="IdentityController.Register"/> POST.
/// </summary>
public sealed record RegisterViewModel
{
	/// <summary>
	/// The user's email.
	/// </summary>
	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	public required string Email { get; init; }

	/// <summary>
	/// User's first name.
	/// </summary>
	[Display(Name = "First Name")]
	[Required(ErrorMessage = "Please enter a first name.")]
	[MaxLength(32)]
	public required string FirstName { get; init; }

	/// <summary>
	/// User's last name.
	/// </summary>
	[Display(Name = "Last Name")]
	[Required(ErrorMessage = "Please enter a last name.")]
	[MaxLength(32)]
	public required string LastName { get; init; }

	/// <summary>
	/// Whether the user is an admin or not.
	/// </summary>
	[Display(Name = "Is Admin")]
	public bool IsAdmin { get; init; }
}
