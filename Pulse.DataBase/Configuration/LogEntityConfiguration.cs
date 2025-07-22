using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Office.DataBase.Configuration
{
	public class LogEntityConfiguration : IEntityTypeConfiguration<Log>
	{
		public void Configure(EntityTypeBuilder<Log> builder)
		{
			
		}
	}
}
