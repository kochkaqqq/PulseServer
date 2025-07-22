using Domain.DTO.Experiences;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Experiences.Queries.GetExperienceDTOListPage
{
	public class GetExperienceDTOListPageQuery : IRequest<List<ExperienceMobileDTO>>
	{
	}
}
