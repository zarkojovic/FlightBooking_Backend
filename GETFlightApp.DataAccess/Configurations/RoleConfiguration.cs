using GETFlightApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GETFlightApp.DataAccess.Configurations;

internal class RoleConfiguration : EntityConfiguration<Role>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
    {
        builder.Property(r => r.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasIndex(r => r.Name)
            .IsUnique();

        builder.HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);

        builder.HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Visitor" },
            new Role { Id = 3, Name = "Agent" }
        );
    }
}
