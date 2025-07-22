using Domain.DTO;
using MediatR;

namespace Application.Managers.Queries.GetManagerDescription
{
    public class GetManagerDescriptionQuery : IRequest<ManagerDTO>
    {
        public int Id { get; set; }
    }
}
