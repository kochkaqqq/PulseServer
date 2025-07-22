using Domain;
using MediatR;

namespace Application.Workers.Queries.GetWorkerList
{
    public class GetWorkerListQuery : IRequest<List<Worker>>
    {

    }
}
