using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ApiKeys.CheckKey
{
	public class CheckApiKeyQueryHandler : IRequestHandler<CheckApiKeyQuery, bool>
	{
		private readonly IDbContext _dbContext;

		public CheckApiKeyQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<bool> Handle(CheckApiKeyQuery request, CancellationToken cancellationToken)
		{
			if (!Guid.TryParse(request.ApiKey, out var guidKey))
			{
				return false;
			}

			var manager = await _dbContext.Managers.AsNoTracking().FirstOrDefaultAsync(m => m.ApiKey == request.ApiKey, cancellationToken);
			if (manager != null)
			{
				return true;
			}

			var worker = await _dbContext.Workers.AsNoTracking().FirstOrDefaultAsync(w => w.ApiKey == request.ApiKey, cancellationToken);
			if (worker != null)
			{
				return true;
			}

			return false;
		}
	}
}
