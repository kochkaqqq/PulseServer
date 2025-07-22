using Domain;
using MediatR;

namespace Application.Requests.Commands.UpdateRequestDocument
{
	public class UpdateRequestDocumentCommand : IRequest<Request>
	{
		public int RequestId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Extension { get; set; } = string.Empty;
		public byte[] File { get; set; } = null!;
	}
}
