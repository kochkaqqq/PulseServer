using MediatR;
using Application.Interfaces;
using Application.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Experiences.Commands.DeleteExperience
{
    public class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand, Unit>
    {
        private readonly IDbContext _dbContext;

        public DeleteExperienceCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Experiences.FirstOrDefaultAsync(e => e.ExperienceId == request.Id, cancellationToken) ?? 
                throw new NotFoundException(nameof(Experience), request.Id.ToString());

			_dbContext.Experiences.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
	}
}
