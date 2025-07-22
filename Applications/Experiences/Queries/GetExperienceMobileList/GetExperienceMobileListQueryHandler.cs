using Application.Interfaces;
using Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Experiences.Queries.GetExperienceMobileList
{
	public class GetExperienceMobileListQueryHandler : IRequestHandler<GetExperienceMobileListQuery, List<ExperienceListElementDTO>>
	{
		private readonly IDbContext _dbContext;

		public GetExperienceMobileListQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<ExperienceListElementDTO>> Handle(GetExperienceMobileListQuery request, CancellationToken cancellationToken)
		{
			return await _dbContext.Experiences
				.AsNoTracking()
				.Include(e => e.Request)
				.Include(e => e.Request.Client)
				.Include(e => e.Workers)
				.Where(e =>
					(request.Date.Date == e.Date.Date) &&
					(e.Workers.Select(w => w.WorkerId).Contains(request.WorkerId)))
				.Select(e => new ExperienceListElementDTO()
				{
					ExperienceId = e.ExperienceId,
					Date = e.Date,
					ClientName = e.Request.Client.Name,
					ReasonRequest = e.Request.ReasonRequest,
					StartTime = e.TimeStart,
					WorkerList = string.Join(", ", e.Workers.Select(w => w.Name)),
					WorkPlan = e.WorkPlan
				}).ToListAsync(cancellationToken);
		}
	}
}
