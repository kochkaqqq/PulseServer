using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Objects.Queries.GetClientsQuantity
{
	public class GetClientsQuantityQueryHandler : IRequestHandler<GetClientsQuantityQuery, int>
	{
		private readonly IDbContext _dbContext;

		public GetClientsQuantityQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<int> Handle(GetClientsQuantityQuery request, CancellationToken cancellationToken)
		{
			return await _dbContext.Clients.CountAsync(cancellationToken);
		}
	}
}
