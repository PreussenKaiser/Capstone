using Microsoft.AspNetCore.Mvc;

namespace Scheduler.Web.Views.Shared.Components.Navigation;

/// <summary>
/// Provides a sub-navbar for views.
/// </summary>
public sealed class Navigation : ViewComponent
{
	/// <summary>
	/// Loads the component.
	/// </summary>
	/// <param name="navItems">Items in the navbar.</param>
	/// <returns>The navigation menu.</returns>
	public async Task<IViewComponentResult> InvokeAsync(IEnumerable<string> navItems)
	{
		IDictionary<string, string> links = new Dictionary<string, string>();

		foreach (var navItem in navItems)
		{
			string activeCssClass = this.ViewContext.RouteData.Values["action"] as string == navItem
				? "active"
				: string.Empty;

			links.Add(navItem, activeCssClass);
		}

		return this.View(links);
	}
}
