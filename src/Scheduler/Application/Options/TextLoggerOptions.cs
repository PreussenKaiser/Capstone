namespace Scheduler.Application.Options;

/// <summary>
/// Configuration options for text logging.
/// </summary>
public sealed class TextLoggerOptions
{
	/// <summary>
	/// References the hierarchy to read from.
	/// </summary>
	public const string TextLogger = "Logging";

	/// <summary>
	/// Gets the logger's default log level.
	/// </summary>
	public required LogLevel Default { get; init; }

	/// <summary>
	/// Gets the file path to log to.
	/// </summary>
	public required string FilePath { get; init; }
}
