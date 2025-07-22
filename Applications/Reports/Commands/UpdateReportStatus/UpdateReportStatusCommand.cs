using Domain;
using Domain.enums;
using MediatR;

namespace Application.Reports.Commands.UpdateReportStatus
{
	public class UpdateReportStatusCommand : IRequest<Report>
	{
		public int ReportId { get; set; }
		public ReportStatus ReportStatus { get; set; }
	}
}
