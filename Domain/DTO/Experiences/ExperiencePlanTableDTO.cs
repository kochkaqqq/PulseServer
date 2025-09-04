namespace Domain.DTO.Experiences
{
	public class ExperiencePlanTableDTO
	{
		public int ExperienceId { get; set; }
		public DateTime Date { get; set; }
		public string ClientName { get; set; } = string.Empty;
		public DateTime? StartTime { get; set; }
		public string WorkerList { get; set; } = string.Empty;
		public string ReasonRequest { get; set; } = string.Empty;
		public string WorkPlan { get; set; } = string.Empty;
	}
}
