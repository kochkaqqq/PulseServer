using Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace Application.Extensions
{
	public static class SaveChangesWithValidationExstension
	{
		public static async Task<IEnumerable<ValidationResult>> SaveChangesWithValidationAsync(this IDbContext dbContext, CancellationToken cancellationToken)
		{
			var result = dbContext.ExecuteValidation();

			if (result.Any()) return result;

		 	await dbContext.SaveChangesAsync(cancellationToken);

			return result;
		}
	}
}
