using System.ComponentModel.DataAnnotations;

namespace Scheduler.ViewModels;

public class ForgottenPasswordViewModal
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }
}
