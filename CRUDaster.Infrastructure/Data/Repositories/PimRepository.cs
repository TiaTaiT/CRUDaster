using CRUDaster.Core.Application.Interfaces;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;


namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class PimRepository(ApplicationDbContext context) : Repository<Pim>(context), IPimRepository
    {
    }
}
