using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class DraftRepository(ApplicationDbContext context) : Repository<Draft>(context), IDraftRepository
    {
    }
}
