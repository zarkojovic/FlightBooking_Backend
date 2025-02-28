using GETFlightApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GETFlightApp.DataAccess.Configurations;

internal class StatusConfiguration : EntityConfiguration<Status>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Status> builder)
    {
        builder.Property(s => s.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasIndex(s => s.Name)
            .IsUnique();

        builder.HasMany(s => s.Reservations)
            .WithOne(r => r.Status)
            .HasForeignKey(r => r.StatusId);

        builder.HasMany(s => s.Flights)
            .WithOne(f => f.Status)
            .HasForeignKey(f => f.StatusId);

        builder.HasData(
            new Status { Id = 1, Name = "Active" },
            new Status { Id = 2, Name = "Pending" },
            new Status { Id = 3, Name = "Approved" },
            new Status { Id = 4, Name = "Rejected" },
            new Status { Id = 5, Name = "Canceled" },
            new Status { Id = 6, Name = "Completed" }
            );
    }
}
