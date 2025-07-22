using Domain.DTO;
using Domain.Filters;
using MediatR;

namespace Application.Requests.Queries.GetRequestDTOList
{
	public class GetRequestDTOListQuery : IRequest<Tuple<IEnumerable<RequestListElementDTO>, int>>
	{
		public int Page {  get; set; }
		public int PageEntitiesCount { get; set; }
		public RequestFilter RequestFilter { get; set; } = new();
	}
}
