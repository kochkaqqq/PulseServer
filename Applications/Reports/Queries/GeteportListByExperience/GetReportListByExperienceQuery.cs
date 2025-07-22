using Domain;
using MediatR;

namespace Application.Reports.Queries.GeteportListByExperience
{
	public class GetReportListByExperienceQuery : IRequest<IEnumerable<Report>>
	{
		public int ExperienceId { get; set; }
	}
}
