using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Application.Reports.Commands.CreateReport
{
    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, Report>
    {
        private readonly IDbContext _dbContext;

        public CreateReportCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Report> Handle(CreateReportCommand command, CancellationToken cancellationToken)
        {
            var dbWorker = await _dbContext.Workers.FirstOrDefaultAsync(w => w.WorkerId == command.WorkerId) ??
                throw new NotFoundException(nameof(Worker), command.WorkerId.ToString());

            var dbExperience = await _dbContext.Experiences.FirstOrDefaultAsync(e => e.ExperienceId == command.ExperienceId) ?? 
                throw new NotFoundException(nameof(Experience), command.ExperienceId.ToString());

            var entity = new Report()
            {
                Worker = dbWorker,
                Experience = dbExperience,
                Date = command.Date,
                StartTime = command.StartTime,
                EndTime = command.EndTime,
                UsedMaterials = command.UsedMaterials,
                DoneWork = command.DoneWork,
                IsWorkDone = command.IsWorkDone,
                RemainWork = command.RemainWork,
                IsWorkplaceClean = command.IsWorkplaceClean,
                IsWorkAccept = command.IsWorkAccept,
                UpdateOn = command.Date,
				MediaIds = Array.Empty<string>(),
				MediaFiles = new()
			};

            await _dbContext.Reports.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
