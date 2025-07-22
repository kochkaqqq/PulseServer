using Application.Interfaces;
using Domain.DTO.Reports;
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
			var res = await _dbContext.Reports.AsNoTracking()
				.Include(r => r.Experience)
				.Include(r => r.Experience.Request)
				.Include(r => r.Experience.Request.Client)
				.Include(r => r.Worker)
				.Where(r => r.Experience.Request.Client.ClientId == request.ReportFilter.ClientId)
				.OrderByDescending(r => r.Date)
				.Select(r => new ReportListDTO()
				{
					ReportId = r.ReportId,
					Worker = r.Worker,
					Date = r.Date,
					MediaCount = r.MediaIds.Count(),
					IsWorkDone = r.IsWorkDone,

				})
				.Skip(request.CountOnPage * (request.Page - 1))
				.Take(request.CountOnPage)
				.ToListAsync(cancellationToken);

			return res;			
		}
	}
}
