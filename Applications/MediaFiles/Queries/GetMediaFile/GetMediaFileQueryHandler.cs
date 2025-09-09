using Application.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Application.MediaFiles.Queries.GetMediaFile
{
	public class GetMediaFileQueryHandler : IRequestHandler<GetMediaFileQuery, MediaFile>
	{
		private readonly IDbContext _dbContext;

		public GetMediaFileQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<MediaFile> Handle(GetMediaFileQuery request, CancellationToken cancellationToken)
		{
			var files = Directory.GetFiles(request.UploadsPath, $"{request.Name}.*");

			if (files.Length == 0)
				throw new NotFoundException(nameof(MediaFile), request.Name);

			var mediaFile = await _dbContext.MediaFiles.FirstOrDefaultAsync(m => m.Name == request.Name) 
				?? throw new NotFoundException(nameof(MediaFile), request.Name); ;

			var filePath = files[0];
			var fileName = Path.GetFileName(filePath);
			var mimeType = GetMimeType(filePath);
			var fileExtension = Path.GetExtension(filePath).TrimStart('.');

			return new MediaFile()
			{
				Name = fileName,
				MimeType = mimeType,
				Description = mediaFile.Description,
				File = File.OpenRead(filePath)
			};
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
