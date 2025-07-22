using Application.Interfaces;
using Domain.DTO.Clients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Objects.Queries.GetSelectionClientList
{
	public class GetSelectionClientListQueryHandler : IRequestHandler<GetSelectionClientListQuery, IEnumerable<ClientSelectionDTO>>
	{
		private readonly IDbContext _dbContext;

		public GetSelectionClientListQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<ClientSelectionDTO>> Handle(GetSelectionClientListQuery request, CancellationToken cancellationToken)
		{
			return await _dbContext.Clients
				.AsNoTracking()
				.Select(c => new ClientSelectionDTO()
				{
					ClientId = c.ClientId,
					Name = c.Name,
				}).ToArrayAsync(cancellationToken);
		}
	}
}
