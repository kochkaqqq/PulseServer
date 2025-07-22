using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Office.DataBase.Configuration
{
	public class ExperienceEntityConfiguration : IEntityTypeConfiguration<Experience>
	{
		public void Configure(EntityTypeBuilder<Experience> builder)
		{
			builder.HasQueryFilter(e => !e.IsArchive);

			builder
				.HasOne(e => e.MainWorker)
				.WithMany()
				.HasForeignKey(e => e.MainWorkerId)
				.OnDelete(DeleteBehavior.SetNull);

			builder
				.HasMany(e => e.Workers)
				.WithMany(w => w.Experiencies);


		}
	}
}
