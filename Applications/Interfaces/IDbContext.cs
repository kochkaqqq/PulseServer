using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Interfaces
{
    public interface IDbContext
    {
        DbSet<Manager> Managers { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Request> Requests { get; set; }
        DbSet<Worker> Workers { get; set; }
        DbSet<Experience> Experiences { get; set; }
        DbSet<Report> Reports { get; set; }
        DbSet<Log> Logs { get; set; }
        DbSet<Document> Documents { get; set; }
		DbSet<MediaFile> MediaFiles { get; set; }

        ChangeTracker ChangeTracker { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
