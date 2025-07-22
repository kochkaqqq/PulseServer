using Domain.DTO;
using Domain.Filters;
using MediatR;

namespace Application.Objects.Queries.GetObjectDTOList
{
	public class GetClientDTOListQuery : IRequest<Tuple<IEnumerable<ClientListElementDTO>, int>>
	{
		public int Page { get; set; } = 1;
		public int PageEntitiesCount { get; set; } = 25;
		public ClientFilter ClientFilter { get; set; } = new();
	}
}
