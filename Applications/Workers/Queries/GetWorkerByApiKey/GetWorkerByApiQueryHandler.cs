using Application.Interfaces;
using Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Application.Workers.Queries.GetWorkerByApiKey
{
	public class GetWorkerByApiQueryHandler : IRequestHandler<GetWorkerByApiKeyQuery, WorkerDTO>
	{
		private readonly IDbContext _dbContext;

		public GetWorkerByApiQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<WorkerDTO> Handle(GetWorkerByApiKeyQuery request, CancellationToken cancellationToken)
		{
			
			var worker = await _dbContext.Workers.AsNoTracking()
				.FirstOrDefaultAsync(w => w.ApiKey == request.ApiKey);

			if (worker == null)
				throw new NotFoundException(nameof(WorkerDTO), request.ApiKey);

			var res = new WorkerDTO()
			{
				Id = worker.WorkerId,
				Name = worker.Name,
			};

			return res;
		}
	}
}
