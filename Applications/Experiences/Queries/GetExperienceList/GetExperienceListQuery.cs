using MediatR;
using Domain;

namespace Application.Experiences.Queries.GetExperienceList
{
    public class GetExperienceListQuery : IRequest<List<Experience>>
    {
    }
}
