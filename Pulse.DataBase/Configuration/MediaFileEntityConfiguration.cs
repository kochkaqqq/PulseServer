using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pulse.DataBase.Configuration
{
	public class MediaFileEntityConfiguration : IEntityTypeConfiguration<MediaFile>
	{
		public void Configure(EntityTypeBuilder<MediaFile> builder)
		{
			builder.HasKey(m => m.Name);

			builder.HasOne(m => m.Report).WithMany(m => m.MediaFiles);

			builder.Ignore(m => m.File);
		}
	}
}
