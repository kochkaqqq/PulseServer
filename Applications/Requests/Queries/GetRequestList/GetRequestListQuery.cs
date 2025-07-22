using Domain;
using MediatR;

namespace Application.Requests.Queries.GetRequestList
{
    public class GetRequestListQuery : IRequest<List<Request>>
    {

    }
}
