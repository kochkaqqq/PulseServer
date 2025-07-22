using Domain.DTO;
using Domain.DTO.Clients;
using Domain.DTO.Requests;

namespace Domain.Filters
{
	public class ExperienceFilter
	{
		public ClientSelectionDTO? Client { get; set; } = null;
		public RequestSelectionDTO? Request { get; set; } = null;
		public WorkerDTO? MainWorker { get; set; } = null;
		public List<WorkerDTO>? Workers { get; set; } = null;
		public DateTime? FromDate { get; set; } = null;
		public DateTime? ToDate { get; set; } = null;
		public bool? Garant { get; set; } = null;
		public string WorkPlan { get; set; } = string.Empty;
		public string DoneWork { get; set; } = string.Empty;
		public string UsedMaterials { get; set; } = string.Empty;
		public bool? IsWorkDone { get; set; } = null;
		public string RemainWork { get; set; } = string.Empty;
		public bool? IsWorkPlaceClean { get; set; } = null;
		public bool? IsTaskAccepted { get; set; } = null;
	}
}
