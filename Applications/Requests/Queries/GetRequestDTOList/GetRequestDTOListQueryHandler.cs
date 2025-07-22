using Application.Interfaces;
using Domain.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.enums;
using Domain;

namespace Application.Requests.Queries.GetRequestDTOList
{
	public class GetRequestDTOListQueryHandler : IRequestHandler<GetRequestDTOListQuery, Tuple<IEnumerable<RequestListElementDTO>, int>>
	{
		private readonly IDbContext _dbContext;

		public GetRequestDTOListQueryHandler(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Tuple<IEnumerable<RequestListElementDTO>, int>> Handle(GetRequestDTOListQuery request, CancellationToken cancellationToken)
		{
			var arr = _dbContext.Requests
				.Include(r => r.Client)
				.Include(r => r.Manager)
				.Include(r => r.Document)
				.Where(r => (request.RequestFilter.ClientId == null || r.Client.ClientId == request.RequestFilter.ClientId) &&
					(request.RequestFilter.ReasonRequest == string.Empty || r.ReasonRequest.ToLower().Contains(request.RequestFilter.ReasonRequest.ToLower())) &&
					(request.RequestFilter.NecessaryFunds == string.Empty || r.NecessaryFunds.ToLower().Contains(request.RequestFilter.NecessaryFunds.ToLower())) &&
					(request.RequestFilter.ManagerId == null || r.Manager.ManagerId == request.RequestFilter.ManagerId) &&
					(request.RequestFilter.InternalInfo == string.Empty || r.InternalInfo.ToLower().Contains(request.RequestFilter.InternalInfo.ToLower())) &&
					(request.RequestFilter.Status == Status.None || r.Status == request.RequestFilter.Status) &&
					(request.RequestFilter.WorkResultType == DoneWorkActType.None || r.WorkResultType == request.RequestFilter.WorkResultType) &&
					(request.RequestFilter.FromDate == null || r.Date >= request.RequestFilter.FromDate) &&
					(request.RequestFilter.ToDate == null || r.Date <= request.RequestFilter.ToDate) &&
					(request.RequestFilter.IsDocAttached == null || (request.RequestFilter.IsDocAttached.Value ? r.Document != null : r.Document == null))
				).OrderByDescending(e => e.Date);

			var count = await arr.CountAsync(cancellationToken);
			var data = await arr.Skip(request.PageEntitiesCount * (request.Page - 1))
				.Take(request.PageEntitiesCount)
				.Select(r => new RequestListElementDTO()
				{
					RequestId = r.RequestId,
					Date = r.Date,
					ClientName = r.Client.Name,
					ReasonRequest = r.ReasonRequest,
					Status = r.Status,
				}).ToArrayAsync(cancellationToken);

			return new Tuple<IEnumerable<RequestListElementDTO>, int>(data, count);
		}
	}
}
