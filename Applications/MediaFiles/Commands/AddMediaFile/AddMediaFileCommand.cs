using MediatR;

namespace Application.MediaFiles.Commands.AddMediaFile
{
	public class AddMediaFileCommand : IRequest<string>
	{
		public int ReportId { get; set; }
		public string Description { get; set; } = string.Empty;
		public string MimeType { get; set; } = string.Empty;
		public byte[] Content { get; set; }

		public string SavePath { get; set; }
	}
}
