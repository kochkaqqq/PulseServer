using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Managers.Queries.CheckApiKey
{
	public class CheckApiKeyQueryHandler : IRequestHandler<CheckApiKeyQuery, bool>
	{
		public readonly IDbContext _dbContext;

		public CheckApiKeyQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<bool> Handle(CheckApiKeyQuery request, CancellationToken cancellationToken)
		{
			return await _dbContext.Managers.AsNoTracking().FirstOrDefaultAsync(m => m.ApiKey == request.ApiKey, cancellationToken) != null;
		}
	}
}
