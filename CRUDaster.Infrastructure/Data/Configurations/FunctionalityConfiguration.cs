using CRUDaster.Core.Domain.Entities.AppUserRights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDaster.Infrastructure.Data.Configurations
{
    public class FunctionalityConfiguration : IEntityTypeConfiguration<Functionality>
    {
        public void Configure(EntityTypeBuilder<Functionality> builder)
        {
            builder.ToTable("Functionality");
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(f => f.Name)
                .IsUnique();

            builder.Property(f => f.Description)
                .IsRequired();

            // Configure many-to-many relationship
            builder.HasMany(f => f.Hardwares)
                .WithMany(h => h.Functionalities)
                .UsingEntity<Dictionary<string, object>>(
                    "HardwareFunctionality",
                    j => j.HasOne<Hardware>().WithMany().HasForeignKey("HardwareId"),
                    j => j.HasOne<Functionality>().WithMany().HasForeignKey("FunctionalityId"),
                    j => j.HasKey("FunctionalityId", "HardwareId")
                );
        }
    }
}
