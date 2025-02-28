using GETFlightApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GETFlightApp.DataAccess.Configurations;

internal class CityConfiguration : EntityConfiguration<City>
{
    protected override void ConfigureEntity(EntityTypeBuilder<City> builder)
    {

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder.HasMany(c => c.Departures)
            .WithOne(f => f.Departure)
            .HasForeignKey(f => f.DepartureId);
        builder.HasMany(c => c.Destinations)
            .WithOne(f => f.Destination)
            .HasForeignKey(f => f.DestinationId);

        builder.HasData(
            new City { Id = 1, Name = "Beograd" },
            new City { Id = 2, Name = "Kraljevo" },
            new City { Id = 3, Name = "Nis" },
            new City { Id = 4, Name = "Pristina" });

    }
}
