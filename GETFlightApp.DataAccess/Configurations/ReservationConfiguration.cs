using GETFlightApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GETFlightApp.DataAccess.Configurations;

internal class ReservationConfiguration : EntityConfiguration<Reservation>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Reservation> builder)
    {
        builder.Property(r => r.SeatsReserved)
            .IsRequired()
            .HasDefaultValue(1);

        builder.ToTable(tb => tb.HasCheckConstraint("CK_Reservation_SeatsReserved", "SeatsReserved BETWEEN 1 AND 5"))
;
        builder.Property(r => r.FlightId)
            .IsRequired();

        builder.Property(r => r.UserId)
            .IsRequired();

        builder.Property(r => r.StatusId)
            .IsRequired();

        builder.HasOne(r => r.Flight)
            .WithMany(f => f.Reservations)
            .HasForeignKey(r => r.FlightId);

        builder.HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId);

        builder.HasOne(r => r.Status)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.StatusId);
    }
}
