using Application.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reports.Commands.DeleteReport
{
    public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, Unit>
    {
        private readonly IDbContext _dbContext;

        public DeleteReportCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteReportCommand command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Reports.FirstOrDefaultAsync(r => r.ReportId == command.Id, cancellationToken) ?? 
                throw new NotFoundException(nameof(Report), command.Id.ToString());

			_dbContext.Reports.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
