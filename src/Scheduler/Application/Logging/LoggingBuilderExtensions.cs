using Microsoft.Extensions.Logging.Configuration;
using Scheduler.Application.Options;

namespace Scheduler.Application.Logging;

public static class LoggingBuilderExtensions
{
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
