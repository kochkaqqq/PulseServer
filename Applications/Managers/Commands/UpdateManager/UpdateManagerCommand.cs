using Domain;
using MediatR;

namespace Application.Managers.Commands.UpdateManager
{
    public class UpdateManagerCommand : IRequest<Manager>
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
    }
}
