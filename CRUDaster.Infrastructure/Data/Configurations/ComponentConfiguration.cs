using CRUDaster.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDaster.Infrastructure.Data.Configurations
{
    public class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.ToTable("Component");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.AlterName)
                .HasMaxLength(255);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.VendorCode)
                .HasMaxLength(32);

            builder.Property(p => p.ErpCode)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(p => p.Length)
                .HasPrecision(18, 2);

            builder.Property(p => p.Width)
                .HasPrecision(18, 2);

            builder.Property(p => p.Height)
                .HasPrecision(18, 2);

            builder.Property(p => p.CanHasChildren)
                .HasDefaultValue(true);

            builder.Property(p => p.Virtual)
                .HasDefaultValue(false);

            builder.Property(p => p.CreatorId)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.UpdaterId)
                .HasMaxLength(255);

            // Required relationships
            builder.HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey("StatusId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Restrict);

            // Optional relationships
            builder.HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey("BrandId")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Model)
                .WithMany()
                .HasForeignKey("ModelId")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Pim)
                .WithMany()
                .HasForeignKey("PimId")
                .OnDelete(DeleteBehavior.SetNull);

            // Many-to-many relationship with Protocol
            builder.HasMany(p => p.Protocols)
                .WithMany(pr => pr.Components)
                .UsingEntity<Dictionary<string, object>>(
                    "ComponentProtocol",
                    j => j.HasOne<Protocol>().WithMany().HasForeignKey("ProtocolId"),
                    j => j.HasOne<Component>().WithMany().HasForeignKey("ComponentId"),
                    j => j.HasKey("ProtocolId", "ComponentId")
                );

            builder.Property(p => p.HasSerial)
                .HasDefaultValue(false);
        }
    }
}
