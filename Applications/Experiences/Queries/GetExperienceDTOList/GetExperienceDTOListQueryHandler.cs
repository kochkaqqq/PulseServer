using Application.Interfaces;
using Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Experiences.Queries.GetExperienceDTOList
{
	public class GetExperienceDTOListQueryHandler : IRequestHandler<GetExperinceDTOListQuery, Tuple<IEnumerable<ExperienceListElementDTO>, int>>
	{
		private readonly IDbContext _dbContext;

		public GetExperienceDTOListQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Tuple<IEnumerable<ExperienceListElementDTO>, int>> Handle(GetExperinceDTOListQuery request, CancellationToken cancellationToken)
		{
			var arr = _dbContext.Experiences
				.AsNoTracking()
				.Include(e => e.Request)
				.Include(e => e.Request.Client)
				.Include(e => e.Workers)
				.Where(e => (request.ExperienceFilter.Client == null || e.Request.Client.ClientId == request.ExperienceFilter.Client.ClientId) &&
					(request.ExperienceFilter.Request == null || request.ExperienceFilter.Request.RequestId == 0 || e.Request.RequestId == request.ExperienceFilter.Request.RequestId) &&
					(request.ExperienceFilter.Request == null ||
						string.IsNullOrEmpty(request.ExperienceFilter.Request.RequestTitle) ||
						e.Request.ReasonRequest.ToLower().Contains(request.ExperienceFilter.Request.RequestTitle.ToLower())) &&
					(request.ExperienceFilter.MainWorker == null || e.MainWorkerId == request.ExperienceFilter.MainWorker.Id) &&
					(request.ExperienceFilter.FromDate == null || e.Date >= request.ExperienceFilter.FromDate) &&
					(request.ExperienceFilter.ToDate == null || e.Date <= request.ExperienceFilter.ToDate) &&
					(request.ExperienceFilter.Garant == null || e.Garant == request.ExperienceFilter.Garant) &&
					(request.ExperienceFilter.WorkPlan == string.Empty || e.WorkPlan.ToLower().Contains(request.ExperienceFilter.WorkPlan.ToLower())) &&
					(request.ExperienceFilter.DoneWork == string.Empty || e.DoneWork.ToLower().Contains(request.ExperienceFilter.DoneWork.ToLower())) &&
					(request.ExperienceFilter.UsedMaterials == string.Empty || e.UsedMaterials.ToLower().Contains(request.ExperienceFilter.UsedMaterials.ToLower())) &&
					(request.ExperienceFilter.IsWorkDone == null || e.IsWorkDone == request.ExperienceFilter.IsWorkDone) &&
					(request.ExperienceFilter.RemainWork == string.Empty || e.RemainWork.ToLower().Contains(request.ExperienceFilter.RemainWork.ToLower())) &&
					(request.ExperienceFilter.IsWorkPlaceClean == null || e.IsWorkPlaceClean == request.ExperienceFilter.IsWorkPlaceClean) &&
					(request.ExperienceFilter.IsTaskAccepted == null || e.IsTaskAccepted == request.ExperienceFilter.IsTaskAccepted));
				
			if (request.ExperienceFilter != null && request.ExperienceFilter.Workers != null && request.ExperienceFilter.Workers.Count > 0)
			{
				foreach (var worker in request.ExperienceFilter.Workers)
				{
					arr = arr.Where(e => e.Workers.FirstOrDefault(w => w.WorkerId == worker.Id) != null);
				}
			}

			var count = await arr.CountAsync(cancellationToken);
			var res = await arr.OrderByDescending(e => e.Date).Skip(request.PageEntityCount * (request.Page - 1))
				.Take(request.PageEntityCount)
				.Select(x => new ExperienceListElementDTO()
				{
					ExperienceId = x.ExperienceId,
					Date = x.Date,
					ClientName = x.Request.Client.Name,
					ReasonRequest = x.Request.ReasonRequest,
					StartTime = x.TimeStart,
					WorkerList = string.Join(", ", x.Workers.Select(w => w.Name)),
					WorkPlan = x.WorkPlan
				}
				).ToArrayAsync(cancellationToken);

			return new Tuple<IEnumerable<ExperienceListElementDTO>, int>(res, count);
		}
	}
}
