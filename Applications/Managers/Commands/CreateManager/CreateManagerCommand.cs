using MediatR;

namespace Application.Managers.Commands.CreateManager
{
    public class CreateManagerCommand : IRequest<Domain.Manager>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
