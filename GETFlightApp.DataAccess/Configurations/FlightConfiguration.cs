using GETFlightApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GETFlightApp.DataAccess.Configurations;

internal class FlightConfiguration : EntityConfiguration<Flight>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Flight> builder)
    {
        builder.Property(f => f.Layovers)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(f => f.StatusId)
            .HasDefaultValue(1);

        builder.ToTable(tb => tb.HasCheckConstraint("CK_Flight_Layovers", "Layovers BETWEEN 0 AND 4"));

        builder.Property(f => f.Seats)
            .IsRequired()
            .HasDefaultValue(10);

        builder.ToTable(tb => tb.HasCheckConstraint("CK_Flight_Seats", "Seats BETWEEN 10 AND 50"));

        builder.Property(f => f.DepartureDate)
            .IsRequired();

        builder.ToTable(tb => tb.HasCheckConstraint("CK_Flight_DepartureDate", "DepartureDate >= GETDATE()"));

        builder.Property(f => f.DepartureId)
            .IsRequired();

        builder.Property(f => f.DestinationId)
            .IsRequired();

        builder.HasOne(f => f.Departure)
            .WithMany(a => a.Departures)
            .HasForeignKey(f => f.DepartureId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.Destination)
            .WithMany(a => a.Destinations)
            .HasForeignKey(f => f.DestinationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.Status)
            .WithMany(s => s.Flights)
            .HasForeignKey(f => f.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
