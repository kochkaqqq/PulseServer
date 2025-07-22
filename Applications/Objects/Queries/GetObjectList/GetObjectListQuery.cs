using MediatR;

namespace Application.Objects.Queries.GetObjectList
{
    public class GetObjectListQuery : IRequest<List<Domain.Client>>
    {

    }
}
