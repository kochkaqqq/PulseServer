using Domain;
using MediatR;

namespace Application.Requests.Queries.GetRequestDetails
{
    public class GetRequestDetailsQuery : IRequest<Request>
    {
        public int RequestId { get; set; }
    }
}
