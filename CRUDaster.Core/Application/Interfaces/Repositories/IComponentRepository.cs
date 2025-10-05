using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Core.Application.Interfaces.Repositories
{
    public interface IComponentRepository : IRepository<Component>
    {
        Task<IEnumerable<Component>> GetProductsByNameAsync(string name);
    }
}