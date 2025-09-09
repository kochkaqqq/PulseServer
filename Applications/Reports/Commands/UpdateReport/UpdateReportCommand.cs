using Domain;
using MediatR;
using System;
namespace Application.Reports.Commands.UpdateReport
{
    public class UpdateReportCommand : IRequest<Report>
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? UsedMaterials { get; set; } = null;
        public string? DoneWork { get; set; } = null;
        public bool? IsWorkDone { get; set; }
        public string? RemainWork { get; set; } = null;
        public bool? IsWorkplaceClean { get; set; }
        public bool? IsWorkAccept { get; set; }
        public DateTime UpdateOn { get; set; }
    }
}
