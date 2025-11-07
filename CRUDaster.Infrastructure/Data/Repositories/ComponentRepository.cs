using Microsoft.EntityFrameworkCore;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class ComponentRepository(ApplicationDbContext context) : Repository<Component>(context), IComponentRepository
    {
        public override async Task<IEnumerable<Component>> GetAllAsync()
        {
            return await _dbSet
                .Include(h => h.Protocols)
                .Include(h => h.Category)
                .Include(h => h.Protocols)
                .Include(h => h.Status)
                .Include(h => h.Brand)
                .Include(h => h.Pim)
                .Include(h => h.Model)
                .ToListAsync();
        }

        public override async Task<Component?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Where(p => p.Id == id)
                .Include(h => h.Protocols)
                .Include(h => h.Category)
                .Include(h => h.Protocols)
                .Include(h => h.Status)
                .Include(h => h.Brand)
                .Include(h => h.Pim)
                .Include(h => h.Model)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Component>> GetComponentByNameAsync(string name)
        {
            return await _dbSet
                .Where(p => p.Name.Contains(name))
                .Include(h => h.Protocols)
                .Include(h => h.Category)
                .Include(h => h.Protocols)
                .Include(h => h.Status)
                .Include(h => h.Brand)
                .Include(h => h.Pim)
                .Include(h => h.Model)
                .ToListAsync();
        }
    }
}
