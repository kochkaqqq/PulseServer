using Application.Interfaces;
using Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Objects.Queries.GetObjectDTOList
{
	public class GetClientDTOListQueryHandler : IRequestHandler<GetClientDTOListQuery, Tuple<IEnumerable<ClientListElementDTO>, int>>
	{
		private readonly IDbContext _dbContext;

		public GetClientDTOListQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Tuple<IEnumerable<ClientListElementDTO>, int>> Handle(GetClientDTOListQuery request, CancellationToken cancellationToken)
		{
			var arr = _dbContext.Clients
				.AsNoTracking()
				.Where(c => (request.ClientFilter.Name == string.Empty || c.Name.ToLower().Contains(request.ClientFilter.Name.ToLower())) &&
					(request.ClientFilter.Address == string.Empty || c.Address.ToLower().Contains(request.ClientFilter.Address.ToLower())) &&
					(request.ClientFilter.Contact == string.Empty || c.Contact.ToLower().Contains(request.ClientFilter.Contact.ToLower())) &&
					(request.ClientFilter.EMail == string.Empty || c.EMail.ToLower().Contains(request.ClientFilter.EMail.ToLower())) &&
					(request.ClientFilter.Phone == string.Empty || c.Phone.ToLower().Contains(request.ClientFilter.Phone.ToLower())) &&
					(request.ClientFilter.FromDate == null || c.CreatedDate >= request.ClientFilter.FromDate) &&
					(request.ClientFilter.ToDate == null || c.CreatedDate <= request.ClientFilter.ToDate))
				.OrderByDescending(c => c.CreatedDate);

			var count = await arr.CountAsync();
			var res = await arr.Skip(request.PageEntitiesCount * (request.Page - 1))
				.Take(request.PageEntitiesCount)
				.Select(x => new ClientListElementDTO()
				{
					ClientId = x.ClientId,
					Name = x.Name,
					Contact = x.Contact,
				}).ToArrayAsync();

			return new Tuple<IEnumerable<ClientListElementDTO>, int>(res, count);
		}
	}
}
