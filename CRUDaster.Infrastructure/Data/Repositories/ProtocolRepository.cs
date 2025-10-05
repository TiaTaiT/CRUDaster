using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class ProtocolRepository(ApplicationDbContext context) : Repository<Protocol>(context), IProtocolRepository
    {
    }
}
