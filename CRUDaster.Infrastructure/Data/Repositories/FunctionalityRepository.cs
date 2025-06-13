using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities.AppUserRights;
using Microsoft.EntityFrameworkCore;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class FunctionalityRepository(ApplicationDbContext context) : RepositoryBase<Functionality>(context), IFunctionalityRepository
    {
        /// <summary>
        /// Returns all Hardwares, including their associated Functionalities.
        /// </summary>
        public override async Task<IEnumerable<Functionality>> GetAllAsync()
        {
            return await _dbSet
                .Include(h => h.Hardwares)
                .ToListAsync();
        }

        /// <summary>
        /// Returns a single Hardware by Id, including its Functionalities.
        /// </summary>
        public override async Task<Functionality?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(h => h.Hardwares)
                .SingleOrDefaultAsync(h => h.Id == id);
        }
    }
}
