using CRUDaster.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Configaster.Infrastructure.Data.Configurations
{
    public class PimConfiguration : IEntityTypeConfiguration<Pim>
    {
        public void Configure(EntityTypeBuilder<Pim> builder)
        {
            builder.ToTable("Pim");
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasIndex(f => f.Name)
                .IsUnique();

            builder.Property(f => f.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.Content)
                .IsRequired()
                .HasMaxLength(65535);
        }
    }
}
