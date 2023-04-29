using Microsoft.Extensions.Logging;
using Scheduler.Application.Logging;
using Scheduler.Application.Options;
using Scheduler.Domain.Services;
using System.Reflection;
using Xunit;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Scheduler.Tests.Unit.IO;

/// <summary>
/// Tests for <see cref="TextLogger"/>.
/// </summary>
public sealed class TextLoggerTests : IDisposable
{
	/// <summary>
	/// The <see cref="TextLogger"/> to test.
	/// </summary>
	private readonly TextLogger logger;

	/// <summary>
	/// Configuration options for <see cref="TextLogger"/>.
	/// </summary>
	private readonly TextLoggerOptions options;

	/// <summary>
	/// Initializes each test.
	/// </summary>
	public TextLoggerTests()
	{
		CullingOptions cullingOptions = new() { Interval = 1, Time = 1 };
		
		this.options = new TextLoggerOptions
		{
			Culling = cullingOptions,
			Default = LogLevel.None,
			FilePath = "logs.txt"
		};

		this.logger = new TextLogger(
			this.options, new SystemDateProvider());
	}

	/// <summary>
	/// Asserts that <see cref="TextLogger"/> is thread-safe.
	/// </summary>
	[Fact]
	public void Thread_Safe()
	{
		const string MESSAGE = "Testing thread safety";

		try
		{
			Parallel.Invoke(
				() => this.logger.LogInformation(MESSAGE),
				() => this.logger.LogInformation(MESSAGE));
		}
		catch (IOException)
		{
			Assert.Fail("Exception thrown!");
		}

		Assert.True(true);
	}

	/// <summary>
	/// Cleans up file after each test.
	/// </summary>
	/// <exception cref="NullReferenceException"></exception>
	public void Dispose()
	{
		string executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
			?? throw new NullReferenceException("Could not determine executing assembly.");

		string filePath = $"{executingPath}\\{this.options.FilePath}";

		File.Delete(filePath);
	}
}
