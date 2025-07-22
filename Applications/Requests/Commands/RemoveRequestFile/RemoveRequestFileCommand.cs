using MediatR;

namespace Application.Requests.Commands.RemoveRequestFile
{
	public class RemoveRequestFileCommand : IRequest<Unit>
	{
		public int RequestId { get; set; }
		public int DocumentId { get; set; }
	}
}
