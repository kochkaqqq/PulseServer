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
			//services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
			//var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION") ?? throw new Exception("Connection string is not defined");
			services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));
			services.AddScoped<IDbContext>(provider => provider.GetService<ApplicationContext>());
		}
	}
}
