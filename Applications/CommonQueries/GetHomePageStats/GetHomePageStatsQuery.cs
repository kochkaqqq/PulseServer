using Domain.enums;
using MediatR;

namespace Application.CommonQueries.GetHomePageStats
{
	public class GetHomePageStatsQuery : IRequest<Dictionary<StatType, int>>
	{
	}
}
