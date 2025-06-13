using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities.AppUserRights;
using Microsoft.EntityFrameworkCore;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class HardwareRepository(ApplicationDbContext context) : Repository<Hardware>(context), IHardwareRepository
    {
        /// <summary>
        /// Returns all Hardwares, including their associated Functionalities.
        /// </summary>
        public override async Task<IEnumerable<Hardware>> GetAllAsync()
        {
            return await _dbSet
                .Include(h => h.Functionalities)
                .ToListAsync();
        }

        /// <summary>
        /// Returns a single Hardware by Id, including its Functionalities.
        /// </summary>
        public override async Task<Hardware?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(h => h.Functionalities)
                .SingleOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Hardware> GetBySerialAsync(string serial)
        {
            return await _dbSet
                .Include(h => h.Functionalities)
                .SingleOrDefaultAsync(h => h.Serial == serial) ?? throw new KeyNotFoundException($"Hardware with serial '{serial}' not found."); ;
        }
    }
}
