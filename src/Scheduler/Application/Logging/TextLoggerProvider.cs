using Microsoft.Extensions.Options;
using Scheduler.Application.Options;
using Scheduler.Domain.Services;
using System.Collections.Concurrent;

namespace Scheduler.Application.Logging;

[ProviderAlias(nameof(TextLogger))]
public sealed class TextLoggerProvider : ILoggerProvider
{
	private readonly TextLoggerOptions options;

	private readonly ConcurrentDictionary<string, TextLogger> loggers;

	public TextLoggerProvider(IOptions<TextLoggerOptions> options)
	{
		this.options = options.Value;

		this.loggers = new ConcurrentDictionary<string, TextLogger>(
			StringComparer.OrdinalIgnoreCase);
	}

	public ILogger CreateLogger(string categoryName)
	{
		return this.loggers.GetOrAdd(
			categoryName,
			new TextLogger(this.options, new SystemDateProvider()));
	}

	public void Dispose()
	{
		this.loggers.Clear();
	}
}
