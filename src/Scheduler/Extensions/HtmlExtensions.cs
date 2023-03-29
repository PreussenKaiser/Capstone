using Microsoft.AspNetCore.Mvc.Rendering;

namespace Scheduler.Web.Extensions;

/// <summary>
/// Extensions for <see cref="IHtmlHelper"/>.
/// </summary>
public static class HtmlExtensions
{
	/// <summary>
	/// Used to determine if a link is active or not.
	/// </summary>
	/// <param name="html">For retrieving <see cref="ViewContext"/>.</param>
	/// <param name="acceptedAction">The action routed to.</param>
	/// <param name="activeClass">The CSS class to apply if active, defaults to 'active'.</param>
	/// <returns>The provided CSS class or an empty string.</returns>
	public static string SelectedLink(
		this IHtmlHelper html,
		string acceptedAction,
		string activeClass = "active")
	{
		string? action = html.ViewContext.RouteData.Values["action"] as string;

		return action == acceptedAction
			? activeClass
			: string.Empty;
	}
}
