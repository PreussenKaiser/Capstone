using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Models.Identity;
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
	public IActionResult Login()
		=> this.View();

	/// <summary>
	/// Post action for the <see cref="Login"/> view.
	/// </summary>
	/// <param name="viewModel">Data supplied by form.</param>
	/// <returns>
	/// The url specified by <paramref name="viewModel"/> if successful.
	/// Redirected to <see cref="Login"/> if unsuccessful.
	/// </returns>
	[HttpPost]
	public async ValueTask<IActionResult> Login(LoginViewModel viewModel)
	{
		if (!this.ModelState.IsValid)
			return this.View(viewModel);

		var result = await this.signInManager.PasswordSignInAsync(
			viewModel.Credentials.UserName,
			viewModel.Credentials.Password,
			viewModel.RememberMe,
			lockoutOnFailure: false);
			
		if (!result.Succeeded)
		{
			this.ModelState.AddModelError(string.Empty, "Incorrect credentials, please try again.");

			return this.View(viewModel);
		}

		return this.RedirectToAction(nameof(AdminController.Index), "Admin");
	}

	/// <summary>
	/// Logs the user out of the application.
	/// </summary>
	/// <returns>Redirected to <see cref="HomeController.Index"/>.</returns>
	[HttpPost]
	[Authorize]	
	public async Task<IActionResult> Logout()
	{
		await this.signInManager.SignOutAsync();

		return this.RedirectToAction(nameof(HomeController.Index), "Home");
	}

	/// <summary>
	/// Displays the <see cref="Register"/> view.
	/// </summary>
	/// <returns>The <see cref="Register"/> view.</returns>
	public IActionResult Register()
		=> this.View();

	/// <summary>
	/// Handles form submission from <see cref="Register"/>.
	/// </summary>
	/// <param name="viewModel">View data.</param>
	/// <returns>
	/// If successful, redirected to <see cref="HomeController.Index"/>.
	/// Otherwise, redirected to <see cref="Register"/>.
	/// </returns>
	[HttpPost]
	public async ValueTask<IActionResult> Register(RegisterViewModel viewModel)
	{
		if (!this.ModelState.IsValid)
			return this.View(viewModel);

		User user = new() { UserName = viewModel.Credentials.UserName };

		IdentityResult result = await this.signInManager.UserManager
			.CreateAsync(user, viewModel.Credentials.Password);

		if (!result.Succeeded)
		{
			foreach (var e in result.Errors)
				this.ModelState.AddModelError(string.Empty, e.Description);

			return this.View(viewModel);
		}

		await this.signInManager.SignInAsync(user, isPersistent: false);

		return this.RedirectToAction(nameof(HomeController.Index), "Home");
	}

	/// <summary>
	/// Displays the <see cref="Profile"/> view.
	/// </summary>
	/// <returns>A form which posts to <see cref="Profile(ProfileViewModel)"/>.</returns>
	[Authorize]
	public async Task<IActionResult> Profile()
	{
		User? user = await this.signInManager.UserManager.GetUserAsync(this.User);

		if (user is null)
			return this.Problem();

		ProfileViewModel viewModel = new()
		{
			UserName = user.UserName ?? string.Empty,
			Email = user.Email ?? string.Empty
		};

		return this.View(viewModel);
	}

	/// <summary>
	/// Handles POST from <see cref="Profile"/>.
	/// </summary>
	/// <param name="viewModel">Values from the form.</param>
	/// <returns>Redirected to <see cref="Profile"/>.</returns>
	[HttpPost]
	[Authorize]
	public async ValueTask<IActionResult> Profile(ProfileViewModel viewModel)
	{
		if (this.ModelState.IsValid)
		{
			User? user = await this.signInManager.UserManager.GetUserAsync(this.User);

			if (user is null)
				return this.Problem();

			user.UserName = viewModel.UserName;
			user.Email = viewModel.Email;

			var result = await this.signInManager.UserManager.UpdateAsync(user);

			if (!result.Succeeded)
				foreach (var error in result.Errors)
					this.ModelState.AddModelError(string.Empty, error.Description);
		}

		return this.View(viewModel);
	}

	/// <summary>
	/// Displays the <see cref="Security"/> view.
	/// </summary>
	/// <returns>A form which POSTs to <see cref="Security"/>.</returns>
	[Authorize]	
	public IActionResult Security()
	{
		return this.View();
	}

	/// <summary>
	/// Handles POST from <see cref="Security"/>.
	/// </summary>
	/// <param name="viewModel">Form values.</param>
	/// <returns>Redirected to <see cref="Security"/>.</returns>
	[HttpPost]
	[Authorize]
	public async ValueTask<IActionResult> Security(SecurityViewModel viewModel)
	{
		if (this.ModelState.IsValid)
		{
			User? user = await this.signInManager.UserManager.GetUserAsync(this.User);

			if (user is null)
				return this.Problem();

			var result = await this.signInManager.UserManager.ChangePasswordAsync(
				user,
				viewModel.OldPassword,
				viewModel.NewPassword);

			if (!result.Succeeded)
				foreach (var error in result.Errors)
					this.ModelState.AddModelError(string.Empty, error.Description);
		}

		return this.View(viewModel);
	}
}
