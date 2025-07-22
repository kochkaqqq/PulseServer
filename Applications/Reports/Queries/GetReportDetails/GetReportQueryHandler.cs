using Application.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reports.Queries.GetReportDetails
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, Report>
    {
        private readonly IDbContext _dbContext;

        public GetReportQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Report> Handle(GetReportQuery query, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Reports
                .AsNoTrackingWithIdentityResolution()
                .Include(r => r.Worker)
                .Include(r => r.Experience)
                .FirstOrDefaultAsync(r => r.ReportId == query.Id, cancellationToken) ?? 
                    throw new NotFoundException(nameof(Request), query.Id.ToString());
			return entity;
        }
    }
}
