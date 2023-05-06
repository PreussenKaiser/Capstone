using Microsoft.Extensions.Logging.Configuration;
using Scheduler.Application.Options;

namespace Scheduler.Application.Logging;

/// <summary>
/// Contains extensions for configuring the application with logging.
/// </summary>
public static class LoggingBuilderExtensions
{
	/// <summary>
	/// Adds <see cref="TextLogger"/> to the application.
	/// </summary>
	/// <param name="builder">The API to configure logging providers with.</param>
	/// <returns>The application with <see cref="Textlogger"/>.</returns>
	public static ILoggingBuilder AddTextLogging(
		this ILoggingBuilder builder)
	{
		builder.AddConfiguration();

		builder.Services.AddSingleton<ILoggerProvider, TextLoggerProvider>();

		LoggerProviderOptions.RegisterProviderOptions
			<TextLoggerOptions, TextLoggerProvider>(builder.Services);

		return builder;
	}
}
