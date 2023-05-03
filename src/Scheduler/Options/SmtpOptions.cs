namespace Scheduler.Options;

/// <summary>
/// Contains configuration options for smtp.
/// </summary>
public sealed class SmtpOptions
{
	/// <summary>
	/// Contains the hierarchy's name.
	/// </summary>
	public const string Smtp = "Smtp";

	/// <summary>
	/// Gets the host to send the email with.
	/// </summary>
	public required string Host { get; init; }

	/// <summary>
	/// Gets the port to send the email from.
	/// </summary>
	public required int Port { get;	init; }

	/// <summary>
	/// Gets whether SSL is enabled or not.
	/// </summary>
	public required bool EnableSsl { get; init; }

	/// <summary>
	/// Gets a value indicating when the smtp call should time out.
	/// </summary>
	public required int Timeout { get; init; }
}
