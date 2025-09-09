using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Office.DataBase.Configuration
{
	public class RequestEntityConfiguration : IEntityTypeConfiguration<Request>
	{
		public void Configure(EntityTypeBuilder<Request> builder)
		{
			builder.HasQueryFilter(r => !r.IsArchive);

			builder.HasKey(r => r.RequestId);

			builder.Property(r => r.RequestId)
				.UseIdentityColumn()
				.HasIdentityOptions(startValue: 795);

			builder.HasOne(r => r.Document).WithMany().HasForeignKey(r => r.DocumentId).OnDelete(DeleteBehavior.SetNull);

			builder.HasMany(r => r.Files).WithMany(d => d.Requests);

			builder.Property(r => r.Date)
				.HasColumnType("date")
				.IsRequired();
		}
	}
}
