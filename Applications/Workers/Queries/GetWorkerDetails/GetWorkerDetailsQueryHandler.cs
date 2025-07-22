using MediatR;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Application.Workers.Queries.GetWorkerDetails
{
    public class GetWorkerDetailsQueryHandler : IRequestHandler<GetWorkerDetailsQuery, Worker>
    {
        private readonly IDbContext _dbContext;

        public GetWorkerDetailsQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Worker> Handle(GetWorkerDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Workers
                .Include(w => w.Experiencies)
                .FirstOrDefaultAsync(worker => worker.WorkerId == request.Id) ?? 
                    throw new NotFoundException(nameof(Worker), request.Id.ToString());
			
            return entity;
        }
    }
}
