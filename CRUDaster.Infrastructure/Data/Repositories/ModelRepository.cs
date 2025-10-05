using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class ModelRepository(ApplicationDbContext context) : Repository<Model>(context), IModelRepository
    {
    }
}
