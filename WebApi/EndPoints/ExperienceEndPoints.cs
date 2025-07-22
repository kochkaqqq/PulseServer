using Application.Exceptions;
using Application.Experiences.Commands.CreateExperience;
using Application.Experiences.Commands.DeleteExperience;
using Application.Experiences.Commands.UpdateExperience;
using Application.Experiences.Queries.GetExperienceDetails;
using Application.Experiences.Queries.GetExperienceDTOList;
using Application.Experiences.Queries.GetExperienceFile;
using Application.Experiences.Queries.GetExperienceGanttListByDate;
using Application.Experiences.Queries.GetExperienceMobileList;
using Application.Experiences.Queries.GetExperiencePlanTableDTOList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApiV2.EndPoints
{
	public static class ExperienceEndPoints
	{
		public static void UseExperienceEndPoints(this WebApplication app)
		{
			// Queries
			app.GetExperienceList();
			app.GetExperience();
			app.GetExperienceGantDTOListByDate();
			app.GetExperienceMobileList();
			app.GetExperiencePlanDTOList();
			app.GetExperienceFiles();

			// Commands
			app.AddExperience();
			app.UpdateExperience();
			app.DeleteExperience();
		}

		private static void GetExperienceList(this WebApplication app)
		{
			app.MapPost("/experiences/GetList", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger,
				[FromBody] GetExperinceDTOListQuery request) =>
			{
				try
				{
					var res = await mediator.Send(request, cancellationToken);
					return Results.Ok(res);
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void GetExperienceMobileList(this WebApplication app)
		{
			app.MapPost("/experiences/GetMobileList", static async (IMediator mediator, CancellationToken cancellation, ILogger<Program> logger,
				[FromBody] GetExperienceMobileListQuery request) =>
			{
				try
				{
					var res = await mediator.Send(request, cancellation);
					return Results.Ok(res);
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void GetExperiencePlanDTOList(this WebApplication app)
		{
			app.MapPost("/experiences/GetPlanListDTO", async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> Logger, [FromBody] GetExperiencePlanTableDTOListQuery query) =>
			{
				try
				{
					var res = await mediator.Send(query, cancellationToken);
					return Results.Ok(res);
				}
				catch (Exception ex)
				{
					Logger.LogError(ex.Message);
					return Results.Problem(ex.Message);
				}
			});
		}

		private static void GetExperience(this WebApplication app)
		{
			app.MapGet("/experiences/GetExperience", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					var res = await mediator.Send(new GetExperienceDetailsQuery() { Id = id }, cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void GetExperienceGantDTOListByDate(this WebApplication app)
		{
			app.MapGet("/experiences/GetExperienceGantDTOList", static async (IMediator mediator, CancellationToken cancellationToken, [FromQuery] int day, 
				[FromQuery] int month, [FromQuery] int year) =>
			{
				var date = new DateTime(year, month, day);
				var res = await mediator.Send(new GetExperienceGanttListByDateQuery() { Date = date });
				return Results.Ok(res);
			});
		}

		private static void GetExperienceFiles(this WebApplication app)
		{
			app.MapGet("/experiences/GetFiles", async (IMediator mediator, [FromQuery] int experienceId, CancellationToken cancellationToken) =>
			{
				try
				{
					var res = await mediator.Send(new GetExperienceFilesQuery() { ExperienceId = experienceId });
					return Results.Ok(res);
				}
				catch (NotFoundException)
				{
					return Results.NotFound(experienceId);
				}
			});
		}

		private static void AddExperience(this WebApplication app)
		{
			app.MapPost("/experiences/AddExperience", static async (IMediator mediator, CancellationToken cancellationToken,
				ILogger<Program> logger, [FromBody] CreateExperienceCommand command) =>
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

		private static void UpdateExperience(this WebApplication app)
		{
			app.MapPut("/experiences/UpdateExperience", static async (IMediator mediator, CancellationToken cancellationToken,
				ILogger<Program> logger, [FromBody] UpdateExperienceCommand command) =>
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

		private static void DeleteExperience(this WebApplication app)
		{
			app.MapDelete("/experiences/DeleteExperience", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					await mediator.Send(new DeleteExperienceCommand() { Id = id }, cancellationToken);
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
