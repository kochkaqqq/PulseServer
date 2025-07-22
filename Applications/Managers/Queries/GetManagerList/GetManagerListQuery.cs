using Domain.DTO;
using MediatR;


namespace Application.Managers.Queries.GetManagerList
{
    public class GetManagerListQuery : IRequest<IEnumerable<ManagerDTO>>
    {

    }
}
