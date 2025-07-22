using Application.CommonQueries.GetHomePageStats;
using MediatR;

namespace WebApiV2.EndPoints
{
	public static class StatEndPoints
	{
		public static void AddStatsEndPoints(this WebApplication app)
		{
			app.GetHomePageStats();
		}

		private static void GetHomePageStats(this WebApplication app)
		{
			app.MapGet("/stats/HomePage", static async (IMediator mediator, CancellationToken cancellationToken, ILogger<Program> logger) =>
			{
				try
				{
					var res = await mediator.Send(new GetHomePageStatsQuery());
					return Results.Ok(res);
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message);
					return Results.Problem(ex.Message);
				}
			});
		}
	}
}
