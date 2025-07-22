using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Workers.Queries.GetWorkerList
{
    public class GetWorkerListQueryHandler : IRequestHandler<GetWorkerListQuery, List<Worker>>
    {
        private readonly IDbContext _dbContext;

        public GetWorkerListQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<Worker>> Handle(GetWorkerListQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Workers
                .Include(w => w.Experiencies)
                .ToListAsync(cancellationToken);
        }
    }
}
