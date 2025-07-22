using Application.Interfaces;
using Domain;
using MediatR;
using Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Experiences.Queries.GetExperienceDetails
{
    public class GetExperienceDetailsQueryHandler : IRequestHandler<GetExperienceDetailsQuery, Experience>
    {
        private readonly IDbContext _dbContext;

        public GetExperienceDetailsQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Experience> Handle(GetExperienceDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Experiences
                .AsNoTrackingWithIdentityResolution()
                .Include(e => e.Request)
                .Include(e => e.Request.Client)
                .Include(e => e.MainWorker)
                .Include(e => e.Workers)
                .FirstOrDefaultAsync(exp => exp.ExperienceId == request.Id, cancellationToken) ?? 
                    throw new NotFoundException(nameof(Experience), request.Id.ToString());

			return entity;
        }
    }
}
