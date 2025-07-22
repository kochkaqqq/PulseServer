using Application.Interfaces;
using Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Workers.Queries.GetWorkerDTOList
{
	public class GetWorkerDTOListQueryHandler : IRequestHandler<GetWorkerDTOListQuery, ICollection<WorkerDTO>>
	{
		private readonly IDbContext _dbContext;

		public GetWorkerDTOListQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ICollection<WorkerDTO>> Handle(GetWorkerDTOListQuery request, CancellationToken cancellationToken)
		{
			return await _dbContext.Workers
				.AsNoTracking()
				.Select(w => new WorkerDTO() { Id = w.WorkerId, Name = w.Name })
				.ToArrayAsync(cancellationToken);
		}
	}
}
