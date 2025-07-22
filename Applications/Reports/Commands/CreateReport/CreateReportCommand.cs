using Domain;
using MediatR;

namespace Application.Reports.Commands.CreateReport
{
    public class CreateReportCommand : IRequest<Report>
    {
        public int WorkerId { get; set; }
        public int ExperienceId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string UsedMaterials { get; set; } = string.Empty;
        public string DoneWork { get; set; } = string.Empty;
        public bool IsWorkDone { get; set; }
        public string RemainWork { get; set; } = string.Empty;
        public bool IsWorkplaceClean { get; set; }
        public bool IsWorkAccept { get; set; }
		public string[] MediaIds { get; set; }
    }
}
