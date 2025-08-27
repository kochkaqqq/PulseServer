using Domain.enums;

namespace Domain.DTO
{
	public class ExperienceListElementDTO
	{
		public int ExperienceId { get; set; }
		public DateTimeOffset Date { get; set; }
		public string ClientName { get; set; } = string.Empty;
		public string ReasonRequest { get; set; } = string.Empty;
		public DateTimeOffset? StartTime { get; set; }
		public string WorkerList { get; set; } = string.Empty;
		public string WorkPlan { get; set; } = string.Empty;
		public ReportState ReportState { get; set; } = ReportState.None;
	}
}
