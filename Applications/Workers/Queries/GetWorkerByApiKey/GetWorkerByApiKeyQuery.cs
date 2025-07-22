using Domain.DTO;
using MediatR;

namespace Application.Workers.Queries.GetWorkerByApiKey
{
	public class GetWorkerByApiKeyQuery : IRequest<WorkerDTO>
	{
		public string ApiKey { get; set; } = string.Empty;
	}
}
