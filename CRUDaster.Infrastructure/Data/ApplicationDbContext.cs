using CRUDaster.Core.Domain.Entities;
using CRUDaster.Core.Domain.Entities.AppUserRights;
using Microsoft.EntityFrameworkCore;

namespace CRUDaster.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Draft> Drafts { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Hardware> Hardwares { get; set; } = null!;
        public DbSet<Functionality> Functionalities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entity configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
