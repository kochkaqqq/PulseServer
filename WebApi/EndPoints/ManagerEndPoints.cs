using Application.Exceptions;
using Application.Managers.Commands.CreateManager;
using Application.Managers.Commands.DeleteManager;
using Application.Managers.Commands.UpdateManager;
using Application.Managers.Queries.GetManagerDescription;
using Application.Managers.Queries.GetManagerList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApiV2.EndPoints
{
	public static class ManagerEndPoints
	{
		public static void UseManagerEndPoints(this WebApplication app)
		{
			// Queries
			app.GetManagerList();
			app.GetManager();

			// Commands
			app.AddManager();
			app.UpdateManager();
			app.DeleteManager();
		}

		private static void GetManagerList(this WebApplication app)
		{
			app.MapGet("/managers/GetList", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger) =>
			{
				try
				{
					var res = await mediator.Send(new GetManagerListQuery(), cancellationToken);
					return Results.Ok(res);
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void GetManager(this WebApplication app)
		{
			app.MapGet("/managers/GetManager", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					var res = await mediator.Send(new GetManagerDescriptionQuery() { Id = id }, cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void AddManager(this WebApplication app)
		{
			app.MapPost("/managers/AddManager", static async (IMediator mediator, CancellationToken cancellationToken,
				ILogger<Program> logger, [FromBody] CreateManagerCommand command) =>
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

		private static void UpdateManager(this WebApplication app)
		{
			app.MapPut("/managers/UpdateManager", static async (IMediator mediator, CancellationToken cancellationToken,
				ILogger<Program> logger, [FromBody] UpdateManagerCommand command) =>
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

		private static void DeleteManager(this WebApplication app)
		{
			app.MapDelete("/managers/DeleteManager", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					await mediator.Send(new DeleteManagerCommand() { Id = id }, cancellationToken);
					return Results.Ok();
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}
	}
}
