using MediatR;

namespace Application.Requests.Commands.DeleteRequest
{
    public class DeleteRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
