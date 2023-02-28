using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Core.Models.Identity;
using Scheduler.Web.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Scheduler.Web.Controllers;

/// <summary>
/// Renders ASP.NET Identity views.
/// </summary>
public sealed class IdentityController : Controller
{
	/// <summary>
	/// Data-access for users.
	/// </summary>
	private readonly UserManager<User> userManager;

	/// <summary>
	/// The API to access <see cref="User"/> login information with.
	/// </summary>
	private readonly SignInManager<User> signInManager;

	/// <summary>
	/// Initializes the <see cref="IdentityController"/> class.
	/// </summary>
	/// <param name="userManager">Data-access for users.</param>
	/// <param name="signInManager">The API to access <see cref="User"/> login information with.</param>
	public IdentityController(
		UserManager<User> userManager,
		SignInManager<User> signInManager)
	{
		this.userManager = userManager;
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
	public async Task<IActionResult> Login(LoginViewModel viewModel)
	{
		if (!this.ModelState.IsValid)
			return this.View(viewModel);

		SignInResult result = await this.signInManager.PasswordSignInAsync(
			viewModel.Credentials.Email,
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
	public async Task<IActionResult> Logout()
	{
		await this.signInManager.SignOutAsync();

		return this.RedirectToAction(nameof(HomeController.Index), "Home");
	}

	/// <summary>
	/// Displays the <see cref="Register"/> view.
	/// </summary>
	/// <returns>The <see cref="Register"/> view.</returns>
	[Authorize(Roles ="Admin")]
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
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Register(RegisterViewModel viewModel)
	{
		if (!this.ModelState.IsValid)
			return this.View(viewModel);

		User user = new()
		{
			UserName = viewModel.Credentials.Email,
			Email = viewModel.Credentials.Email,
			FirstName = viewModel.Credentials.FirstName,
			LastName = viewModel.Credentials.LastName
		};

		IdentityResult result = await this.userManager.CreateAsync(user, viewModel.Credentials.Password);

		if (!result.Succeeded)
		{
			foreach (var e in result.Errors)
				this.ModelState.AddModelError(string.Empty, e.Description);

			return this.View(viewModel);
		}
		else if (result.Succeeded && viewModel.IsAdmin) {
			await userManager.AddToRoleAsync(user, "Admin");
		}

		return this.RedirectToAction(nameof(IdentityController.ManageUsers), "Identity");
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> ManageUsers()
	{
		var users = await userManager.Users.ToListAsync();
		return this.View(users);
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Update(Guid id)
	{
		var user = await userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
		return this.View(user);
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Update(User user)
	{
		await userManager.UpdateAsync(user);

		return this.RedirectToAction(nameof(IdentityController.ManageUsers), "Identity");
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var user = await userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

		if (user is not null)
		{
			await userManager.DeleteAsync(user);
		}

		return this.RedirectToAction(nameof(IdentityController.ManageUsers), "Identity");
	}
}
