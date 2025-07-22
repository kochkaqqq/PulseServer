using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reports.Queries.GetReportList
{
    public class GetReportListQueryHandler : IRequestHandler<GetReportListQuery, List<Report>>
    {
        private readonly IDbContext _dbContext;

        public GetReportListQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<Report>> Handle(GetReportListQuery query, CancellationToken cancellationToken)
        {
            var res = await _dbContext.Reports
                .AsNoTracking()
                .Include(r => r.Worker)
                .Include(r => r.Experience)
                .ToListAsync(cancellationToken);

            return res;
        }
    }
}
