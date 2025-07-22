using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Office.DataBase;

namespace WebApi.DependencyInjections
{
	public static class InjectDataBase
	{
		public static void AddDataBase(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("WebApiDatabase");
			services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
			services.AddScoped<IDbContext>(provider => provider.GetService<ApplicationContext>());
		}
	}
}
