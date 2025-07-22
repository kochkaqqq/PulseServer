using Application.Interfaces;
using Domain.enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommonQueries.GetHomePageStats
{
	public class GetHomePageStatsQueryHandler : IRequestHandler<GetHomePageStatsQuery, Dictionary<StatType, int>>
	{
		private readonly IDbContext _dbContext;

		public GetHomePageStatsQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Dictionary<StatType, int>> Handle(GetHomePageStatsQuery request, CancellationToken cancellationToken)
		{
			var res = new Dictionary<StatType, int>();

			var reqWithoutDocCount = await _dbContext.Requests.AsNoTracking().Include(r => r.Document).Where(r => r.Document == null).CountAsync(cancellationToken);
			res.Add(StatType.RequestWithoutDocument, reqWithoutDocCount);

			var tomorrowExps = await _dbContext.Experiences.AsNoTracking().Include(e => e.Workers).Where(e => e.Date.Date == DateTime.Now.Date).ToListAsync(cancellationToken);
			var tomorrowExpWithoutWorkersCount = tomorrowExps.Where(r => r.Workers == null || r.Workers.Count == 0).Count();
			res.Add(StatType.TomorrowExpWithoutWorkers, tomorrowExpWithoutWorkersCount);

			var uncheckedReportsCount = await _dbContext.Reports.AsNoTracking().Where(r => r.Status == ReportStatus.WaitingForAccept).CountAsync(cancellationToken);
			res.Add(StatType.UncheckedReports, uncheckedReportsCount);

			var expsWithoutReport = await _dbContext.Experiences.AsNoTracking().Include(e => e.Reports).ToListAsync(cancellationToken);
			var expsWithoutReportCount = expsWithoutReport.Where(r => r.Reports == null || r.Reports.Count == 0).Count();
			res.Add(StatType.ExperienceWithoutReport, expsWithoutReportCount);

			return res;
		}
	}
}
