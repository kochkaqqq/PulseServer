using Domain.DTO;
using MediatR;

namespace Application.Experiences.Queries.GetExperienceMobileList
{
	public class GetExperienceMobileListQuery : IRequest<List<ExperienceListElementDTO>>
	{
		public DateTime Date { get; set; }
		public int WorkerId { get; set; }
	}
}
