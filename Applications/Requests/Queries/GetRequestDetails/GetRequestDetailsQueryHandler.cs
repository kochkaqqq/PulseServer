using MediatR;
using Application.Interfaces;
using Application.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Queries.GetRequestDetails
{
    public class GetRequestDetailsQueryHandler : IRequestHandler<GetRequestDetailsQuery, Request>
    {
        private readonly IDbContext _dbContext;

        public GetRequestDetailsQueryHandler(IDbContext dbContext) => _dbContext = dbContext;

        public async Task<Request> Handle(GetRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Requests
				.AsNoTrackingWithIdentityResolution()
                .Include(r => r.Manager)
                .Include(r => r.Client)
                .Include(r => r.Document)
				.Include(r => r.Files)
                .FirstOrDefaultAsync(req => req.RequestId == request.RequestId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Request), request.RequestId.ToString());
            }

			if (entity.Files != null)
				foreach (var file in entity.Files)
					file.File = null;
			if (entity.Document != null)
				entity.Document.File = null;

            return entity;
        }
    }
}
