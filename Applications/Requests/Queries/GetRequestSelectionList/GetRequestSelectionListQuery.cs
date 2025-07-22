using Domain.DTO.Requests;
using MediatR;

namespace Application.Requests.Queries.GetRequestSelectionList
{
	public class GetRequestSelectionListQuery : IRequest<IEnumerable<RequestSelectionDTO>>
	{
	}
}
