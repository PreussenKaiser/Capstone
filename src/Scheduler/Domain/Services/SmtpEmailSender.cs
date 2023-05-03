using Microsoft.Extensions.Options;
using Scheduler.Options;
using System.Net;
using System.Net.Mail;

namespace Scheduler.Domain.Services;

/// <summary>
/// Uses <see cref="SmtpClient"/> to send emails.
/// </summary>
public sealed class SmtpEmailSender : IEmailSender
{
	/// <summary>
	/// The <see cref="SmtpClient"/> to use.
	/// </summary>
	private readonly SmtpClient client;

	/// <summary>
	/// Options for sending emails via SMTP.
	/// </summary>
	private readonly SmtpOptions smtpOptions;

	/// <summary>
	/// Options for how to send the email.
	/// </summary>
	private readonly EmailOptions emailOptions;

	/// <summary>
	/// Initializes the <see cref="SmtpEmailSender"/> class.
	/// </summary>
	/// <param name="smtpOptions">Options for sending emails via SMTP.</param>
	/// <param name="emailOptions">Options for how to send the email.</param>
	public SmtpEmailSender(
		IOptions<SmtpOptions> smtpOptions,
		IOptions<EmailOptions> emailOptions)
	{
		this.smtpOptions = smtpOptions.Value;
		this.emailOptions = emailOptions.Value;

		this.client = new SmtpClient
		{
			Host = this.smtpOptions.Host,
			Port = this.smtpOptions.Port,
			DeliveryMethod = SmtpDeliveryMethod.Network,
			EnableSsl = this.smtpOptions.EnableSsl,
			Timeout = this.smtpOptions.Timeout,

			Credentials = new NetworkCredential(
				this.emailOptions.Address,
				this.emailOptions.Password)
		};
	}

	/// <inheritdoc/>
	public async Task SendAsync(
		string to, string subject, string body)
	{
		MailMessage message = new(
			this.emailOptions.Address,
			to, subject, body)
		{
			IsBodyHtml = true
		};

		await this.client.SendMailAsync(message);
	}
}
