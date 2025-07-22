using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Office.DataBase.Configuration
{
	public class DocumentEntityConfiguration : IEntityTypeConfiguration<Document>
	{
		public void Configure(EntityTypeBuilder<Document> builder)
		{
			builder.Property(d => d.File).IsRequired();
			builder.Property(d => d.Hash).IsRequired();
		}
	}
}
