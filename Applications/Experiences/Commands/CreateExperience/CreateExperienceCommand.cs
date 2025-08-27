using Domain;
using MediatR;
using static Domain.Experience;

namespace Application.Experiences.Commands.CreateExperience
{
    public class CreateExperienceCommand : IRequest<Experience>
    {
        public int? RequestId { get; set; } = null!;
        public int? MainWorkerId { get; set; }
        public IEnumerable<int> WorkersId { get; set; } = null!;
        public DateTimeOffset Date { get; set; }
        public bool Garant { get; set; }
        public string? WorkPlan { get; set; } = string.Empty;
        public string? DoneWork { get; set; } = string.Empty;
		public string? UsedMaterials { get; set; } = string.Empty;
		public bool IsWorkDone { get; set; } = false;
		public string? RemainWork { get; set; } = string.Empty;
		public bool IsWorkPlaceClean { get; set; }
		public bool IsTaskAccepted { get; set; }
		public DateTimeOffset? TimeStart { get; set; }
        public DateTimeOffset? TimeEnd { get; set; }
        
    }
}
