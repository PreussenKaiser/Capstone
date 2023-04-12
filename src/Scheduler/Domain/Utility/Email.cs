namespace Scheduler.Domain.Utility;
using System.Net;
using System.Net.Mail;

public static class Email
{
	private const string FROMADDRESS = "noahwoyak.wi@gmail.com";
	private const string FROMNAME = "Noah Woyak Test";
	private const string FROMPASSWORD = "gcueywlzgffymnae";

	public static void sendEmail(string toEmail, string toName, string subject, string body)
	{
		var fromAddress = new MailAddress(FROMADDRESS, FROMNAME);
		var toAddress = new MailAddress(toEmail, toName);

		var smtp = new SmtpClient()
		{
			Host = "smtp.gmail.com",
			Port = 587,
			EnableSsl = true,
			DeliveryMethod = SmtpDeliveryMethod.Network,
			Credentials = new NetworkCredential(fromAddress.Address, FROMPASSWORD),
			Timeout = 20000
		};

		using (var message = new MailMessage(fromAddress, toAddress)
		{
			Subject = subject,
			Body = body,
			IsBodyHtml = true
		})
		{
			smtp.Send(message);
		}
	}
}
