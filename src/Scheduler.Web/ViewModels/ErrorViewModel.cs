namespace Scheduler.Web.ViewModels;

/// <summary>
/// Represents an error.
/// </summary>
public sealed record ErrorViewModel
{
	/// <summary>
	/// The request which caused the error.
	/// </summary>
	public string? RequestId { get; init; }

	/// <summary>
	/// Will show <see cref="RequestId"/> if one was supplied.
	/// </summary>
	public bool ShowRequestId
		=> !string.IsNullOrEmpty(this.RequestId);
}
