using MediatR;

namespace Application.Managers.Commands.DeleteManager
{
    public class DeleteManagerCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
