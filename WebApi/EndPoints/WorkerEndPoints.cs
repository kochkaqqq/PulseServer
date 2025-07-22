using Application.Exceptions;
using Application.Workers.Commands.CreateWorker;
using Application.Workers.Commands.DeleteWorker;
using Application.Workers.Commands.UpdateWorker;
using Application.Workers.Queries.GetWorkerByApiKey;
using Application.Workers.Queries.GetWorkerDetails;
using Application.Workers.Queries.GetWorkerDTOList;
using Application.Workers.Queries.GetWorkerList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApiV2.EndPoints
{
	public static class WorkerEndPoints
	{
		public static void UseWorkerEndPoints(this WebApplication app)
		{
			// Queries
			app.GetWorkerList();
			app.GetWorker();
			app.GetWorkerDTOList();
			app.GetWorkerByApiKey();

			// Commands
			app.AddWorker();
			app.UpdateWorker();
			app.DeleteWorker();
		}

		private static void GetWorkerList(this WebApplication app)
		{
			app.MapPost("/workers/GetList", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger) =>
			{
				try
				{
					var res = await mediator.Send(new GetWorkerListQuery(), cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void GetWorkerDTOList(this WebApplication app)
		{
			app.MapGet("/workers/GetWorkerDTOList", async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger) =>
			{
				try
				{
					var res = await mediator.Send(new GetWorkerDTOListQuery(), cancellationToken);
					return Results.Ok(res);
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message);
					return Results.Problem();
				}
			});
		}

		private static void GetWorker(this WebApplication app)
		{
			app.MapGet("/workers/GetWorker", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					var res = await mediator.Send(new GetWorkerDetailsQuery() { Id = id }, cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void AddWorker(this WebApplication app)
		{
			app.MapPost("/workers/AddWorker", static async (IMediator mediator, CancellationToken cancellationToken,
				ILogger<Program> logger, [FromBody] CreateWorkerCommand command) =>
			{
				try
				{
					var res = await mediator.Send(command, cancellationToken);
					return Results.Ok(res);
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message);
					return Results.BadRequest(ex.Message);
				}
			});
		}

		private static void UpdateWorker(this WebApplication app)
		{
			app.MapPut("/workers/UpdateWorker", static async (IMediator mediator, CancellationToken cancellationToken,
				ILogger<Program> logger, [FromBody] UpdateWorkerCommand command) =>
			{
				try
				{
					var res = await mediator.Send(command, cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void DeleteWorker(this WebApplication app)
		{
			app.MapDelete("/workers/DeleteWorker", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					await mediator.Send(new DeleteWorkerCommand() { Id = id }, cancellationToken);
					return Results.Ok();
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void GetWorkerByApiKey(this WebApplication app)
		{
			app.MapGet("/workers/GetWorkerByApiKey", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] string ApiKey) =>
			{
				try
				{
					var res = await mediator.Send(new GetWorkerByApiKeyQuery() { ApiKey = ApiKey });
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					return Results.NotFound(ex.Message);
				}
			});
		}
	}
}
