using CRUDaster.Core.Application.Interfaces;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class DraftRepository(ApplicationDbContext appContext, IUserContextService userContext) : Repository<Draft>(appContext, userContext), IDraftRepository
    {
    }
}
