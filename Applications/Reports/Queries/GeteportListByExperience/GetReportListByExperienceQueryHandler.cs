using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reports.Queries.GeteportListByExperience
{
	public class GetReportListByExperienceQueryHandler : IRequestHandler<GetReportListByExperienceQuery, IEnumerable<Report>>
	{
		private readonly IDbContext _dbContext;

		public GetReportListByExperienceQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Report>> Handle(GetReportListByExperienceQuery request, CancellationToken cancellationToken)
		{
			return await _dbContext.Reports
				.Include(r => r.Worker)
				.Include(r => r.Experience)
				.Where(r => r.Experience.ExperienceId == request.ExperienceId)
				.ToListAsync();
		}
	}
}
