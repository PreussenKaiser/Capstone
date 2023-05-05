namespace Scheduler.Domain.Services;

/// <summary>
/// Implements methods to send emails.
/// </summary>
public interface IEmailSender
{
	/// <summary>
	/// Sends an email asynchronously.
	/// </summary>
	/// <param name="to">The recipient's email address.</param>
	/// <param name="subject">The email's title.</param>
	/// <param name="body">The email's body.</param>
	/// <returns>Whether the task was completed or not.</returns>
	Task SendAsync(string to, string subject, string body);
}
