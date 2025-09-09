using MediatR;
using Application.Interfaces;
using Application.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Experiences.Commands.UpdateExperience
{
    public class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, Experience>
    {
        private readonly IDbContext _dbContext;

        public UpdateExperienceCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Experience> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {
            var exp = await _dbContext.Experiences.Include(e => e.Workers).FirstOrDefaultAsync(exp => exp.ExperienceId == request.ExperienceId, cancellationToken) ?? 
                throw new NotFoundException(nameof(Experience), request.ExperienceId.ToString());
            
            if (request.MainWorkerId != null)
            {
				var dbMainWorker = await _dbContext.Workers.FirstOrDefaultAsync(w => w.WorkerId == request.MainWorkerId.Value, cancellationToken) ?? 
                    throw new NotFoundException(nameof(Worker), request.MainWorkerId.Value.ToString());

                exp.MainWorker = dbMainWorker;
                exp.MainWorkerId = dbMainWorker.WorkerId;
			}

			if (request.WorkersId != null)
            {
                exp.Workers.Clear();

                foreach (var worker in request.WorkersId)
                {
                    var dbWorker = await _dbContext.Workers.FirstOrDefaultAsync(w => w.WorkerId == worker, cancellationToken) ??
                        throw new NotFoundException(nameof(Worker), worker.ToString());

                    exp.Workers.Add(dbWorker);
                }
            }

            if (request.Date != null)
            {
                exp.Date = request.Date.Value;
            }

            if (request.Garant != null)
            {
                exp.Garant = request.Garant.Value;
            }

            if (!string.IsNullOrEmpty(request.WorkPlan))
            {
                exp.WorkPlan = request.WorkPlan;
            }

            if (!string.IsNullOrEmpty(request.DoneWork))
            {
                exp.DoneWork = request.DoneWork;
            }

            if (!string.IsNullOrEmpty(request.UsedMaterials))
            {
                exp.UsedMaterials = request.UsedMaterials;
            }

            if (request.IsWorkDone != null)
            {
                exp.IsWorkDone = request.IsWorkDone.Value;
            }

            if (!string.IsNullOrEmpty(request.RemainWork))
            {
                exp.RemainWork = request.RemainWork;
            }

            if (request.IsWorkPlaceClean != null)
            {
                exp.IsWorkPlaceClean = request.IsWorkPlaceClean.Value;
            }

            if (request.IsTaskAccepted != null)
            {
                exp.IsTaskAccepted = request.IsTaskAccepted.Value;
            }

            if (request.TimeStart != null)
            {
                exp.TimeStart = request.TimeStart.Value;
            }

            if (request.TimeEnd != null)
            {
                exp.TimeEnd = request.TimeEnd.Value;
            }

			if (request.ReportState != null)
			{
				exp.ReportState = request.ReportState.Value;
			}

            await _dbContext.SaveChangesAsync(cancellationToken);

            return exp;
        }
    }
}
