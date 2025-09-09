using MediatR;

namespace Application.MediaFiles.Commands.UnpinMediaFile
{
	public class UnpinMediaFileCommand : IRequest
	{
		public int ReportId { get; set; }
		public string FileName { get; set; } = string.Empty;
	}
}
