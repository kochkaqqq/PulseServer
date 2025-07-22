using Application.Exceptions;
using Application.Objects.Commands.CreateObject;
using Application.Objects.Commands.DeleteObject;
using Application.Objects.Commands.UpdateObject;
using Application.Objects.Queries.GetClientsQuantity;
using Application.Objects.Queries.GetObjectDetails;
using Application.Objects.Queries.GetObjectDTOList;
using Application.Objects.Queries.GetSelectionClientList;
using Domain.Filters;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace WebApiV2.EndPoints
{
	public static class ClientEndPoints
	{
		public static void UseClientEndPoints(this WebApplication app)
		{
			//Queries
			app.GetClientList();
			app.GetClient();
			app.GetClientsQuantity();
			app.GetClientSelectionDTOList();

			//Commands
			app.AddClient();
			app.UpdateClient();
			app.DeleteClient();
		}

		private static void GetClientList(this WebApplication app)
		{
			app.MapPost("/clients/GetList", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger,
				[FromBody] GetClientDTOListQuery query) =>
			{
				try
				{
					var res = await mediator.Send(query, cancellationToken);
					return Results.Ok(res);
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void GetClient(this WebApplication app)
		{
			app.MapGet("/clients/GetClient", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					var res = await mediator.Send(new GetObjectDetailsQuery() { Id = id }, cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void AddClient(this WebApplication app)
		{
			app.MapPost("/clients/AddClient", static async (IMediator mediator, CancellationToken cancellationToken, 
				ILogger<Program> logger, [FromBody] CreateObjectCommand command) =>
			{
				try
				{
					var res = await mediator.Send(command, cancellationToken);
					return Results.Ok(res);	
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void UpdateClient(this WebApplication app)
		{
			app.MapPut("/clients/UpdateClient", static async (IMediator mediator, CancellationToken cancellationToken, 
				ILogger<Program> logger, [FromBody] UpdateObjectCommand command) =>
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

		private static void DeleteClient(this WebApplication app)
		{
			app.MapDelete("/clients/DeleteClient", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					await mediator.Send(new DeleteObjectCommand() { ClientId = id }, cancellationToken);
					return Results.Ok();
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);	
				}
			});
		}

		private static void GetClientsQuantity(this WebApplication app)
		{
			app.MapGet("/clients/GetQuantity", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger) =>
			{
				try
				{
					var res = await mediator.Send(new GetClientsQuantityQuery());
					return Results.Ok(res);
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message);
					return Results.Problem(ex.Message);
				}
			});
		}

		private static void GetClientSelectionDTOList(this WebApplication app)
		{
			app.MapGet("/clients/GetSelectionDTOList", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger) =>
			{
				try
				{
					var res = await mediator.Send(new GetSelectionClientListQuery(), cancellationToken);
					return Results.Ok(res);
				}
				catch (Exception ex) 
				{
					logger.LogError(ex.Message);
					return Results.Problem(ex.Message);
				}
			});
		}
	}
}
