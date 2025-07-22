using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Application.Reports.Commands.UpdateReportStatus
{
	public class UpdateReportStatusCommandHandler : IRequestHandler<UpdateReportStatusCommand, Report>
	{
		private readonly IDbContext _dbContext;

		public UpdateReportStatusCommandHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Report> Handle(UpdateReportStatusCommand request, CancellationToken cancellationToken)
		{
			var report = await _dbContext.Reports.FirstOrDefaultAsync(r => r.ReportId == request.ReportId, cancellationToken) ?? 
				throw new NotFoundException(nameof(Report), request.ReportId.ToString());
			
			report.Status = request.ReportStatus;

			await _dbContext.SaveChangesAsync(cancellationToken);
			return report;
		}
	}
}
