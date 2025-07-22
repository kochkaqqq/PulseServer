using Domain;
using MediatR;

namespace Application.Experiences.Commands.UpdateExperience
{
    public class UpdateExperienceCommand : IRequest<Experience>
    {
		public int ExperienceId { get; set; }
		public int? MainWorkerId { get; set; } = null;
		public IEnumerable<int>? WorkersId { get; set; } = null;
		public DateTime? Date { get; set; } = null;
		public bool? Garant { get; set; } = null;
		public string? WorkPlan { get; set; } = null;
		public string? DoneWork { get; set; } = null;
		public string? UsedMaterials { get; set; } = null;
		public bool? IsWorkDone { get; set; } = null;
		public string? RemainWork { get; set; } = null;
		public bool? IsWorkPlaceClean { get; set; } = null;
		public bool? IsTaskAccepted { get; set; } = null;
		public DateTime? TimeStart { get; set; } = null;
		public DateTime? TimeEnd { get; set; } = null;

	}
}
