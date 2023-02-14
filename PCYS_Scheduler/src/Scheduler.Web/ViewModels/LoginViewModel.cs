using Scheduler.Core.Models.Identity;
using Scheduler.Web.Controllers;

namespace Scheduler.Web.ViewModels;

/// <summary>
/// Submission data from <see cref="IdentityController.Login"/> POST.
/// </summary>
public sealed record LoginViewModel
{
	/// <summary>
	/// User credentials.
	/// </summary>
	public Credentials Credentials { get; init; }

	/// <summary>
	/// Whether to presist the user or not.
	/// </summary>
	public bool RememberMe { get; init; }
}
