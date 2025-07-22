using Application.Interfaces;
using Domain.DTO.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Queries.GetRequestSelectionList
{
	public class GetRequestSelectionListQueryHandler : IRequestHandler<GetRequestSelectionListQuery, IEnumerable<RequestSelectionDTO>>
	{
		private readonly IDbContext _dbContext;

		public GetRequestSelectionListQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<RequestSelectionDTO>> Handle(GetRequestSelectionListQuery request, CancellationToken cancellationToken)
		{
			return await _dbContext.Requests
				.Include(r => r.Client)
				.AsNoTracking()
				.Select(r => new RequestSelectionDTO()
				{
					RequestId = r.RequestId,
					ClientId = r.Client.ClientId,
					ClientName = r.Client.Name,
					RequestTitle = r.ReasonRequest,
				}).ToArrayAsync(cancellationToken);
		}
	}
}
