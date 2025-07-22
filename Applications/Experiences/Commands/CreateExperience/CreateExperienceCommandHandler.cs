using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Application.Experiences.Commands.CreateExperience
{
    public class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, Experience>
    {
        private readonly IDbContext _dbContext;

        public CreateExperienceCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Experience> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {
            var req = await _dbContext.Requests.FirstOrDefaultAsync(r => r.RequestId == request.RequestId, cancellationToken) ?? 
                throw new NotFoundException(nameof(Experience.Request), request.RequestId.ToString());

			Worker? mainWorker = null;
            if (request.MainWorkerId != null)
				mainWorker = await _dbContext.Workers.FirstOrDefaultAsync(w => w.WorkerId == request.MainWorkerId, cancellationToken) ?? 
                    throw new NotFoundException(nameof(Worker), request.MainWorkerId.ToString());

            var workers = new List<Worker>();
            foreach (var worker in request.WorkersId)
            {
                var dbWorker = await _dbContext.Workers.FirstOrDefaultAsync(w => w.WorkerId == worker, cancellationToken) ?? 
                    throw new NotFoundException(nameof(Worker), worker.ToString());
                workers.Add(dbWorker);
			}

			var entity = new Experience()
            {
                Request = req,
                MainWorkerId = request.MainWorkerId,
                MainWorker = mainWorker,
                Workers = workers,
                Date = request.Date,
                Garant = request.Garant,
                DoneWork = request.DoneWork ?? string.Empty,
                UsedMaterials = request.UsedMaterials ?? string.Empty,
				IsWorkDone = request.IsWorkDone,
				RemainWork = request.RemainWork ?? string.Empty,
				IsWorkPlaceClean = request.IsWorkPlaceClean,
                IsTaskAccepted = request.IsTaskAccepted,
				TimeStart = request.TimeStart,
                TimeEnd = request.TimeEnd,
            };

            await _dbContext.Experiences.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
