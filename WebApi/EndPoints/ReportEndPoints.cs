using Application.Reports.Commands.CreateReport;
using Application.Reports.Commands.UpdateReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Reports.Queries.GetReportList;
using Application.Exceptions;
using Application.Reports.Queries.GetReportDetails;
using Application.Reports.Commands.DeleteReport;
using Application.Reports.Queries.GeteportListByExperience;
using Application.Reports.Queries.GetReportListByClientFilter;
using Microsoft.AspNetCore.StaticFiles;

namespace WebApiV2.EndPoints
{
	public static class ReportEndPoints 
	{
		public static void UseReportEndPoints(this WebApplication app)
		{
			// Queries
			app.GetReportList();
			app.GetReport();
			app.GetReportListByExperience();
			app.GetReportDTOList();
			app.DownloadFile();

			// Commands
			app.AddReport();
			app.UpdateReport();
			app.DeleteReport();
			app.SaveFile();
		}

		private static void GetReportList(this WebApplication app)
		{
			app.MapPost("/reports/GetList", static async ([FromQuery] int? id, IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger) =>
			{
				try
				{
					var query = new GetReportListQuery();
					if (id != null)
						query.WorkerId = id.Value;
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

		private static void GetReport(this WebApplication app)
		{
			app.MapGet("/reports/GetReport", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					var res = await mediator.Send(new GetReportQuery() { Id = id }, cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void GetReportListByExperience(this WebApplication app)
		{
			app.MapGet("/reports/GetReportListByExperience", async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int ExperienceId) =>
			{
				try
				{
					var res = await mediator.Send(new GetReportListByExperienceQuery() { ExperienceId = ExperienceId }, cancellationToken);
					return Results.Ok(res);
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void GetReportDTOList(this WebApplication app)
		{
			app.MapPost("/reports/GetReportDTOListByClientFilter", static async (IMediator mediator, [FromBody] GetReportListByClientFilterQuery query, CancellationToken cancellationToken, ILogger<Program> logger) =>
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

		private static void AddReport(this WebApplication app)
		{
			app.MapPost("/reports/AddReport", static async (IMediator mediator, CancellationToken cancellationToken,
				ILogger<Program> logger, [FromBody] CreateReportCommand command) =>
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

		private static void UpdateReport(this WebApplication app)
		{
			app.MapPut("/reports/UpdateReport", static async (IMediator mediator, CancellationToken cancellationToken,
				ILogger<Program> logger, [FromBody] UpdateReportCommand command) =>
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

		private static void DeleteReport(this WebApplication app)
		{
			app.MapDelete("/reports/DeleteReport", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger, [FromQuery] int id) =>
			{
				try
				{
					await mediator.Send(new DeleteReportCommand() { Id = id }, cancellationToken);
					return Results.Ok();
				}
				catch (NotFoundException ex)
				{
					logger.LogError(ex.Message);
					return Results.NotFound(ex.Message);
				}
			});
		}

		private static void SaveFile(this WebApplication app)
		{
			app.MapPost("/reports/saveFile", async (HttpRequest request) =>
			{
				const int maxFileSize = 5 * 1024 * 1024; 

				var file = request.Form.Files[0];

				if (file.Length > maxFileSize)
				{
					return Results.BadRequest(new
					{
						ErrorCode = "FILE_TOO_LARGE",
						Message = $"Файл превышает максимальный размер {maxFileSize / (1024 * 1024)} МБ",
						MaxAllowedSize = maxFileSize,
						ActualSize = file.Length
					});
				}

				var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
				Directory.CreateDirectory(uploadsFolder);

				var newName = Guid.NewGuid().ToString();
				var originalFileName = file.FileName;
				var fileExtension = Path.GetExtension(originalFileName);
				var newFileName = $"{newName}{fileExtension}";
				var filePath = Path.Combine(uploadsFolder, newFileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				return Results.Ok(new
				{
					FileName = newName,
					Size = file.Length,
					SavedPath = filePath
				});
			});
		}

		private static void DownloadFile(this WebApplication app)
		{
			app.MapGet("/reports/GetMedia", ([FromQuery] string fileId) =>
			{
				var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

				// Находим первый файл, который начинается с fileId
				var files = Directory.GetFiles(uploadsFolder, $"{fileId}.*");

				if (files.Length == 0)
				{
					return Results.NotFound(new { Error = "File not found" });
				}

				// Берем первый найденный файл
				var filePath = files[0];
				var fileName = Path.GetFileName(filePath);
				var mimeType = GetMimeType(filePath);

				return Results.File(
					fileStream: File.OpenRead(filePath),
					contentType: mimeType,
					fileDownloadName: fileName // Сохранит оригинальное имя с расширением
				);
			});
		}

		static string GetMimeType(string filePath)
		{
			var provider = new FileExtensionContentTypeProvider();
			if (!provider.TryGetContentType(filePath, out var contentType))
			{
				contentType = "application/octet-stream";
			}
			return contentType;
		}
	}
}
