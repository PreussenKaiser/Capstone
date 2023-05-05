using System.Net;

namespace Scheduler.ViewModels;

/// <summary>
/// The view model for displaying an error with an HTTP status code.
/// </summary>
public sealed record ErrorViewModel
{
	/// <summary>
	/// Initializes the <see cref="ErrorViewModel"/> record.
	/// </summary>
	/// <param name="statusCode">The HTTP error's status code.</param>
	public ErrorViewModel(int statusCode)
	{
		this.StatusCode = statusCode;

		this.Message = statusCode switch
		{
			StatusCodes.Status400BadRequest => "Bad Request",
			StatusCodes.Status404NotFound => "Not Found",
			StatusCodes.Status500InternalServerError => "Internal Server Error",
			_ => "Problem"
		};
	}

	/// <summary>
	/// The HTTP error's status code.
	/// </summary>
	public int StatusCode { get; }

	/// <summary>
	/// A brief description of <see cref="StatusCode"/>.
	/// </summary>
	public string Message { get; }
}
