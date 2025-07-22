using Application.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reports.Commands.UpdateReport
{
    public class UpdateReportCommandHadler : IRequestHandler<UpdateReportCommand, Report>
    {
        private readonly IDbContext _dbContext;

        public UpdateReportCommandHadler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Report> Handle(UpdateReportCommand command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Reports.FirstOrDefaultAsync(r => r.ReportId == command.Id, cancellationToken) ?? 
                throw new NotFoundException(nameof(Report), command.Id.ToString());

            if (command.StartTime != null)
				entity.StartTime = command.StartTime.Value;

            if (command.EndTime != null)
				entity.EndTime = command.EndTime.Value;

            if (command.UsedMaterials != null)
				entity.UsedMaterials = command.UsedMaterials;

            if (command.DoneWork != null)
                entity.DoneWork = command.DoneWork;

            if (command.IsWorkDone != null)
				entity.IsWorkDone = command.IsWorkDone.Value;

            if (command.RemainWork != null)
                entity.RemainWork = command.RemainWork;

			if (command.IsWorkplaceClean != null)
				entity.IsWorkplaceClean = command.IsWorkplaceClean.Value;

            if (command.IsWorkAccept != null)
				entity.IsWorkAccept = command.IsWorkAccept.Value;

			if (command.MediaIds != null)
				entity.MediaIds = command.MediaIds;

            entity.UpdateOn = command.UpdateOn;

			await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
