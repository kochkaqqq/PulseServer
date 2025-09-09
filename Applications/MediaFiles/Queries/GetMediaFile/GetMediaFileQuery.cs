using Domain;
using MediatR;

namespace Application.MediaFiles.Queries.GetMediaFile
{
	public class GetMediaFileQuery : IRequest<MediaFile>
	{
		public string Name { get; set; } = string.Empty;
		public string UploadsPath { get; set; } = string.Empty;
	}
}
