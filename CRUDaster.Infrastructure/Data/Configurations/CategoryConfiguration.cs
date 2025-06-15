using CRUDaster.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDaster.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasIndex(f => f.Name)
                .IsUnique();

            builder.Property(f => f.Description)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasIndex(f => f.Description);
        }
    }
}
