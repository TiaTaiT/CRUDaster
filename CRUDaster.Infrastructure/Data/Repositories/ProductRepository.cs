using Microsoft.EntityFrameworkCore;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            return await _dbSet
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }
    }
}
