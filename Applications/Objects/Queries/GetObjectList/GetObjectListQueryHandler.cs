using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Objects.Queries.GetObjectList
{
    public class GetObjectListQueryHandler : IRequestHandler<GetObjectListQuery, List<Domain.Client>>
    {
        private readonly IDbContext _dbContext;

        public GetObjectListQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<Domain.Client>> Handle(GetObjectListQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Clients.ToListAsync(cancellationToken);
        }
    }
}
