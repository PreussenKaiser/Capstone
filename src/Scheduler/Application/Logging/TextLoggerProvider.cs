using Microsoft.Extensions.Options;
using Scheduler.Application.Options;
using Scheduler.Domain.Services;
using System.Collections.Concurrent;

namespace Scheduler.Application.Logging;

/// <summary>
/// Provides the application with <see cref="TextLogger"/>.
/// </summary>
[ProviderAlias(nameof(TextLogger))]
public sealed class TextLoggerProvider : ILoggerProvider
{
	/// <summary>
	/// Configuration options for <see cref="TextLogger"/>.
	/// </summary>
	private readonly TextLoggerOptions options;

	/// <summary>
	/// The current collection of loggers.
	/// </summary>
	private readonly ConcurrentDictionary<string, TextLogger> loggers;

	/// <summary>
	/// Initializes the <see cref="TextLoggerProvider"/> class.
	/// </summary>
	/// <param name="options">Configuration options for <see cref="TextLogger"/>.</param>
	public TextLoggerProvider(
		IOptions<TextLoggerOptions> options)
	{
		this.options = options.Value;

		this.loggers = new ConcurrentDictionary<string, TextLogger>(
			StringComparer.OrdinalIgnoreCase);
	}

	/// <summary>
	/// Creates a new instance of <see cref="TextLogger"/>.
	/// If the logger already exists, it is pulled from <see cref="loggers"/>.
	/// </summary>
	/// <param name="categoryName">The logger's name.</param>
	/// <returns>An instance of <see cref="TextLogger"/>.</returns>
	public ILogger CreateLogger(string categoryName)
	{
		TextLogger logger = this.loggers.GetOrAdd(
			categoryName,
			new TextLogger(this.options, new SystemDateProvider()));

		return logger;
	}

	/// <summary>
	/// Clears the current list of loggers.
	/// </summary>
	public void Dispose()
	{
		this.loggers.Clear();
	}
}
