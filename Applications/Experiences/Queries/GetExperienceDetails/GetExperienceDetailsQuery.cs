using MediatR;
using Domain;

namespace Application.Experiences.Queries.GetExperienceDetails
{
    public class GetExperienceDetailsQuery : IRequest<Experience>
    {
        public int Id { get; set; }
    }
}
