using Application.ApiKeys.CheckKey;
using MediatR;

namespace WebApi.Middlewares
{
	public class Autorization
	{
		private readonly RequestDelegate _next;
		//private readonly IMediator _mediator;
		private const string API_KEY_HEADER = "apikey";

		public Autorization(RequestDelegate next, IMediator mediator)
		{
			_next = next;
			//_mediator = mediator;
		}

		public async Task InvokeAsync(HttpContext context, IMediator _mediator)
		{
			if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("API Key is missing");
				return;
			}

			if (!await _mediator.Send(new CheckApiKeyQuery() { ApiKey = extractedApiKey}))
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("API Key is invalid");
				return;
			}

			await _next(context);
		}
	}
}
