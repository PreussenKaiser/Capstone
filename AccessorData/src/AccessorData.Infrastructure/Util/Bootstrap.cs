using AccessorData.Core.Services;
using AccessorData.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AccessorData.Infrastructure.Util;

/// <summary>
/// Provides data access extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class Bootstrap
{
	/// <summary>
	/// Configures data access to use a local XML document.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
	/// <param name="path">The XML document's file path.</param>
	/// <returns>The configured <see cref="IServiceCollection"/>.</returns>
	public static IServiceCollection UseLocalXML(
		this IServiceCollection services,
		string path)
	{
		return services.AddSingleton<IPropertyService>(p => new PropertyXML(path));
	}
}
