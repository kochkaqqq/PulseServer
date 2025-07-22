using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Experiences.Queries.GetExperienceFile
{
	public class GetExperienceFilesQuery : IRequest<List<Document>>
	{
		public int ExperienceId { get; set; }
	}
}
