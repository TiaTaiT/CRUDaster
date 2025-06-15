using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class StatusRepository(ApplicationDbContext context) : Repository<Status>(context), IStatusRepository
    {
    }
}
