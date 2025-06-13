using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class BrandRepository(ApplicationDbContext context) : Repository<Brand>(context), IBrandRepository
    {
    }
}
