using Application.Interfaces;
using Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Managers.Queries.GetManagerList
{
    public class GetManagerListQueryHandler : IRequestHandler<GetManagerListQuery, IEnumerable<ManagerDTO>>
    {
        private readonly IDbContext _dbContext;

        public GetManagerListQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<ManagerDTO>> Handle(GetManagerListQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Managers
                .AsNoTracking()
                .Select(m => new ManagerDTO() { Id = m.ManagerId, Name = m.Name, Description = m.Description })
                .ToArrayAsync(cancellationToken);
        }
    }
}
