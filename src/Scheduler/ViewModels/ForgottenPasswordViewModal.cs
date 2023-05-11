using System.ComponentModel.DataAnnotations;

namespace Scheduler.ViewModels;

public class ForgottenPasswordViewModal
{
	[Required]
	[EmailAddress]
	public required string Email { get; set; }
}
