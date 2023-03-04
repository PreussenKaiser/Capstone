using Scheduler.Core.Models.Identity;
using Scheduler.Web.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// Submission data from <see cref="IdentityController.Update(User)"/> POST.
/// </summary>
public sealed record ManageUserViewModel
{
	/// <summary>
	/// The user's ID.
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// The user's email.
	/// </summary>
	[Required(ErrorMessage = "Please enter an email address.")]
	[MaxLength(256)]
	[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
	public string Email { get; init; } = string.Empty;

	/// <summary>
	/// The user's first name.
	/// </summary>
	[Required(ErrorMessage = "Please enter a first name.")]
	[MaxLength(32)]
	public string FirstName { get; init; } = string.Empty;

	/// <summary>
	/// The user's last name.
	/// </summary>
	[Required(ErrorMessage = "Please enter a last name.")]
	[MaxLength(32)]
	public string LastName { get; init; } = string.Empty;

	/// <summary>
	/// Indicates hether the user is an admin or not.
	/// </summary>
	public bool IsAdmin { get; init; }
}
