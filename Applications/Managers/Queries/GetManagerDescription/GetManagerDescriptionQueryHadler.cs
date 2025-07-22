using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Domain.DTO;

namespace Application.Managers.Queries.GetManagerDescription
{
    public class GetManagerDescriptionQueryHadler : IRequestHandler<GetManagerDescriptionQuery, ManagerDTO>
    {
        private readonly IDbContext _dbContext;

        public GetManagerDescriptionQueryHadler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<ManagerDTO> Handle(GetManagerDescriptionQuery request, CancellationToken cancellationToken)
        {
            var manager = await _dbContext.Managers
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(entity => entity.ManagerId == request.Id, cancellationToken) ??
                    throw new NotFoundException(nameof(Manager), request.Id.ToString());

            return new() { Id = manager.ManagerId, Name = manager.Name, Description = manager.Description };
        }
    }
}
