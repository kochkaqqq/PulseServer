using Domain.DTO.Reports;
using Domain.Filters;
using MediatR;

namespace Application.Reports.Queries.GetReportListByClientFilter
{
	public class GetReportListByClientFilterQuery : IRequest<List<ReportListDTO>>
	{
		public int Page { get; set; } = 1;
		public int CountOnPage { get; set; } = 5;
		public ReportFilter ReportFilter { get; set; } = null!;
	}
}
