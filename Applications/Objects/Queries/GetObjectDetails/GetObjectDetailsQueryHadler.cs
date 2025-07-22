using Application.Interfaces;
using MediatR;
using Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Application.Objects.Queries.GetObjectDetails
{
    public class GetObjectDetailsQueryHadler : IRequestHandler<GetObjectDetailsQuery, Client>
    {
        private readonly IDbContext _dbContext;

        public GetObjectDetailsQueryHadler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Client> Handle(GetObjectDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Clients
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(c => c.ClientId == request.Id, cancellationToken) ?? 
                    throw new NotFoundException(nameof(Client), request.Id.ToString());

			return entity;
        }
    }
}
