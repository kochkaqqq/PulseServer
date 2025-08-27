using Application.Interfaces;
using Domain;
using Domain.DTO.Reports;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reports.Queries.GetReportListByClientFilter
{
	public class GetReportListByClientFilterQueryHandler : IRequestHandler<GetReportListByClientFilterQuery, List<ReportListDTO>>
	{
		private readonly IDbContext _dbContext;
		
		public GetReportListByClientFilterQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<ReportListDTO>> Handle(GetReportListByClientFilterQuery request, CancellationToken cancellationToken)
		{
			var predicate = PredicateBuilder.New<Report>(true);

			if (request.ReportFilter != null && request.ReportFilter.ClientId != null)
				predicate = predicate.And(r => r.Experience.Request.Client.ClientId == request.ReportFilter.ClientId.Value);

			if (request.ReportFilter != null && request.ReportFilter.WorkerId != null)
				predicate = predicate.And(r => r.Worker.WorkerId == request.ReportFilter.WorkerId);

			if (request.ReportFilter != null && request.ReportFilter.ReportState != null)
				predicate = predicate.And(r => r.Experience.ReportState == request.ReportFilter.ReportState);

			var res = await _dbContext.Reports.AsNoTracking()
				.Include(r => r.Experience)
				.Include(r => r.Experience.Request)
				.Include(r => r.Experience.Request.Client)
				.Include(r => r.Worker)
				.Where(predicate)
				.OrderByDescending(r => r.Date)
				.Select(r => new ReportListDTO()
				{
					ReportId = r.ReportId,
					Worker = r.Worker,
					Date = r.Date,
					MediaCount = r.MediaIds.Count(),
					IsWorkDone = r.IsWorkDone,
					ClientName = r.Experience.Request.Client.Name,
				})
				.Skip(request.CountOnPage * (request.Page - 1))
				.Take(request.CountOnPage)
				.ToListAsync(cancellationToken);

			return res;			
		}
	}
}
