using MediatR;
using Domain;

namespace Application.Requests.Commands.AddRequestFile
{
	public class AddRequestFileCommand : IRequest<Document>
	{
		public int RequestId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Extension { get; set; } = string.Empty;
		public byte[] File { get; set; } = null!;
	}
}
