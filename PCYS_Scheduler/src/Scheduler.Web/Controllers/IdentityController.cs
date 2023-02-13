using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models;
using Scheduler.Web.ViewModels;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders ASP.NET Identity views.
/// </summary>
public sealed class IdentityController : Controller
{
	/// <summary>
	/// The API to access <see cref="User"/> login information with.
	/// </summary>
	private readonly SignInManager<User> signInManager;

	/// <summary>
	/// Initializes the <see cref="IdentityController"/> class.
	/// </summary>
	/// <param name="signInManager">The API to access <see cref="User"/> login information with.</param>
	public IdentityController(SignInManager<User> signInManager)
	{
		this.signInManager = signInManager;
	}

	/// <summary>
	/// Displays the <see cref="Login"/> view.
	/// </summary>
	/// <returns>The <see cref="Login"/> view.</returns>
	public async Task<IActionResult> Login()
	{
		// Clear the existing external cookie to ensure a clean login process.
		await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

		return this.View(new LoginViewModel { ReturnUrl = this.Url.Content("~/") });
	}

	/// <summary>
	/// Post action for the <see cref="Login"/> view.
	/// </summary>
	/// <param name="viewModel">Data supplied by form.</param>
	/// <returns>
	/// The url specified by <paramref name="viewModel"/> if successful.
	/// Redirected to <see cref="Login"/> if unsuccessful.
	/// </returns>
	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel viewModel)
	{
		if (!this.ModelState.IsValid)
			return this.View(viewModel);

		var result = await this.signInManager.PasswordSignInAsync(
			viewModel.Email,
			viewModel.Password,
			viewModel.RememberMe,
			lockoutOnFailure: false);
			
		if (!result.Succeeded)
		{
			this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");

			return this.View(viewModel);
		}

		return this.Redirect(viewModel.ReturnUrl);
	}

	/// <summary>
	/// Logs the user out of the application.
	/// </summary>
	/// <returns>Redirected to <see cref="HomeController.Index"/>.</returns>
	[HttpPost]
	public async Task<IActionResult> Logout()
	{
		await this.signInManager.SignOutAsync();

		return this.RedirectToAction(nameof(HomeController.Index), "Home");
	}

	public async Task<IActionResult> Register()
	{
		return this.View();
	}

	[HttpPost]
	public async Task<IActionResult> Register(RegisterViewModel viewModel)
	{
		return this.View();
	}
}
