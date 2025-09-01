using Application.Interfaces;
using Domain.DTO.Experiences;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Experiences.Queries.GetExperienceGanttListByDate
{
	public class GetExperienceGanttListByDateQueryHandler : IRequestHandler<GetExperienceGanttListByDateQuery, List<ExperienceGantDTO>>
	{
		private readonly IDbContext _dbContext;

		public GetExperienceGanttListByDateQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<ExperienceGantDTO>> Handle(GetExperienceGanttListByDateQuery request, CancellationToken cancellationToken)
		{
			var res = await _dbContext.Experiences
				.AsNoTracking()
				.Include(exp => exp.Workers)
				.Where(exp => exp.Date.UtcDateTime == request.Date.UtcDateTime)
				.Select(exp => new ExperienceGantDTO() { ExperienceId = exp.ExperienceId, WorkerIds = exp.Workers.Select(w => w.WorkerId), TimeStart = exp.TimeStart, TimeEnd = exp.TimeEnd })
				.ToListAsync(cancellationToken);

			return res;
		}
	}
}
