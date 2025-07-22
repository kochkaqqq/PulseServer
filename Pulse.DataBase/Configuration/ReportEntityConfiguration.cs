using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Office.DataBase.Configuration
{
	public class ReportEntityConfiguration : IEntityTypeConfiguration<Report>
	{
		public void Configure(EntityTypeBuilder<Report> builder)
		{
			builder.HasQueryFilter(r => !r.IsArchive);

			builder.Property(r => r.MediaIds)
				.HasConversion(
					m => JsonSerializer.Serialize(m, new JsonSerializerOptions()),
					m => JsonSerializer.Deserialize<string[]>(m, new JsonSerializerOptions())
				);

			builder.Property(r => r.MediaIds)
				.HasColumnType("nvarchar(max)");
		}
	}
}
