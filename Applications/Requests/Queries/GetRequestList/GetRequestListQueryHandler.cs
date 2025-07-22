using MediatR;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Application.Requests.Queries.GetRequestList
{
    public class GetRequestListQueryHandler : IRequestHandler<GetRequestListQuery, List<Request>>
    {
        private readonly IDbContext _dbContext;

        public GetRequestListQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<Request>> Handle(GetRequestListQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Requests
				.Include(r => r.Manager)
				.Include(r => r.Client)
				.ToListAsync(cancellationToken);
        }
    }
}
