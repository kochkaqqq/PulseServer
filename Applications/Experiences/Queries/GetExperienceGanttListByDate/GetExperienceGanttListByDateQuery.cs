using Domain.DTO.Experiences;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Experiences.Queries.GetExperienceGanttListByDate
{
	public class GetExperienceGanttListByDateQuery : IRequest<List<ExperienceGantDTO>>
	{
		public DateTimeOffset Date { get; set; }
	}
}
