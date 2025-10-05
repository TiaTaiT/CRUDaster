using CRUDaster.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDaster.Infrastructure.Data.Configurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Model");
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasIndex(f => f.Name)
                .IsUnique();

            builder.Property(f => f.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.ImageFile)
                .IsRequired()
                .HasMaxLength(65535);

            builder.Property(f => f.Model2dFile)
                .IsRequired()
                .HasMaxLength(65535);

            builder.Property(f => f.Model3dFile)
                .IsRequired()
                .HasMaxLength(65535);
        }
    }
}
