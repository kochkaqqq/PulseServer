using Microsoft.EntityFrameworkCore;
using Domain;
using Application.Interfaces;
using Office.DataBase.Configuration;
using Domain.Interfaces;

namespace Office.DataBase
{
    public class ApplicationContext : DbContext, IDbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Document> Documents { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientEntityConfiguration).Assembly);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
            var deletedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted && e.Entity is IArchivable);

            foreach (var entity in deletedEntities)
            {
                entity.State = EntityState.Modified;
                ((IArchivable)entity.Entity).IsArchive = true;
            }

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
