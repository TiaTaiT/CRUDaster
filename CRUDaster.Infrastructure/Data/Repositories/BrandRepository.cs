using CRUDaster.Core.Application.Interfaces;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class BrandRepository(ApplicationDbContext appContext, IUserContextService userContext) : Repository<Brand>(appContext, userContext), IBrandRepository
    {
    }
}
