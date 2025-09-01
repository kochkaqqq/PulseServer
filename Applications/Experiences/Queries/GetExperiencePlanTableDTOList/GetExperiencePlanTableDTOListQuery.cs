using Domain.DTO.Experiences;
using MediatR;

namespace Application.Experiences.Queries.GetExperiencePlanTableDTOList
{
	public class GetExperiencePlanTableDTOListQuery : IRequest<List<ExperiencePlanTableDTO>>
	{
		public DateTimeOffset Date { get; set; }
	}
}
