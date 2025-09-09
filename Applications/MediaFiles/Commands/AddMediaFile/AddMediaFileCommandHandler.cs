using Application.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MediaFiles.Commands.AddMediaFile
{
	public class AddMediaFileCommandHandler : IRequestHandler<AddMediaFileCommand, string>
	{
		private readonly IDbContext _dbContext;

		public AddMediaFileCommandHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<string> Handle(AddMediaFileCommand request, CancellationToken cancellationToken)
		{
			const long maxFileSize = 40 * 1024 * 1024;

			if (request.Content.Length > maxFileSize)
			{
				throw new FileTooBigException();
			}

			Directory.CreateDirectory(request.SavePath);

			var report = await _dbContext.Reports.FirstOrDefaultAsync(r => r.ReportId == request.ReportId, cancellationToken) 
				?? throw new NotFoundException(nameof(Report), request.ReportId.ToString());

			var mediaFile = new MediaFile()
			{
				Name = Guid.NewGuid().ToString(),
				Description = request.Description,
				MimeType = request.MimeType,
				Report = report,
			};

			//здесь должен сохраняться файл по пути request.SavePath
			var fileName = mediaFile.Name;
			var fileExtension = GetFileExtension(request.MimeType);
			var fullFileName = $"{fileName}{fileExtension}";
			var filePath = Path.Combine(request.SavePath, fullFileName);

			await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
			await fileStream.WriteAsync(request.Content, 0, request.Content.Length, cancellationToken);

			await _dbContext.MediaFiles.AddAsync(mediaFile);

			await _dbContext.SaveChangesAsync(cancellationToken);

			return mediaFile.Name;
		}

		private string GetFileExtension(string mimeType)
		{
			return mimeType.ToLower() switch
			{
				// Изображения
				"image/jpeg" => ".jpg",
				"image/png" => ".png",
				"image/gif" => ".gif",
				"image/bmp" => ".bmp",
				"image/svg+xml" => ".svg",
				"image/webp" => ".webp",
				"image/tiff" => ".tiff",

				// Видео
				"video/mp4" => ".mp4",
				"video/mpeg" => ".mpeg",
				"video/ogg" => ".ogv",
				"video/webm" => ".webm",
				"video/quicktime" => ".mov",
				"video/x-msvideo" => ".avi",
				"video/x-ms-wmv" => ".wmv",

				_ => ".bin"
			};
		}
	}
}
