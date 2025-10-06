using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations;

public class GymUserConfiguration : IEntityTypeConfiguration<GymUser>
{
    public void Configure(EntityTypeBuilder<GymUser> builder)
    {
        builder.Property(x => x.Name)
               .HasColumnType("varchar")
               .HasMaxLength(50);

        builder.Property(x => x.Email)
               .HasColumnType("varchar")
               .HasMaxLength(100);

        builder.Property(x => x.Phone)
               .HasColumnType("varchar")
               .HasMaxLength(11);

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(x => x.BuildingNumber)
                   .HasColumnName("BuildingNumber"); // Without this line EF Core will name the column Address_BuildingNumber

            address.Property(x => x.City)
                   .HasColumnType("varchar")
                   .HasColumnName("City") // Without this line EF Core will name the column Address_City
                   .HasMaxLength(30);

            address.Property(x => x.Street)
                   .HasColumnType("varchar")
                   .HasColumnName("Street") // Without this line EF Core will name the column Address_Street
                   .HasMaxLength(30);
        });

        builder.ToTable(x => {
            x.HasCheckConstraint("GymUser_EmailCheck", "Email LIKE '_%@_%._%'");
            x.HasCheckConstraint("GymUser_PhoneCheck", "Phone LIKE '01%' AND Phone NOT LIKE '%[^0-9]%'");
        });
    }
}
