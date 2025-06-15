using CRUDaster.Core.Domain.Entities.AppUserRights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDaster.Infrastructure.Data.Configurations
{
    public class HardwareConfiguration : IEntityTypeConfiguration<Hardware>
    {
        public void Configure(EntityTypeBuilder<Hardware> builder)
        {
            builder.ToTable("Hardware");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Serial)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(h => h.Serial)
                .IsUnique();

            builder.Property(h => h.Description)
                .IsRequired();
        }
    }
}
