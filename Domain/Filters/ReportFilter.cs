using Domain.enums;

namespace Domain.Filters
{
	public class ReportFilter
	{
		public int? ClientId { get; set; }
		public int? WorkerId { get; set; }
		public ReportState? ReportState { get; set; }
	}
}
