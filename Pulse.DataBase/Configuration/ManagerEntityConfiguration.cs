using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.DataBase.Configuration
{
	public class ManagerEntityConfiguration : IEntityTypeConfiguration<Manager>
	{
		public void Configure(EntityTypeBuilder<Manager> builder)
		{
			builder.HasQueryFilter(m => !m.IsArchive);

			builder
				.HasIndex(m => m.Name)
				.IsUnique();

			builder
				.Property(m => m.Name)
				.HasMaxLength(128);

			builder
				.Property(m => m.Description)
				.HasMaxLength(256);
		}
	}
}
