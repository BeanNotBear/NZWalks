using System.Net;

namespace NZWalks.API.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly ILogger<ExceptionHandlerMiddleware> logger;
		private readonly RequestDelegate next;

		public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
		{
			this.logger = logger;
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch (Exception exception)
			{
				var errorId = Guid.NewGuid();

				// Log this exception
				logger.LogError(exception, $"{errorId} : {exception.Message}");

				// Return A Custom Error Response
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";

				var error = new
				{
					Id = errorId,
					ErrorMessage = "Something went wrong! We are looking into resolving this."
				};

				await httpContext.Response.WriteAsJsonAsync(error);
			}
		}

	}
}
