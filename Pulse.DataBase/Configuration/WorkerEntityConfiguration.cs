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

			builder.HasKey(w => w.WorkerId);

			builder.Property(w => w.WorkerId)
				.UseIdentityColumn()
				.HasIdentityOptions(startValue: 37);

			builder
				.HasIndex(e => e.Name)
				.IsUnique();

			builder
				.Property(e => e.Name)
				.HasMaxLength(128);
		}
	}
}
