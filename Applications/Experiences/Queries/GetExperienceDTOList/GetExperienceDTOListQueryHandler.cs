using Application.Interfaces;
using Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

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
			var filter = request.ExperienceFilter;

			var exps = await _dbContext.Experiences.AsNoTracking().Where(e => e.ExperienceId <= 1826 && e.ExperienceId >= 1814).ToListAsync();

			foreach (var experience in exps)
			{
				var expDate = experience.Date;
				if (filter.FromDate.HasValue)
				{
					var filterFrom = filter.FromDate.Value.Date;
				}
				if (filter.ToDate.HasValue)
				{
					var filterTo = filter.ToDate.Value.Date;
				}
			}

			var arr = _dbContext.Experiences
				.AsNoTracking()
				.Include(e => e.Request)
				.Include(e => e.Request.Client)
				.Include(e => e.Workers)
				.Where(e => (filter.Client == null || e.Request.Client.ClientId == filter.Client.ClientId) &&
					(filter.Request == null || filter.Request.RequestId == 0 || e.Request.RequestId == filter.Request.RequestId) &&
					(filter.MainWorker == null || e.MainWorkerId == filter.MainWorker.Id) &&
					(filter.FromDate == null || e.Date >= filter.FromDate.Value.Date) &&
					(filter.ToDate == null || e.Date <= filter.ToDate.Value.Date) &&
					(filter.Garant == null || e.Garant == filter.Garant) &&
					(filter.WorkPlan == string.Empty || e.WorkPlan.ToLower().Contains(filter.WorkPlan.ToLower())) &&
					(filter.DoneWork == string.Empty || e.DoneWork.ToLower().Contains(filter.DoneWork.ToLower())) &&
					(filter.UsedMaterials == string.Empty || e.UsedMaterials.ToLower().Contains(filter.UsedMaterials.ToLower())) &&
					(filter.IsWorkDone == null || e.IsWorkDone == filter.IsWorkDone) &&
					(filter.RemainWork == string.Empty || e.RemainWork.ToLower().Contains(filter.RemainWork.ToLower())) &&
					(filter.IsWorkPlaceClean == null || e.IsWorkPlaceClean == filter.IsWorkPlaceClean) &&
					(filter.IsTaskAccepted == null || e.IsTaskAccepted == filter.IsTaskAccepted) &&
					(filter.ReportState == null || e.ReportState == filter.ReportState)
					);

			if (filter != null && filter.Workers != null && filter.Workers.Count > 0)
			{
				foreach (var worker in filter.Workers)
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
					WorkPlan = x.WorkPlan,
					ReportState = x.ReportState,
				}
				).ToArrayAsync(cancellationToken);

			return new Tuple<IEnumerable<ExperienceListElementDTO>, int>(res, 0);
		}
	}
}
