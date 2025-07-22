using Domain.DTO;
using Domain.Filters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Experiences.Queries.GetExperienceDTOList
{
	public class GetExperinceDTOListQuery : IRequest<Tuple<IEnumerable<ExperienceListElementDTO>, int>>
	{
		public int Page { get; set; } = 1;
		public int PageEntityCount { get; set; } = 25;
		public ExperienceFilter ExperienceFilter { get; set; } = new();
	}
}
