namespace Scheduler.Web.ViewModels;

public sealed record ErrorViewModel
{
	public string? RequestId { get; init; }

	public bool ShowRequestId
		=> !string.IsNullOrEmpty(this.RequestId);
}
