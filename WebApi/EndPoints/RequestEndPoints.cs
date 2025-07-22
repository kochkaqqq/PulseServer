using Application.Exceptions;
using Application.Requests.Commands.CreateRequest;
using Application.Requests.Commands.DeleteRequest;
using Application.Requests.Commands.UpdateRequest;
using Application.Requests.Queries.GetRequestDetails;
using Application.Requests.Queries.GetRequestDTOList;
using Application.Requests.Queries.GetRequestSelectionList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Requests.Commands.UpdateRequestDocument;
using Application.Requests.Commands.AddRequestFile;
using Application.Requests.Commands.RemoveRequestFile;
using Application.Requests.Queries.GetDocument;
using Application.Requests.Commands.RemoveRequestDocument;

namespace WebApiV2.EndPoints
{
	public static class RequestEndPoints
	{
		public static void UseRequestEndPoints(this WebApplication app)
		{
			// Queries
			app.GetRequestList();
			app.GetRequest();
			app.GetRequestSelectionDTO();
			app.GetDocument();

			// Commands
			app.AddRequest();
			app.UpdateRequest();
			app.DeleteRequest();
			app.UpdateRequestDocument();
			app.AddRequestFile();
			app.RemoveRequestFile();
			app.RemoveRequestDocument();
		}

		private static void GetRequestList(this WebApplication app)
		{
			app.MapPost("/requests/GetList", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger,
				[FromBody] GetRequestDTOListQuery request) =>
			{
				try
				{
					var res = await mediator.Send(request, cancellationToken);
					return Results.Ok(res);
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message);
					return Results.Problem(ex.Message);
				}
			});
		}

		private static void GetRequest(this WebApplication app)
		{
			app.MapGet("/requests/GetRequest", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					var res = await mediator.Send(new GetRequestDetailsQuery() { RequestId = id }, cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void GetRequestSelectionDTO(this WebApplication app)
		{
			app.MapGet("/requests/GetSelectionList", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger) =>
			{
				var res = await mediator.Send(new GetRequestSelectionListQuery());
				return Results.Ok(res);
			});
		}

		private static void GetDocument(this WebApplication app)
		{
			app.MapGet("/documents/GetDocument", async (IMediator mediator, [FromQuery] int documentId, CancellationToken cancellationToken) =>
			{
				try
				{
					var res = await mediator.Send(new GetDocumentQuery() { DocumentId = documentId }, cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void AddRequest(this WebApplication app)
		{
			app.MapPost("/requests/AddRequest", static async (IMediator mediator, CancellationToken cancellationToken,
				ILogger<Program> logger, [FromBody] CreateRequestCommand command) =>
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

		private static void UpdateRequest(this WebApplication app)
		{
			app.MapPut("/requests/UpdateRequest", static async (IMediator mediator, CancellationToken cancellationToken,
				ILogger<Program> logger, [FromBody] UpdateRequestCommand command) =>
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

		private static void DeleteRequest(this WebApplication app)
		{
			app.MapDelete("/requests/DeleteRequest", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					await mediator.Send(new DeleteRequestCommand() { Id = id }, cancellationToken);
					return Results.Ok();
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void UpdateRequestDocument(this WebApplication app)
		{
			app.MapPost("/requests/UpdateRequestDocument", static async (IMediator mediator, CancellationToken cancellationToken, [FromBody] UpdateRequestDocumentCommand request, ILogger<Program> logger) =>
			{
				try
				{
					var res = await mediator.Send(request, cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void AddRequestFile(this WebApplication app)
		{
			app.MapPut("/requests/AddFile", async (IMediator mediator, [FromBody] AddRequestFileCommand command, CancellationToken cancellationToken) =>
			{
				try
				{
					var res = await mediator.Send(command);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void RemoveRequestFile(this WebApplication app)
		{
			app.MapPut("/requests/RemoveFile", async (IMediator mediator, [FromBody] RemoveRequestFileCommand command, CancellationToken cancellationToken) =>
			{
				try
				{
					var res = await mediator.Send(command, cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void RemoveRequestDocument(this WebApplication app)
		{
			app.MapPut("/requests/RemoveDocument", async (IMediator mediator, [FromQuery] int requestId, CancellationToken cancellationToken) =>
			{
				try
				{
					var res = await mediator.Send(new RemoveRequestDocumentCommand() { RequestId = requestId}, cancellationToken);
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
