using Domain.DTO.Clients;
using MediatR;

namespace Application.Objects.Queries.GetSelectionClientList
{
	public class GetSelectionClientListQuery : IRequest<IEnumerable<ClientSelectionDTO>>
	{
	}
}
