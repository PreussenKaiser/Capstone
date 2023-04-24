using Microsoft.Extensions.Options;
using Scheduler.Application.Options;
using Scheduler.Domain.Services;
using System.Reflection;

namespace Scheduler.Application.Logging;

/// <summary>
/// Logs to a text file.
/// </summary>
public sealed class TextLogger : ILogger
{
	/// <summary>
	/// Configuration options for <see cref="TextLogger"/>.
	/// </summary>
	private readonly TextLoggerOptions options;

	/// <summary>
	/// The API to get dates from.
	/// </summary>
	private readonly IDateProvider dateProvider;

	/// <summary>
	/// Initializes the <see cref="TextLogger"/> class.
	/// </summary>
	/// <param name="options">Configuration options for <see cref="TextLogger"/>.</param>
	/// <param name="dateProvider">The API to get dates from.</param>
	public TextLogger(
		IOptions<TextLoggerOptions> options,
		IDateProvider dateProvider)
	{
		this.options = options.Value;
		this.dateProvider = dateProvider;
	}

	/// <inheritdoc/>
	public IDisposable? BeginScope<TState>(TState state)
		where TState : notnull
	{
		return default!;
	}

	/// <inheritdoc/>
	public bool IsEnabled(LogLevel logLevel)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc/>
	public void Log<TState>(
		LogLevel logLevel,
		EventId eventId,
		TState state,
		Exception? exception,
		Func<TState, Exception?, string> formatter)
	{
		string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
			?? throw new NullReferenceException("Could not determine executing assembly.");

		string filePath = $"{executingPath}\\{this.options.FilePath}";

		using StreamWriter writer = new StreamWriter(filePath);

		writer.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");
		writer.Write($"{this.dateProvider.Now.ToLongTimeString()}");
		writer.WriteLine($"{formatter(state, exception)}");
	}
}
