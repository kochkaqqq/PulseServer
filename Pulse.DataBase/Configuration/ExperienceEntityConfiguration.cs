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

			builder.Property(e => e.Date)
				.HasColumnType("date")
				.IsRequired();

			//builder.Property(e => e.TimeStart)
			//	.HasColumnType("time");

			//builder.Property(e => e.TimeEnd)
			//	.HasColumnType("time");

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
