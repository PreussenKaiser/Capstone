namespace Scheduler.Application.Options;

/// <summary>
/// Contains options for sending emails.
/// </summary>
public sealed class EmailOptions
{
	/// <summary>
	/// Contains the hierarchy's name.
	/// </summary>
	public const string Email = "Email";

	/// <summary>
	/// Gets the address to send the email with.
	/// </summary>
	public required string Address { get; init; }

	/// <summary>
	/// Gets the sender's name.
	/// </summary>
	public required string Name { get; init; }

	/// <summary>
	/// Gets the API key (I think).
	/// </summary>
	public required string Password { get; init; }
}
