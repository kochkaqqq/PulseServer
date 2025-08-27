using Application.Interfaces;
using Domain.DTO.Experiences;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Experiences.Queries.GetExperiencePlanTableDTOList
{
	public class GetExperiencePlanTableDTOListQueryHandler : IRequestHandler<GetExperiencePlanTableDTOListQuery, List<ExperiencePlanTableDTO>>
	{
		private readonly IDbContext dbContext;

		public GetExperiencePlanTableDTOListQueryHandler(IDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<List<ExperiencePlanTableDTO>> Handle(GetExperiencePlanTableDTOListQuery request, CancellationToken cancellationToken)
		{
			var res = await dbContext.Experiences
				.AsNoTracking()
				.Include(e => e.Request)
				.Include(e => e.Request.Client)
				.Include(e => e.Workers)
				.Where(e => e.Date.Date == request.Date)
				.Select(e => new ExperiencePlanTableDTO()
				{
					ExperienceId = e.ExperienceId,
					Date = e.Date,
					ClientName = e.Request.Client.Name,
					StartTime = e.TimeStart,
					WorkerList = string.Join(", ", e.Workers.Select(w => w.Name)),
					ReasonRequest = e.Request.ReasonRequest,
					WorkPlan = e.WorkPlan
				}).ToListAsync(cancellationToken);

			return res;
		}
	}
}
