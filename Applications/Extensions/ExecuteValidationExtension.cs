using Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Application.Extensions
{
	public static class ExecuteValidationExtension 
	{
		public static IEnumerable<ValidationResult> ExecuteValidation(this IDbContext dbContext)
		{
			var result = new List<ValidationResult>();
			foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
			{
				var entity = entry.Entity;
				var context = new ValidationContext(entity);
				var entityErrors = new List<ValidationResult>();
				if (!Validator.TryValidateObject(entity, context, entityErrors, true))
				{
					result.AddRange(entityErrors);
				}
			}
			return result;
		}
	}
}
