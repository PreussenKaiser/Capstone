namespace Scheduler.Application.Middleware;

/// <summary>
/// Logs application HTTP requests.
/// </summary>
public sealed class LoggingMiddleware
{
	/// <summary>
	/// For calling the next item in the requst pipeline.
	/// </summary>
	private readonly RequestDelegate next;

	/// <summary>
	/// Initializes the <see cref="LoggingMiddleware"/> class.
	/// </summary>
	/// <param name="next">For calling the next item in the request pipeline.</param>
	public LoggingMiddleware(RequestDelegate next)
	{
		this.next = next;
	}

	/// <summary>
	/// Logs the <see cref="HttpContext.Request"/> and <see cref="HttpContext.Response"/> of a call.
	/// </summary>
	/// <param name="context">The current <see cref="HttpContext"/>.</param>
	/// <param name="logger">Logs request and response.</param>
	public async Task InvokeAsync(
		HttpContext context, ILogger<LoggingMiddleware> logger)
	{
		string path = context.Request.Path;
		string method = context.Request.Method;
		int statusCode = context.Response.StatusCode;

		logger.LogInformation($"{method} {path} executed with status code {statusCode}");

		await this.next(context);
	}
}
