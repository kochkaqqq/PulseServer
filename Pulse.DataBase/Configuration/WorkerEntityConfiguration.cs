using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Office.DataBase.Configuration
{
	public class WorkerEntityConfiguration : IEntityTypeConfiguration<Worker>
	{
		public void Configure(EntityTypeBuilder<Worker> builder)
		{
			builder.HasQueryFilter(w => !w.IsArchive);

			builder
				.HasIndex(e => e.Name)
				.IsUnique();

			builder
				.Property(e => e.Name)
				.HasMaxLength(128);
		}
	}
}
