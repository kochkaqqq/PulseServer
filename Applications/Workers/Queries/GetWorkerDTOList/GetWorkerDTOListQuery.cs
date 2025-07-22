using Domain.DTO;
using MediatR;

namespace Application.Workers.Queries.GetWorkerDTOList
{
	public class GetWorkerDTOListQuery : IRequest<ICollection<WorkerDTO>>
	{
	}
}
