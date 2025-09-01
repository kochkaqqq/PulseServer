using Domain.DTO;
using MediatR;

namespace Application.Experiences.Queries.GetExperienceMobileList
{
	public class GetExperienceMobileListQuery : IRequest<List<ExperienceListElementDTO>>
	{
		public DateTimeOffset Date { get; set; }
		public int WorkerId { get; set; }
	}
}
