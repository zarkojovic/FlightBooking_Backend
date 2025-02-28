using Microsoft.EntityFrameworkCore;
using GETFlightApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using GETFlightApp.Domain.Primitives;

namespace GETFlightApp.DataAccess;

public class AspContext : DbContext
{
    public AspContext(DbContextOptions<AspContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        modelBuilder.Entity<RoleUseCase>()
            .HasKey(ruc => new { ruc.RoleId, ruc.UseCaseId });

        modelBuilder.Entity<RoleUseCase>()
            .HasData(
                new RoleUseCase { RoleId = 1, UseCaseId = 1 },
                new RoleUseCase { RoleId = 1, UseCaseId = 4 },
                new RoleUseCase { RoleId = 3, UseCaseId = 2 },
                new RoleUseCase { RoleId = 3, UseCaseId = 3 },
                new RoleUseCase { RoleId = 3, UseCaseId = 6 },
                new RoleUseCase { RoleId = 2, UseCaseId = 5 },
                new RoleUseCase { RoleId = 2, UseCaseId = 3 },
                new RoleUseCase { RoleId = 2, UseCaseId = 7 }
            );

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=ZARKO\\SQLEXPRESS;Initial Catalog=GET_FlightApp;Integrated Security=True;Trust Server Certificate=True").UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }

    public override int SaveChanges()
    {
        IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();

        foreach (EntityEntry entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity is Entity e)
                {
                    e.IsActive = true;
                    e.CreatedAt = DateTime.UtcNow;
                }
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is Entity e)
                {
                    e.UpdatedAt = DateTime.UtcNow;
                }
            }
        }

        return base.SaveChanges();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Status> Status { get; set; }

}
