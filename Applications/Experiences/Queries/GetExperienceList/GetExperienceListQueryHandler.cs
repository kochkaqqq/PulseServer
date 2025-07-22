using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Experiences.Queries.GetExperienceList
{
    public class GetExperienceListQueryHandler : IRequestHandler<GetExperienceListQuery, List<Experience>>
    {
        private readonly IDbContext _dbContext;

        public GetExperienceListQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<Experience>> Handle(GetExperienceListQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Experiences
                .AsNoTracking()
                .Include(e => e.MainWorker)
                .Include(e => e.Workers)
                .Include(e => e.Request)
                .Include(e => e.Request.Client)
                .ToListAsync(cancellationToken);
        }
    }
}
