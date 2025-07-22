using MediatR;

namespace Application.Objects.Commands.DeleteObject
{
    public class DeleteObjectCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
    }
}
