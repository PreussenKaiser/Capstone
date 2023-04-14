using System.ComponentModel.DataAnnotations;

namespace Scheduler.ViewModels;

public class ForgotPasswordViewModal
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }
}
