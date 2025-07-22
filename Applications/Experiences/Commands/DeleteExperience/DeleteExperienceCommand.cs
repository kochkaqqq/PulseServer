using MediatR;

namespace Application.Experiences.Commands.DeleteExperience
{
    public class DeleteExperienceCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
