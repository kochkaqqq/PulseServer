using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Office.DataBase.Configuration
{
	class ClientEntityConfiguration : IEntityTypeConfiguration<Client>
	{
		public void Configure(EntityTypeBuilder<Client> builder)
		{
			builder.HasQueryFilter(c => !c.IsArchive);

			builder.HasKey(c => c.ClientId);

			builder.Property(c => c.ClientId)
				.UseIdentityColumn()
				.HasIdentityOptions(startValue: 165);

			builder
				.HasMany(c => c.Requests)
				.WithOne(r => r.Client);

			builder
				.HasIndex(c => c.Name)
				.IsUnique();

			builder
				.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(256);

			builder
				.Property(c => c.Address)
				.HasMaxLength(256);

			builder
				.Property(c => c.Contact)
				.HasMaxLength(256);

			builder
				.Property(c => c.EMail)
				.HasMaxLength(128);

			builder
				.Property(c => c.Phone)
				.HasMaxLength(64);

			builder.Property(c => c.CreatedDate)
				.HasColumnType("date")
				.IsRequired();
		}
	}
}
