using Application.Exceptions;
using Application.MediaFiles.Commands.AddMediaFile;
using Application.MediaFiles.Commands.UnpinMediaFile;
using Application.MediaFiles.Queries.GetMediaFile;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.EndPoints
{
	public static class MediaFileEndPonts
	{
		public static void UseMediaFilesEndPoints(this WebApplication app)
		{
			//Commands
			app.AddMediaFile();
			app.UnpinMediaFile();

			//Queries
			app.GetMediaFile();
		}

		private static void AddMediaFile(this WebApplication app)
		{
			app.MapPost("/MediaFiles/Add", async (IMediator _mediator, IWebHostEnvironment _enviroment, CancellationToken cancellationToken, HttpContext _httpContext) =>
			{
				try
				{
					var savePath = Path.Combine(_enviroment.ContentRootPath, "Uploads");

					var form = await _httpContext.Request.ReadFormAsync();

					var file = form.Files[0];

					if (file == null || file.Length == 0)
						return Results.BadRequest("File is required");

					using var memoryStream = new MemoryStream();
					await file.CopyToAsync(memoryStream);
					byte[] fileBytes = memoryStream.ToArray();

					var command = new AddMediaFileCommand()
					{
						SavePath = savePath,
						ReportId = int.Parse(form["reportId"].ToString()),
						Description = form["description"].ToString(),
						MimeType = form["mimeType"].ToString(),
						Content = fileBytes
					};

					var res = await _mediator.Send(command);
					return Results.Ok(res);
				}
				catch (FileTooBigException)
				{
					return Results.BadRequest("File too big");
				}
				catch (NotFoundException ex)
				{
					return Results.NotFound(ex.Message);
				}
				catch (Exception ex) 
				{
					return Results.BadRequest(ex.Message);
				}
			});
		}

		private static void GetMediaFile(this WebApplication app)
		{
			app.MapGet("MediaFile/Get", async (IMediator mediator, [FromQuery] string name, IWebHostEnvironment _environment, HttpContext _httpContext) =>
			{
				try
				{
					var path = Path.Combine(_environment.ContentRootPath, "Uploads");
					var res = await mediator.Send(new GetMediaFileQuery()
					{
						Name = name,
						UploadsPath = path
					});
					var engDescription = WebUtility.UrlEncode(res.Description);
					_httpContext.Response.Headers.Append("name", res.Name);
					_httpContext.Response.Headers.Append("mimeType", res.MimeType);
					_httpContext.Response.Headers.Append("description", engDescription);
					return Results.File(res.File, res.MimeType, res.Name);
				}
				catch (NotFoundException)
				{
					return Results.NotFound();
				}
				catch (Exception ex)
				{
					return Results.BadRequest(ex.Message);
				}
			});
		}

		private static void UnpinMediaFile(this WebApplication app)
		{
			//TODO also delete file
			app.MapDelete("MediaFile/Unpin", async (IMediator mediator, [FromQuery] string name) =>
			{
				try
				{
					await mediator.Send(new UnpinMediaFileCommand() { FileName = name });
					return Results.Ok();
				}
				catch (NotFoundException ex)
				{
					return Results.NotFound(ex.Message);
				}
				catch (Exception ex)
				{
					return Results.BadRequest(ex.Message);
				}
			});
		}
	}
}
