namespace Scheduler.Domain.Utility;

using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Scheduler.Domain.Models;
using Scheduler.Web.Controllers;
using System.Net;
using System.Net.Mail;

public static class Email
{
	private const string FROMADDRESS = "noahwoyak.wi@gmail.com";
	private const string FROMNAME = "PCYS Scheduler";
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

	public static async void eventChangeEmails(string subject, string body, List<Team> changedTeams, Guid currentUser, UserManager<User> userManager)
	{
		List<User> usersToEmail = new List<User>();

		foreach(Team team in changedTeams)
		{
			User userToAdd = await userManager.FindByIdAsync(team.UserId.ToString());
			usersToEmail.Add(userToAdd);
		}

		foreach (User user in usersToEmail) {
			if (user.Id != currentUser)
			{
				sendEmail(user.Email, user.FirstName, subject, body);
			}
		}
	}
}
